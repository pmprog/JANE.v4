Imports System.Windows.Forms

Public Class NewImageDialog

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Dim iC As Integer
    Dim bOut As Bitmap = Nothing
    Dim fImgEd As ImageEditor

    If sImageDetails Is Nothing Then iC = 0 Else iC = UBound(sImageDetails) + 1
    ReDim Preserve sImageDetails(iC)

    If RadioButton1.Checked Then bOut = New Bitmap(My.Resources.default_floor_tile)
    If RadioButton2.Checked Then bOut = New Bitmap(My.Resources.wall_left)
    If RadioButton3.Checked Then bOut = New Bitmap(My.Resources.wall_right)

    bOut.Save(sProjectPath & "Images\" & Format(iC, "00000000") & ".png", Imaging.ImageFormat.Png)

    With nImages.Nodes.Add(Format(iC, "00000000") & " - New Image")
      .ImageIndex = 1
      .SelectedImageIndex = 1
      .Tag = iC
    End With
    sImageDetails(iC).Description = "New Image"
    sImageDetails(iC).Hotspot = New Point(0, 0)
    SaveImageDetails()

    fImgEd = New ImageEditor
    fImgEd.MdiParent = Me.Tag
    fImgEd.Show()
    fImgEd.LoadImageDetail(iC)

    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

End Class
