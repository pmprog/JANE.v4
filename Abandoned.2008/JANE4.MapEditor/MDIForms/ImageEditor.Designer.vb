<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImageEditor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.tDesc = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.nX = New System.Windows.Forms.NumericUpDown
    Me.nY = New System.Windows.Forms.NumericUpDown
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.pImage = New System.Windows.Forms.PictureBox
    CType(Me.nX, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nY, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    CType(Me.pImage, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'tDesc
    '
    Me.tDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tDesc.Location = New System.Drawing.Point(100, 4)
    Me.tDesc.Name = "tDesc"
    Me.tDesc.Size = New System.Drawing.Size(368, 20)
    Me.tDesc.TabIndex = 3
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(4, 8)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(90, 13)
    Me.Label1.TabIndex = 2
    Me.Label1.Text = "Script Description"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(4, 32)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(49, 13)
    Me.Label2.TabIndex = 5
    Me.Label2.Text = "Hot Spot"
    '
    'nX
    '
    Me.nX.Location = New System.Drawing.Point(100, 28)
    Me.nX.Name = "nX"
    Me.nX.Size = New System.Drawing.Size(84, 20)
    Me.nX.TabIndex = 6
    '
    'nY
    '
    Me.nY.Location = New System.Drawing.Point(188, 28)
    Me.nY.Name = "nY"
    Me.nY.Size = New System.Drawing.Size(84, 20)
    Me.nY.TabIndex = 7
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.AutoScroll = True
    Me.Panel1.BackColor = System.Drawing.SystemColors.AppWorkspace
    Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel1.Controls.Add(Me.pImage)
    Me.Panel1.Location = New System.Drawing.Point(4, 56)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(468, 272)
    Me.Panel1.TabIndex = 8
    '
    'pImage
    '
    Me.pImage.BackgroundImage = Global.JANE4.MapEditor.My.Resources.Resources.bkgImg
    Me.pImage.Location = New System.Drawing.Point(0, 0)
    Me.pImage.Name = "pImage"
    Me.pImage.Size = New System.Drawing.Size(64, 64)
    Me.pImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.pImage.TabIndex = 0
    Me.pImage.TabStop = False
    '
    'ImageEditor
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(476, 332)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.nY)
    Me.Controls.Add(Me.nX)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.tDesc)
    Me.Controls.Add(Me.Label1)
    Me.Name = "ImageEditor"
    Me.Text = "ImageEditor"
    CType(Me.nX, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nY, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    CType(Me.pImage, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents tDesc As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents nX As System.Windows.Forms.NumericUpDown
  Friend WithEvents nY As System.Windows.Forms.NumericUpDown
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents pImage As System.Windows.Forms.PictureBox
End Class
