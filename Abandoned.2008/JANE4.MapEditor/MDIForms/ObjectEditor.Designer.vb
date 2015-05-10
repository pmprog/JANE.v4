<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ObjectEditor
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
    Me.cMove = New System.Windows.Forms.CheckBox
    Me.cGrav = New System.Windows.Forms.CheckBox
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.cTeamS = New System.Windows.Forms.CheckBox
    Me.cTeamN = New System.Windows.Forms.CheckBox
    Me.rCPUH = New System.Windows.Forms.RadioButton
    Me.rCPUM = New System.Windows.Forms.RadioButton
    Me.rCPUE = New System.Windows.Forms.RadioButton
    Me.rPlayer = New System.Windows.Forms.RadioButton
    Me.rNoStart = New System.Windows.Forms.RadioButton
    Me.tDesc = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.cRepeat = New System.Windows.Forms.CheckBox
    Me.nRepeatAt = New System.Windows.Forms.NumericUpDown
    Me.gArmoury = New System.Windows.Forms.GroupBox
    Me.cKitana = New System.Windows.Forms.CheckBox
    Me.cKusamigara = New System.Windows.Forms.CheckBox
    Me.cSleep = New System.Windows.Forms.CheckBox
    Me.cSmoke = New System.Windows.Forms.CheckBox
    Me.cYari = New System.Windows.Forms.CheckBox
    Me.cShuriken = New System.Windows.Forms.CheckBox
    Me.cNunchaku = New System.Windows.Forms.CheckBox
    Me.cNinto = New System.Windows.Forms.CheckBox
    Me.pFrames = New System.Windows.Forms.Panel
    Me.pNewFrame = New System.Windows.Forms.PictureBox
    Me.cmnu = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.AddImageToAnimationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.DeleteImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.GroupBox2 = New System.Windows.Forms.GroupBox
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.cScriptCollideScenery = New System.Windows.Forms.ComboBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.Label5 = New System.Windows.Forms.Label
    Me.cScriptDamage = New System.Windows.Forms.ComboBox
    Me.cScriptPickedup = New System.Windows.Forms.ComboBox
    Me.cScriptCollidePlayer = New System.Windows.Forms.ComboBox
    Me.Label3 = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.GroupBox1.SuspendLayout()
    CType(Me.nRepeatAt, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gArmoury.SuspendLayout()
    Me.pFrames.SuspendLayout()
    CType(Me.pNewFrame, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.cmnu.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'cMove
    '
    Me.cMove.AutoSize = True
    Me.cMove.Location = New System.Drawing.Point(4, 32)
    Me.cMove.Name = "cMove"
    Me.cMove.Size = New System.Drawing.Size(91, 17)
    Me.cMove.TabIndex = 0
    Me.cMove.Text = "Object moves"
    Me.cMove.UseVisualStyleBackColor = True
    '
    'cGrav
    '
    Me.cGrav.AutoSize = True
    Me.cGrav.Location = New System.Drawing.Point(104, 32)
    Me.cGrav.Name = "cGrav"
    Me.cGrav.Size = New System.Drawing.Size(78, 17)
    Me.cGrav.TabIndex = 1
    Me.cGrav.Text = "Object falls"
    Me.cGrav.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.cTeamS)
    Me.GroupBox1.Controls.Add(Me.cTeamN)
    Me.GroupBox1.Controls.Add(Me.rCPUH)
    Me.GroupBox1.Controls.Add(Me.rCPUM)
    Me.GroupBox1.Controls.Add(Me.rCPUE)
    Me.GroupBox1.Controls.Add(Me.rPlayer)
    Me.GroupBox1.Controls.Add(Me.rNoStart)
    Me.GroupBox1.Location = New System.Drawing.Point(4, 60)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(204, 128)
    Me.GroupBox1.TabIndex = 3
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Start Points"
    '
    'cTeamS
    '
    Me.cTeamS.AutoSize = True
    Me.cTeamS.Location = New System.Drawing.Point(8, 104)
    Me.cTeamS.Name = "cTeamS"
    Me.cTeamS.Size = New System.Drawing.Size(178, 17)
    Me.cTeamS.TabIndex = 6
    Me.cTeamS.Text = "Samurai Start (Team Multiplayer)"
    Me.cTeamS.UseVisualStyleBackColor = True
    '
    'cTeamN
    '
    Me.cTeamN.AutoSize = True
    Me.cTeamN.Location = New System.Drawing.Point(8, 84)
    Me.cTeamN.Name = "cTeamN"
    Me.cTeamN.Size = New System.Drawing.Size(164, 17)
    Me.cTeamN.TabIndex = 5
    Me.cTeamN.Text = "Ninja Start (Team Multiplayer)"
    Me.cTeamN.UseVisualStyleBackColor = True
    '
    'rCPUH
    '
    Me.rCPUH.AutoSize = True
    Me.rCPUH.Location = New System.Drawing.Point(100, 60)
    Me.rCPUH.Name = "rCPUH"
    Me.rCPUH.Size = New System.Drawing.Size(73, 17)
    Me.rCPUH.TabIndex = 4
    Me.rCPUH.Text = "CPU Hard"
    Me.rCPUH.UseVisualStyleBackColor = True
    '
    'rCPUM
    '
    Me.rCPUM.AutoSize = True
    Me.rCPUM.Location = New System.Drawing.Point(100, 40)
    Me.rCPUM.Name = "rCPUM"
    Me.rCPUM.Size = New System.Drawing.Size(87, 17)
    Me.rCPUM.TabIndex = 3
    Me.rCPUM.Text = "CPU Medium"
    Me.rCPUM.UseVisualStyleBackColor = True
    '
    'rCPUE
    '
    Me.rCPUE.AutoSize = True
    Me.rCPUE.Location = New System.Drawing.Point(100, 20)
    Me.rCPUE.Name = "rCPUE"
    Me.rCPUE.Size = New System.Drawing.Size(73, 17)
    Me.rCPUE.TabIndex = 2
    Me.rCPUE.Text = "CPU Easy"
    Me.rCPUE.UseVisualStyleBackColor = True
    '
    'rPlayer
    '
    Me.rPlayer.AutoSize = True
    Me.rPlayer.Location = New System.Drawing.Point(8, 40)
    Me.rPlayer.Name = "rPlayer"
    Me.rPlayer.Size = New System.Drawing.Size(54, 17)
    Me.rPlayer.TabIndex = 1
    Me.rPlayer.Text = "Player"
    Me.rPlayer.UseVisualStyleBackColor = True
    '
    'rNoStart
    '
    Me.rNoStart.AutoSize = True
    Me.rNoStart.Checked = True
    Me.rNoStart.Location = New System.Drawing.Point(8, 20)
    Me.rNoStart.Name = "rNoStart"
    Me.rNoStart.Size = New System.Drawing.Size(51, 17)
    Me.rNoStart.TabIndex = 0
    Me.rNoStart.TabStop = True
    Me.rNoStart.Text = "None"
    Me.rNoStart.UseVisualStyleBackColor = True
    '
    'tDesc
    '
    Me.tDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tDesc.Location = New System.Drawing.Point(100, 4)
    Me.tDesc.Name = "tDesc"
    Me.tDesc.Size = New System.Drawing.Size(450, 20)
    Me.tDesc.TabIndex = 6
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(4, 8)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(94, 13)
    Me.Label1.TabIndex = 5
    Me.Label1.Text = "Object Description"
    '
    'cRepeat
    '
    Me.cRepeat.AutoSize = True
    Me.cRepeat.Checked = True
    Me.cRepeat.CheckState = System.Windows.Forms.CheckState.Checked
    Me.cRepeat.Location = New System.Drawing.Point(8, 192)
    Me.cRepeat.Name = "cRepeat"
    Me.cRepeat.Size = New System.Drawing.Size(154, 17)
    Me.cRepeat.TabIndex = 7
    Me.cRepeat.Text = "Repeat Animation at frame:"
    Me.cRepeat.UseVisualStyleBackColor = True
    '
    'nRepeatAt
    '
    Me.nRepeatAt.Location = New System.Drawing.Point(164, 192)
    Me.nRepeatAt.Name = "nRepeatAt"
    Me.nRepeatAt.Size = New System.Drawing.Size(48, 20)
    Me.nRepeatAt.TabIndex = 9
    '
    'gArmoury
    '
    Me.gArmoury.Controls.Add(Me.cKitana)
    Me.gArmoury.Controls.Add(Me.cKusamigara)
    Me.gArmoury.Controls.Add(Me.cSleep)
    Me.gArmoury.Controls.Add(Me.cSmoke)
    Me.gArmoury.Controls.Add(Me.cYari)
    Me.gArmoury.Controls.Add(Me.cShuriken)
    Me.gArmoury.Controls.Add(Me.cNunchaku)
    Me.gArmoury.Controls.Add(Me.cNinto)
    Me.gArmoury.Location = New System.Drawing.Point(216, 60)
    Me.gArmoury.Name = "gArmoury"
    Me.gArmoury.Size = New System.Drawing.Size(208, 128)
    Me.gArmoury.TabIndex = 10
    Me.gArmoury.TabStop = False
    Me.gArmoury.Text = "Armoury (CPU Start Points)"
    '
    'cKitana
    '
    Me.cKitana.AutoSize = True
    Me.cKitana.Location = New System.Drawing.Point(96, 92)
    Me.cKitana.Name = "cKitana"
    Me.cKitana.Size = New System.Drawing.Size(56, 17)
    Me.cKitana.TabIndex = 7
    Me.cKitana.Text = "Kitana"
    Me.cKitana.UseVisualStyleBackColor = True
    '
    'cKusamigara
    '
    Me.cKusamigara.AutoSize = True
    Me.cKusamigara.Location = New System.Drawing.Point(96, 68)
    Me.cKusamigara.Name = "cKusamigara"
    Me.cKusamigara.Size = New System.Drawing.Size(81, 17)
    Me.cKusamigara.TabIndex = 6
    Me.cKusamigara.Text = "Kusamigara"
    Me.cKusamigara.UseVisualStyleBackColor = True
    '
    'cSleep
    '
    Me.cSleep.AutoSize = True
    Me.cSleep.Location = New System.Drawing.Point(96, 44)
    Me.cSleep.Name = "cSleep"
    Me.cSleep.Size = New System.Drawing.Size(83, 17)
    Me.cSleep.TabIndex = 5
    Me.cSleep.Text = "Sleep Bomb"
    Me.cSleep.UseVisualStyleBackColor = True
    '
    'cSmoke
    '
    Me.cSmoke.AutoSize = True
    Me.cSmoke.Location = New System.Drawing.Point(96, 20)
    Me.cSmoke.Name = "cSmoke"
    Me.cSmoke.Size = New System.Drawing.Size(89, 17)
    Me.cSmoke.TabIndex = 4
    Me.cSmoke.Text = "Smoke Bomb"
    Me.cSmoke.UseVisualStyleBackColor = True
    '
    'cYari
    '
    Me.cYari.AutoSize = True
    Me.cYari.Location = New System.Drawing.Point(8, 92)
    Me.cYari.Name = "cYari"
    Me.cYari.Size = New System.Drawing.Size(44, 17)
    Me.cYari.TabIndex = 3
    Me.cYari.Text = "Yari"
    Me.cYari.UseVisualStyleBackColor = True
    '
    'cShuriken
    '
    Me.cShuriken.AutoSize = True
    Me.cShuriken.Location = New System.Drawing.Point(8, 68)
    Me.cShuriken.Name = "cShuriken"
    Me.cShuriken.Size = New System.Drawing.Size(68, 17)
    Me.cShuriken.TabIndex = 2
    Me.cShuriken.Text = "Shuriken"
    Me.cShuriken.UseVisualStyleBackColor = True
    '
    'cNunchaku
    '
    Me.cNunchaku.AutoSize = True
    Me.cNunchaku.Location = New System.Drawing.Point(8, 44)
    Me.cNunchaku.Name = "cNunchaku"
    Me.cNunchaku.Size = New System.Drawing.Size(76, 17)
    Me.cNunchaku.TabIndex = 1
    Me.cNunchaku.Text = "Nunchaku"
    Me.cNunchaku.UseVisualStyleBackColor = True
    '
    'cNinto
    '
    Me.cNinto.AutoSize = True
    Me.cNinto.Location = New System.Drawing.Point(8, 20)
    Me.cNinto.Name = "cNinto"
    Me.cNinto.Size = New System.Drawing.Size(51, 17)
    Me.cNinto.TabIndex = 0
    Me.cNinto.Text = "Ninto"
    Me.cNinto.UseVisualStyleBackColor = True
    '
    'pFrames
    '
    Me.pFrames.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pFrames.AutoScroll = True
    Me.pFrames.BackColor = System.Drawing.SystemColors.AppWorkspace
    Me.pFrames.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.pFrames.Controls.Add(Me.pNewFrame)
    Me.pFrames.Location = New System.Drawing.Point(4, 216)
    Me.pFrames.Name = "pFrames"
    Me.pFrames.Size = New System.Drawing.Size(548, 136)
    Me.pFrames.TabIndex = 11
    '
    'pNewFrame
    '
    Me.pNewFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.pNewFrame.ContextMenuStrip = Me.cmnu
    Me.pNewFrame.Location = New System.Drawing.Point(0, 0)
    Me.pNewFrame.Name = "pNewFrame"
    Me.pNewFrame.Size = New System.Drawing.Size(96, 116)
    Me.pNewFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.pNewFrame.TabIndex = 0
    Me.pNewFrame.TabStop = False
    '
    'cmnu
    '
    Me.cmnu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddImageToAnimationToolStripMenuItem, Me.DeleteImageToolStripMenuItem})
    Me.cmnu.Name = "cmnu"
    Me.cmnu.Size = New System.Drawing.Size(158, 48)
    '
    'AddImageToAnimationToolStripMenuItem
    '
    Me.AddImageToAnimationToolStripMenuItem.Name = "AddImageToAnimationToolStripMenuItem"
    Me.AddImageToAnimationToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
    Me.AddImageToAnimationToolStripMenuItem.Text = "&Add Image"
    '
    'DeleteImageToolStripMenuItem
    '
    Me.DeleteImageToolStripMenuItem.Name = "DeleteImageToolStripMenuItem"
    Me.DeleteImageToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
    Me.DeleteImageToolStripMenuItem.Text = "&Remove Image"
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.TableLayoutPanel1)
    Me.GroupBox2.Location = New System.Drawing.Point(4, 356)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(548, 112)
    Me.GroupBox2.TabIndex = 12
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Scripts"
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.cScriptCollideScenery, 1, 3)
    Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Label5, 1, 2)
    Me.TableLayoutPanel1.Controls.Add(Me.cScriptDamage, 0, 1)
    Me.TableLayoutPanel1.Controls.Add(Me.cScriptPickedup, 0, 3)
    Me.TableLayoutPanel1.Controls.Add(Me.cScriptCollidePlayer, 1, 1)
    Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
    Me.TableLayoutPanel1.Controls.Add(Me.Label4, 1, 0)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 4
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(542, 93)
    Me.TableLayoutPanel1.TabIndex = 13
    '
    'cScriptCollideScenery
    '
    Me.cScriptCollideScenery.Dock = System.Windows.Forms.DockStyle.Fill
    Me.cScriptCollideScenery.FormattingEnabled = True
    Me.cScriptCollideScenery.Location = New System.Drawing.Point(274, 62)
    Me.cScriptCollideScenery.Name = "cScriptCollideScenery"
    Me.cScriptCollideScenery.Size = New System.Drawing.Size(265, 21)
    Me.cScriptCollideScenery.TabIndex = 7
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(3, 0)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(138, 13)
    Me.Label2.TabIndex = 0
    Me.Label2.Text = "When object takes damage"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(274, 46)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(186, 13)
    Me.Label5.TabIndex = 6
    Me.Label5.Text = "When object collides with the scenery"
    '
    'cScriptDamage
    '
    Me.cScriptDamage.Dock = System.Windows.Forms.DockStyle.Fill
    Me.cScriptDamage.FormattingEnabled = True
    Me.cScriptDamage.Location = New System.Drawing.Point(3, 16)
    Me.cScriptDamage.Name = "cScriptDamage"
    Me.cScriptDamage.Size = New System.Drawing.Size(265, 21)
    Me.cScriptDamage.TabIndex = 1
    '
    'cScriptPickedup
    '
    Me.cScriptPickedup.Dock = System.Windows.Forms.DockStyle.Fill
    Me.cScriptPickedup.FormattingEnabled = True
    Me.cScriptPickedup.Location = New System.Drawing.Point(3, 62)
    Me.cScriptPickedup.Name = "cScriptPickedup"
    Me.cScriptPickedup.Size = New System.Drawing.Size(265, 21)
    Me.cScriptPickedup.TabIndex = 3
    '
    'cScriptCollidePlayer
    '
    Me.cScriptCollidePlayer.Dock = System.Windows.Forms.DockStyle.Fill
    Me.cScriptCollidePlayer.FormattingEnabled = True
    Me.cScriptCollidePlayer.Location = New System.Drawing.Point(274, 16)
    Me.cScriptCollidePlayer.Name = "cScriptCollidePlayer"
    Me.cScriptCollidePlayer.Size = New System.Drawing.Size(265, 21)
    Me.cScriptCollidePlayer.TabIndex = 5
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(3, 46)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(128, 13)
    Me.Label3.TabIndex = 2
    Me.Label3.Text = "When object is picked up"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(274, 0)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(185, 13)
    Me.Label4.TabIndex = 4
    Me.Label4.Text = "When object collides with a character"
    '
    'ObjectEditor
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(556, 484)
    Me.Controls.Add(Me.pFrames)
    Me.Controls.Add(Me.gArmoury)
    Me.Controls.Add(Me.nRepeatAt)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.cRepeat)
    Me.Controls.Add(Me.tDesc)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.cGrav)
    Me.Controls.Add(Me.cMove)
    Me.Name = "ObjectEditor"
    Me.Text = "Object Editor"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.nRepeatAt, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gArmoury.ResumeLayout(False)
    Me.gArmoury.PerformLayout()
    Me.pFrames.ResumeLayout(False)
    CType(Me.pNewFrame, System.ComponentModel.ISupportInitialize).EndInit()
    Me.cmnu.ResumeLayout(False)
    Me.GroupBox2.ResumeLayout(False)
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.TableLayoutPanel1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cMove As System.Windows.Forms.CheckBox
  Friend WithEvents cGrav As System.Windows.Forms.CheckBox
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents rCPUH As System.Windows.Forms.RadioButton
  Friend WithEvents rCPUM As System.Windows.Forms.RadioButton
  Friend WithEvents rCPUE As System.Windows.Forms.RadioButton
  Friend WithEvents rPlayer As System.Windows.Forms.RadioButton
  Friend WithEvents rNoStart As System.Windows.Forms.RadioButton
  Friend WithEvents cTeamS As System.Windows.Forms.CheckBox
  Friend WithEvents cTeamN As System.Windows.Forms.CheckBox
  Friend WithEvents tDesc As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents cRepeat As System.Windows.Forms.CheckBox
  Friend WithEvents nRepeatAt As System.Windows.Forms.NumericUpDown
  Friend WithEvents gArmoury As System.Windows.Forms.GroupBox
  Friend WithEvents cKitana As System.Windows.Forms.CheckBox
  Friend WithEvents cKusamigara As System.Windows.Forms.CheckBox
  Friend WithEvents cSleep As System.Windows.Forms.CheckBox
  Friend WithEvents cSmoke As System.Windows.Forms.CheckBox
  Friend WithEvents cYari As System.Windows.Forms.CheckBox
  Friend WithEvents cShuriken As System.Windows.Forms.CheckBox
  Friend WithEvents cNunchaku As System.Windows.Forms.CheckBox
  Friend WithEvents cNinto As System.Windows.Forms.CheckBox
  Friend WithEvents pFrames As System.Windows.Forms.Panel
  Friend WithEvents pNewFrame As System.Windows.Forms.PictureBox
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cScriptCollideScenery As System.Windows.Forms.ComboBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents cScriptCollidePlayer As System.Windows.Forms.ComboBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents cScriptPickedup As System.Windows.Forms.ComboBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cScriptDamage As System.Windows.Forms.ComboBox
  Friend WithEvents cmnu As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents AddImageToAnimationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents DeleteImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
End Class
