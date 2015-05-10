Public Class ObjectEditor
  Implements IMDIForm

  Dim workingID As Integer

  Private Sub ObjectEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, MyBase.Activated
    Dim iC As Integer

    DirectCast(Me.MdiParent, EditorMain).SetHelpFile(".\help\object.rtf")

    cScriptDamage.Items.Clear()
    cScriptDamage.Items.Add("")
    cScriptPickedup.Items.Clear()
    cScriptPickedup.Items.Add("")
    cScriptCollidePlayer.Items.Clear()
    cScriptCollidePlayer.Items.Add("")
    cScriptCollideScenery.Items.Clear()
    cScriptCollideScenery.Items.Add("")
    For iC = 0 To UBound(sScriptDetails)
      cScriptDamage.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
      cScriptPickedup.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
      cScriptCollidePlayer.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
      cScriptCollideScenery.Items.Add(Format(iC, "00000000") & " - " & sScriptDetails(iC))
    Next

  End Sub

  Public Sub Save() Implements IMDIForm.Save
    Dim sScript() As String

    sObjectsDetails(workingID) = tDesc.Text
    SaveObjectDetails()

    FileOpen(1, sProjectPath & "Objects\" & Format(workingID, "00000000") & ".jno", OpenMode.Output)
    If cMove.Checked Then PrintLine(1, "Moving=Yes") Else PrintLine(1, "Moving=No")
    If cGrav.Checked Then PrintLine(1, "Falling=Yes") Else PrintLine(1, "Falling=No")

    If rNoStart.Checked Then PrintLine(1, "Start=None")
    If rPlayer.Checked Then PrintLine(1, "Start=Player")
    If rCPUE.Checked Then PrintLine(1, "Start=CPU Easy")
    If rCPUM.Checked Then PrintLine(1, "Start=CPU Medium")
    If rCPUH.Checked Then PrintLine(1, "Start=CPU Hard")

    If cTeamN.Checked Then PrintLine(1, "TeamNinjaStart=Yes") Else PrintLine(1, "TeamNinjaStart=No")
    If cTeamS.Checked Then PrintLine(1, "TeamSamuraiStart=Yes") Else PrintLine(1, "TeamSamuraiStart=No")

    If cNinto.Checked Then PrintLine(1, "CPUWeapon=Ninto")
    If cNunchaku.Checked Then PrintLine(1, "CPUWeapon=Nunchaku")
    If cShuriken.Checked Then PrintLine(1, "CPUWeapon=Shuriken")
    If cYari.Checked Then PrintLine(1, "CPUWeapon=Yari")
    If cSmoke.Checked Then PrintLine(1, "CPUWeapon=Smokebomb")
    If cSleep.Checked Then PrintLine(1, "CPUWeapon=Sleepbomb")
    If cKusamigara.Checked Then PrintLine(1, "CPUWeapon=Kusamigara")
    If cKitana.Checked Then PrintLine(1, "CPUWeapon=Kitana")

    If cRepeat.Checked Then PrintLine(1, "RepeatAnimation=Yes") Else PrintLine(1, "RepeatAnimation=No")
    PrintLine(1, "RepeatFrame=" & nRepeatAt.Value)


    If cScriptDamage.Text = "" Then
      PrintLine(1, "OnDamage=-1")
    Else
      sScript = Split(cScriptDamage.Text, " - ")
      PrintLine(1, "OnDamage=" & CInt(sScript(0)))
    End If

    If cScriptPickedup.Text = "" Then
      PrintLine(1, "OnPickup=-1")
    Else
      sScript = Split(cScriptPickedup.Text, " - ")
      PrintLine(1, "OnPickup=" & CInt(sScript(0)))
    End If

    If cScriptCollidePlayer.Text = "" Then
      PrintLine(1, "OnCollidePlayer=-1")
    Else
      sScript = Split(cScriptCollidePlayer.Text, " - ")
      PrintLine(1, "OnCollidePlayer=" & CInt(sScript(0)))
    End If

    If cScriptCollideScenery.Text = "" Then
      PrintLine(1, "OnCollideScenery=-1")
    Else
      sScript = Split(cScriptCollideScenery.Text, " - ")
      PrintLine(1, "OnCollideScenery=" & CInt(sScript(0)))
    End If

    FileClose()

  End Sub

  Public Sub LoadObject(ByVal objectID As Integer)
    Dim sLine() As String

    workingID = objectID
    tDesc.Text = sObjectsDetails(objectID)

    FileOpen(1, sProjectPath & "Objects\" & Format(objectID, "00000000") & ".jno", OpenMode.Input)
    Do Until EOF(1)
      sLine = Split(LineInput(1), "=")
      Select Case sLine(0)
        Case "Moving"
          If UCase(sLine(1)) = "YES" Then cMove.Checked = True Else cMove.Checked = False
        Case "Falling"
          If UCase(sLine(1)) = "YES" Then cGrav.Checked = True Else cGrav.Checked = False
        Case "Start"
          Select Case LCase(sLine(1))
            Case "none"
              rNoStart.Checked = True
            Case "player"
              rPlayer.Checked = True
            Case "cpu easy"
              rCPUE.Checked = True
            Case "cpu medium"
              rCPUM.Checked = True
            Case "cpu hard"
              rCPUH.Checked = True
          End Select
        Case "TeamNinjaStart"
          If UCase(sLine(1)) = "YES" Then cTeamN.Checked = True Else cTeamN.Checked = False
        Case "TeamSamuraiStart"
          If UCase(sLine(1)) = "YES" Then cTeamS.Checked = True Else cTeamS.Checked = False
        Case "CPUWeapon"
          If LCase(sLine(1)) = "ninto" Then cNinto.Checked = True
          If LCase(sLine(1)) = "nunchaku" Then cNunchaku.Checked = True
          If LCase(sLine(1)) = "shuriken" Then cShuriken.Checked = True
          If LCase(sLine(1)) = "yari" Then cYari.Checked = True
          If LCase(sLine(1)) = "smokebomb" Then cSmoke.Checked = True
          If LCase(sLine(1)) = "sleepbomb" Then cSleep.Checked = True
          If LCase(sLine(1)) = "kusamigara" Then cKusamigara.Checked = True
          If LCase(sLine(1)) = "kitana" Then cKitana.Checked = True
        Case "RepeatAnimation"
          If UCase(sLine(1)) = "YES" Then cRepeat.Checked = True Else cRepeat.Checked = False
        Case "RepeatFrame"
          nRepeatAt.Value = sLine(1)
        Case "OnDamage"
          cScriptDamage.SelectedIndex = sLine(1) + 1
        Case "OnPickup"
          cScriptPickedup.SelectedIndex = sLine(1) + 1
        Case "OnCollidePlayer"
          cScriptCollidePlayer.SelectedIndex = sLine(1) + 1
        Case "OnCollideScenery"
          cScriptCollideScenery.SelectedIndex = sLine(1) + 1
      End Select
    Loop
    FileClose()
  End Sub

  Private Sub cRepeat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cRepeat.CheckedChanged
    nRepeatAt.Enabled = cRepeat.Checked
  End Sub
End Class