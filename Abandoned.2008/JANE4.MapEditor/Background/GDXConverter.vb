Friend Enum GDXFlags As Integer
  GDX_FLAG_24BIT = 1
  GDX_FLAG_32BIT = 2
  GDX_FLAG_PALETTED = 4
  GDX_FLAG_BGR = 8
  GDX_FLAG_COMPRESSED = 16
End Enum

Friend Module GDXConverter


  Friend Function LoadGDX(ByVal inFilename As String) As Bitmap
    Dim gdxImage As Bitmap = Nothing
    Dim fStream As IO.FileStream = Nothing
    Dim bReader As IO.BinaryReader = Nothing
    Dim bData() As Byte = Nothing
    Dim iW As Integer
    Dim iH As Integer
    Dim iPalLen As Integer
    Dim iDataLen As Integer
    Dim eFlags As GDXFlags
    Dim cPalette() As Color = Nothing
    Dim iTemp As Integer

    Try

      fStream = New IO.FileStream(inFilename, IO.FileMode.Open)
      bReader = New IO.BinaryReader(fStream)

      If System.Text.ASCIIEncoding.ASCII.GetString(bReader.ReadBytes(4)) <> "GDX2" Then Throw New Exception("File is not a GDX v2 format image")
      iW = BitConverter.ToInt32(bReader.ReadBytes(4), 0)
      iH = BitConverter.ToInt32(bReader.ReadBytes(4), 0)
      iPalLen = BitConverter.ToInt32(bReader.ReadBytes(4), 0)
      iDataLen = BitConverter.ToInt32(bReader.ReadBytes(4), 0)
      ' TODO: Read transparent colour
      eFlags = BitConverter.ToInt32(bReader.ReadBytes(4), 0)

      gdxImage = New Bitmap(iW, iH, Imaging.PixelFormat.Format32bppArgb)

      If (eFlags And GDXFlags.GDX_FLAG_PALETTED) = GDXFlags.GDX_FLAG_PALETTED Then
        ' TODO: Decompress, if necessary

        ' Read palette entry count
        iTemp = BitConverter.ToInt32(bReader.ReadBytes(4), 0)
        ReDim cPalette(iTemp - 1)

        For iTemp = 0 To UBound(cPalette)
          cPalette(iTemp) = ReadColour(bReader, eFlags)
        Next
      End If

      For iH = 0 To gdxImage.Height - 1
        For iW = 0 To gdxImage.Width - 1
          If (eFlags And GDXFlags.GDX_FLAG_PALETTED) = GDXFlags.GDX_FLAG_PALETTED Then
            If UBound(cPalette) < 256 Then
              ' 1 byte per pixel
              gdxImage.SetPixel(iW, iH, cPalette(bReader.ReadByte))
            ElseIf UBound(cPalette) < 65535 Then
              ' 2 bytes per pixel
              gdxImage.SetPixel(iW, iH, cPalette(BitConverter.ToUInt16(bReader.ReadBytes(2), 0)))
            Else
              ' 4 bytes per pixel
              gdxImage.SetPixel(iW, iH, cPalette(BitConverter.ToInt32(bReader.ReadBytes(4), 0)))
            End If
          Else
            gdxImage.SetPixel(iW, iH, ReadColour(bReader, eFlags))
          End If
        Next
      Next

      bReader.Close()
      bReader = Nothing
      fStream.Close()
      fStream = Nothing

    Catch ex As Exception
      gdxImage = Nothing
    End Try

    Try
      If Not bReader Is Nothing Then bReader.Close()
    Catch ex As Exception
    End Try

    Try
      If Not fStream Is Nothing Then fStream.Close()
    Catch ex As Exception
    End Try

    Return gdxImage
  End Function

  Friend Function SaveGDX(ByVal inImage As Bitmap, ByVal inFlags As GDXFlags, ByVal inFilename As String) As Boolean
    Dim cPalette() As Color = Nothing
    Dim iX As Integer
    Dim iY As Integer
    Dim iC As Integer
    Dim bFound As Boolean
    Dim cTemp As Color

    ' If we're paletted, let's do all discovery first
    If (inFlags And GDXFlags.GDX_FLAG_PALETTED) = GDXFlags.GDX_FLAG_PALETTED Then
      For iY = 0 To inImage.Height - 1
        For iX = 0 To inImage.Width - 1
          If cPalette Is Nothing Then
            ReDim cPalette(0)
            cPalette(0) = inImage.GetPixel(iX, iY)
          Else
            cTemp = inImage.GetPixel(iX, iY)
            bFound = False
            For iC = 0 To UBound(cPalette)
              If cPalette(iC) = cTemp Then
                bFound = True
                Exit For
              End If
            Next
            If Not bFound Then
              ReDim Preserve cPalette(UBound(cPalette) + 1)
              cPalette(UBound(cPalette)) = cTemp
            End If
          End If
        Next
      Next
    End If
    Return SaveGDX(inImage, inFlags, inFilename, cPalette)

  End Function

  Friend Function SaveGDX(ByVal inImage As Bitmap, ByVal inFlags As GDXFlags, ByVal inFilename As String, ByVal inPalette() As Color) As Boolean
    Dim bWorked As Boolean = False
    Dim iX As Integer
    Dim iY As Integer
    Dim iC As Integer
    Dim cTemp As Color
    Dim fStream As IO.FileStream = Nothing
    Dim bWriter As IO.BinaryWriter = Nothing
    Dim bPal As Byte
    Dim sPal As UShort
    Dim iPDSize As Integer = 1

    Try

      ' TODO: If Compression flagged, do necessary, before outputting header

      fStream = New IO.FileStream(inFilename, IO.FileMode.Create)
      bWriter = New IO.BinaryWriter(fStream)

      bWriter.Write(System.Text.ASCIIEncoding.ASCII.GetBytes("GDX2"))
      bWriter.Write(BitConverter.GetBytes(inImage.Width))
      bWriter.Write(BitConverter.GetBytes(inImage.Height))

      ' TODO: Write transparent colour

      ' Calculate palette binary size
      If (inFlags And GDXFlags.GDX_FLAG_PALETTED) = GDXFlags.GDX_FLAG_PALETTED Then
        iC = 4
        If (inFlags And GDXFlags.GDX_FLAG_24BIT) = GDXFlags.GDX_FLAG_24BIT Then
          iC += (UBound(inPalette) + 1) * 3
        ElseIf (inFlags And GDXFlags.GDX_FLAG_32BIT) = GDXFlags.GDX_FLAG_32BIT Then
          iC += (UBound(inPalette) + 1) * 4
        End If
      Else
        iC = 0
      End If
      bWriter.Write(BitConverter.GetBytes(iC))

      ' Calculate pixel data binary size
      If (inFlags And GDXFlags.GDX_FLAG_PALETTED) = GDXFlags.GDX_FLAG_PALETTED Then
        If (UBound(inPalette) + 1) <= 256 Then
          iPDSize = 1                               ' 1 byte per pixel
        ElseIf (UBound(inPalette) + 1) <= 65536 Then
          iPDSize = 2                               ' 2 bytes per pixel
        Else
          iPDSize = 4                               ' 4 bytes per pixel
        End If
        iC = (inImage.Width * inImage.Height) * iPDSize
      Else
        If (inFlags And GDXFlags.GDX_FLAG_24BIT) = GDXFlags.GDX_FLAG_24BIT Then
          iC = (inImage.Width * inImage.Height) * 3
        ElseIf (inFlags And GDXFlags.GDX_FLAG_32BIT) = GDXFlags.GDX_FLAG_32BIT Then
          iC = (inImage.Width * inImage.Height) * 4
        End If
      End If
      bWriter.Write(BitConverter.GetBytes(iC))

      bWriter.Write(BitConverter.GetBytes(inFlags))


      If (inFlags And GDXFlags.GDX_FLAG_PALETTED) = GDXFlags.GDX_FLAG_PALETTED Then
        ' Palette Table
        iC = (UBound(inPalette) + 1)
        bWriter.Write(BitConverter.GetBytes(iC))
        For iC = 0 To UBound(inPalette)
          If (inFlags And GDXFlags.GDX_FLAG_BGR) = GDXFlags.GDX_FLAG_BGR Then
            If (inFlags And GDXFlags.GDX_FLAG_24BIT) = GDXFlags.GDX_FLAG_24BIT Then
              bWriter.Write(inPalette(iC).B)
              bWriter.Write(inPalette(iC).G)
              bWriter.Write(inPalette(iC).R)
            ElseIf (inFlags And GDXFlags.GDX_FLAG_32BIT) = GDXFlags.GDX_FLAG_32BIT Then
              bWriter.Write(inPalette(iC).B)
              bWriter.Write(inPalette(iC).G)
              bWriter.Write(inPalette(iC).R)
              bWriter.Write(CByte(0))
            End If
          Else
            If (inFlags And GDXFlags.GDX_FLAG_24BIT) = GDXFlags.GDX_FLAG_24BIT Then
              bWriter.Write(inPalette(iC).R)
              bWriter.Write(inPalette(iC).G)
              bWriter.Write(inPalette(iC).B)
            ElseIf (inFlags And GDXFlags.GDX_FLAG_32BIT) = GDXFlags.GDX_FLAG_32BIT Then
              bWriter.Write(CByte(0))
              bWriter.Write(inPalette(iC).R)
              bWriter.Write(inPalette(iC).G)
              bWriter.Write(inPalette(iC).B)
            End If
          End If
        Next

        ' Pixel Data
        For iY = 0 To inImage.Height - 1
          For iX = 0 To inImage.Width - 1
            cTemp = inImage.GetPixel(iX, iY)
            For iC = 0 To UBound(inPalette)
              If inPalette(iC) = cTemp Then
                Select Case iPDSize
                  Case 1
                    bPal = iC
                    bWriter.Write(bPal)
                  Case 2
                    sPal = iC
                    bWriter.Write(BitConverter.GetBytes(sPal))
                  Case 4
                    bWriter.Write(BitConverter.GetBytes(iC))
                End Select
                Exit For
              End If
            Next
          Next
        Next

      Else
        ' Simple loop/dump, raw picture
        For iY = 0 To inImage.Height - 1
          For iX = 0 To inImage.Width - 1
            cTemp = inImage.GetPixel(iX, iY)
            If (inFlags And GDXFlags.GDX_FLAG_BGR) = GDXFlags.GDX_FLAG_BGR Then
              If (inFlags And GDXFlags.GDX_FLAG_24BIT) = GDXFlags.GDX_FLAG_24BIT Then
                bWriter.Write(cTemp.B)
                bWriter.Write(cTemp.G)
                bWriter.Write(cTemp.R)
              ElseIf (inFlags And GDXFlags.GDX_FLAG_32BIT) = GDXFlags.GDX_FLAG_32BIT Then
                bWriter.Write(cTemp.B)
                bWriter.Write(cTemp.G)
                bWriter.Write(cTemp.R)
                bWriter.Write(CByte(0))
              End If
            Else
              If (inFlags And GDXFlags.GDX_FLAG_24BIT) = GDXFlags.GDX_FLAG_24BIT Then
                bWriter.Write(cTemp.R)
                bWriter.Write(cTemp.G)
                bWriter.Write(cTemp.B)
              ElseIf (inFlags And GDXFlags.GDX_FLAG_32BIT) = GDXFlags.GDX_FLAG_32BIT Then
                bWriter.Write(CByte(0))
                bWriter.Write(cTemp.R)
                bWriter.Write(cTemp.G)
                bWriter.Write(cTemp.B)
              End If
            End If
          Next
        Next
      End If

      bWriter.Close()
      bWriter = Nothing
      fStream.Close()
      fStream = Nothing

      bWorked = True
    Catch ex As Exception
      bWorked = False
    End Try

    Try
      If Not bWriter Is Nothing Then bWriter.Close()
    Catch ex As Exception
    End Try

    Try
      If Not fStream Is Nothing Then fStream.Close()
    Catch ex As Exception
    End Try

    Return bWorked
  End Function

  Private Function ReadColour(ByVal bReader As IO.BinaryReader, ByVal eFlags As GDXFlags) As Color
    Dim bData() As Byte = Nothing
    Dim cTemp As Color

    ' Read colour chunk
    If (eFlags And GDXFlags.GDX_FLAG_24BIT) = GDXFlags.GDX_FLAG_24BIT Then
      ReDim bData(2)
      bData = bReader.ReadBytes(3)
    ElseIf (eFlags And GDXFlags.GDX_FLAG_32BIT) = GDXFlags.GDX_FLAG_32BIT Then
      ReDim bData(3)
      bData = bReader.ReadBytes(4)
    End If

    ' Generate colour class
    If (eFlags And GDXFlags.GDX_FLAG_BGR) = GDXFlags.GDX_FLAG_BGR Then
      cTemp = Color.FromArgb(bData(2), bData(1), bData(0))
    Else
      cTemp = Color.FromArgb(bData(UBound(bData) - 2), bData(UBound(bData) - 1), bData(UBound(bData)))
    End If

    Return cTemp
  End Function

End Module
