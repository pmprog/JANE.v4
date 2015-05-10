
Public Class pmCompiler

  Private Const RemarkChar As String = ";"
  Private Const StringChar As String = "'"
  Private Const HexChars As String = "0123456789ABCDEF"

  Private Enum ParamIs As Integer
    UNKNOWN = 0
    REGISTER = 1
    LABEL = 2
    VARIABLE = 3
    IMMEDIATE = 4
  End Enum

  Structure NameLoc
    Dim sName As String
    Dim iLoc As Integer
  End Structure

  Structure NameEqu
    Dim sName As String
    Dim sValue As String
  End Structure

  Public Event Feedback(ByVal comment As String)
  Public Event UIAllowRefresh()

  Friend sScript As String
  Friend bData() As Byte = Nothing
  Friend CompileOk As Boolean = False

  Friend StackSize As Integer
  Friend StartIP As Integer
  Friend StartLabel As String

  Dim Ops() As Byte = Nothing
  Dim iLengths() As Integer = Nothing
  Dim Labels() As NameLoc = Nothing
  Dim Variables() As NameLoc = Nothing
  Dim iAppLen As Integer
  Dim bReprocess() As Boolean = Nothing

  Public Function LoadScript(ByVal inFilename As String) As Boolean
    Dim LoadOk As Boolean = False
    Dim sPath() As String

    Try
      sPath = Split(inFilename, "\")
      sPath(UBound(sPath)) = ""
      sScript = GetFileContents(inFilename, Join(sPath, "\"))
      LoadOk = True
    Catch ex As Exception
      RaiseEvent Feedback("Script Load Failed: " & ex.Message)
      LoadOk = False
    End Try

    Return LoadOk
  End Function

  Public Sub SaveBinary(ByVal inFilename As String)
    SaveBinary(inFilename, True)
  End Sub

  Public Sub SaveBinary(ByVal inFilename As String, ByVal includeHeader As Boolean)
    Dim myFile As IO.FileStream
    Dim binWrite As IO.BinaryWriter

    If bData Is Nothing Then Compile()

    myFile = New IO.FileStream(inFilename, IO.FileMode.Create)
    binWrite = New IO.BinaryWriter(myFile)
    If includeHeader Then
      binWrite.Write(CByte(Asc("P")))                      ' ID
      binWrite.Write(CByte(Asc("M")))                      ' ID
      binWrite.Write(CByte(1))                  '\_ Version
      binWrite.Write(CByte(0))                  '/
      binWrite.Write(CInt(&H14))                ' Header size
      binWrite.Write(CInt(UBound(bData) + 1))   ' Code size
      binWrite.Write(StartIP)
      binWrite.Write(StackSize)
    End If
    binWrite.Write(bData)
    binWrite.Close()
    myFile.Close()
  End Sub

  Private Function GetFileContents(ByVal inFile As String, ByVal inPath As String) As String
    Dim bTemp As System.Text.StringBuilder = New System.Text.StringBuilder
    Dim myFile As IO.FileStream
    Dim strRead As IO.StreamReader
    Dim sTemp As String
    Dim iIsInclude As Integer


    Dim sFileContents() As String
    Dim iC As Integer

    myFile = New IO.FileStream(inFile, IO.FileMode.Open)
    strRead = New IO.StreamReader(myFile)

    sFileContents = Split(Replace(strRead.ReadToEnd, vbCr, ""), vbLf)

    For iC = 0 To UBound(sFileContents)
      sTemp = sFileContents(iC)
      sTemp = Replace(sTemp, vbTab, " ")
      While InStr(sTemp, "  ")
        sTemp = Replace(sTemp, "  ", " ")
      End While
      sTemp = Replace(sTemp, ", ", ",")
      sTemp = RTrim(sTemp)
      iIsInclude = InStr(sTemp, "Include", CompareMethod.Text)
      If iIsInclude > 0 And iIsInclude < 3 Then
        If Mid(sTemp, 1, 1) <> RemarkChar Then
          sTemp = GetFileContents(inPath & Trim(Replace(sTemp, "INCLUDE", "", , , CompareMethod.Text)), inPath)
        End If
      End If
      bTemp.Append(sTemp & vbCrLf)
    Next

    myFile.Close()

    Return bTemp.ToString
  End Function

  Public Sub Compile()
    Dim sCode() As String

    CompileOk = False

    Try
      StackSize = 100
      StartLabel = ""

      bData = Nothing
      sCode = Split(sScript, vbCrLf)

      RaiseEvent Feedback("Pass 1")
      Pass1(sCode)

      RaiseEvent Feedback("Pass 2")
      iAppLen = 0
      ReDim iLengths(UBound(sCode))
      ReDim Ops(UBound(sCode))
      ReDim bReprocess(UBound(sCode))
      Pass2(sCode)

      RaiseEvent Feedback("Pass 3")
      ReDim bData(iAppLen - 1)
      Pass3(sCode)

      'sScript = Join(sCode, vbCrLf)
      RaiseEvent Feedback("Compile Succeeded")
      RaiseEvent Feedback("Compiled Size: " & iAppLen & " bytes")
      CompileOk = True

    Catch ex As Exception
      RaiseEvent Feedback("Compile Failed: " & ex.Message)
      CompileOk = False
    End Try
  End Sub

  Private Sub Pass1(ByRef sCode() As String)
    Dim iLine As Integer
    Dim sTemp As String
    Dim sLine() As String
    Dim iC As Integer
    Dim bInStr As Boolean

    For iLine = 0 To UBound(sCode)
      RaiseEvent UIAllowRefresh()

      ' Recode Strings to be identifiable
      If InStr(sCode(iLine), "str", CompareMethod.Text) <> 0 Then ' And InStr(sCode(iLine), RemarkChar, CompareMethod.Text) <> 0 Then
        sCode(iLine) = Replace(Replace(sCode(iLine), StringChar, Chr(161)), Chr(161) & Chr(161), StringChar)
        'Else
        '  sTemp = sCode(iLine)
      End If

      If InStr(sCode(iLine), "str", CompareMethod.Text) <> 0 Then
        ' Strip comments outside of string
        bInStr = False
        For iC = 1 To len(sCode(iLine))
          If Mid(sCode(iLine), iC, Len(RemarkChar)) = RemarkChar And bInStr = False Then
            sCode(iLine) = Mid(sCode(iLine), 1, iC - 1)
            Exit For
          ElseIf Mid(sCode(iLine), iC, 1) = Chr(161) Then
            bInStr = Not bInStr
          End If
        Next
      Else
        sLine = Split(sCode(iLine), RemarkChar)
        sTemp = sLine(0)
      End If

      sLine = Split(sCode(iLine), " ")
      If UBound(sLine) > 0 Then
        If LCase(sLine(1)) = "equ" Then
          sCode(iLine) = ""
          sTemp = "("
          For iC = 2 To UBound(sLine)
            sTemp &= sLine(iC)
          Next
          sTemp &= ")"
          For iC = 0 To UBound(sCode)
            sCode(iC) = Replace(sCode(iC), sLine(0), sTemp)
            RaiseEvent UIAllowRefresh()
          Next
        End If
        If LCase(sLine(0)) = ".stack" Then
          StackSize = sLine(1)
          sCode(iLine) = ""
        End If
        If LCase(sLine(1)) = ".stack" Then
          StackSize = sLine(2)
          sCode(iLine) = ""
        End If
        If LCase(sLine(0)) = ".start" Then
          StartLabel = sLine(1)
          sCode(iLine) = ""
        End If
        If LCase(sLine(1)) = ".start" Then
          StartLabel = sLine(2)
          sCode(iLine) = ""
        End If
      End If
    Next
  End Sub

  Private Sub Pass2(ByRef sCode() As String)
    Dim iLine As Integer
    Dim sLine() As String
    Dim sTmp As String
    Dim sParams() As String
    Dim sParamLine As String
    Dim iC As Integer

    For iLine = 0 To UBound(sCode)
      bReprocess(iLine) = False
      RaiseEvent UIAllowRefresh()
      sLine = Split(sCode(iLine), " ")

      If Right(sLine(0), 1) = ":" Then
        If Labels Is Nothing Then ReDim Labels(0) Else ReDim Preserve Labels(UBound(Labels) + 1)
        Labels(UBound(Labels)).sName = Left(sLine(0), Len(sLine(0)) - 1)
        Labels(UBound(Labels)).iLoc = iAppLen

        ' Update Entry IP
        If Labels(UBound(Labels)).sName = StartLabel Then StartIP = iAppLen
      End If

      Ops(iLine) = &HFF
      If UBound(sLine) > 0 Then

        sParamLine = ""
        If UBound(sLine) > 1 Then
          For iC = 2 To UBound(sLine)
            sParamLine &= sLine(iC) & " "
          Next
          sParamLine = Trim(sParamLine)
        End If

        Select Case LCase(sLine(1))

          Case "var"
            iLengths(iLine) = 4
            If Variables Is Nothing Then ReDim Variables(0) Else ReDim Preserve Variables(UBound(Variables) + 1)
            Variables(UBound(Variables)).sName = sLine(0)
            Variables(UBound(Variables)).iLoc = iAppLen

          Case "str"
            If Variables Is Nothing Then ReDim Variables(0) Else ReDim Preserve Variables(UBound(Variables) + 1)
            Variables(UBound(Variables)).sName = sLine(0)
            Variables(UBound(Variables)).iLoc = iAppLen
            sTmp = sParamLine 'Just quote's and strings
            If Not sTmp.StartsWith(Chr(161)) Then Throw New Exception("Line " & iLine & ": String not quoted")
            If Not sTmp.EndsWith(Chr(161)) Then Throw New Exception("Line " & iLine & ": String not quoted")
            sTmp = Mid(sTmp, 2, Len(sTmp) - 2)  ' Remove quotes
            iLengths(iLine) = Len(sTmp) + 1
            sLine(1) = "str"

          Case "nop"
            Ops(iLine) = 0
            iLengths(iLine) = 1

          Case "push"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Ops(iLine) = 3
                iLengths(iLine) = 1
              Case "cnt"
                Ops(iLine) = 4
                iLengths(iLine) = 1
              Case "dat"
                Ops(iLine) = 5
                iLengths(iLine) = 1
              Case "rnd"
                Throw New Exception("Line " & iLine & ": push rnd unsupported")
              Case "cps"
                Ops(iLine) = 6
                iLengths(iLine) = 1
              Case "cpd"
                Ops(iLine) = 7
                iLengths(iLine) = 1
              Case Else
                iLengths(iLine) = 5
            End Select

          Case "pop"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Ops(iLine) = 9
                iLengths(iLine) = 1
              Case "cnt"
                Ops(iLine) = 10
                iLengths(iLine) = 1
              Case "dat"
                Ops(iLine) = 11
                iLengths(iLine) = 1
              Case "rnd"
                Throw New Exception("Line " & iLine & ": pop rnd unsupported")
              Case "cps"
                Ops(iLine) = 12
                iLengths(iLine) = 1
              Case "cpd"
                Ops(iLine) = 13
                iLengths(iLine) = 1
              Case Else
                Ops(iLine) = 8
                iLengths(iLine) = 5
                If IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": pop <imm> is unsupported")
            End Select

          Case "mov"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            If InStr(sLine(2), ",") = 0 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            sParams = Split(LCase(sLine(2)), ",")
            Select Case sParams(0)
              Case "acc"
                Select Case sParams(1)
                  Case "acc"
                    Throw New Exception("Line " & iLine & ": mov acc, acc is unsupported")
                  Case "cnt"
                    Ops(iLine) = &H11
                    iLengths(iLine) = 1
                  Case "dat"
                    Ops(iLine) = &H12
                    iLengths(iLine) = 1
                  Case "rnd"
                    Ops(iLine) = &H13
                    iLengths(iLine) = 1
                  Case "cps"
                    Ops(iLine) = &HA4
                    iLengths(iLine) = 1
                  Case "cpd"
                    Ops(iLine) = &HA5
                    iLengths(iLine) = 1
                  Case Else
                    iLengths(iLine) = 5
                End Select

              Case "cnt"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H14
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": mov cnt, cnt is unsupported")
                  Case "dat"
                    Ops(iLine) = &H15
                    iLengths(iLine) = 1
                  Case "rnd"
                    Ops(iLine) = &H16
                    iLengths(iLine) = 1
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": mov cnt, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": mov cnt, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select

              Case "dat"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H17
                    iLengths(iLine) = 1
                  Case "cnt"
                    Ops(iLine) = &H18
                    iLengths(iLine) = 1
                  Case "dat"
                    Throw New Exception("Line " & iLine & ": mov dat, dat is unsupported")
                  Case "rnd"
                    Ops(iLine) = &H19
                    iLengths(iLine) = 1
                  Case "cps"
                    Ops(iLine) = &HA8
                    iLengths(iLine) = 1
                  Case "cpd"
                    Ops(iLine) = &HA9
                    iLengths(iLine) = 1
                  Case Else
                    iLengths(iLine) = 5
                End Select

              Case "rnd"
                Throw New Exception("Line " & iLine & ": mov rnd, <reg|var|imm> is unsupported")

              Case "cps"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &HA2
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": mov cps, cnt is unsupported")
                  Case "dat"
                    Ops(iLine) = &HA6
                    iLengths(iLine) = 1
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": mov cps, rnd is unsupported")
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": mov cps, cps is unsupported")
                  Case "cpd"
                    Ops(iLine) = &HAA
                    iLengths(iLine) = 1
                  Case Else
                    Ops(iLine) = &H1D
                    iLengths(iLine) = 5
                End Select

              Case "cpd"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &HA3
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": mov cpd, cnt is unsupported")
                  Case "dat"
                    Ops(iLine) = &HA7
                    iLengths(iLine) = 1
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": mov cpd, rnd is unsupported")
                  Case "cps"
                    Ops(iLine) = &HAB
                    iLengths(iLine) = 1
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": mov cpd, cpd is unsupported")
                  Case Else
                    Ops(iLine) = &H1E
                    iLengths(iLine) = 5
                End Select

              Case Else
                ' Variable = 
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H20
                    iLengths(iLine) = 5
                  Case "cnt"
                    Ops(iLine) = &H21
                    iLengths(iLine) = 5
                  Case "dat"
                    Ops(iLine) = &H22
                    iLengths(iLine) = 5
                  Case "rnd"
                    Ops(iLine) = &H23
                    iLengths(iLine) = 5
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": mov <var>, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": mov <var>, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 9
                End Select

            End Select

          Case "lods"
            Ops(iLine) = &H24
            iLengths(iLine) = 1
          Case "stos"
            Ops(iLine) = &H25
            iLengths(iLine) = 1
          Case "movs"
            Ops(iLine) = &H26
            iLengths(iLine) = 1
          Case "cmp"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            If InStr(sLine(2), ",") = 0 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            sParams = Split(LCase(sLine(2)), ",")
            Select Case sParams(0)
              Case "acc"
                Select Case sParams(1)
                  Case "acc"
                    Throw New Exception("Line " & iLine & ": cmp acc, acc is unsupported")
                  Case "cnt"
                    Ops(iLine) = &H27
                    iLengths(iLine) = 1
                  Case "dat"
                    Ops(iLine) = &H28
                    iLengths(iLine) = 1
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": cmp acc, rnd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select

              Case "cnt"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H2B
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": cmp cnt, cnt is unsupported")
                  Case "dat"
                    Ops(iLine) = &H2C
                    iLengths(iLine) = 1
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": cmp cnt, rnd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select

              Case "dat"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H2F
                    iLengths(iLine) = 1
                  Case "cnt"
                    Ops(iLine) = &H30
                    iLengths(iLine) = 1
                  Case "dat"
                    Throw New Exception("Line " & iLine & ": cmp dat, dat is unsupported")
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": cmp dat, rnd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select

              Case Else
                Throw New Exception("Line " & iLine & ": Unsupported cmp statement")
            End Select

          Case "jmp"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Ops(iLine) = &H33
            iLengths(iLine) = 5
            If IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": jmp <imm> is unsupported")
          Case "je"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Ops(iLine) = &H34
            iLengths(iLine) = 5
            If IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": je <imm> is unsupported")
          Case "jne"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Ops(iLine) = &H35
            iLengths(iLine) = 5
            If IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": jne <imm> is unsupported")
          Case "jb"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Ops(iLine) = &H36
            iLengths(iLine) = 5
            If IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": jb <imm> is unsupported")
          Case "jbe"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Ops(iLine) = &H37
            iLengths(iLine) = 5
            If IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": jbe <imm> is unsupported")
          Case "loop"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Ops(iLine) = &H38
            iLengths(iLine) = 5
            If IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": loop <imm> is unsupported")
          Case "call"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Ops(iLine) = &H39
            iLengths(iLine) = 5
            If IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": call <imm> is unsupported")
          Case "api"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Ops(iLine) = &H3A
            iLengths(iLine) = 5
          Case "ret"
            Ops(iLine) = &H3B
            iLengths(iLine) = 1
          Case "end"
            Ops(iLine) = &H3C
            iLengths(iLine) = 1

          Case "add"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            If InStr(sLine(2), ",") = 0 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            sParams = Split(LCase(sLine(2)), ",")
            Select Case sParams(0)
              Case "acc"
                Select Case sParams(1)
                  Case "acc"
                    Throw New Exception("Line " & iLine & ": add acc, acc is unsupported")
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": add acc, cnt is unsupported")
                  Case "dat"
                    Ops(iLine) = &H3E
                    iLengths(iLine) = 1
                  Case "rnd"
                    Ops(iLine) = &H3F
                    iLengths(iLine) = 1
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": add acc, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": add acc, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "cnt"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H42
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": add cnt, cnt is unsupported")
                  Case "dat"
                    Ops(iLine) = &H43
                    iLengths(iLine) = 1
                  Case "rnd"
                    Ops(iLine) = &H44
                    iLengths(iLine) = 1
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": add cnt, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": add cnt, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "dat"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H47
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": add dat, cnt is unsupported")
                  Case "dat"
                    Throw New Exception("Line " & iLine & ": add dat, dat is unsupported")
                  Case "rnd"
                    Ops(iLine) = &H48
                    iLengths(iLine) = 1
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": add dat, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": add dat, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "rnd"
                Throw New Exception("Line " & iLine & ": add rnd, <reg|var|imm> is unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": add cps, <reg|var|imm> is unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": add cpd, <reg|var|imm> is unsupported")
              Case Else
                Throw New Exception("Line " & iLine & ": add <var>, <reg|var|imm> is unsupported")
            End Select

          Case "sub"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            If InStr(sLine(2), ",") = 0 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            sParams = Split(LCase(sLine(2)), ",")
            Select Case sParams(0)
              Case "acc"
                Select Case sParams(1)
                  Case "acc"
                    Throw New Exception("Line " & iLine & ": sub acc, acc is unsupported")
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": sub acc, cnt is unsupported")
                  Case "dat"
                    Ops(iLine) = &H4B
                    iLengths(iLine) = 1
                  Case "rnd"
                    Ops(iLine) = &H4C
                    iLengths(iLine) = 1
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": sub acc, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": sub acc, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "cnt"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H4F
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": sub cnt, cnt is unsupported")
                  Case "dat"
                    Ops(iLine) = &H50
                    iLengths(iLine) = 1
                  Case "rnd"
                    Ops(iLine) = &H51
                    iLengths(iLine) = 1
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": sub cnt, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": sub cnt, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "dat"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H54
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": sub dat, cnt is unsupported")
                  Case "dat"
                    Throw New Exception("Line " & iLine & ": sub dat, dat is unsupported")
                  Case "rnd"
                    Ops(iLine) = &H55
                    iLengths(iLine) = 1
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": sub dat, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": sub dat, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "rnd"
                Throw New Exception("Line " & iLine & ": sub rnd, <reg|var|imm> is unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": sub cps, <reg|var|imm> is unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": sub cpd, <reg|var|imm> is unsupported")
              Case Else
                Throw New Exception("Line " & iLine & ": sub <var>, <reg|var|imm> is unsupported")
            End Select

          Case "inc"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            iLengths(iLine) = 1
            Select Case LCase(sLine(2))
              Case "acc"
                Ops(iLine) = &H57
                iLengths(iLine) = 1
              Case "cnt"
                Ops(iLine) = &H58
                iLengths(iLine) = 1
              Case "dat"
                Ops(iLine) = &H59
                iLengths(iLine) = 1
              Case "rnd"
                Throw New Exception("Line " & iLine & ": inc rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": inc cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": inc cpd unsupported")
              Case Else
                Throw New Exception("Line " & iLine & ": inc <var|imm> unsupported")
            End Select

          Case "dec"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            iLengths(iLine) = 1
            Select Case LCase(sLine(2))
              Case "acc"
                Ops(iLine) = &H5A
                iLengths(iLine) = 1
              Case "cnt"
                Ops(iLine) = &H5B
                iLengths(iLine) = 1
              Case "dat"
                Ops(iLine) = &H5C
                iLengths(iLine) = 1
              Case "rnd"
                Throw New Exception("Line " & iLine & ": dec rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": dec cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": dec cpd unsupported")
              Case Else
                Throw New Exception("Line " & iLine & ": dec <var|imm> unsupported")
            End Select

          Case "mul"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Throw New Exception("Line " & iLine & ": mul acc unsupported")
              Case "cnt"
                Ops(iLine) = &H5F
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": mul dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": mul rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": mul cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": mul cpd unsupported")
              Case Else
                iLengths(iLine) = 5
            End Select

          Case "div"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Throw New Exception("Line " & iLine & ": div acc unsupported")
              Case "cnt"
                Ops(iLine) = &H62
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": div dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": div rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": div cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": div cpd unsupported")
              Case Else
                iLengths(iLine) = 5
            End Select

          Case "and"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            If InStr(sLine(2), ",") = 0 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            sParams = Split(LCase(sLine(2)), ",")
            Select Case sParams(0)
              Case "acc"
                Select Case sParams(1)
                  Case "acc"
                    Throw New Exception("Line " & iLine & ": and acc, acc is unsupported")
                  Case "cnt"
                    Ops(iLine) = &H9D
                    iLengths(iLine) = 1
                  Case "dat"
                    Ops(iLine) = &H64
                    iLengths(iLine) = 1
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": and acc, rnd is unsupported")
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": and acc, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": and acc, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "cnt"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H95
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": and cnt, cnt is unsupported")
                  Case "dat"
                    Throw New Exception("Line " & iLine & ": and cnt, dat is unsupported")
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": and cnt, rnd is unsupported")
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": and cnt, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": and cnt, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "dat"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H67
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": and dat, cnt is unsupported")
                  Case "dat"
                    Throw New Exception("Line " & iLine & ": and dat, dat is unsupported")
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": and dat, rnd is unsupported")
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": and dat, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": and dat, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "rnd"
                Throw New Exception("Line " & iLine & ": and rnd, <reg|var|imm> is unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": and cps, <reg|var|imm> is unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": and cpd, <reg|var|imm> is unsupported")
              Case Else
                Throw New Exception("Line " & iLine & ": and <var>, <reg|var|imm> is unsupported")
            End Select


          Case "or"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            If InStr(sLine(2), ",") = 0 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            sParams = Split(LCase(sLine(2)), ",")
            Select Case sParams(0)
              Case "acc"
                Select Case sParams(1)
                  Case "acc"
                    Throw New Exception("Line " & iLine & ": or acc, acc is unsupported")
                  Case "cnt"
                    Ops(iLine) = &H9E
                    iLengths(iLine) = 1
                  Case "dat"
                    Ops(iLine) = &H6A
                    iLengths(iLine) = 1
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": or acc, rnd is unsupported")
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": or acc, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": or acc, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "cnt"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H98
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": or cnt, cnt is unsupported")
                  Case "dat"
                    Throw New Exception("Line " & iLine & ": or cnt, dat is unsupported")
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": or cnt, rnd is unsupported")
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": or cnt, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": or cnt, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "dat"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H6D
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": or dat, cnt is unsupported")
                  Case "dat"
                    Throw New Exception("Line " & iLine & ": or dat, dat is unsupported")
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": or dat, rnd is unsupported")
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": or dat, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": or dat, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "rnd"
                Throw New Exception("Line " & iLine & ": or rnd, <reg|var|imm> is unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": or cps, <reg|var|imm> is unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": or cpd, <reg|var|imm> is unsupported")
              Case Else
                Throw New Exception("Line " & iLine & ": or <var>, <reg|var|imm> is unsupported")
            End Select

          Case "xor"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            If InStr(sLine(2), ",") = 0 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            sParams = Split(LCase(sLine(2)), ",")
            Select Case sParams(0)
              Case "acc"
                Select Case sParams(1)
                  Case "acc"
                    Throw New Exception("Line " & iLine & ": xor acc, acc is unsupported")
                  Case "cnt"
                    Ops(iLine) = &H9F
                    iLengths(iLine) = 1
                  Case "dat"
                    Ops(iLine) = &H70
                    iLengths(iLine) = 1
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": xor acc, rnd is unsupported")
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": xor acc, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": xor acc, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "cnt"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H9B
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": xor cnt, cnt is unsupported")
                  Case "dat"
                    Throw New Exception("Line " & iLine & ": xor cnt, dat is unsupported")
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": xor cnt, rnd is unsupported")
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": xor cnt, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": xor cnt, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "dat"
                Select Case sParams(1)
                  Case "acc"
                    Ops(iLine) = &H73
                    iLengths(iLine) = 1
                  Case "cnt"
                    Throw New Exception("Line " & iLine & ": xor dat, cnt is unsupported")
                  Case "dat"
                    Throw New Exception("Line " & iLine & ": xor dat, dat is unsupported")
                  Case "rnd"
                    Throw New Exception("Line " & iLine & ": xor dat, rnd is unsupported")
                  Case "cps"
                    Throw New Exception("Line " & iLine & ": xor dat, cps is unsupported")
                  Case "cpd"
                    Throw New Exception("Line " & iLine & ": xor dat, cpd is unsupported")
                  Case Else
                    iLengths(iLine) = 5
                End Select
              Case "rnd"
                Throw New Exception("Line " & iLine & ": xor rnd, <reg|var|imm> is unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": xor cps, <reg|var|imm> is unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": xor cpd, <reg|var|imm> is unsupported")
              Case Else
                Throw New Exception("Line " & iLine & ": xor <var>, <reg|var|imm> is unsupported")
            End Select


          Case "not"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            iLengths(iLine) = 1
            Select Case LCase(sLine(2))
              Case "acc"
                Ops(iLine) = &H75
                iLengths(iLine) = 1
              Case "cnt"
                Throw New Exception("Line " & iLine & ": not cnt unsupported")
              Case "dat"
                Ops(iLine) = &H76
                iLengths(iLine) = 1
              Case "rnd"
                Throw New Exception("Line " & iLine & ": not rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": not cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": not cpd unsupported")
              Case Else
                Throw New Exception("Line " & iLine & ": not <var|imm> unsupported")
            End Select

          Case "neg"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            iLengths(iLine) = 1
            Select Case LCase(sLine(2))
              Case "acc"
                Ops(iLine) = &H77
                iLengths(iLine) = 1
              Case "cnt"
                Throw New Exception("Line " & iLine & ": neg cnt unsupported")
              Case "dat"
                Ops(iLine) = &H78
                iLengths(iLine) = 1
              Case "rnd"
                Throw New Exception("Line " & iLine & ": neg rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": neg cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": neg cpd unsupported")
              Case Else
                Throw New Exception("Line " & iLine & ": neg <var|imm> unsupported")
            End Select

          Case "sin"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Throw New Exception("Line " & iLine & ": sin acc unsupported")
              Case "cnt"
                Ops(iLine) = &H7A
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": sin dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": sin rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": sin cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": sin cpd unsupported")
              Case Else
                iLengths(iLine) = 5
            End Select

          Case "cos"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Throw New Exception("Line " & iLine & ": cos acc unsupported")
              Case "cnt"
                Ops(iLine) = &H7D
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": cos dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": cos rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": cos cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": cos cpd unsupported")
              Case Else
                iLengths(iLine) = 5
            End Select

          Case "tan"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Throw New Exception("Line " & iLine & ": tan acc unsupported")
              Case "cnt"
                Ops(iLine) = &H80
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": tan dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": tan rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": tan cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": tan cpd unsupported")
              Case Else
                iLengths(iLine) = 5
            End Select

          Case "rol"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Throw New Exception("Line " & iLine & ": rol acc unsupported")
              Case "cnt"
                Ops(iLine) = &H83
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": rol dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": rol rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": rol cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": rol cpd unsupported")
              Case Else
                iLengths(iLine) = 5
                If Not IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": rol <var> is unsupported")
            End Select

          Case "ror"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Throw New Exception("Line " & iLine & ": ror acc unsupported")
              Case "cnt"
                Ops(iLine) = &H85
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": ror dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": ror rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": ror cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": ror cpd unsupported")
              Case Else
                iLengths(iLine) = 5
                If Not IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": ror <var> is unsupported")
            End Select

          Case "shl"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Throw New Exception("Line " & iLine & ": shl acc unsupported")
              Case "cnt"
                Ops(iLine) = &H87
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": shl dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": shl rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": shl cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": shl cpd unsupported")
              Case Else
                iLengths(iLine) = 5
                If Not IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": shl <var> is unsupported")
            End Select

          Case "shr"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Throw New Exception("Line " & iLine & ": shr acc unsupported")
              Case "cnt"
                Ops(iLine) = &H89
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": shr dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": shr rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": shr cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": shr cpd unsupported")
              Case Else
                iLengths(iLine) = 5
                If Not IsNumeric(SolveEquation(sParamLine)) Then Throw New Exception("Line " & iLine & ": shr <var> is unsupported")
            End Select

          Case "xchg"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            If InStr(sLine(2), ",") = 0 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            sParams = Split(LCase(sLine(2)), ",")
            If (sParams(0) = "acc" And sParams(1) = "dat") Or (sParams(0) = "dat" And sParams(1) = "acc") Then
              Ops(iLine) = &H8A
              iLengths(iLine) = 1
            ElseIf (sParams(0) = "acc" And sParams(1) = "cnt") Or (sParams(0) = "cnt" And sParams(1) = "acc") Then
              Ops(iLine) = &H93
              iLengths(iLine) = 1
            Else
              Throw New Exception("Line " & iLine & ": xchg parameters unsupported")
            End If

          Case "mod"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Throw New Exception("Line " & iLine & ": mod acc unsupported")
              Case "cnt"
                Ops(iLine) = &H8D
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": mod dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": mod rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": mod cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": mod cpd unsupported")
              Case Else
                iLengths(iLine) = 5
            End Select

          Case "movsb"
            Ops(iLine) = &H90
            iLengths(iLine) = 1
          Case "lodsb"
            Ops(iLine) = &H8E
            iLengths(iLine) = 1
          Case "stosb"
            Ops(iLine) = &H8F
            iLengths(iLine) = 1
          Case "std"
            Ops(iLine) = &H91
            iLengths(iLine) = 1
          Case "cld"
            Ops(iLine) = &H92
            iLengths(iLine) = 1

          Case "alloc"
            If UBound(sLine) < 2 Then Throw New Exception("Line " & iLine & ": Missing parameter")
            Select Case LCase(sLine(2))
              Case "acc"
                Ops(iLine) = &HAC
                iLengths(iLine) = 1
              Case "cnt"
                Ops(iLine) = &HA0
                iLengths(iLine) = 1
              Case "dat"
                Throw New Exception("Line " & iLine & ": alloc dat unsupported")
              Case "rnd"
                Throw New Exception("Line " & iLine & ": alloc rnd unsupported")
              Case "cps"
                Throw New Exception("Line " & iLine & ": alloc cps unsupported")
              Case "cpd"
                Throw New Exception("Line " & iLine & ": alloc cpd unsupported")
              Case Else
                If IsNumeric(SolveEquation(sLine(2))) Then
                  Ops(iLine) = &HA1
                  iLengths(iLine) = 5
                Else
                  Throw New Exception("Line " & iLine & ": alloc " & sLine(2) & " unsupported")
                End If
            End Select

          Case Else
            Throw New Exception("Line " & iLine & ": Unknown opcode " & sLine(1))
        End Select
        If (LCase(sLine(1)) <> "var" And LCase(sLine(1)) <> "str") And iLengths(iLine) > 1 Then bReprocess(iLine) = True
      End If

      iAppLen += iLengths(iLine)
    Next
  End Sub

  Private Sub Pass3(ByRef sCode() As String)
    Dim iLine As Integer
    Dim iC As Integer
    Dim iChk As Integer
    Dim sLine() As String = Nothing
    Dim sParam As String
    Dim sParams() As String = Nothing
    Dim eParams() As ParamIs = Nothing
    Dim bWriter As IO.BinaryWriter = New IO.BinaryWriter(New IO.MemoryStream(bData))

    For iLine = 0 To UBound(sCode)
      If sCode(iLine) <> "" Then

        sLine = Split(sCode(iLine), " ")
        sParam = ""
        If UBound(sLine) > 1 Then
          For iC = 2 To UBound(sLine)
            sParam &= sLine(iC) & " "
          Next
          sParam = Trim(sParam)
          sParams = Split(sParam, ",")
        End If
        If UBound(sLine) > 0 Then
          If bReprocess(iLine) Then
            ReDim eParams(UBound(sParams))

            For iC = 0 To UBound(sParams)
              eParams(iC) = ParamIs.UNKNOWN
              If InStr("|acc|cnt|dat|rnd|cps|cpd|", "|" & sParams(iC) & "|", CompareMethod.Text) <> 0 Then
                eParams(iC) = ParamIs.REGISTER
              Else

                If Not Labels Is Nothing Then
                  For iChk = 0 To UBound(Labels)
                    If Labels(iChk).sName = sParams(iC) Then
                      eParams(iC) = ParamIs.LABEL
                      Exit For
                    End If
                  Next
                End If

                If Not Variables Is Nothing And eParams(iC) = ParamIs.UNKNOWN Then
                  For iChk = 0 To UBound(Variables)
                    If Variables(iChk).sName = sParams(iC) Then
                      eParams(iC) = ParamIs.VARIABLE
                      Exit For
                    End If
                  Next
                End If

                If eParams(iC) = ParamIs.UNKNOWN Then
                  If IsNumeric(SolveEquation(sParams(iC))) Then eParams(iC) = ParamIs.IMMEDIATE
                End If

              End If
            Next

            If Ops(iLine) = &HFF Then
              Select Case sLine(1)
                Case "push"
                  Select Case eParams(0)
                    Case ParamIs.IMMEDIATE
                      Ops(iLine) = &H1
                    Case ParamIs.VARIABLE
                      Ops(iLine) = &H2
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "mov"
                  If eParams(0) = ParamIs.VARIABLE Then

                    If eParams(1) = ParamIs.IMMEDIATE Then
                      Ops(iLine) = &H1F
                    ElseIf LCase(sParams(1)) = "acc" Then
                      Ops(iLine) = &H20
                    ElseIf LCase(sParams(1)) = "cnt" Then
                      Ops(iLine) = &H21
                    ElseIf LCase(sParams(1)) = "dat" Then
                      Ops(iLine) = &H22
                    ElseIf LCase(sParams(1)) = "rnd" Then
                      Ops(iLine) = &H23
                    Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                    End If

                  ElseIf LCase(sParams(0)) = "cps" Then
                    If eParams(1) = ParamIs.LABEL Or eParams(1) = ParamIs.VARIABLE Then
                      Ops(iLine) = &H1D
                    Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                    End If

                  ElseIf LCase(sParams(0)) = "cpd" Then
                    If eParams(1) = ParamIs.LABEL Or eParams(1) = ParamIs.VARIABLE Then
                      Ops(iLine) = &H1E
                    Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                    End If

                  ElseIf LCase(sParams(0)) = "acc" Then
                    If eParams(1) = ParamIs.IMMEDIATE Then
                      Ops(iLine) = &HE
                    ElseIf eParams(1) = ParamIs.VARIABLE Then
                      Ops(iLine) = &H1A
                    Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                    End If

                  ElseIf LCase(sParams(0)) = "cnt" Then
                    If eParams(1) = ParamIs.IMMEDIATE Then
                      Ops(iLine) = &HF
                    ElseIf eParams(1) = ParamIs.VARIABLE Then
                      Ops(iLine) = &H1B
                    Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                    End If

                  ElseIf LCase(sParams(0)) = "dat" Then
                    If eParams(1) = ParamIs.IMMEDIATE Then
                      Ops(iLine) = &H10
                    ElseIf eParams(1) = ParamIs.VARIABLE Then
                      Ops(iLine) = &H1C
                    Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                    End If

                  Else
                    Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End If

                Case "cmp"
                  Select Case sParams(0)
                    Case "acc"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H29
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H2A
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "cnt"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H2D
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H2E
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "dat"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H31
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H32
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "add"
                  Select Case sParams(0)
                    Case "acc"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H3D
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H40
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "cnt"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H41
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H45
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "dat"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H46
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H49
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "sub"
                  Select Case sParams(0)
                    Case "acc"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H4A
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H4D
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "cnt"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H4E
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H52
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "dat"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H53
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H56
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "mul"
                  Select Case eParams(0)
                    Case ParamIs.IMMEDIATE
                      Ops(iLine) = &H5D
                    Case ParamIs.VARIABLE
                      Ops(iLine) = &H5E
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "div"
                  Select Case eParams(0)
                    Case ParamIs.IMMEDIATE
                      Ops(iLine) = &H60
                    Case ParamIs.VARIABLE
                      Ops(iLine) = &H61
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "and"
                  Select Case sParams(0)
                    Case "acc"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H63
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H65
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "cnt"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H94
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H96
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "dat"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H66
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H68
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "or"
                  Select Case sParams(0)
                    Case "acc"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H69
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H6B
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "cnt"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H97
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H99
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "dat"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H6C
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H6E
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "xor"
                  Select Case sParams(0)
                    Case "acc"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H6F
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H71
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "cnt"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H9A
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H9C
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case "dat"
                      Select Case eParams(1)
                        Case ParamIs.IMMEDIATE
                          Ops(iLine) = &H72
                        Case ParamIs.VARIABLE
                          Ops(iLine) = &H74
                        Case Else
                          Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(1).ToString)
                      End Select
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "sin"
                  Select Case eParams(0)
                    Case ParamIs.IMMEDIATE
                      Ops(iLine) = &H79
                    Case ParamIs.VARIABLE
                      Ops(iLine) = &H7B
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "cos"
                  Select Case eParams(0)
                    Case ParamIs.IMMEDIATE
                      Ops(iLine) = &H7C
                    Case ParamIs.VARIABLE
                      Ops(iLine) = &H7E
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "tan"
                  Select Case eParams(0)
                    Case ParamIs.IMMEDIATE
                      Ops(iLine) = &H7F
                    Case ParamIs.VARIABLE
                      Ops(iLine) = &H81
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

                Case "mod"
                  Select Case eParams(0)
                    Case ParamIs.IMMEDIATE
                      Ops(iLine) = &H8B
                    Case ParamIs.VARIABLE
                      Ops(iLine) = &H8C
                    Case Else
                      Throw New Exception("Line " & iLine & ": Invalid " & sLine(1) & " parameter " & eParams(0).ToString)
                  End Select

              End Select

            End If
            bWriter.Write(Ops(iLine))

            RaiseEvent Feedback(sLine(1))
            For iC = 0 To UBound(sParams)
              RaiseEvent Feedback(" -> " & SolveEquation(sParams(iC)) & " [" & eParams(iC).ToString & "]")
            Next

            If iLengths(iLine) > 1 Then
              For iC = 0 To UBound(sParams)
                Select Case eParams(iC)
                  Case ParamIs.IMMEDIATE
                    Dim sTmp As String
                    sTmp = SolveEquation(sParams(iC))
                    If Not IsNumeric(sTmp) Then Throw New Exception("Line " & iLine & ": Immediate '" & sTmp & "' is not numeric")
                    bWriter.Write(CInt(sTmp))

                  Case ParamIs.LABEL
                    For iChk = 0 To UBound(Labels)
                      If Labels(iChk).sName = sParams(iC) Then
                        bWriter.Write(Labels(iChk).iLoc)
                        Exit For
                      End If
                    Next

                  Case ParamIs.REGISTER
                    ' Don't do anything for reg's

                  Case ParamIs.VARIABLE
                    For iChk = 0 To UBound(Variables)
                      If Variables(iChk).sName = sParams(iC) Then
                        bWriter.Write(Variables(iChk).iLoc)
                        Exit For
                      End If
                    Next

                  Case Else
                    Throw New Exception("Line " & iLine & ": Unknown parameter type " & eParams(iC).ToString)
                End Select
              Next
            End If

          Else
            If Ops(iLine) = &HFF Then
              ' Variable
              If LCase(sLine(1)) = "var" Then bWriter.Write(CInt(SolveEquation(sLine(2))))
              If LCase(sLine(1)) = "str" Then
                For iC = 2 To Len(sParam) - 1
                  bWriter.Write(System.Text.Encoding.ASCII.GetBytes(Mid(sParam, iC, 1)))
                Next
                bWriter.Write(CByte(0)) ' Null terminate
              End If
            Else
              bWriter.Write(Ops(iLine))
            End If
          End If
        End If

      End If
    Next
  End Sub

  Private Function SolveEquation(ByVal Equation As String) As String
    Dim iStart As Integer
    Dim iEnd As Integer
    Dim sOutput As String = ""
    Dim iOpenBrackets As Integer
    Dim sEval() As String

    If Equation.StartsWith("(") And Equation.EndsWith(")") Then sOutput = Mid(Equation, 2, Len(Equation) - 2) Else sOutput = Equation

    If IsNumeric(sOutput) Then Return sOutput
    If IsHexidecimal(sOutput) Then Return ConvertHexidecimal(sOutput)

    sOutput = Replace(sOutput, " ", "")
reprocessEqu:
    For iStart = 1 To Len(sOutput)
      If Mid(sOutput, iStart, 1) = "(" Then
        iOpenBrackets = 1
        For iEnd = iStart + 1 To Len(sOutput)
          If Mid(sOutput, iEnd, 1) = "(" Then iOpenBrackets += 1
          If Mid(sOutput, iEnd, 1) = ")" Then iOpenBrackets -= 1
          If iOpenBrackets = 0 Then Exit For
        Next
        ' iStart = open bracket, iEnd = close bracket/end of string
        sOutput = Mid(sOutput, 1, iStart - 1) & SolveEquation(Mid(sOutput, iStart + 1, iEnd - iStart - 1)) & Mid(sOutput, iEnd + 1)
        GoTo reprocessEqu
      End If
    Next

    sEval = Split(sOutput, "+")
    If sEval.Length > 1 Then
      sOutput = 0
      For iStart = 0 To UBound(sEval)
        sOutput = CInt(sOutput) + SolveEquation(sEval(iStart))
      Next
    End If

    sEval = Split(sOutput, "-")
    If sEval.Length > 1 Then
      sOutput = sEval(0)
      For iStart = 1 To UBound(sEval)
        sOutput = CInt(sOutput) - SolveEquation(sEval(iStart))
      Next
    End If

    sEval = Split(sOutput, "*")
    If sEval.Length > 1 Then
      sOutput = sEval(0)
      For iStart = 1 To UBound(sEval)
        sOutput = CInt(sOutput) * SolveEquation(sEval(iStart))
      Next
    End If

    sEval = Split(sOutput, "/")
    If sEval.Length > 1 Then
      sOutput = sEval(0)
      For iStart = 1 To UBound(sEval)
        sOutput = CInt(sOutput) / SolveEquation(sEval(iStart))
      Next
    End If

    Return sOutput
  End Function

  Private Function IsHexidecimal(ByVal TestString As String) As Boolean
    Dim iC As Integer

    If Not TestString.EndsWith("h") Then Return False

    ' Overflow
    If TestString.StartsWith("0") Then
      If Len(TestString) > 10 Then Return False
    Else
      If Len(TestString) > 9 Then Return False
    End If

    ' Hex values starting in the letters must have an extra 0 prefix, eg. DEADh = 0DEADh
    iC = Asc(Mid(TestString, 1, 1))
    If iC < Asc("0") Or iC > Asc("9") Then Return False

    For iC = 1 To Len(TestString) - 1
      If InStr(HexChars, Mid(TestString, iC, 1), CompareMethod.Text) = 0 Then Return False
    Next
    Return True
  End Function

  Private Function ConvertHexidecimal(ByVal HexValue As String) As Integer
    Return System.Int32.Parse(Mid(HexValue, 1, Len(HexValue) - 1), Globalization.NumberStyles.AllowHexSpecifier)
  End Function

End Class
