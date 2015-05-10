Imports System.Windows.Forms

Public Class TileScriptDialog

  Dim myTile As Tile

  Public Sub LoadTile(ByVal inTile As Tile)
    myTile = inTile
    cFloorEnter.SelectedIndex = inTile.Floor.OnEntryID + 1
    cFloorExit.SelectedIndex = inTile.Floor.OnExitID + 1
    cFloorWalk.SelectedIndex = inTile.Floor.OnWalkID + 1
    cUpperEnter.SelectedIndex = inTile.UpperLevel.OnEntryID + 1
    cUpperExit.SelectedIndex = inTile.UpperLevel.OnExitID + 1
    cUpperWalk.SelectedIndex = inTile.UpperLevel.OnWalkID + 1
  End Sub

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    myTile.Floor.OnEntryID = cFloorEnter.SelectedIndex - 1
    myTile.Floor.OnExitID = cFloorExit.SelectedIndex - 1
    myTile.Floor.OnWalkID = cFloorWalk.SelectedIndex - 1
    myTile.UpperLevel.OnEntryID = cUpperEnter.SelectedIndex - 1
    myTile.UpperLevel.OnExitID = cUpperExit.SelectedIndex - 1
    myTile.UpperLevel.OnWalkID = cUpperWalk.SelectedIndex - 1

    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub TileScriptDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim iC As Integer

    cFloorEnter.Items.Clear()
    cFloorEnter.Items.Add("")
    cFloorExit.Items.Clear()
    cFloorExit.Items.Add("")
    cFloorWalk.Items.Clear()
    cFloorWalk.Items.Add("")
    cUpperEnter.Items.Clear()
    cUpperEnter.Items.Add("")
    cUpperExit.Items.Clear()
    cUpperExit.Items.Add("")
    cUpperWalk.Items.Clear()
    cUpperWalk.Items.Add("")

    For iC = 0 To UBound(sScriptDetails)
      cFloorEnter.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
      cFloorExit.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
      cFloorWalk.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
      cUpperEnter.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
      cUpperExit.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
      cUpperWalk.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
    Next
  End Sub
End Class
