<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FlagsDialog
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.OK_Button = New System.Windows.Forms.Button
    Me.Cancel_Button = New System.Windows.Forms.Button
    Me.cCanEnter = New System.Windows.Forms.CheckBox
    Me.cClimbFL = New System.Windows.Forms.CheckBox
    Me.cClimbBL = New System.Windows.Forms.CheckBox
    Me.cClimbBR = New System.Windows.Forms.CheckBox
    Me.cClimbFR = New System.Windows.Forms.CheckBox
    Me.cNonSolid = New System.Windows.Forms.CheckBox
    Me.cCeiling = New System.Windows.Forms.CheckBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(181, 172)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Cancel"
    '
    'cCanEnter
    '
    Me.cCanEnter.AutoSize = True
    Me.cCanEnter.Location = New System.Drawing.Point(24, 28)
    Me.cCanEnter.Name = "cCanEnter"
    Me.cCanEnter.Size = New System.Drawing.Size(93, 17)
    Me.cCanEnter.TabIndex = 1
    Me.cCanEnter.Text = "Can Enter Tile"
    Me.cCanEnter.UseVisualStyleBackColor = True
    '
    'cClimbFL
    '
    Me.cClimbFL.AutoSize = True
    Me.cClimbFL.Location = New System.Drawing.Point(24, 60)
    Me.cClimbFL.Name = "cClimbFL"
    Me.cClimbFL.Size = New System.Drawing.Size(145, 17)
    Me.cClimbFL.TabIndex = 2
    Me.cClimbFL.Text = "Can Climb Front-Left Wall"
    Me.cClimbFL.UseVisualStyleBackColor = True
    '
    'cClimbBL
    '
    Me.cClimbBL.AutoSize = True
    Me.cClimbBL.Location = New System.Drawing.Point(24, 84)
    Me.cClimbBL.Name = "cClimbBL"
    Me.cClimbBL.Size = New System.Drawing.Size(146, 17)
    Me.cClimbBL.TabIndex = 3
    Me.cClimbBL.Text = "Can Climb Back-Left Wall"
    Me.cClimbBL.UseVisualStyleBackColor = True
    '
    'cClimbBR
    '
    Me.cClimbBR.AutoSize = True
    Me.cClimbBR.Location = New System.Drawing.Point(180, 84)
    Me.cClimbBR.Name = "cClimbBR"
    Me.cClimbBR.Size = New System.Drawing.Size(153, 17)
    Me.cClimbBR.TabIndex = 5
    Me.cClimbBR.Text = "Can Climb Back-Right Wall"
    Me.cClimbBR.UseVisualStyleBackColor = True
    '
    'cClimbFR
    '
    Me.cClimbFR.AutoSize = True
    Me.cClimbFR.Location = New System.Drawing.Point(180, 60)
    Me.cClimbFR.Name = "cClimbFR"
    Me.cClimbFR.Size = New System.Drawing.Size(152, 17)
    Me.cClimbFR.TabIndex = 4
    Me.cClimbFR.Text = "Can Climb Front-Right Wall"
    Me.cClimbFR.UseVisualStyleBackColor = True
    '
    'cNonSolid
    '
    Me.cNonSolid.AutoSize = True
    Me.cNonSolid.Location = New System.Drawing.Point(180, 140)
    Me.cNonSolid.Name = "cNonSolid"
    Me.cNonSolid.Size = New System.Drawing.Size(72, 17)
    Me.cNonSolid.TabIndex = 7
    Me.cNonSolid.Text = "Non-Solid"
    Me.cNonSolid.UseVisualStyleBackColor = True
    '
    'cCeiling
    '
    Me.cCeiling.AutoSize = True
    Me.cCeiling.Location = New System.Drawing.Point(24, 140)
    Me.cCeiling.Name = "cCeiling"
    Me.cCeiling.Size = New System.Drawing.Size(68, 17)
    Me.cCeiling.TabIndex = 6
    Me.cCeiling.Text = "Is Ceiling"
    Me.cCeiling.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(8, 120)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(92, 13)
    Me.Label1.TabIndex = 8
    Me.Label1.Text = "Platform only flags"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(8, 8)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(98, 13)
    Me.Label2.TabIndex = 9
    Me.Label2.Text = "Floor/Platform flags"
    '
    'FlagsDialog
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(339, 213)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.cNonSolid)
    Me.Controls.Add(Me.cCeiling)
    Me.Controls.Add(Me.cClimbBR)
    Me.Controls.Add(Me.cClimbFR)
    Me.Controls.Add(Me.cClimbBL)
    Me.Controls.Add(Me.cClimbFL)
    Me.Controls.Add(Me.cCanEnter)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FlagsDialog"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Select Flags"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents cCanEnter As System.Windows.Forms.CheckBox
  Friend WithEvents cClimbFL As System.Windows.Forms.CheckBox
  Friend WithEvents cClimbBL As System.Windows.Forms.CheckBox
  Friend WithEvents cClimbBR As System.Windows.Forms.CheckBox
  Friend WithEvents cClimbFR As System.Windows.Forms.CheckBox
  Friend WithEvents cNonSolid As System.Windows.Forms.CheckBox
  Friend WithEvents cCeiling As System.Windows.Forms.CheckBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
