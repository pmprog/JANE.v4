Public Class ScriptEditor
  Implements IMDIForm

  Private Const WORDSEPERATORS As String = vbTab & " ,.(){}[]'@#:;!""-+=<>" & vbCrLf

  Private Enum AutocompleteTypes As Integer
    AUTOCOMPLETE_NONE = 0
    AUTOCOMPLETE_TYPE = 1
    AUTOCOMPLETE_PROPERTY = 2
    AUTOCOMPLETE_FUNCTION = 3
  End Enum

  Private Structure AutocompleteKeyword
    Public keyWord As String
    Public keyType As String
  End Structure

  Dim myID As Integer
  Dim AutoCompleteList() As AutocompleteKeyword

  Public Sub Save() Implements IMDIForm.Save
    rtfCode.SaveFile(sProjectPath & "Scripts\" & Format(myID, "00000000") & ".jns", RichTextBoxStreamType.PlainText)
    sScriptDetails(myID) = tDesc.Text
    SaveScriptDetails()
  End Sub

  Public Sub LoadScript(ByVal IDnumber As Integer)
    Dim sFile As String
    sFile = Dir(sProjectPath & "Scripts\" & Format(IDnumber, "00000000") & ".jns")
    If sFile = "" Then
      Me.Close()
      Exit Sub
    End If

    myID = IDnumber
    Me.Text = "Script: " & myID & " [" & sScriptDetails(myID) & "]"
    tDesc.Text = sScriptDetails(myID)
    rtfCode.LoadFile(sProjectPath & "Scripts\" & sFile, RichTextBoxStreamType.PlainText)

    FindKeywords()
  End Sub

  Private Sub tDesc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tDesc.TextChanged
    Me.Text = "Script: " & myID & " [" & tDesc.Text & "]"
  End Sub

  Private Sub ScriptEditor_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    DirectCast(Me.MdiParent, EditorMain).SetHelpFile(".\help\script.rtf")
  End Sub

  Private Function GetSubstringContents(ByVal MasterText As String, ByVal OpeningString As String, ByVal ClosingString As String) As String
    Dim iStartIndex As Integer
    Dim iEndIndex As Integer
    iStartIndex = InStr(MasterText, OpeningString, CompareMethod.Text) + Len(OpeningString)
    iEndIndex = InStr(Mid(MasterText, iStartIndex), ClosingString, CompareMethod.Text) - 1
    Return Mid(MasterText, iStartIndex, iEndIndex)
  End Function

  Private Sub rtfCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtfCode.KeyDown
    Dim iC As Integer
    Dim iR As Integer
    Dim sPrefix As String
    Dim bAutoProcess As Boolean = False

    If e.Control And (Not e.Alt) And (Not e.Shift) And e.KeyCode = Keys.Space Then
      FindKeywords()
      lAutocomplete.Items.Clear()
      If Not AutoCompleteList Is Nothing Then
        sPrefix = ""
        For iC = rtfCode.SelectionStart To 1 Step -1
          If InStr(WORDSEPERATORS, Mid(rtfCode.Text, iC, 1), CompareMethod.Text) <> 0 Then
            sPrefix = Mid(rtfCode.Text, iC + 1, rtfCode.SelectionStart - iC)
            Exit For
          End If
        Next
        For iC = 0 To UBound(AutoCompleteList)
          If LCase(Mid(AutoCompleteList(iC).keyWord, 1, Len(sPrefix))) = LCase(sPrefix) Then lAutocomplete.Items.Add(iC)
        Next
      End If
      ShowAutoComplete()
      e.Handled = True
      e.SuppressKeyPress = True
      Exit Sub
    End If
    If lAutocomplete.Visible = False Then Exit Sub

    ' Handle autocomplete keys
    If e.KeyCode = Keys.Escape Then
      lAutocomplete.Visible = False
      e.Handled = True
      e.SuppressKeyPress = True
    ElseIf e.KeyCode = Keys.Up Then
      lAutocomplete.SelectedIndex = Math.Max(0, lAutocomplete.SelectedIndex - 1)
      e.Handled = True
      e.SuppressKeyPress = True
    ElseIf e.KeyCode = Keys.Down Then
      lAutocomplete.SelectedIndex = Math.Min(lAutocomplete.Items.Count - 1, lAutocomplete.SelectedIndex + 1)
      e.Handled = True
      e.SuppressKeyPress = True
    ElseIf e.KeyCode = Windows.Forms.Keys.PageUp Then
      lAutocomplete.SelectedIndex = Math.Max(0, lAutocomplete.SelectedIndex - 6)
      e.SuppressKeyPress = True
      e.Handled = True
    ElseIf e.KeyCode = Windows.Forms.Keys.PageDown Then
      lAutocomplete.SelectedIndex = Math.Min(lAutocomplete.Items.Count - 1, lAutocomplete.SelectedIndex + 6)
      e.SuppressKeyPress = True
      e.Handled = True
    ElseIf e.KeyCode = Windows.Forms.Keys.Tab Or e.KeyCode = Windows.Forms.Keys.Enter Or e.KeyCode = Windows.Forms.Keys.Return Or e.KeyCode = Windows.Forms.Keys.Space Then
      For iC = rtfCode.SelectionStart To 1 Step -1
        If InStr(WORDSEPERATORS, Mid(rtfCode.Text, iC, 1), CompareMethod.Text) <> 0 Then
          iR = rtfCode.SelectionStart - iC
          rtfCode.SelectionStart = iC ' + 1
          rtfCode.SelectionLength = iR
          rtfCode.SelectedText = lAutocomplete.SelectedItem
          Exit For
        End If
      Next
      lAutocomplete.Visible = False
      If e.KeyCode <> Windows.Forms.Keys.Space Then e.SuppressKeyPress = True
      e.Handled = True
    End If
  End Sub

  Private Sub rtfCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rtfCode.KeyPress
    Dim sPrefix As String
    Dim iC As Integer

    If e.KeyChar = "." Then
      lAutocomplete.Items.Clear()
      If Not AutoCompleteList Is Nothing Then
        sPrefix = ""
        For iC = rtfCode.SelectionStart To 1 Step -1
          If InStr(WORDSEPERATORS, Mid(rtfCode.Text, iC, 1), CompareMethod.Text) <> 0 Then
            sPrefix = Mid(rtfCode.Text, iC + 1, rtfCode.SelectionStart - iC)
            Exit For
          End If
        Next
        For iC = 0 To UBound(AutoCompleteList)
          If LCase(Mid(AutoCompleteList(iC).keyWord, 1, Len(sPrefix))) = LCase(sPrefix) Then
            Select Case UCase(AutoCompleteList(iC).keyType)
              Case "CHARACTER"
                lAutocomplete.Items.Add("Elevation")
                lAutocomplete.Items.Add("Facing")
                lAutocomplete.Items.Add("NextCharacter")
                lAutocomplete.Items.Add("PreviousCharacter")
                lAutocomplete.Items.Add("State")
                lAutocomplete.Items.Add("X")
                lAutocomplete.Items.Add("Y")
              Case "OBJECT"
              Case "ROOM"
              Case "TILE"
                lAutocomplete.Items.Add("Floor")
                lAutocomplete.Items.Add("Platform")
              Case "PLATFORM"
                lAutocomplete.Items.Add("Elevation")
                lAutocomplete.Items.Add("Height")
                lAutocomplete.Items.Add("FloorImage")
                lAutocomplete.Items.Add("LeftWallImage")
                lAutocomplete.Items.Add("RightWallImage")
                lAutocomplete.Items.Add("Flags")
                lAutocomplete.Items.Add("OnEntryScript")
                lAutocomplete.Items.Add("OnExitScript")
                lAutocomplete.Items.Add("OnWalkScript")
            End Select
            'lAutocomplete.Items.Add(AutoCompleteList(iC).keyWord)
          End If
        Next
      End If
      ShowAutoComplete()
    End If
  End Sub

  Private Sub ShowAutoComplete()
    Dim myPoint As Point
    myPoint = rtfCode.GetPositionFromCharIndex(rtfCode.SelectionStart)
    myPoint.Y += rtfCode.Font.GetHeight(rtfCode.CreateGraphics) + 2
    If myPoint.Y + lAutocomplete.Height > Panel3.Height Then myPoint.Y -= (lAutocomplete.Height + rtfCode.Font.GetHeight(rtfCode.CreateGraphics) + 4)
    If myPoint.X + lAutocomplete.Width > Panel3.Width Then myPoint.X = Panel3.Width - lAutocomplete.Width
    lAutocomplete.Location = myPoint
    lAutocomplete.Visible = True
  End Sub

  Private Function GetCompilerOrganisedCode() As String
    Dim sReturn As String

    sReturn = Replace(rtfCode.Text, vbTab, " ")
    While InStr(sReturn, "  ")
      sReturn = Replace(sReturn, "  ", " ")
    End While
    sReturn = Replace(sReturn, vbCrLf & " ", vbCrLf)

    Return sReturn
  End Function

  Private Sub FindKeywords()
    Dim sLines() As String
    Dim iC As Integer
    Dim sLine() As String

    AutoCompleteList = Nothing
    If rtfCode.Text = "" Then Exit Sub
    sLines = Split("Var Character ActiveCharacter" & vbCrLf & "Var Object ActiveObject" & vbCrLf & "Var Room ActiveRoom" & vbCrLf & "Var Integer ActiveGame" & vbCrLf & GetCompilerOrganisedCode(), vbCrLf)

    For iC = 0 To UBound(sLines)
      If UCase(Mid(Trim(sLines(iC)), 1, 3)) = "VAR" Then
        sLine = Split(sLines(iC), " ")
        If AutoCompleteList Is Nothing Then ReDim AutoCompleteList(0) Else ReDim Preserve AutoCompleteList(UBound(AutoCompleteList) + 1)
        AutoCompleteList(UBound(AutoCompleteList)).keyWord = sLine(2)
        AutoCompleteList(UBound(AutoCompleteList)).keyType = sLine(1)
      End If
    Next

    AddSystemInclude("Kill", "Function")
    AddSystemInclude("Hurt", "Function")
    AddSystemInclude("ChangeState", "Function")
    AddSystemInclude("WarpTo", "Function")
    AddSystemInclude("Spawn", "Function")
    AddSystemInclude("Destroy", "Function")
    AddSystemInclude("FirstObject", "Function")
    AddSystemInclude("FirstCharacter", "Function")
    AddSystemInclude("CallScript", "Function")
    AddSystemInclude("Random", "Function")
    AddSystemInclude("GiveWeapon", "Function")
    AddSystemInclude("HasWeapon", "Function")
    AddSystemInclude("TakeWeapon", "Function")
    AddSystemInclude("GetSkinType", "Function")
    AddSystemInclude("SetTemporarySkin", "Function")
    AddSystemInclude("RestoreSkin", "Function")
    AddSystemInclude("AddScore", "Function")
    AddSystemInclude("PostNetworkMessage", "Function")

    AddSystemInclude("WEAPON_NONE", "Constant")
    AddSystemInclude("WEAPON_NINTO", "Constant")
    AddSystemInclude("WEAPON_NUNCHAKU", "Constant")
    AddSystemInclude("WEAPON_SHURIKEN", "Constant")
    AddSystemInclude("WEAPON_STAFF", "Constant")
    AddSystemInclude("WEAPON_SMOKEBOMB", "Constant")
    AddSystemInclude("WEAPON_SLEEPBOMB", "Constant")
    AddSystemInclude("WEAPON_KUSARIGAMA", "Constant")
    AddSystemInclude("WEAPON_KITANA", "Constant")
    AddSystemInclude("WEAPON_NINJAFLAG", "Constant")
    AddSystemInclude("WEAPON_GUARDFLAG", "Constant")

    AddSystemInclude("SKINTYPE_NINJA", "Constant")
    AddSystemInclude("SKINTYPE_GUARD", "Constant")
    AddSystemInclude("SKINTYPE_SAMURAI", "Constant")

    AddSystemInclude("SKIN_NINJA_DKBLUE", "Constant")
    AddSystemInclude("SKIN_NINJA_RED", "Constant")
    AddSystemInclude("SKIN_NINJA_GREEN", "Constant")
    AddSystemInclude("SKIN_NINJA_YELLOW", "Constant")
    AddSystemInclude("SKIN_NINJA_DKGREY", "Constant")
    AddSystemInclude("SKIN_NINJA_CYAN", "Constant")
    AddSystemInclude("SKIN_NINJA_WHITE", "Constant")
    AddSystemInclude("SKIN_SAMURAI_DKBLUE", "Constant")
    AddSystemInclude("SKIN_SAMURAI_RED", "Constant")
    AddSystemInclude("SKIN_SAMURAI_GREEN", "Constant")
    AddSystemInclude("SKIN_SAMURAI_YELLOW", "Constant")
    AddSystemInclude("SKIN_SAMURAI_DKGREY", "Constant")
    AddSystemInclude("SKIN_SAMURAI_CYAN", "Constant")
    AddSystemInclude("SKIN_SAMURAI_WHITE", "Constant")

    AddSystemInclude("FACING_NORTH", "Constant")
    AddSystemInclude("FACING_EAST", "Constant")
    AddSystemInclude("FACING_SOUTH", "Constant")
    AddSystemInclude("FACING_WEST", "Constant")

    AddSystemInclude("STATE_STANDING", "Constant")
    AddSystemInclude("STATE_WALKING", "Constant")
    AddSystemInclude("STATE_HURTING", "Constant")
    AddSystemInclude("STATE_DYING", "Constant")
    AddSystemInclude("STATE_FALLING", "Constant")
    AddSystemInclude("STATE_CLIMBING", "Constant")
    AddSystemInclude("STATE_JUMPING", "Constant")
    AddSystemInclude("STATE_ROLLING", "Constant")
    AddSystemInclude("STATE_LYINGDOWN", "Constant")
    AddSystemInclude("STATE_JABBING", "Constant")
    AddSystemInclude("STATE_SLASHING", "Constant")
    AddSystemInclude("STATE_STABBING", "Constant")
    AddSystemInclude("STATE_BLOCKING", "Constant")
    AddSystemInclude("STATE_PICKUP", "Constant")

  End Sub

  Private Sub AddSystemInclude(ByVal systemName As String, ByVal systemType As String)
    If AutoCompleteList Is Nothing Then ReDim AutoCompleteList(0) Else ReDim Preserve AutoCompleteList(UBound(AutoCompleteList) + 1)
    AutoCompleteList(UBound(AutoCompleteList)).keyWord = systemName
    AutoCompleteList(UBound(AutoCompleteList)).keyType = systemType
  End Sub

  Private Sub ScriptEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub
End Class