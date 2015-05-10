Imports System.Windows.Forms

Public Class ImageSelector

  Dim iChecked As Integer

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.Tag = iChecked
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Hide()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub ImageSelector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    vScroll_Scroll(Nothing, Nothing)
  End Sub

  Private Sub vScroll_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles vScroll.Scroll
    Dim sFile As String

    sFile = Dir(sProjectPath & "Images\" & Format(vScroll.Value, "00000000") & ".*")
    If sFile <> "" Then
      PictureBox1.Image = New Bitmap(sProjectPath & "Images\" & sFile)
      RadioButton1.Enabled = True
      If iChecked = vScroll.Value Then RadioButton1.Checked = True Else RadioButton1.Checked = False
      RadioButton1.Tag = vScroll.Value
    Else
      PictureBox1.Image = Nothing
      RadioButton1.Enabled = False
      RadioButton1.Checked = False
    End If

    sFile = Dir(sProjectPath & "Images\" & Format(vScroll.Value + 1, "00000000") & ".*")
    If sFile <> "" Then
      PictureBox2.Image = New Bitmap(sProjectPath & "Images\" & sFile)
      RadioButton2.Enabled = True
      If iChecked = vScroll.Value + 1 Then RadioButton2.Checked = True Else RadioButton2.Checked = False
      RadioButton2.Tag = vScroll.Value + 1
    Else
      PictureBox2.Image = Nothing
      RadioButton2.Enabled = False
      RadioButton2.Checked = False
    End If

    sFile = Dir(sProjectPath & "Images\" & Format(vScroll.Value + 2, "00000000") & ".*")
    If sFile <> "" Then
      PictureBox3.Image = New Bitmap(sProjectPath & "Images\" & sFile)
      RadioButton3.Enabled = True
      If iChecked = vScroll.Value + 2 Then RadioButton3.Checked = True Else RadioButton3.Checked = False
      RadioButton3.Tag = vScroll.Value + 2
    Else
      PictureBox3.Image = Nothing
      RadioButton3.Enabled = False
      RadioButton3.Checked = False
    End If

    sFile = Dir(sProjectPath & "Images\" & Format(vScroll.Value + 3, "00000000") & ".*")
    If sFile <> "" Then
      PictureBox4.Image = New Bitmap(sProjectPath & "Images\" & sFile)
      RadioButton4.Enabled = True
      If iChecked = vScroll.Value + 3 Then RadioButton4.Checked = True Else RadioButton4.Checked = False
      RadioButton4.Tag = vScroll.Value + 3
    Else
      PictureBox4.Image = Nothing
      RadioButton4.Enabled = False
      RadioButton4.Checked = False
    End If


    sFile = Dir(sProjectPath & "Images\" & Format(vScroll.Value + 4, "00000000") & ".*")
    If sFile <> "" Then
      PictureBox5.Image = New Bitmap(sProjectPath & "Images\" & sFile)
      RadioButton5.Enabled = True
      If iChecked = vScroll.Value + 4 Then RadioButton5.Checked = True Else RadioButton5.Checked = False
      RadioButton5.Tag = vScroll.Value + 4
    Else
      PictureBox5.Image = Nothing
      RadioButton5.Enabled = False
      RadioButton5.Checked = False
    End If

    sFile = Dir(sProjectPath & "Images\" & Format(vScroll.Value + 5, "00000000") & ".*")
    If sFile <> "" Then
      PictureBox6.Image = New Bitmap(sProjectPath & "Images\" & sFile)
      RadioButton6.Enabled = True
      If iChecked = vScroll.Value + 5 Then RadioButton6.Checked = True Else RadioButton6.Checked = False
      RadioButton6.Tag = vScroll.Value + 5
    Else
      PictureBox6.Image = Nothing
      RadioButton6.Enabled = False
      RadioButton6.Checked = False
    End If

    sFile = Dir(sProjectPath & "Images\" & Format(vScroll.Value + 6, "00000000") & ".*")
    If sFile <> "" Then
      PictureBox7.Image = New Bitmap(sProjectPath & "Images\" & sFile)
      RadioButton7.Enabled = True
      If iChecked = vScroll.Value + 6 Then RadioButton7.Checked = True Else RadioButton7.Checked = False
      RadioButton7.Tag = vScroll.Value + 6
    Else
      PictureBox7.Image = Nothing
      RadioButton7.Enabled = False
      RadioButton7.Checked = False
    End If

    sFile = Dir(sProjectPath & "Images\" & Format(vScroll.Value + 7, "00000000") & ".*")
    If sFile <> "" Then
      PictureBox8.Image = New Bitmap(sProjectPath & "Images\" & sFile)
      RadioButton8.Enabled = True
      If iChecked = vScroll.Value + 7 Then RadioButton8.Checked = True Else RadioButton8.Checked = False
      RadioButton8.Tag = vScroll.Value + 7
    Else
      PictureBox8.Image = Nothing
      RadioButton8.Enabled = False
      RadioButton8.Checked = False
    End If

    GC.Collect()
  End Sub

  Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged, RadioButton4.CheckedChanged, RadioButton5.CheckedChanged, RadioButton6.CheckedChanged, RadioButton7.CheckedChanged, RadioButton8.CheckedChanged
    iChecked = DirectCast(sender, RadioButton).Tag
  End Sub

  Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
    RadioButton1.PerformClick()
  End Sub

  Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
    RadioButton2.PerformClick()
  End Sub

  Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
    RadioButton3.PerformClick()
  End Sub

  Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
    RadioButton4.PerformClick()
  End Sub

  Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
    RadioButton5.PerformClick()
  End Sub

  Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
    RadioButton6.PerformClick()
  End Sub

  Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
    RadioButton7.PerformClick()
  End Sub

  Private Sub PictureBox8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox8.Click
    RadioButton8.PerformClick()
  End Sub
End Class
