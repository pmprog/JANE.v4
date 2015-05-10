Public Class RoomEditor
  Implements IMDIForm


  Dim Tiles(34, 34) As Tile
  Dim OnExitID As Integer
  Dim OnEntryID As Integer
  Dim TimerCount As Integer
  Dim OnTimerID As Integer

  Dim CopyTile As Tile

  Dim iSelectX As Integer = 20
  Dim iSelectY As Integer = 20

  Dim myID As Integer


  Private Function RenderScreen() As Bitmap
    Dim canvas As Bitmap = New Bitmap(768, 572, Imaging.PixelFormat.Format32bppRgb)
    Dim g As Graphics = Graphics.FromImage(canvas)
    Dim iX As Integer
    Dim iY As Integer
    Dim iTX As Integer
    Dim iTY As Integer
    Dim bLoadedImages() As Bitmap = Nothing
    Dim sFile As String
    Dim bSelBack As Bitmap = New Bitmap(My.Resources.SelectorBlock_Back)
    Dim bSelFront As Bitmap = New Bitmap(My.Resources.SelectorBlock_Front)

    ReDim bLoadedImages(UBound(sImageDetails))

    For iY = 0 To 34
      For iX = 0 To 34
        If (iY < 12 Or iX > iY - 12) And (iX < 12 Or iY > iX - 12) Then

          ' Load and cache tiles on usage
          If bLoadedImages(Tiles(iX, iY).Floor.FloorImage) Is Nothing Then
            sFile = Dir(sProjectPath & "Images\" & Format(Tiles(iX, iY).Floor.FloorImage, "00000000") & ".*")
            bLoadedImages(Tiles(iX, iY).Floor.FloorImage) = New Bitmap(New Bitmap(sProjectPath & "Images\" & sFile))
          End If
          If bLoadedImages(Tiles(iX, iY).Floor.LeftWallImage) Is Nothing Then
            sFile = Dir(sProjectPath & "Images\" & Format(Tiles(iX, iY).Floor.LeftWallImage, "00000000") & ".*")
            bLoadedImages(Tiles(iX, iY).Floor.LeftWallImage) = New Bitmap(New Bitmap(sProjectPath & "Images\" & sFile))
          End If
          If bLoadedImages(Tiles(iX, iY).Floor.RightWallImage) Is Nothing Then
            sFile = Dir(sProjectPath & "Images\" & Format(Tiles(iX, iY).Floor.RightWallImage, "00000000") & ".*")
            bLoadedImages(Tiles(iX, iY).Floor.RightWallImage) = New Bitmap(New Bitmap(sProjectPath & "Images\" & sFile))
          End If
          If bLoadedImages(Tiles(iX, iY).UpperLevel.FloorImage) Is Nothing Then
            sFile = Dir(sProjectPath & "Images\" & Format(Tiles(iX, iY).UpperLevel.FloorImage, "00000000") & ".*")
            bLoadedImages(Tiles(iX, iY).UpperLevel.FloorImage) = New Bitmap(New Bitmap(sProjectPath & "Images\" & sFile))
          End If
          If bLoadedImages(Tiles(iX, iY).UpperLevel.LeftWallImage) Is Nothing Then
            sFile = Dir(sProjectPath & "Images\" & Format(Tiles(iX, iY).UpperLevel.LeftWallImage, "00000000") & ".*")
            bLoadedImages(Tiles(iX, iY).UpperLevel.LeftWallImage) = New Bitmap(New Bitmap(sProjectPath & "Images\" & sFile))
          End If
          If bLoadedImages(Tiles(iX, iY).UpperLevel.RightWallImage) Is Nothing Then
            sFile = Dir(sProjectPath & "Images\" & Format(Tiles(iX, iY).UpperLevel.RightWallImage, "00000000") & ".*")
            bLoadedImages(Tiles(iX, iY).UpperLevel.RightWallImage) = New Bitmap(New Bitmap(sProjectPath & "Images\" & sFile))
          End If

          ' Draw floor tile
          iTX = (canvas.Width / 2) - 32 - (iY * 32) + (iX * 32)
          iTY = (iY * 8) + (iX * 8)

          If iX = iSelectX And iY = iSelectY Then g.DrawImage(bSelBack, iTX, iTY - 304)

          With Tiles(iX, iY).Floor
            g.DrawImage(bLoadedImages(.FloorImage), iTX, iTY - .Height)
            If .Height > 0 Then
              g.DrawImage(GenerateWall(bLoadedImages(.LeftWallImage), .Height, True), iTX, iTY - .Height + 8)
              g.DrawImage(GenerateWall(bLoadedImages(.RightWallImage), .Height, False), iTX + 32, iTY - .Height + 8)
            End If
          End With


          ' Draw platform tile
          With Tiles(iX, iY).UpperLevel
            If .Elevation > 0 Then
              If (.Flags And IS_CEILING) = IS_CEILING Then
                g.DrawImage(GenerateWall(bLoadedImages(.LeftWallImage), iTY, True), iTX, 8 - .Elevation)
                g.DrawImage(GenerateWall(bLoadedImages(.RightWallImage), iTY, False), iTX + 32, 8 - .Elevation)
              ElseIf .Height > 0 Then
                g.DrawImage(bLoadedImages(.FloorImage), iTX, iTY - .Height - .Elevation)
                g.DrawImage(GenerateWall(bLoadedImages(.LeftWallImage), .Height, True), iTX, iTY - .Height + 8 - .Elevation)
                g.DrawImage(GenerateWall(bLoadedImages(.RightWallImage), .Height, False), iTX + 32, iTY - .Height + 8 - .Elevation)
              End If
            End If
          End With

          If iX = iSelectX And iY = iSelectY Then g.DrawImage(bSelFront, iTX, iTY - 304)

        End If
      Next
    Next

    bLoadedImages = Nothing

    g.DrawRectangle(New Pen(Color.Red, 2), CInt((canvas.Width / 2) - 320), 80, 640, 380)

    GC.Collect()
    Return canvas
  End Function

  Public Sub LoadRoom(ByVal roomID As Integer)
    Dim sLineData() As String
    Dim sLoc() As String

    myID = roomID

    tDesc.Text = sRoomDetails(myID)

    FileOpen(1, sProjectPath & "Rooms\" & Format(myID, "00000000") & ".jnr", OpenMode.Input)
    Do Until EOF(1)
      sLineData = Split(LineInput(1), vbTab)
      Select Case sLineData(0)
        Case "OnEntry"
          OnEntryID = sLineData(1)
          cScriptEnter.SelectedIndex = OnEntryID + 1
        Case "OnExit"
          OnExitID = sLineData(1)
          cScriptLeave.SelectedIndex = OnExitID + 1
        Case "TimerInterval"
          TimerCount = Math.Max(CInt(sLineData(1)), 0)
          nTimer.Value = TimerCount
        Case "OnTimer"
          OnTimerID = sLineData(1)
          cScriptTimer.SelectedIndex = OnTimerID + 1
        Case Else
          sLoc = Split(sLineData(0), ",")
          Select Case sLineData(1)
            Case "Floor"
              Tiles(sLoc(0), sLoc(1)).Floor.Elevation = sLineData(2)
              Tiles(sLoc(0), sLoc(1)).Floor.Height = sLineData(3)
              Tiles(sLoc(0), sLoc(1)).Floor.FloorImage = sLineData(4)
              Tiles(sLoc(0), sLoc(1)).Floor.LeftWallImage = sLineData(5)
              Tiles(sLoc(0), sLoc(1)).Floor.RightWallImage = sLineData(6)
              Tiles(sLoc(0), sLoc(1)).Floor.Flags = sLineData(7)
              Tiles(sLoc(0), sLoc(1)).Floor.OnExitID = sLineData(8)
              Tiles(sLoc(0), sLoc(1)).Floor.OnEntryID = sLineData(9)
              Tiles(sLoc(0), sLoc(1)).Floor.OnWalkID = sLineData(10)
            Case "Platform"
              Tiles(sLoc(0), sLoc(1)).UpperLevel.Elevation = sLineData(2)
              Tiles(sLoc(0), sLoc(1)).UpperLevel.Height = sLineData(3)
              Tiles(sLoc(0), sLoc(1)).UpperLevel.FloorImage = sLineData(4)
              Tiles(sLoc(0), sLoc(1)).UpperLevel.LeftWallImage = sLineData(5)
              Tiles(sLoc(0), sLoc(1)).UpperLevel.RightWallImage = sLineData(6)
              Tiles(sLoc(0), sLoc(1)).UpperLevel.Flags = sLineData(7)
              Tiles(sLoc(0), sLoc(1)).UpperLevel.OnExitID = sLineData(8)
              Tiles(sLoc(0), sLoc(1)).UpperLevel.OnEntryID = sLineData(9)
              Tiles(sLoc(0), sLoc(1)).UpperLevel.OnWalkID = sLineData(10)
          End Select
      End Select
    Loop
    FileClose()

    pRoomRender.Image = RenderScreen()
  End Sub

  Public Sub Save() Implements IMDIForm.Save
    Dim sLineData(1) As String
    Dim iX As Integer
    Dim iY As Integer
    Dim sScript() As String

    FileOpen(1, sProjectPath & "Rooms\" & Format(myID, "00000000") & ".jnr", OpenMode.Output)

    ReDim sLineData(1)
    sLineData(0) = "OnEntry"
    If cScriptEnter.Text = "" Then
      sLineData(1) = -1
    Else
      sScript = Split(cScriptEnter.Text, " - ")
      sLineData(1) = sScript(0)
    End If
    PrintLine(1, Join(sLineData, vbTab))

    sLineData(0) = "OnExit"
    If cScriptLeave.Text = "" Then
      sLineData(1) = -1
    Else
      sScript = Split(cScriptLeave.Text, " - ")
      sLineData(1) = sScript(0)
    End If
    PrintLine(1, Join(sLineData, vbTab))

    sLineData(0) = "TimerInterval"
    sLineData(1) = nTimer.Value
    PrintLine(1, Join(sLineData, vbTab))

    sLineData(0) = "OnTimer"
    If cScriptTimer.Text = "" Then
      sLineData(1) = -1
    Else
      sScript = Split(cScriptTimer.Text, " - ")
      sLineData(1) = sScript(0)
    End If
    PrintLine(1, Join(sLineData, vbTab))


    For iY = 0 To 34
      For iX = 0 To 34
        If (iY < 12 Or iX > iY - 12) And (iX < 12 Or iY > iX - 12) Then
          ReDim sLineData(10)
          sLineData(0) = iX & "," & iY
          sLineData(1) = "Floor"
          sLineData(2) = Tiles(iX, iY).Floor.Elevation
          sLineData(3) = Tiles(iX, iY).Floor.Height
          sLineData(4) = Tiles(iX, iY).Floor.FloorImage
          sLineData(5) = Tiles(iX, iY).Floor.LeftWallImage
          sLineData(6) = Tiles(iX, iY).Floor.RightWallImage
          sLineData(7) = Tiles(iX, iY).Floor.Flags
          sLineData(8) = Tiles(iX, iY).Floor.OnExitID
          sLineData(9) = Tiles(iX, iY).Floor.OnEntryID
          sLineData(10) = Tiles(iX, iY).Floor.OnWalkID
          PrintLine(1, Join(sLineData, vbTab))
          sLineData(1) = "Platform"
          sLineData(2) = Tiles(iX, iY).UpperLevel.Elevation
          sLineData(3) = Tiles(iX, iY).UpperLevel.Height
          sLineData(4) = Tiles(iX, iY).UpperLevel.FloorImage
          sLineData(5) = Tiles(iX, iY).UpperLevel.LeftWallImage
          sLineData(6) = Tiles(iX, iY).UpperLevel.RightWallImage
          sLineData(7) = Tiles(iX, iY).UpperLevel.Flags
          sLineData(8) = Tiles(iX, iY).UpperLevel.OnExitID
          sLineData(9) = Tiles(iX, iY).UpperLevel.OnEntryID
          sLineData(10) = Tiles(iX, iY).UpperLevel.OnWalkID
          PrintLine(1, Join(sLineData, vbTab))
        End If
      Next
    Next

    FileClose()

    sRoomDetails(myID) = tDesc.Text
    SaveRoomDetails()
  End Sub

  Private Function GenerateWall(ByVal SourceImg As Bitmap, ByVal NewHeight As Integer, ByVal IsLeftSide As Boolean) As Bitmap
    Dim bNewWall As Bitmap = New Bitmap(32, NewHeight + 8, Imaging.PixelFormat.Format32bppArgb)
    Dim g As Graphics = Graphics.FromImage(bNewWall)
    Dim iY As Integer

    For iY = 0 To CInt(NewHeight / (SourceImg.Height - 8)) + 1
      g.DrawImage(SourceImg, 0, iY * (SourceImg.Height - 8))
    Next

    If IsLeftSide Then
      For iY = 0 To 27
        If iY < 4 Then bNewWall.SetPixel(iY, NewHeight + 1, Color.FromArgb(0, 0, 0, 0))
        If iY < 8 Then bNewWall.SetPixel(iY, NewHeight + 2, Color.FromArgb(0, 0, 0, 0))
        If iY < 12 Then bNewWall.SetPixel(iY, NewHeight + 3, Color.FromArgb(0, 0, 0, 0))
        If iY < 16 Then bNewWall.SetPixel(iY, NewHeight + 4, Color.FromArgb(0, 0, 0, 0))
        If iY < 20 Then bNewWall.SetPixel(iY, NewHeight + 5, Color.FromArgb(0, 0, 0, 0))
        If iY < 24 Then bNewWall.SetPixel(iY, NewHeight + 6, Color.FromArgb(0, 0, 0, 0))
        bNewWall.SetPixel(iY, NewHeight + 7, Color.FromArgb(0, 0, 0, 0))
      Next
    Else
      For iY = 0 To 27
        If iY < 4 Then bNewWall.SetPixel(31 - iY, NewHeight + 1, Color.FromArgb(0, 0, 0, 0))
        If iY < 8 Then bNewWall.SetPixel(31 - iY, NewHeight + 2, Color.FromArgb(0, 0, 0, 0))
        If iY < 12 Then bNewWall.SetPixel(31 - iY, NewHeight + 3, Color.FromArgb(0, 0, 0, 0))
        If iY < 16 Then bNewWall.SetPixel(31 - iY, NewHeight + 4, Color.FromArgb(0, 0, 0, 0))
        If iY < 20 Then bNewWall.SetPixel(31 - iY, NewHeight + 5, Color.FromArgb(0, 0, 0, 0))
        If iY < 24 Then bNewWall.SetPixel(31 - iY, NewHeight + 6, Color.FromArgb(0, 0, 0, 0))
        bNewWall.SetPixel(31 - iY, NewHeight + 7, Color.FromArgb(0, 0, 0, 0))
      Next
    End If

    Return bNewWall
  End Function

  Private Sub tHandleMove_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tHandleMove.KeyDown
    Dim fImgSel As ImageSelector
    Dim fFlgSel As FlagsDialog

    If e.KeyCode = Keys.Up Then
      iSelectY -= 1
      e.Handled = True
      e.SuppressKeyPress = True
    End If
    If e.KeyCode = Keys.Down Then
      iSelectY += 1
      e.Handled = True
      e.SuppressKeyPress = True
    End If
    If e.KeyCode = Keys.Left Then
      iSelectX -= 1
      e.Handled = True
      e.SuppressKeyPress = True
    End If
    If e.KeyCode = Keys.Right Then
      iSelectX += 1
      e.Handled = True
      e.SuppressKeyPress = True
    End If
    If iSelectX < 0 Then iSelectX = 0
    If iSelectY < 0 Then iSelectY = 0
    If iSelectX > 34 Then iSelectX = 34
    If iSelectY > 34 Then iSelectY = 34

    If e.KeyCode = Keys.C And e.Control And (Not e.Alt) And (Not e.Shift) Then
      CopyTile.Floor.Flags = Tiles(iSelectX, iSelectY).Floor.Flags
      CopyTile.Floor.FloorImage = Tiles(iSelectX, iSelectY).Floor.FloorImage
      CopyTile.Floor.Height = Tiles(iSelectX, iSelectY).Floor.Height
      CopyTile.Floor.LeftWallImage = Tiles(iSelectX, iSelectY).Floor.LeftWallImage
      CopyTile.Floor.OnEntryID = Tiles(iSelectX, iSelectY).Floor.OnEntryID
      CopyTile.Floor.OnExitID = Tiles(iSelectX, iSelectY).Floor.OnExitID
      CopyTile.Floor.OnWalkID = Tiles(iSelectX, iSelectY).Floor.OnWalkID
      CopyTile.Floor.RightWallImage = Tiles(iSelectX, iSelectY).Floor.RightWallImage

      CopyTile.UpperLevel.Elevation = Tiles(iSelectX, iSelectY).UpperLevel.Elevation
      CopyTile.UpperLevel.Flags = Tiles(iSelectX, iSelectY).UpperLevel.Flags
      CopyTile.UpperLevel.FloorImage = Tiles(iSelectX, iSelectY).UpperLevel.FloorImage
      CopyTile.UpperLevel.Height = Tiles(iSelectX, iSelectY).UpperLevel.Height
      CopyTile.UpperLevel.LeftWallImage = Tiles(iSelectX, iSelectY).UpperLevel.LeftWallImage
      CopyTile.UpperLevel.OnEntryID = Tiles(iSelectX, iSelectY).UpperLevel.OnEntryID
      CopyTile.UpperLevel.OnExitID = Tiles(iSelectX, iSelectY).UpperLevel.OnExitID
      CopyTile.UpperLevel.OnWalkID = Tiles(iSelectX, iSelectY).UpperLevel.OnWalkID
      CopyTile.UpperLevel.RightWallImage = Tiles(iSelectX, iSelectY).UpperLevel.RightWallImage
      e.Handled = True
      e.SuppressKeyPress = True
      Exit Sub
    End If

    If e.KeyCode = Keys.V And e.Control And (Not e.Alt) And (Not e.Shift) Then
      Tiles(iSelectX, iSelectY).Floor.Flags = CopyTile.Floor.Flags
      Tiles(iSelectX, iSelectY).Floor.FloorImage = CopyTile.Floor.FloorImage
      Tiles(iSelectX, iSelectY).Floor.Height = CopyTile.Floor.Height
      Tiles(iSelectX, iSelectY).Floor.LeftWallImage = CopyTile.Floor.LeftWallImage
      Tiles(iSelectX, iSelectY).Floor.OnEntryID = CopyTile.Floor.OnEntryID
      Tiles(iSelectX, iSelectY).Floor.OnExitID = CopyTile.Floor.OnExitID
      Tiles(iSelectX, iSelectY).Floor.OnWalkID = CopyTile.Floor.OnWalkID
      Tiles(iSelectX, iSelectY).Floor.RightWallImage = CopyTile.Floor.RightWallImage

      Tiles(iSelectX, iSelectY).UpperLevel.Elevation = CopyTile.UpperLevel.Elevation
      Tiles(iSelectX, iSelectY).UpperLevel.Flags = CopyTile.UpperLevel.Flags
      Tiles(iSelectX, iSelectY).UpperLevel.FloorImage = CopyTile.UpperLevel.FloorImage
      Tiles(iSelectX, iSelectY).UpperLevel.Height = CopyTile.UpperLevel.Height
      Tiles(iSelectX, iSelectY).UpperLevel.LeftWallImage = CopyTile.UpperLevel.LeftWallImage
      Tiles(iSelectX, iSelectY).UpperLevel.OnEntryID = CopyTile.UpperLevel.OnEntryID
      Tiles(iSelectX, iSelectY).UpperLevel.OnExitID = CopyTile.UpperLevel.OnExitID
      Tiles(iSelectX, iSelectY).UpperLevel.OnWalkID = CopyTile.UpperLevel.OnWalkID
      Tiles(iSelectX, iSelectY).UpperLevel.RightWallImage = CopyTile.UpperLevel.RightWallImage
      e.Handled = True
      e.SuppressKeyPress = True
      Exit Sub
    End If

    If e.KeyCode = Keys.PageDown Then
      If Tiles(iSelectX, iSelectY).Floor.Height > 0 Then Tiles(iSelectX, iSelectY).Floor.Height -= 1
      e.Handled = True
      e.SuppressKeyPress = True
    End If
    If e.KeyCode = Keys.PageUp Then
      Tiles(iSelectX, iSelectY).Floor.Height += 1
      e.Handled = True
      e.SuppressKeyPress = True
    End If

    If e.KeyCode = Keys.End Then
      If Tiles(iSelectX, iSelectY).UpperLevel.Elevation > Tiles(iSelectX, iSelectY).Floor.Height Then Tiles(iSelectX, iSelectY).UpperLevel.Elevation -= 1 Else Tiles(iSelectX, iSelectY).UpperLevel.Elevation = 0
      e.Handled = True
      e.SuppressKeyPress = True
    End If
    If e.KeyCode = Keys.Home Then
      If Tiles(iSelectX, iSelectY).UpperLevel.Elevation = 0 Then
        If Tiles(iSelectX, iSelectY).Floor.Height = 0 Then
          Tiles(iSelectX, iSelectY).UpperLevel.Elevation = 1
        Else
          Tiles(iSelectX, iSelectY).UpperLevel.Elevation = Tiles(iSelectX, iSelectY).Floor.Height
        End If
      Else
        Tiles(iSelectX, iSelectY).UpperLevel.Elevation += 1
      End If
      e.Handled = True
      e.SuppressKeyPress = True
    End If

    If e.KeyCode = Keys.Delete Then
      If Tiles(iSelectX, iSelectY).UpperLevel.Height > 0 Then Tiles(iSelectX, iSelectY).UpperLevel.Height -= 1
      e.Handled = True
      e.SuppressKeyPress = True
    End If
    If e.KeyCode = Keys.Insert Then
      Tiles(iSelectX, iSelectY).UpperLevel.Height += 1
      e.Handled = True
      e.SuppressKeyPress = True
    End If

    If e.KeyCode = Keys.C Then
      If (Tiles(iSelectX, iSelectY).UpperLevel.Flags And IS_CEILING) = IS_CEILING Then
        Tiles(iSelectX, iSelectY).UpperLevel.Flags = Tiles(iSelectX, iSelectY).UpperLevel.Flags And (Not IS_CEILING)
      Else
        Tiles(iSelectX, iSelectY).UpperLevel.Flags = Tiles(iSelectX, iSelectY).UpperLevel.Flags Or IS_CEILING
      End If
    End If

    If e.KeyCode = Keys.A Then
      fImgSel = New ImageSelector
      If fImgSel.ShowDialog(Me.MdiParent) = Windows.Forms.DialogResult.OK Then Tiles(iSelectX, iSelectY).Floor.FloorImage = fImgSel.Tag
      fImgSel.Close()
    End If
    If e.KeyCode = Keys.S Then
      fImgSel = New ImageSelector
      If fImgSel.ShowDialog(Me.MdiParent) = Windows.Forms.DialogResult.OK Then Tiles(iSelectX, iSelectY).Floor.LeftWallImage = fImgSel.Tag
      fImgSel.Close()
    End If
    If e.KeyCode = Keys.D Then
      fImgSel = New ImageSelector
      If fImgSel.ShowDialog(Me.MdiParent) = Windows.Forms.DialogResult.OK Then Tiles(iSelectX, iSelectY).Floor.RightWallImage = fImgSel.Tag
      fImgSel.Close()
    End If
    If e.KeyCode = Keys.Q Then
      fImgSel = New ImageSelector
      If fImgSel.ShowDialog(Me.MdiParent) = Windows.Forms.DialogResult.OK Then Tiles(iSelectX, iSelectY).UpperLevel.FloorImage = fImgSel.Tag
      fImgSel.Close()
    End If
    If e.KeyCode = Keys.W Then
      fImgSel = New ImageSelector
      If fImgSel.ShowDialog(Me.MdiParent) = Windows.Forms.DialogResult.OK Then Tiles(iSelectX, iSelectY).UpperLevel.LeftWallImage = fImgSel.Tag
      fImgSel.Close()
    End If
    If e.KeyCode = Keys.E Then
      fImgSel = New ImageSelector
      If fImgSel.ShowDialog(Me.MdiParent) = Windows.Forms.DialogResult.OK Then Tiles(iSelectX, iSelectY).UpperLevel.RightWallImage = fImgSel.Tag
      fImgSel.Close()
    End If

    If e.KeyCode = Keys.F Then
      fFlgSel = New FlagsDialog
      fFlgSel.SetFlags(Tiles(iSelectX, iSelectY).Floor.Flags)
      If fFlgSel.ShowDialog(Me.MdiParent) = Windows.Forms.DialogResult.OK Then Tiles(iSelectX, iSelectY).Floor.Flags = fFlgSel.Tag
      fFlgSel.Close()
    End If

    If e.KeyCode = Keys.R Then
      fFlgSel = New FlagsDialog
      fFlgSel.SetFlags(Tiles(iSelectX, iSelectY).UpperLevel.Flags)
      If fFlgSel.ShowDialog(Me.MdiParent) = Windows.Forms.DialogResult.OK Then Tiles(iSelectX, iSelectY).UpperLevel.Flags = fFlgSel.Tag
      fFlgSel.Close()
    End If

    If e.KeyCode = Keys.T Then
      Dim tsd As TileScriptDialog
      tsd = New TileScriptDialog
      tsd.LoadTile(Tiles(iSelectX, iSelectY))
      tsd.ShowDialog()
    End If

    pRoomRender.Image = RenderScreen()
  End Sub


  Private Sub pRoomRender_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pRoomRender.Click
    tHandleMove.Focus()
    DirectCast(Me.MdiParent, EditorMain).SetHelpFile(".\help\mapedit.rtf")
  End Sub

  Private Sub RoomEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim iC As Integer

    cScriptEnter.Items.Clear()
    cScriptEnter.Items.Add("")
    cScriptLeave.Items.Clear()
    cScriptLeave.Items.Add("")
    cScriptTimer.Items.Clear()
    cScriptTimer.Items.Add("")

    For iC = 0 To UBound(sScriptDetails)
      cScriptEnter.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
      cScriptLeave.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
      cScriptTimer.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
    Next

  End Sub

End Class