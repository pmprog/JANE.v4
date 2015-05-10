<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditorMain
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditorMain))
    Me.mainMenu = New System.Windows.Forms.MenuStrip
    Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuNewMap = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuLoadMap = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
    Me.mnuSaveDoc = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
    Me.mnuBuildMap = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
    Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuProject = New System.Windows.Forms.ToolStripMenuItem
    Me.AddNewRoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuAddImage = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuAddRoom = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuAddObject = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuAddScript = New System.Windows.Forms.ToolStripMenuItem
    Me.OpenSelectedRoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuImportImage = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuImportRoom = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuImportObject = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuImportScript = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuDeleteItem = New System.Windows.Forms.ToolStripMenuItem
    Me.mnuWindows = New System.Windows.Forms.ToolStripMenuItem
    Me.SaveLayoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator
    Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.DynamicHelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator
    Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.dockMan = New NETXP.Controls.Docking.DockingManagerExtender(Me.components)
    Me.prjTree = New System.Windows.Forms.TreeView
    Me.ilProj = New System.Windows.Forms.ImageList(Me.components)
    Me.rtfHelp = New System.Windows.Forms.RichTextBox
    Me.fbd = New System.Windows.Forms.FolderBrowserDialog
    Me.iid = New System.Windows.Forms.OpenFileDialog
    Me.mainMenu.SuspendLayout()
    Me.SuspendLayout()
    '
    'mainMenu
    '
    Me.dockMan.SetFullTitle(Me.mainMenu, "mainMenu")
    Me.mainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuProject, Me.mnuWindows, Me.HelpToolStripMenuItem})
    Me.mainMenu.Location = New System.Drawing.Point(0, 0)
    Me.mainMenu.MdiWindowListItem = Me.mnuWindows
    Me.mainMenu.Name = "mainMenu"
    Me.mainMenu.Size = New System.Drawing.Size(854, 24)
    Me.mainMenu.TabIndex = 0
    Me.mainMenu.Text = "MenuStrip1"
    Me.dockMan.SetTitle(Me.mainMenu, "mainMenu")
    '
    'mnuFile
    '
    Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewMap, Me.mnuLoadMap, Me.ToolStripMenuItem1, Me.mnuSaveDoc, Me.ToolStripMenuItem3, Me.mnuBuildMap, Me.ToolStripMenuItem2, Me.mnuExit})
    Me.mnuFile.Name = "mnuFile"
    Me.mnuFile.Size = New System.Drawing.Size(35, 20)
    Me.mnuFile.Text = "&File"
    '
    'mnuNewMap
    '
    Me.mnuNewMap.Name = "mnuNewMap"
    Me.mnuNewMap.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
    Me.mnuNewMap.Size = New System.Drawing.Size(198, 22)
    Me.mnuNewMap.Text = "&New Map"
    '
    'mnuLoadMap
    '
    Me.mnuLoadMap.Name = "mnuLoadMap"
    Me.mnuLoadMap.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
    Me.mnuLoadMap.Size = New System.Drawing.Size(198, 22)
    Me.mnuLoadMap.Text = "&Load Map"
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(195, 6)
    '
    'mnuSaveDoc
    '
    Me.mnuSaveDoc.Enabled = False
    Me.mnuSaveDoc.Name = "mnuSaveDoc"
    Me.mnuSaveDoc.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
    Me.mnuSaveDoc.Size = New System.Drawing.Size(198, 22)
    Me.mnuSaveDoc.Text = "Save &Document"
    '
    'ToolStripMenuItem3
    '
    Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
    Me.ToolStripMenuItem3.Size = New System.Drawing.Size(195, 6)
    '
    'mnuBuildMap
    '
    Me.mnuBuildMap.Enabled = False
    Me.mnuBuildMap.Name = "mnuBuildMap"
    Me.mnuBuildMap.ShortcutKeys = System.Windows.Forms.Keys.F8
    Me.mnuBuildMap.Size = New System.Drawing.Size(198, 22)
    Me.mnuBuildMap.Text = "&Build Map"
    '
    'ToolStripMenuItem2
    '
    Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
    Me.ToolStripMenuItem2.Size = New System.Drawing.Size(195, 6)
    '
    'mnuExit
    '
    Me.mnuExit.Name = "mnuExit"
    Me.mnuExit.Size = New System.Drawing.Size(198, 22)
    Me.mnuExit.Text = "E&xit"
    '
    'mnuProject
    '
    Me.mnuProject.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewRoomToolStripMenuItem, Me.OpenSelectedRoomToolStripMenuItem, Me.mnuDeleteItem})
    Me.mnuProject.Enabled = False
    Me.mnuProject.Name = "mnuProject"
    Me.mnuProject.Size = New System.Drawing.Size(53, 20)
    Me.mnuProject.Text = "&Project"
    '
    'AddNewRoomToolStripMenuItem
    '
    Me.AddNewRoomToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAddImage, Me.mnuAddRoom, Me.mnuAddObject, Me.mnuAddScript})
    Me.AddNewRoomToolStripMenuItem.Name = "AddNewRoomToolStripMenuItem"
    Me.AddNewRoomToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
    Me.AddNewRoomToolStripMenuItem.Text = "&Add New"
    '
    'mnuAddImage
    '
    Me.mnuAddImage.Name = "mnuAddImage"
    Me.mnuAddImage.Size = New System.Drawing.Size(117, 22)
    Me.mnuAddImage.Text = "&Image"
    '
    'mnuAddRoom
    '
    Me.mnuAddRoom.Name = "mnuAddRoom"
    Me.mnuAddRoom.Size = New System.Drawing.Size(117, 22)
    Me.mnuAddRoom.Text = "&Room"
    '
    'mnuAddObject
    '
    Me.mnuAddObject.Name = "mnuAddObject"
    Me.mnuAddObject.Size = New System.Drawing.Size(117, 22)
    Me.mnuAddObject.Text = "&Object"
    '
    'mnuAddScript
    '
    Me.mnuAddScript.Name = "mnuAddScript"
    Me.mnuAddScript.Size = New System.Drawing.Size(117, 22)
    Me.mnuAddScript.Text = "&Script"
    '
    'OpenSelectedRoomToolStripMenuItem
    '
    Me.OpenSelectedRoomToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuImportImage, Me.mnuImportRoom, Me.mnuImportObject, Me.mnuImportScript})
    Me.OpenSelectedRoomToolStripMenuItem.Name = "OpenSelectedRoomToolStripMenuItem"
    Me.OpenSelectedRoomToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
    Me.OpenSelectedRoomToolStripMenuItem.Text = "&Import"
    '
    'mnuImportImage
    '
    Me.mnuImportImage.Name = "mnuImportImage"
    Me.mnuImportImage.Size = New System.Drawing.Size(117, 22)
    Me.mnuImportImage.Text = "&Image"
    '
    'mnuImportRoom
    '
    Me.mnuImportRoom.Name = "mnuImportRoom"
    Me.mnuImportRoom.Size = New System.Drawing.Size(117, 22)
    Me.mnuImportRoom.Text = "&Room"
    '
    'mnuImportObject
    '
    Me.mnuImportObject.Name = "mnuImportObject"
    Me.mnuImportObject.Size = New System.Drawing.Size(117, 22)
    Me.mnuImportObject.Text = "&Object"
    '
    'mnuImportScript
    '
    Me.mnuImportScript.Name = "mnuImportScript"
    Me.mnuImportScript.Size = New System.Drawing.Size(117, 22)
    Me.mnuImportScript.Text = "&Script"
    '
    'mnuDeleteItem
    '
    Me.mnuDeleteItem.Name = "mnuDeleteItem"
    Me.mnuDeleteItem.Size = New System.Drawing.Size(185, 22)
    Me.mnuDeleteItem.Text = "&Delete Selected Item"
    '
    'mnuWindows
    '
    Me.mnuWindows.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveLayoutToolStripMenuItem, Me.ToolStripMenuItem5})
    Me.mnuWindows.Name = "mnuWindows"
    Me.mnuWindows.Size = New System.Drawing.Size(62, 20)
    Me.mnuWindows.Text = "&Windows"
    '
    'SaveLayoutToolStripMenuItem
    '
    Me.SaveLayoutToolStripMenuItem.Name = "SaveLayoutToolStripMenuItem"
    Me.SaveLayoutToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
    Me.SaveLayoutToolStripMenuItem.Text = "&Save Layout"
    '
    'ToolStripMenuItem5
    '
    Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
    Me.ToolStripMenuItem5.Size = New System.Drawing.Size(142, 6)
    '
    'HelpToolStripMenuItem
    '
    Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DynamicHelpToolStripMenuItem, Me.ToolStripMenuItem4, Me.AboutToolStripMenuItem})
    Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
    Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
    Me.HelpToolStripMenuItem.Text = "&Help"
    '
    'DynamicHelpToolStripMenuItem
    '
    Me.DynamicHelpToolStripMenuItem.Name = "DynamicHelpToolStripMenuItem"
    Me.DynamicHelpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
    Me.DynamicHelpToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
    Me.DynamicHelpToolStripMenuItem.Text = "Dynamic &Help"
    '
    'ToolStripMenuItem4
    '
    Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
    Me.ToolStripMenuItem4.Size = New System.Drawing.Size(165, 6)
    '
    'AboutToolStripMenuItem
    '
    Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
    Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
    Me.AboutToolStripMenuItem.Text = "&About"
    '
    'dockMan
    '
    Me.dockMan.ContainerControl = Me
    Me.dockMan.InnerControl = Nothing
    Me.dockMan.OuterControl = Nothing
    '
    'prjTree
    '
    Me.dockMan.SetAllowDocking(Me.prjTree, True)
    Me.dockMan.SetCloseButton(Me.prjTree, False)
    Me.prjTree.Dock = System.Windows.Forms.DockStyle.Right
    Me.dockMan.SetFullTitle(Me.prjTree, "Project")
    Me.dockMan.SetIcon(Me.prjTree, CType(resources.GetObject("prjTree.Icon"), System.Drawing.Image))
    Me.prjTree.ImageIndex = 0
    Me.prjTree.ImageList = Me.ilProj
    Me.prjTree.Location = New System.Drawing.Point(657, 24)
    Me.prjTree.Name = "prjTree"
    Me.prjTree.SelectedImageIndex = 0
    Me.prjTree.Size = New System.Drawing.Size(197, 613)
    Me.prjTree.TabIndex = 5
    Me.dockMan.SetTitle(Me.prjTree, "Project")
    '
    'ilProj
    '
    Me.ilProj.ImageStream = CType(resources.GetObject("ilProj.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ilProj.TransparentColor = System.Drawing.Color.Transparent
    Me.ilProj.Images.SetKeyName(0, "folder_closed.ICO")
    Me.ilProj.Images.SetKeyName(1, "Image2.ico")
    Me.ilProj.Images.SetKeyName(2, "Room.ico")
    Me.ilProj.Images.SetKeyName(3, "Object.ico")
    Me.ilProj.Images.SetKeyName(4, "script.ICO")
    '
    'rtfHelp
    '
    Me.rtfHelp.AcceptsTab = True
    Me.dockMan.SetAllowDocking(Me.rtfHelp, True)
    Me.rtfHelp.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.rtfHelp.Dock = System.Windows.Forms.DockStyle.Right
    Me.dockMan.SetFullTitle(Me.rtfHelp, "Help")
    Me.dockMan.SetIcon(Me.rtfHelp, CType(resources.GetObject("rtfHelp.Icon"), System.Drawing.Image))
    Me.rtfHelp.Location = New System.Drawing.Point(441, 24)
    Me.rtfHelp.Name = "rtfHelp"
    Me.rtfHelp.ReadOnly = True
    Me.rtfHelp.Size = New System.Drawing.Size(216, 613)
    Me.rtfHelp.TabIndex = 7
    Me.rtfHelp.Text = ""
    Me.dockMan.SetTitle(Me.rtfHelp, "Help")
    '
    'fbd
    '
    Me.fbd.RootFolder = System.Environment.SpecialFolder.MyComputer
    '
    'iid
    '
    Me.iid.Filter = "Image Files|*.bmp;*.png|All Files|*.*"
    '
    'EditorMain
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(854, 637)
    Me.Controls.Add(Me.rtfHelp)
    Me.Controls.Add(Me.prjTree)
    Me.Controls.Add(Me.mainMenu)
    Me.IsMdiContainer = True
    Me.MainMenuStrip = Me.mainMenu
    Me.Name = "EditorMain"
    Me.Text = "JANE v4 Map Editor"
    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
    Me.mainMenu.ResumeLayout(False)
    Me.mainMenu.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents mainMenu As System.Windows.Forms.MenuStrip
  Friend WithEvents dockMan As NETXP.Controls.Docking.DockingManagerExtender
  Friend WithEvents prjTree As System.Windows.Forms.TreeView
  Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ilProj As System.Windows.Forms.ImageList
  Friend WithEvents mnuNewMap As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuLoadMap As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents mnuBuildMap As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuProject As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents AddNewRoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents OpenSelectedRoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuDeleteItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuAddImage As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuAddRoom As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuAddObject As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuAddScript As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuImportImage As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuImportRoom As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuImportObject As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuImportScript As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents mnuSaveDoc As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents fbd As System.Windows.Forms.FolderBrowserDialog
  Friend WithEvents mnuWindows As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents rtfHelp As System.Windows.Forms.RichTextBox
  Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents DynamicHelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents iid As System.Windows.Forms.OpenFileDialog
  Friend WithEvents SaveLayoutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator

End Class
