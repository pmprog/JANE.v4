<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScriptEditor
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
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScriptEditor))
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.rtfCode = New System.Windows.Forms.RichTextBox
    Me.Panel2 = New System.Windows.Forms.Panel
    Me.tDesc = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.ilIcos = New System.Windows.Forms.ImageList(Me.components)
    Me.lAutocomplete = New System.Windows.Forms.ListBox
    Me.Panel3 = New System.Windows.Forms.Panel
    Me.Panel2.SuspendLayout()
    Me.Panel3.SuspendLayout()
    Me.SuspendLayout()
    '
    'Panel1
    '
    Me.Panel1.BackColor = System.Drawing.SystemColors.AppWorkspace
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
    Me.Panel1.Location = New System.Drawing.Point(0, 28)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(16, 343)
    Me.Panel1.TabIndex = 0
    '
    'rtfCode
    '
    Me.rtfCode.AcceptsTab = True
    Me.rtfCode.Dock = System.Windows.Forms.DockStyle.Fill
    Me.rtfCode.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.rtfCode.Location = New System.Drawing.Point(0, 0)
    Me.rtfCode.Name = "rtfCode"
    Me.rtfCode.Size = New System.Drawing.Size(487, 343)
    Me.rtfCode.TabIndex = 1
    Me.rtfCode.Text = ""
    Me.rtfCode.WordWrap = False
    '
    'Panel2
    '
    Me.Panel2.Controls.Add(Me.tDesc)
    Me.Panel2.Controls.Add(Me.Label1)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
    Me.Panel2.Location = New System.Drawing.Point(0, 0)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(503, 28)
    Me.Panel2.TabIndex = 5
    '
    'tDesc
    '
    Me.tDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tDesc.Location = New System.Drawing.Point(100, 4)
    Me.tDesc.Name = "tDesc"
    Me.tDesc.Size = New System.Drawing.Size(400, 20)
    Me.tDesc.TabIndex = 1
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(4, 8)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(90, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Script Description"
    '
    'ilIcos
    '
    Me.ilIcos.ImageStream = CType(resources.GetObject("ilIcos.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ilIcos.TransparentColor = System.Drawing.Color.Transparent
    Me.ilIcos.Images.SetKeyName(0, "question.ico")
    Me.ilIcos.Images.SetKeyName(1, "struct.ico")
    Me.ilIcos.Images.SetKeyName(2, "variable.ico")
    Me.ilIcos.Images.SetKeyName(3, "function.ico")
    '
    'lAutocomplete
    '
    Me.lAutocomplete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lAutocomplete.FormattingEnabled = True
    Me.lAutocomplete.Location = New System.Drawing.Point(88, 80)
    Me.lAutocomplete.Name = "lAutocomplete"
    Me.lAutocomplete.Size = New System.Drawing.Size(268, 119)
    Me.lAutocomplete.TabIndex = 6
    Me.lAutocomplete.Visible = False
    '
    'Panel3
    '
    Me.Panel3.Controls.Add(Me.lAutocomplete)
    Me.Panel3.Controls.Add(Me.rtfCode)
    Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel3.Location = New System.Drawing.Point(16, 28)
    Me.Panel3.Name = "Panel3"
    Me.Panel3.Size = New System.Drawing.Size(487, 343)
    Me.Panel3.TabIndex = 7
    '
    'ScriptEditor
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(503, 371)
    Me.Controls.Add(Me.Panel3)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.Panel2)
    Me.Name = "ScriptEditor"
    Me.Text = "Script Editor"
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.Panel3.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents rtfCode As System.Windows.Forms.RichTextBox
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents tDesc As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents ilIcos As System.Windows.Forms.ImageList
  Friend WithEvents lAutocomplete As System.Windows.Forms.ListBox
  Friend WithEvents Panel3 As System.Windows.Forms.Panel
End Class
