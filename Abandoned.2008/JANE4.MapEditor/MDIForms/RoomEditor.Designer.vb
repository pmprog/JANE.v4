<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RoomEditor
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
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.Label6 = New System.Windows.Forms.Label
    Me.nTimer = New System.Windows.Forms.NumericUpDown
    Me.Label3 = New System.Windows.Forms.Label
    Me.Label5 = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.cScriptTimer = New System.Windows.Forms.ComboBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.cScriptEnter = New System.Windows.Forms.ComboBox
    Me.tDesc = New System.Windows.Forms.TextBox
    Me.cScriptLeave = New System.Windows.Forms.ComboBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.Panel2 = New System.Windows.Forms.Panel
    Me.pRoomRender = New System.Windows.Forms.PictureBox
    Me.tHandleMove = New System.Windows.Forms.TextBox
    Me.Panel1.SuspendLayout()
    CType(Me.nTimer, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel2.SuspendLayout()
    CType(Me.pRoomRender, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.Label6)
    Me.Panel1.Controls.Add(Me.nTimer)
    Me.Panel1.Controls.Add(Me.Label3)
    Me.Panel1.Controls.Add(Me.Label5)
    Me.Panel1.Controls.Add(Me.Label4)
    Me.Panel1.Controls.Add(Me.cScriptTimer)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Controls.Add(Me.cScriptEnter)
    Me.Panel1.Controls.Add(Me.tDesc)
    Me.Panel1.Controls.Add(Me.cScriptLeave)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(604, 128)
    Me.Panel1.TabIndex = 0
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(312, 80)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(29, 13)
    Me.Label6.TabIndex = 9
    Me.Label6.Text = "ticks"
    '
    'nTimer
    '
    Me.nTimer.Location = New System.Drawing.Point(200, 76)
    Me.nTimer.Name = "nTimer"
    Me.nTimer.Size = New System.Drawing.Size(108, 20)
    Me.nTimer.TabIndex = 8
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(4, 80)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(106, 13)
    Me.Label3.TabIndex = 2
    Me.Label3.Text = "Timer Activates On..."
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(4, 104)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(78, 13)
    Me.Label5.TabIndex = 6
    Me.Label5.Text = "Script on Timer"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(4, 56)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(189, 13)
    Me.Label4.TabIndex = 4
    Me.Label4.Text = "Script when character leaves the room"
    '
    'cScriptTimer
    '
    Me.cScriptTimer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cScriptTimer.FormattingEnabled = True
    Me.cScriptTimer.Location = New System.Drawing.Point(200, 100)
    Me.cScriptTimer.Name = "cScriptTimer"
    Me.cScriptTimer.Size = New System.Drawing.Size(397, 21)
    Me.cScriptTimer.TabIndex = 7
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(4, 32)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(187, 13)
    Me.Label2.TabIndex = 0
    Me.Label2.Text = "Script when character enters the room"
    '
    'cScriptEnter
    '
    Me.cScriptEnter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cScriptEnter.FormattingEnabled = True
    Me.cScriptEnter.Location = New System.Drawing.Point(200, 28)
    Me.cScriptEnter.Name = "cScriptEnter"
    Me.cScriptEnter.Size = New System.Drawing.Size(397, 21)
    Me.cScriptEnter.TabIndex = 1
    '
    'tDesc
    '
    Me.tDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tDesc.Location = New System.Drawing.Point(100, 4)
    Me.tDesc.Name = "tDesc"
    Me.tDesc.Size = New System.Drawing.Size(497, 20)
    Me.tDesc.TabIndex = 3
    '
    'cScriptLeave
    '
    Me.cScriptLeave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cScriptLeave.FormattingEnabled = True
    Me.cScriptLeave.Location = New System.Drawing.Point(200, 52)
    Me.cScriptLeave.Name = "cScriptLeave"
    Me.cScriptLeave.Size = New System.Drawing.Size(397, 21)
    Me.cScriptLeave.TabIndex = 5
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(4, 8)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(91, 13)
    Me.Label1.TabIndex = 2
    Me.Label1.Text = "Room Description"
    '
    'Panel2
    '
    Me.Panel2.AutoScroll = True
    Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel2.Controls.Add(Me.pRoomRender)
    Me.Panel2.Controls.Add(Me.tHandleMove)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel2.Location = New System.Drawing.Point(0, 128)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(604, 439)
    Me.Panel2.TabIndex = 1
    '
    'pRoomRender
    '
    Me.pRoomRender.Location = New System.Drawing.Point(0, 0)
    Me.pRoomRender.Name = "pRoomRender"
    Me.pRoomRender.Size = New System.Drawing.Size(52, 44)
    Me.pRoomRender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.pRoomRender.TabIndex = 0
    Me.pRoomRender.TabStop = False
    '
    'tHandleMove
    '
    Me.tHandleMove.BackColor = System.Drawing.Color.Black
    Me.tHandleMove.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.tHandleMove.Location = New System.Drawing.Point(0, 0)
    Me.tHandleMove.Name = "tHandleMove"
    Me.tHandleMove.Size = New System.Drawing.Size(8, 13)
    Me.tHandleMove.TabIndex = 1
    '
    'RoomEditor
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(604, 567)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Name = "RoomEditor"
    Me.Text = "Room Editor"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    CType(Me.nTimer, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    CType(Me.pRoomRender, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents pRoomRender As System.Windows.Forms.PictureBox
  Friend WithEvents tDesc As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents tHandleMove As System.Windows.Forms.TextBox
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents cScriptTimer As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents cScriptEnter As System.Windows.Forms.ComboBox
  Friend WithEvents cScriptLeave As System.Windows.Forms.ComboBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents nTimer As System.Windows.Forms.NumericUpDown
End Class
