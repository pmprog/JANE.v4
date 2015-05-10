<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TileScriptDialog
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
    Me.gFloor = New System.Windows.Forms.GroupBox
    Me.Label5 = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.cFloorWalk = New System.Windows.Forms.ComboBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.cFloorEnter = New System.Windows.Forms.ComboBox
    Me.cFloorExit = New System.Windows.Forms.ComboBox
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.Label3 = New System.Windows.Forms.Label
    Me.cUpperWalk = New System.Windows.Forms.ComboBox
    Me.Label6 = New System.Windows.Forms.Label
    Me.cUpperEnter = New System.Windows.Forms.ComboBox
    Me.cUpperExit = New System.Windows.Forms.ComboBox
    Me.TableLayoutPanel1.SuspendLayout()
    Me.gFloor.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(323, 214)
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
    'gFloor
    '
    Me.gFloor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gFloor.Controls.Add(Me.Label5)
    Me.gFloor.Controls.Add(Me.Label4)
    Me.gFloor.Controls.Add(Me.cFloorWalk)
    Me.gFloor.Controls.Add(Me.Label2)
    Me.gFloor.Controls.Add(Me.cFloorEnter)
    Me.gFloor.Controls.Add(Me.cFloorExit)
    Me.gFloor.Location = New System.Drawing.Point(8, 8)
    Me.gFloor.Name = "gFloor"
    Me.gFloor.Size = New System.Drawing.Size(466, 92)
    Me.gFloor.TabIndex = 1
    Me.gFloor.TabStop = False
    Me.gFloor.Text = "Floor Tile"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(8, 68)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(172, 13)
    Me.Label5.TabIndex = 12
    Me.Label5.Text = "Script when character walks on tile"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(8, 44)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(179, 13)
    Me.Label4.TabIndex = 10
    Me.Label4.Text = "Script when character leaves the tile"
    '
    'cFloorWalk
    '
    Me.cFloorWalk.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cFloorWalk.FormattingEnabled = True
    Me.cFloorWalk.Location = New System.Drawing.Point(204, 64)
    Me.cFloorWalk.Name = "cFloorWalk"
    Me.cFloorWalk.Size = New System.Drawing.Size(254, 21)
    Me.cFloorWalk.TabIndex = 13
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(8, 20)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(177, 13)
    Me.Label2.TabIndex = 8
    Me.Label2.Text = "Script when character enters the tile"
    '
    'cFloorEnter
    '
    Me.cFloorEnter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cFloorEnter.FormattingEnabled = True
    Me.cFloorEnter.Location = New System.Drawing.Point(204, 16)
    Me.cFloorEnter.Name = "cFloorEnter"
    Me.cFloorEnter.Size = New System.Drawing.Size(254, 21)
    Me.cFloorEnter.TabIndex = 9
    '
    'cFloorExit
    '
    Me.cFloorExit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cFloorExit.FormattingEnabled = True
    Me.cFloorExit.Location = New System.Drawing.Point(204, 40)
    Me.cFloorExit.Name = "cFloorExit"
    Me.cFloorExit.Size = New System.Drawing.Size(254, 21)
    Me.cFloorExit.TabIndex = 11
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.cUpperWalk)
    Me.GroupBox1.Controls.Add(Me.Label6)
    Me.GroupBox1.Controls.Add(Me.cUpperEnter)
    Me.GroupBox1.Controls.Add(Me.cUpperExit)
    Me.GroupBox1.Location = New System.Drawing.Point(7, 111)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(466, 92)
    Me.GroupBox1.TabIndex = 2
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Upper Platform Tile"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(8, 68)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(172, 13)
    Me.Label1.TabIndex = 12
    Me.Label1.Text = "Script when character walks on tile"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(8, 44)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(179, 13)
    Me.Label3.TabIndex = 10
    Me.Label3.Text = "Script when character leaves the tile"
    '
    'cUpperWalk
    '
    Me.cUpperWalk.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cUpperWalk.FormattingEnabled = True
    Me.cUpperWalk.Location = New System.Drawing.Point(204, 64)
    Me.cUpperWalk.Name = "cUpperWalk"
    Me.cUpperWalk.Size = New System.Drawing.Size(254, 21)
    Me.cUpperWalk.TabIndex = 13
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(8, 20)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(177, 13)
    Me.Label6.TabIndex = 8
    Me.Label6.Text = "Script when character enters the tile"
    '
    'cUpperEnter
    '
    Me.cUpperEnter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cUpperEnter.FormattingEnabled = True
    Me.cUpperEnter.Location = New System.Drawing.Point(204, 16)
    Me.cUpperEnter.Name = "cUpperEnter"
    Me.cUpperEnter.Size = New System.Drawing.Size(254, 21)
    Me.cUpperEnter.TabIndex = 9
    '
    'cUpperExit
    '
    Me.cUpperExit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cUpperExit.FormattingEnabled = True
    Me.cUpperExit.Location = New System.Drawing.Point(204, 40)
    Me.cUpperExit.Name = "cUpperExit"
    Me.cUpperExit.Size = New System.Drawing.Size(254, 21)
    Me.cUpperExit.TabIndex = 11
    '
    'TileScriptDialog
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(481, 255)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.gFloor)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "TileScriptDialog"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Select Tile Scripts"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.gFloor.ResumeLayout(False)
    Me.gFloor.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents gFloor As System.Windows.Forms.GroupBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents cFloorWalk As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cFloorEnter As System.Windows.Forms.ComboBox
  Friend WithEvents cFloorExit As System.Windows.Forms.ComboBox
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cUpperWalk As System.Windows.Forms.ComboBox
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents cUpperEnter As System.Windows.Forms.ComboBox
  Friend WithEvents cUpperExit As System.Windows.Forms.ComboBox

End Class
