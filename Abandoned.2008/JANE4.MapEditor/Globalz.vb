Public Module Globalz

  Public sProjectPath As String
  Public nImages As TreeNode
  Public nRooms As TreeNode
  Public nObjects As TreeNode
  Public nScripts As TreeNode

  Public Structure ImageDetail
    Dim Description As String
    Dim Hotspot As Point
  End Structure

  Public sImageDetails() As ImageDetail = Nothing
  Public sRoomDetails() As String = Nothing
  Public sObjectsDetails() As String = Nothing
  Public sScriptDetails() As String = Nothing

  Public Structure Platform
    Dim Elevation As Integer
    Dim Height As Integer
    Dim FloorImage As Integer
    Dim LeftWallImage As Integer
    Dim RightWallImage As Integer
    Dim Flags As Integer
    Dim OnExitID As Integer
    Dim OnEntryID As Integer
    Dim OnWalkID As Integer
  End Structure

  Public Structure Tile
    Dim Floor As Platform
    Dim UpperLevel As Platform
  End Structure


  Public Sub SaveImageDetails()
    Dim iC As Integer
    FileOpen(1, sProjectPath & "Images\detail.txt", OpenMode.Output)
    If Not sImageDetails Is Nothing Then
      For iC = 0 To UBound(sImageDetails)
        PrintLine(1, sImageDetails(iC).Hotspot.X & "," & sImageDetails(iC).Hotspot.Y & vbTab & sImageDetails(iC).Description)
      Next
    End If
    FileClose()
  End Sub

  Public Sub SaveRoomDetails()
    Dim iC As Integer
    FileOpen(1, sProjectPath & "Rooms\detail.txt", OpenMode.Output)
    If Not sRoomDetails Is Nothing Then
      For iC = 0 To UBound(sRoomDetails)
        PrintLine(1, sRoomDetails(iC))
      Next
    End If
    FileClose()
  End Sub

  Public Sub SaveObjectDetails()
    Dim iC As Integer
    FileOpen(1, sProjectPath & "Objects\detail.txt", OpenMode.Output)
    If Not sObjectsDetails Is Nothing Then
      For iC = 0 To UBound(sObjectsDetails)
        PrintLine(1, sObjectsDetails(iC))
      Next
    End If
    FileClose()
  End Sub

  Public Sub SaveScriptDetails()
    Dim iC As Integer
    FileOpen(1, sProjectPath & "Scripts\detail.txt", OpenMode.Output)
    If Not sScriptDetails Is Nothing Then
      For iC = 0 To UBound(sScriptDetails)
        PrintLine(1, sScriptDetails(iC))
      Next
    End If
    FileClose()
  End Sub

End Module
