Public Class EditorMain

  Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
    Dim fFrm As Form

    For Each fFrm In Me.MdiChildren
      fFrm.Close()
    Next

    If Me.MdiChildren.Length > 0 Then Exit Sub

    Me.Close()
  End Sub

  Private Sub mnuSaveMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Dim fFrm As Form

    For Each fFrm In Me.MdiChildren
      DirectCast(fFrm, IMDIForm).Save()
    Next
  End Sub

  Private Sub mnuSaveDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveDoc.Click
    DirectCast(Me.ActiveMdiChild, IMDIForm).Save()
  End Sub

  Private Sub mnuNewMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewMap.Click
    fbd.SelectedPath = IO.Path.GetDirectoryName(Application.ExecutablePath)
    fbd.Description = "Select folder to store new map in"
    fbd.ShowNewFolderButton = True
    If fbd.ShowDialog = Windows.Forms.DialogResult.OK Then CreateProject(fbd.SelectedPath)
  End Sub

  Private Sub mnuLoadMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLoadMap.Click
    fbd.SelectedPath = IO.Path.GetDirectoryName(Application.ExecutablePath)
    fbd.Description = "Select folder to load map from"
    fbd.ShowNewFolderButton = False
    If fbd.ShowDialog = Windows.Forms.DialogResult.OK Then OpenProject(fbd.SelectedPath)
  End Sub

#Region " Project Funcs "

  Private Sub CreateProject(ByVal inPath As String)
    Dim bTemp As Bitmap

    If sProjectPath <> "" Then CloseProject()

    sProjectPath = inPath
    If Not sProjectPath.EndsWith("\") Then sProjectPath &= "\"
    If Dir("Images", FileAttribute.Directory) <> "" Or Dir("Rooms", FileAttribute.Directory) <> "" Or Dir("Objects", FileAttribute.Directory) <> "" Or Dir("Scripts", FileAttribute.Directory) <> "" Then
      CloseProject()
      Exit Sub
    End If
    MkDir(sProjectPath & "Images")
    MkDir(sProjectPath & "Rooms")
    MkDir(sProjectPath & "Objects")
    MkDir(sProjectPath & "Scripts")

    nImages = prjTree.Nodes.Add("Images")
    nImages.ImageIndex = 0
    nImages.SelectedImageIndex = 0
    nRooms = prjTree.Nodes.Add("Rooms")
    nRooms.ImageIndex = 0
    nRooms.SelectedImageIndex = 0
    nObjects = prjTree.Nodes.Add("Objects")
    nObjects.ImageIndex = 0
    nObjects.SelectedImageIndex = 0
    nScripts = prjTree.Nodes.Add("Scripts")
    nScripts.ImageIndex = 0
    nScripts.SelectedImageIndex = 0

    FileOpen(1, sProjectPath & "Images\detail.txt", OpenMode.Output)
    PrintLine(1, "0,0" & vbTab & "Default Floor Tile")
    bTemp = New Bitmap(My.Resources.default_floor_tile)
    bTemp.Save(sProjectPath & "Images\00000000.png", Imaging.ImageFormat.Png)
    With nImages.Nodes.Add("00000000 - Default Floor Tile")
      .ImageIndex = 1
      .SelectedImageIndex = 1
      .Tag = 0
    End With
    PrintLine(1, "0,0" & vbTab & "Default Left Wall")
    bTemp = New Bitmap(My.Resources.wall_left)
    bTemp.Save(sProjectPath & "Images\00000001.png", Imaging.ImageFormat.Png)
    With nImages.Nodes.Add("00000001 - Default Left Wall")
      .ImageIndex = 1
      .SelectedImageIndex = 1
      .Tag = 0
    End With
    PrintLine(1, "0,0" & vbTab & "Default Right Wall")
    bTemp = New Bitmap(My.Resources.wall_right)
    bTemp.Save(sProjectPath & "Images\00000002.png", Imaging.ImageFormat.Png)
    With nImages.Nodes.Add("00000002 - Default Right Wall")
      .ImageIndex = 1
      .SelectedImageIndex = 1
      .Tag = 0
    End With
    FileClose()
    FileOpen(1, sProjectPath & "Rooms\detail.txt", OpenMode.Output)
    FileClose()
    FileOpen(1, sProjectPath & "Objects\detail.txt", OpenMode.Output)
    FileClose()
    FileOpen(1, sProjectPath & "Scripts\detail.txt", OpenMode.Output)
    FileClose()

    mnuBuildMap.Enabled = True
    mnuProject.Enabled = True

  End Sub

  Private Sub OpenProject(ByVal inPath As String)
    Dim iC As Integer
    Dim sTmp() As String
    Dim iTmp() As String

    If sProjectPath <> "" Then CloseProject()

    sProjectPath = inPath
    If Not sProjectPath.EndsWith("\") Then sProjectPath &= "\"

    nImages = prjTree.Nodes.Add("Images")
    nImages.ImageIndex = 0
    nImages.SelectedImageIndex = 0
    nRooms = prjTree.Nodes.Add("Rooms")
    nRooms.ImageIndex = 0
    nRooms.SelectedImageIndex = 0
    nObjects = prjTree.Nodes.Add("Objects")
    nObjects.ImageIndex = 0
    nObjects.SelectedImageIndex = 0
    nScripts = prjTree.Nodes.Add("Scripts")
    nScripts.ImageIndex = 0
    nScripts.SelectedImageIndex = 0

    iC = 0
    FileOpen(1, sProjectPath & "Images\detail.txt", OpenMode.Input)
    Do Until EOF(1)
      ' Load Image descriptions
      ReDim Preserve sImageDetails(iC)
      sTmp = Split(LineInput(1), vbTab)
      sImageDetails(iC).Description = sTmp(1)
      iTmp = Split(sTmp(0), ",")
      sImageDetails(iC).Hotspot = New Point(iTmp(0), iTmp(1))
      With nImages.Nodes.Add(Format(iC, "00000000") & " - " & sTmp(1))
        .ImageIndex = 1
        .SelectedImageIndex = 1
        .Tag = iC
      End With
      iC += 1
    Loop
    FileClose()

    iC = 0
    FileOpen(1, sProjectPath & "Rooms\detail.txt", OpenMode.Input)
    Do Until EOF(1)
      ' Load Image descriptions
      ReDim Preserve sRoomDetails(iC)
      sRoomDetails(iC) = LineInput(1)
      With nRooms.Nodes.Add(Format(iC, "00000000") & " - " & sRoomDetails(iC))
        .ImageIndex = 2
        .SelectedImageIndex = 2
        .Tag = iC
      End With
      iC += 1
    Loop
    FileClose()

    iC = 0
    FileOpen(1, sProjectPath & "Objects\detail.txt", OpenMode.Input)
    Do Until EOF(1)
      ' Load script descriptions
      ReDim Preserve sObjectsDetails(iC)
      sObjectsDetails(iC) = LineInput(1)
      With nObjects.Nodes.Add(Format(iC, "00000000") & " - " & sObjectsDetails(iC))
        .ImageIndex = 3
        .SelectedImageIndex = 3
        .Tag = iC
      End With
      iC += 1
    Loop
    FileClose()

    iC = 0
    FileOpen(1, sProjectPath & "Scripts\detail.txt", OpenMode.Input)
    Do Until EOF(1)
      ' Load script descriptions
      ReDim Preserve sScriptDetails(iC)
      sScriptDetails(iC) = LineInput(1)
      With nScripts.Nodes.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
        .ImageIndex = 4
        .SelectedImageIndex = 4
        .Tag = iC
      End With
      iC += 1
    Loop
    FileClose()


    mnuBuildMap.Enabled = True
    mnuProject.Enabled = True

  End Sub

  Private Sub BuildProject(ByVal inPath As String)
    ' TODO: Implement Builder
    MsgBox("Feature is not yet implemented!")
  End Sub

  Private Sub CloseProject()
    sProjectPath = ""
    mnuBuildMap.Enabled = False
    mnuProject.Enabled = False
    sImageDetails = Nothing
    sScriptDetails = Nothing
    prjTree.Nodes.Clear()
  End Sub

  Public Sub RefreshNodes()
    ' TODO: Refresh nodes
  End Sub

#End Region

#Region " Add Components "

  Private Sub mnuAddImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddImage.Click
    Dim fNID As NewImageDialog
    fNID = New NewImageDialog
    fNID.Tag = Me
    fNID.ShowDialog(Me)
  End Sub

  Private Sub mnuAddScript_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddScript.Click
    Dim fSE As ScriptEditor
    Dim iC As Integer

    If sScriptDetails Is Nothing Then iC = 0 Else iC = UBound(sScriptDetails) + 1
    ReDim Preserve sScriptDetails(iC)
    sScriptDetails(iC) = "New Script"
    SaveScriptDetails()

    FileOpen(1, sProjectPath & "Scripts\" & Format(iC, "00000000") & ".jns", OpenMode.Output)
    FileClose()

    With nScripts.Nodes.Add(Format(iC, "00000000") & " - New Script")
      .ImageIndex = 4
      .SelectedImageIndex = 4
      .Tag = iC
    End With


    fSE = New ScriptEditor
    fSE.MdiParent = Me
    fSE.Show()
    fSE.LoadScript(iC)

  End Sub


  Private Sub mnuAddRoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddRoom.Click
    Dim fRE As RoomEditor
    Dim iC As Integer
    Dim iX As Integer
    Dim iY As Integer
    Dim sLineData() As String

    ' TODO: Create room file template, and update descriptions
    If sRoomDetails Is Nothing Then iC = 0 Else iC = UBound(sRoomDetails) + 1
    ReDim Preserve sRoomDetails(iC)
    sRoomDetails(iC) = "New Room"
    SaveRoomDetails()

    FileOpen(1, sProjectPath & "Rooms\" & Format(iC, "00000000") & ".jnr", OpenMode.Output)

    ReDim sLineData(1)
    sLineData(0) = "OnEntry"
    sLineData(1) = -1
    PrintLine(1, Join(sLineData, vbTab))
    sLineData(0) = "OnExit"
    sLineData(1) = -1
    PrintLine(1, Join(sLineData, vbTab))
    sLineData(0) = "TimerInterval"
    sLineData(1) = 0
    PrintLine(1, Join(sLineData, vbTab))
    sLineData(0) = "OnTimer"
    sLineData(1) = -1
    PrintLine(1, Join(sLineData, vbTab))


    For iY = 0 To 34
      For iX = 0 To 34
        If (iY < 12 Or iX > iY - 12) And (iX < 12 Or iY > iX - 12) Then
          ReDim sLineData(10)
          sLineData(0) = iX & "," & iY
          sLineData(1) = "Floor"
          sLineData(2) = 0
          sLineData(3) = 0
          sLineData(4) = 0
          sLineData(5) = 1
          sLineData(6) = 2
          sLineData(7) = 0
          sLineData(8) = -1
          sLineData(9) = -1
          sLineData(10) = -1
          PrintLine(1, Join(sLineData, vbTab))
          sLineData(1) = "Platform"
          PrintLine(1, Join(sLineData, vbTab))
        End If
      Next
    Next

    FileClose()

    With nRooms.Nodes.Add(Format(iC, "00000000") & " - New Script")
      .ImageIndex = 2
      .SelectedImageIndex = 2
      .Tag = iC
    End With

    fRE = New RoomEditor
    fRE.MdiParent = Me
    fRE.Show()
    fRE.LoadRoom(iC)
  End Sub


  Private Sub mnuAddObject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddObject.Click
    Dim fObjEd As ObjectEditor
    Dim iC As Integer

    If sObjectsDetails Is Nothing Then iC = 0 Else iC = UBound(sObjectsDetails) + 1
    ReDim Preserve sObjectsDetails(iC)
    sObjectsDetails(iC) = "New Object"
    SaveObjectDetails()

    FileOpen(1, sProjectPath & "Objects\" & Format(iC, "00000000") & ".jno", OpenMode.Output)
    FileClose()

    With nObjects.Nodes.Add(Format(iC, "00000000") & " - New Object")
      .ImageIndex = 3
      .SelectedImageIndex = 3
      .Tag = iC
    End With

    fObjEd = New ObjectEditor
    fObjEd.MdiParent = Me
    fObjEd.Show()
  End Sub


#End Region

  Private Sub EditorMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    rtfHelp.Tag = ""
    rtfHelp.LoadFile(".\help\welcome.rtf")
  End Sub

  Private Sub EditorMain_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MdiChildActivate
    Dim bActive As Boolean = False
    Dim fTmp As Form

    For Each fTmp In Me.MdiChildren
      If fTmp.Visible Then
        bActive = True
        Exit For
      End If
    Next

    mnuSaveDoc.Enabled = bActive
  End Sub

  Private Sub prjTree_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles prjTree.DoubleClick
    Dim fImg As ImageEditor
    Dim fRoom As RoomEditor
    Dim fObj As ObjectEditor
    Dim fScr As ScriptEditor

    If prjTree.SelectedNode Is Nothing Then Exit Sub

    Select Case prjTree.SelectedNode.ImageIndex
      Case 1
        fImg = New ImageEditor
        fImg.MdiParent = Me
        fImg.Show()
        fImg.LoadImageDetail(prjTree.SelectedNode.Tag)
      Case 2
        fRoom = New RoomEditor
        fRoom.MdiParent = Me
        fRoom.Show()
        fRoom.LoadRoom(prjTree.SelectedNode.Tag)
      Case 3
        fObj = New ObjectEditor
        fObj.MdiParent = Me
        fObj.Show()
        fObj.LoadObject(prjTree.SelectedNode.Tag)
      Case 4
        fScr = New ScriptEditor
        fScr.MdiParent = Me
        fScr.Show()
        fScr.LoadScript(prjTree.SelectedNode.Tag)
    End Select
  End Sub

  Public Sub SetHelpFile(ByVal rtfFileName As String)
    rtfHelp.Tag = rtfFileName
  End Sub

  Private Sub DynamicHelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DynamicHelpToolStripMenuItem.Click
    dockMan.DockingManager.ShowDockObject(dockMan.DockingManager.DockObjects.Item("Help"))
    If rtfHelp.Tag <> "" Then rtfHelp.LoadFile(rtfHelp.Tag)
  End Sub

  Private Sub mnuImportImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportImage.Click
    Dim iC As Integer

    If iid.ShowDialog = Windows.Forms.DialogResult.OK Then
      If sImageDetails Is Nothing Then iC = 0 Else iC = UBound(sImageDetails) + 1
      ReDim Preserve sImageDetails(iC)

      IO.File.Copy(iid.FileName, sProjectPath & "Images\" & Format(iC, "00000000") & "." & IO.Path.GetExtension(iid.FileName))
      With nImages.Nodes.Add(Format(iC, "00000000") & " - Imported Image")
        .ImageIndex = 1
        .SelectedImageIndex = 1
        .Tag = iC
      End With
      sImageDetails(iC).Description = "Imported Image"
      sImageDetails(iC).Hotspot = New Point(0, 0)
      SaveImageDetails()
    End If
  End Sub

  Private Sub SaveLayoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveLayoutToolStripMenuItem.Click
    Dim iFile As IO.FileStream

    If Dir(Replace(Application.ExecutablePath, ".exe", ".wnd", , , CompareMethod.Text)) <> "" Then Kill(Replace(Application.ExecutablePath, ".exe", ".wnd", , , CompareMethod.Text))
    iFile = IO.File.Open(Replace(Application.ExecutablePath, ".exe", ".wnd", , , CompareMethod.Text), IO.FileMode.Create)
    dockMan.SaveConfigToStream(iFile)
    iFile.Close()
  End Sub

  Private Sub dockMan_Loaded(ByVal sender As Object, ByVal e As System.EventArgs) Handles dockMan.Loaded
    Dim iFile As IO.FileStream
    If Dir(Replace(Application.ExecutablePath, ".exe", ".wnd", , , CompareMethod.Text)) <> "" Then
      iFile = IO.File.Open(Replace(Application.ExecutablePath, ".exe", ".wnd", , , CompareMethod.Text), IO.FileMode.Open)
      dockMan.LoadConfigFromStream(iFile)
      iFile.Close()
    End If

  End Sub

End Class
