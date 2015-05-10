Public Class ImageEditor
  Implements IMDIForm

  Dim bBaseImage As Bitmap
  Dim iScale As Integer = 1
  Dim myID As Integer

  Private Sub ImageEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub

  Public Sub LoadImageDetail(ByVal IDnumber As Integer)
    Dim sFile As String
    sFile = Dir(sProjectPath & "Images\" & Format(IDnumber, "00000000") & ".*")
    If sFile = "" Then
      Me.Close()
      Exit Sub
    End If
    myID = IDnumber

    bBaseImage = New Bitmap(sProjectPath & "Images\" & sFile)
    pImage.Image = bBaseImage
    iScale = 1
    Me.Text = "Image: " & myID & " [" & sImageDetails(myID).Description & "] (1:1)"
    tDesc.Text = sImageDetails(myID).Description
    nX.Value = sImageDetails(myID).Hotspot.X
    nY.Value = sImageDetails(myID).Hotspot.Y

  End Sub

  Private Sub ImageEditor_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
    Dim bNewImg As Bitmap = Nothing

    If e.Delta > 0 Then
      If iScale < 16 Then
        iScale += 1
        If iScale = -1 Then iScale = 1
      End If
    Else
      If iScale > -16 Then
        iScale -= 1
        If iScale = 0 Then iScale = -2
      End If
    End If
    If iScale < 0 Then
      Me.Text = "Image: " & myID & " [" & tDesc.Text & "] (1:" & Math.Abs(iScale) & ")"
      bNewImg = New Bitmap(bBaseImage, bBaseImage.Width / Math.Abs(iScale), bBaseImage.Height / Math.Abs(iScale))
    Else
      Me.Text = "Image: " & myID & " [" & tDesc.Text & "] (" & iScale & ":1)"
      bNewImg = New Bitmap(bBaseImage, bBaseImage.Width * iScale, bBaseImage.Height * iScale)
    End If
    pImage.Image = bNewImg
    GC.Collect()
  End Sub

  Public Sub Save() Implements IMDIForm.Save
    sImageDetails(myID).Description = tDesc.Text
    sImageDetails(myID).Hotspot = New Point(nX.Value, nY.Value)
    SaveImageDetails()
  End Sub
End Class