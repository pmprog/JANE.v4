Imports System.Windows.Forms

Public Class FlagsDialog

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Dim iResult As Integer = 0

    If cCanEnter.Checked Then iResult += CAN_ENTER
    If cCeiling.Checked Then iResult += IS_CEILING
    If cClimbFL.Checked Then iResult += CLIMB_LEFTF
    If cClimbFR.Checked Then iResult += CLIMB_RIGHTF
    If cClimbBL.Checked Then iResult += CLIMB_LEFTR
    If cClimbBR.Checked Then iResult += CLIMB_RIGHTR
    If cNonSolid.Checked Then iResult += NONSOLID

    Me.Tag = iResult
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Public Sub SetFlags(ByVal iFlags As Integer)
    If (iFlags And CAN_ENTER) = CAN_ENTER Then cCanEnter.Checked = True Else cCanEnter.Checked = False
    If (iFlags And IS_CEILING) = IS_CEILING Then cCeiling.Checked = True Else cCeiling.Checked = False
    If (iFlags And CLIMB_LEFTF) = CLIMB_LEFTF Then cClimbFL.Checked = True Else cClimbFL.Checked = False
    If (iFlags And CLIMB_RIGHTF) = CLIMB_RIGHTF Then cClimbFR.Checked = True Else cClimbFR.Checked = False
    If (iFlags And CLIMB_LEFTR) = CLIMB_LEFTR Then cClimbBL.Checked = True Else cClimbBL.Checked = False
    If (iFlags And CLIMB_RIGHTR) = CLIMB_RIGHTR Then cClimbBR.Checked = True Else cClimbBR.Checked = False
    If (iFlags And NONSOLID) = NONSOLID Then cNonSolid.Checked = True Else cNonSolid.Checked = False
  End Sub
End Class
