namespace CritterBrowser.Forms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpenDatafile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpenDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.tabGame = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.tabFallout = new System.Windows.Forms.TabPage();
            this.falloutCrittersLst = new System.Windows.Forms.TextBox();
            this.txtFalloutCrittersLst = new System.Windows.Forms.Label();
            this.grpFalloutConfiguration = new System.Windows.Forms.GroupBox();
            this.flwFalloutConfiguration = new System.Windows.Forms.FlowLayoutPanel();
            this.txtFalloutAlias = new System.Windows.Forms.Label();
            this.falloutAlias = new System.Windows.Forms.NumericUpDown();
            this.tabFOnline = new System.Windows.Forms.TabPage();
            this.fonlineCritterTypesCfg = new System.Windows.Forms.TextBox();
            this.txtFonlineCritterTypesCfg = new System.Windows.Forms.Label();
            this.grpFonlineConfiguration = new System.Windows.Forms.GroupBox();
            this.tblFonlineConfiguration = new System.Windows.Forms.TableLayoutPanel();
            this.txtFonlineEnabled = new System.Windows.Forms.Label();
            this.txtFonlineId = new System.Windows.Forms.Label();
            this.txtFonlineAlias = new System.Windows.Forms.Label();
            this.txtFonlineMultihex = new System.Windows.Forms.Label();
            this.txtFonlineAim = new System.Windows.Forms.Label();
            this.txtFonlineArmor = new System.Windows.Forms.Label();
            this.txtFonlineRotate = new System.Windows.Forms.Label();
            this.txtFonlineWalk = new System.Windows.Forms.Label();
            this.txtFonlineRun = new System.Windows.Forms.Label();
            this.txtFonlineSteps = new System.Windows.Forms.Label();
            this.txtFonlineSound = new System.Windows.Forms.Label();
            this.txtFonlineComment = new System.Windows.Forms.Label();
            this.fonlineEnabled = new System.Windows.Forms.CheckBox();
            this.fonlineID = new System.Windows.Forms.NumericUpDown();
            this.fonlineAlias = new System.Windows.Forms.NumericUpDown();
            this.fonlineMultihex = new System.Windows.Forms.NumericUpDown();
            this.fonlineAim = new System.Windows.Forms.CheckBox();
            this.fonlineArmor = new System.Windows.Forms.CheckBox();
            this.fonlineRotate = new System.Windows.Forms.CheckBox();
            this.fonlineWalk = new System.Windows.Forms.NumericUpDown();
            this.fonlineRun = new System.Windows.Forms.NumericUpDown();
            this.fonlineSteps1 = new System.Windows.Forms.NumericUpDown();
            this.fonlineSteps2 = new System.Windows.Forms.NumericUpDown();
            this.fonlineSteps3 = new System.Windows.Forms.NumericUpDown();
            this.fonlineSteps4 = new System.Windows.Forms.NumericUpDown();
            this.fonlineSound = new System.Windows.Forms.TextBox();
            this.fonlineComment = new System.Windows.Forms.TextBox();
            this.animations = new System.Windows.Forms.Panel();
            this.lstCritters = new System.Windows.Forms.ListBox();
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.frmChecker = new System.ComponentModel.BackgroundWorker();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptionsGeneral = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptionsTarget = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.tabGame.SuspendLayout();
            this.tabFallout.SuspendLayout();
            this.grpFalloutConfiguration.SuspendLayout();
            this.flwFalloutConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.falloutAlias)).BeginInit();
            this.tabFOnline.SuspendLayout();
            this.grpFonlineConfiguration.SuspendLayout();
            this.tblFonlineConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineAlias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineMultihex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineWalk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineSteps1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineSteps2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineSteps3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineSteps4)).BeginInit();
            this.status.SuspendLayout();
            this.SuspendLayout();
            // 
            // openDirectory
            // 
            this.openDirectory.Description = "Select a directory containing critters animations.";
            this.openDirectory.ShowNewFolderButton = false;
            // 
            // menu
            // 
            this.menu.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout,
            this.menuFile,
            this.menuOptions} );
            this.menu.Location = new System.Drawing.Point( 5, 0 );
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size( 678, 24 );
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // menuAbout
            // 
            this.menuAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.menuAbout.Enabled = false;
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size( 61, 20 );
            this.menuAbout.Text = "About...";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.menuFileOpen,
            this.toolStripSeparator1,
            this.menuFileExit} );
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size( 37, 20 );
            this.menuFile.Text = "File";
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.menuFileOpenDatafile,
            this.menuFileOpenDirectory} );
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.Size = new System.Drawing.Size( 152, 22 );
            this.menuFileOpen.Text = "Open...";
            // 
            // menuFileOpenDatafile
            // 
            this.menuFileOpenDatafile.Enabled = false;
            this.menuFileOpenDatafile.Name = "menuFileOpenDatafile";
            this.menuFileOpenDatafile.Size = new System.Drawing.Size( 131, 22 );
            this.menuFileOpenDatafile.Text = "Datafile...";
            this.menuFileOpenDatafile.Click += new System.EventHandler( this.menuFileOpenDatafile_Click );
            // 
            // menuFileOpenDirectory
            // 
            this.menuFileOpenDirectory.Name = "menuFileOpenDirectory";
            this.menuFileOpenDirectory.Size = new System.Drawing.Size( 131, 22 );
            this.menuFileOpenDirectory.Text = "Directory...";
            this.menuFileOpenDirectory.Click += new System.EventHandler( this.menuFileOpenDirectory_Click );
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size( 149, 6 );
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size( 152, 22 );
            this.menuFileExit.Text = "Exit";
            this.menuFileExit.Click += new System.EventHandler( this.menuFileExit_Click );
            // 
            // openFile
            // 
            this.openFile.Filter = "Fallout/FOnline datafiles|*.dat;*.zip";
            this.openFile.ReadOnlyChecked = true;
            this.openFile.RestoreDirectory = true;
            // 
            // tabGame
            // 
            this.tabGame.Controls.Add( this.tabGeneral );
            this.tabGame.Controls.Add( this.tabFallout );
            this.tabGame.Controls.Add( this.tabFOnline );
            this.tabGame.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabGame.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.tabGame.Location = new System.Drawing.Point( 5, 298 );
            this.tabGame.Name = "tabGame";
            this.tabGame.SelectedIndex = 0;
            this.tabGame.Size = new System.Drawing.Size( 678, 133 );
            this.tabGame.TabIndex = 3;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Location = new System.Drawing.Point( 4, 22 );
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size( 670, 107 );
            this.tabGeneral.TabIndex = 2;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // tabFallout
            // 
            this.tabFallout.Controls.Add( this.falloutCrittersLst );
            this.tabFallout.Controls.Add( this.txtFalloutCrittersLst );
            this.tabFallout.Controls.Add( this.grpFalloutConfiguration );
            this.tabFallout.Location = new System.Drawing.Point( 4, 22 );
            this.tabFallout.Name = "tabFallout";
            this.tabFallout.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabFallout.Size = new System.Drawing.Size( 670, 107 );
            this.tabFallout.TabIndex = 0;
            this.tabFallout.Text = "Fallout";
            this.tabFallout.UseVisualStyleBackColor = true;
            // 
            // falloutCrittersLst
            // 
            this.falloutCrittersLst.Dock = System.Windows.Forms.DockStyle.Top;
            this.falloutCrittersLst.Font = new System.Drawing.Font( "Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.falloutCrittersLst.Location = new System.Drawing.Point( 3, 74 );
            this.falloutCrittersLst.Name = "falloutCrittersLst";
            this.falloutCrittersLst.ReadOnly = true;
            this.falloutCrittersLst.Size = new System.Drawing.Size( 664, 20 );
            this.falloutCrittersLst.TabIndex = 1;
            // 
            // txtFalloutCrittersLst
            // 
            this.txtFalloutCrittersLst.AutoSize = true;
            this.txtFalloutCrittersLst.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFalloutCrittersLst.Location = new System.Drawing.Point( 3, 61 );
            this.txtFalloutCrittersLst.Name = "txtFalloutCrittersLst";
            this.txtFalloutCrittersLst.Size = new System.Drawing.Size( 51, 13 );
            this.txtFalloutCrittersLst.TabIndex = 0;
            this.txtFalloutCrittersLst.Text = "critters.lst";
            // 
            // grpFalloutConfiguration
            // 
            this.grpFalloutConfiguration.AutoSize = true;
            this.grpFalloutConfiguration.Controls.Add( this.flwFalloutConfiguration );
            this.grpFalloutConfiguration.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFalloutConfiguration.Location = new System.Drawing.Point( 3, 3 );
            this.grpFalloutConfiguration.Name = "grpFalloutConfiguration";
            this.grpFalloutConfiguration.Size = new System.Drawing.Size( 664, 58 );
            this.grpFalloutConfiguration.TabIndex = 2;
            this.grpFalloutConfiguration.TabStop = false;
            this.grpFalloutConfiguration.Text = "Configuration";
            // 
            // flwFalloutConfiguration
            // 
            this.flwFalloutConfiguration.AutoSize = true;
            this.flwFalloutConfiguration.Controls.Add( this.txtFalloutAlias );
            this.flwFalloutConfiguration.Controls.Add( this.falloutAlias );
            this.flwFalloutConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flwFalloutConfiguration.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flwFalloutConfiguration.Location = new System.Drawing.Point( 3, 16 );
            this.flwFalloutConfiguration.Name = "flwFalloutConfiguration";
            this.flwFalloutConfiguration.Size = new System.Drawing.Size( 658, 39 );
            this.flwFalloutConfiguration.TabIndex = 0;
            // 
            // txtFalloutAlias
            // 
            this.txtFalloutAlias.AutoSize = true;
            this.txtFalloutAlias.Location = new System.Drawing.Point( 3, 0 );
            this.txtFalloutAlias.Name = "txtFalloutAlias";
            this.txtFalloutAlias.Size = new System.Drawing.Size( 29, 13 );
            this.txtFalloutAlias.TabIndex = 0;
            this.txtFalloutAlias.Text = "Alias";
            // 
            // falloutAlias
            // 
            this.falloutAlias.AutoSize = true;
            this.falloutAlias.Location = new System.Drawing.Point( 3, 16 );
            this.falloutAlias.Maximum = new decimal( new int[] {
            999,
            0,
            0,
            0} );
            this.falloutAlias.MaximumSize = new System.Drawing.Size( 40, 0 );
            this.falloutAlias.Name = "falloutAlias";
            this.falloutAlias.Size = new System.Drawing.Size( 40, 20 );
            this.falloutAlias.TabIndex = 1;
            this.falloutAlias.ValueChanged += new System.EventHandler( this.falloutAlias_ValueChanged );
            // 
            // tabFOnline
            // 
            this.tabFOnline.Controls.Add( this.fonlineCritterTypesCfg );
            this.tabFOnline.Controls.Add( this.txtFonlineCritterTypesCfg );
            this.tabFOnline.Controls.Add( this.grpFonlineConfiguration );
            this.tabFOnline.Location = new System.Drawing.Point( 4, 22 );
            this.tabFOnline.Name = "tabFOnline";
            this.tabFOnline.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabFOnline.Size = new System.Drawing.Size( 670, 107 );
            this.tabFOnline.TabIndex = 1;
            this.tabFOnline.Text = "FOnline";
            this.tabFOnline.UseVisualStyleBackColor = true;
            // 
            // fonlineCritterTypesCfg
            // 
            this.fonlineCritterTypesCfg.Dock = System.Windows.Forms.DockStyle.Top;
            this.fonlineCritterTypesCfg.Font = new System.Drawing.Font( "Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.fonlineCritterTypesCfg.Location = new System.Drawing.Point( 3, 74 );
            this.fonlineCritterTypesCfg.Multiline = true;
            this.fonlineCritterTypesCfg.Name = "fonlineCritterTypesCfg";
            this.fonlineCritterTypesCfg.ReadOnly = true;
            this.fonlineCritterTypesCfg.Size = new System.Drawing.Size( 664, 38 );
            this.fonlineCritterTypesCfg.TabIndex = 1;
            // 
            // txtFonlineCritterTypesCfg
            // 
            this.txtFonlineCritterTypesCfg.AutoSize = true;
            this.txtFonlineCritterTypesCfg.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFonlineCritterTypesCfg.Location = new System.Drawing.Point( 3, 61 );
            this.txtFonlineCritterTypesCfg.Name = "txtFonlineCritterTypesCfg";
            this.txtFonlineCritterTypesCfg.Size = new System.Drawing.Size( 81, 13 );
            this.txtFonlineCritterTypesCfg.TabIndex = 0;
            this.txtFonlineCritterTypesCfg.Text = "CritterTypes.cfg";
            // 
            // grpFonlineConfiguration
            // 
            this.grpFonlineConfiguration.AutoSize = true;
            this.grpFonlineConfiguration.Controls.Add( this.tblFonlineConfiguration );
            this.grpFonlineConfiguration.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFonlineConfiguration.Location = new System.Drawing.Point( 3, 3 );
            this.grpFonlineConfiguration.Name = "grpFonlineConfiguration";
            this.grpFonlineConfiguration.Size = new System.Drawing.Size( 664, 58 );
            this.grpFonlineConfiguration.TabIndex = 3;
            this.grpFonlineConfiguration.TabStop = false;
            this.grpFonlineConfiguration.Text = "Configuration";
            // 
            // tblFonlineConfiguration
            // 
            this.tblFonlineConfiguration.AutoSize = true;
            this.tblFonlineConfiguration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblFonlineConfiguration.ColumnCount = 15;
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle() );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineEnabled, 0, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineId, 1, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineAlias, 2, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineMultihex, 3, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineAim, 4, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineArmor, 5, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineRotate, 6, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineWalk, 7, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineRun, 8, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineSteps, 9, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineSound, 13, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.txtFonlineComment, 14, 0 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineEnabled, 0, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineID, 1, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineAlias, 2, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineMultihex, 3, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineAim, 4, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineArmor, 5, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineRotate, 6, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineWalk, 7, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineRun, 8, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineSteps1, 9, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineSteps2, 10, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineSteps3, 11, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineSteps4, 12, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineSound, 13, 1 );
            this.tblFonlineConfiguration.Controls.Add( this.fonlineComment, 14, 1 );
            this.tblFonlineConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFonlineConfiguration.Location = new System.Drawing.Point( 3, 16 );
            this.tblFonlineConfiguration.Name = "tblFonlineConfiguration";
            this.tblFonlineConfiguration.RowCount = 2;
            this.tblFonlineConfiguration.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            this.tblFonlineConfiguration.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            this.tblFonlineConfiguration.Size = new System.Drawing.Size( 658, 39 );
            this.tblFonlineConfiguration.TabIndex = 6;
            // 
            // txtFonlineEnabled
            // 
            this.txtFonlineEnabled.AutoSize = true;
            this.txtFonlineEnabled.Location = new System.Drawing.Point( 3, 0 );
            this.txtFonlineEnabled.Name = "txtFonlineEnabled";
            this.txtFonlineEnabled.Size = new System.Drawing.Size( 46, 13 );
            this.txtFonlineEnabled.TabIndex = 25;
            this.txtFonlineEnabled.Text = "Enabled";
            // 
            // txtFonlineId
            // 
            this.txtFonlineId.AutoSize = true;
            this.txtFonlineId.Location = new System.Drawing.Point( 55, 0 );
            this.txtFonlineId.Name = "txtFonlineId";
            this.txtFonlineId.Size = new System.Drawing.Size( 18, 13 );
            this.txtFonlineId.TabIndex = 3;
            this.txtFonlineId.Text = "ID";
            this.txtFonlineId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFonlineAlias
            // 
            this.txtFonlineAlias.AutoSize = true;
            this.txtFonlineAlias.Location = new System.Drawing.Point( 101, 0 );
            this.txtFonlineAlias.Name = "txtFonlineAlias";
            this.txtFonlineAlias.Size = new System.Drawing.Size( 29, 13 );
            this.txtFonlineAlias.TabIndex = 6;
            this.txtFonlineAlias.Text = "Alias";
            this.txtFonlineAlias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFonlineMultihex
            // 
            this.txtFonlineMultihex.AutoSize = true;
            this.txtFonlineMultihex.Location = new System.Drawing.Point( 147, 0 );
            this.txtFonlineMultihex.Name = "txtFonlineMultihex";
            this.txtFonlineMultihex.Size = new System.Drawing.Size( 46, 13 );
            this.txtFonlineMultihex.TabIndex = 7;
            this.txtFonlineMultihex.Text = "Multihex";
            this.txtFonlineMultihex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFonlineAim
            // 
            this.txtFonlineAim.AutoSize = true;
            this.txtFonlineAim.Location = new System.Drawing.Point( 199, 0 );
            this.txtFonlineAim.Name = "txtFonlineAim";
            this.txtFonlineAim.Size = new System.Drawing.Size( 24, 13 );
            this.txtFonlineAim.TabIndex = 9;
            this.txtFonlineAim.Text = "Aim";
            // 
            // txtFonlineArmor
            // 
            this.txtFonlineArmor.AutoSize = true;
            this.txtFonlineArmor.Location = new System.Drawing.Point( 229, 0 );
            this.txtFonlineArmor.Name = "txtFonlineArmor";
            this.txtFonlineArmor.Size = new System.Drawing.Size( 34, 13 );
            this.txtFonlineArmor.TabIndex = 10;
            this.txtFonlineArmor.Text = "Armor";
            // 
            // txtFonlineRotate
            // 
            this.txtFonlineRotate.AutoSize = true;
            this.txtFonlineRotate.Location = new System.Drawing.Point( 269, 0 );
            this.txtFonlineRotate.Name = "txtFonlineRotate";
            this.txtFonlineRotate.Size = new System.Drawing.Size( 39, 13 );
            this.txtFonlineRotate.TabIndex = 11;
            this.txtFonlineRotate.Text = "Rotate";
            // 
            // txtFonlineWalk
            // 
            this.txtFonlineWalk.AutoSize = true;
            this.txtFonlineWalk.Location = new System.Drawing.Point( 314, 0 );
            this.txtFonlineWalk.Name = "txtFonlineWalk";
            this.txtFonlineWalk.Size = new System.Drawing.Size( 32, 13 );
            this.txtFonlineWalk.TabIndex = 12;
            this.txtFonlineWalk.Text = "Walk";
            // 
            // txtFonlineRun
            // 
            this.txtFonlineRun.AutoSize = true;
            this.txtFonlineRun.Location = new System.Drawing.Point( 360, 0 );
            this.txtFonlineRun.Name = "txtFonlineRun";
            this.txtFonlineRun.Size = new System.Drawing.Size( 27, 13 );
            this.txtFonlineRun.TabIndex = 13;
            this.txtFonlineRun.Text = "Run";
            // 
            // txtFonlineSteps
            // 
            this.txtFonlineSteps.AutoSize = true;
            this.txtFonlineSteps.Location = new System.Drawing.Point( 406, 0 );
            this.txtFonlineSteps.Name = "txtFonlineSteps";
            this.txtFonlineSteps.Size = new System.Drawing.Size( 34, 13 );
            this.txtFonlineSteps.TabIndex = 14;
            this.txtFonlineSteps.Text = "Steps";
            // 
            // txtFonlineSound
            // 
            this.txtFonlineSound.AutoSize = true;
            this.txtFonlineSound.Location = new System.Drawing.Point( 591, 0 );
            this.txtFonlineSound.Name = "txtFonlineSound";
            this.txtFonlineSound.Size = new System.Drawing.Size( 38, 13 );
            this.txtFonlineSound.TabIndex = 21;
            this.txtFonlineSound.Text = "Sound";
            // 
            // txtFonlineComment
            // 
            this.txtFonlineComment.AutoSize = true;
            this.txtFonlineComment.Location = new System.Drawing.Point( 697, 0 );
            this.txtFonlineComment.Name = "txtFonlineComment";
            this.txtFonlineComment.Size = new System.Drawing.Size( 51, 13 );
            this.txtFonlineComment.TabIndex = 22;
            this.txtFonlineComment.Text = "Comment";
            // 
            // fonlineEnabled
            // 
            this.fonlineEnabled.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fonlineEnabled.AutoSize = true;
            this.fonlineEnabled.Location = new System.Drawing.Point( 18, 19 );
            this.fonlineEnabled.Name = "fonlineEnabled";
            this.fonlineEnabled.Size = new System.Drawing.Size( 15, 14 );
            this.fonlineEnabled.TabIndex = 26;
            this.fonlineEnabled.UseVisualStyleBackColor = true;
            this.fonlineEnabled.CheckedChanged += new System.EventHandler( this.fonlineEnabled_CheckedChanged );
            // 
            // fonlineID
            // 
            this.fonlineID.AutoSize = true;
            this.fonlineID.Location = new System.Drawing.Point( 55, 16 );
            this.fonlineID.Maximum = new decimal( new int[] {
            999,
            0,
            0,
            0} );
            this.fonlineID.MaximumSize = new System.Drawing.Size( 40, 0 );
            this.fonlineID.Name = "fonlineID";
            this.fonlineID.Size = new System.Drawing.Size( 40, 20 );
            this.fonlineID.TabIndex = 2;
            this.fonlineID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fonlineID.ValueChanged += new System.EventHandler( this.fonlineID_ValueChanged );
            // 
            // fonlineAlias
            // 
            this.fonlineAlias.Location = new System.Drawing.Point( 101, 16 );
            this.fonlineAlias.Maximum = new decimal( new int[] {
            999,
            0,
            0,
            0} );
            this.fonlineAlias.MaximumSize = new System.Drawing.Size( 40, 0 );
            this.fonlineAlias.Name = "fonlineAlias";
            this.fonlineAlias.Size = new System.Drawing.Size( 40, 20 );
            this.fonlineAlias.TabIndex = 5;
            this.fonlineAlias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fonlineAlias.ValueChanged += new System.EventHandler( this.fonlineAlias_ValueChanged );
            // 
            // fonlineMultihex
            // 
            this.fonlineMultihex.Location = new System.Drawing.Point( 147, 16 );
            this.fonlineMultihex.Maximum = new decimal( new int[] {
            50,
            0,
            0,
            0} );
            this.fonlineMultihex.MaximumSize = new System.Drawing.Size( 40, 0 );
            this.fonlineMultihex.Name = "fonlineMultihex";
            this.fonlineMultihex.Size = new System.Drawing.Size( 40, 20 );
            this.fonlineMultihex.TabIndex = 8;
            this.fonlineMultihex.ValueChanged += new System.EventHandler( this.fonlineMultihex_ValueChanged );
            // 
            // fonlineAim
            // 
            this.fonlineAim.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fonlineAim.AutoSize = true;
            this.fonlineAim.Location = new System.Drawing.Point( 203, 19 );
            this.fonlineAim.Name = "fonlineAim";
            this.fonlineAim.Size = new System.Drawing.Size( 15, 14 );
            this.fonlineAim.TabIndex = 0;
            this.fonlineAim.UseVisualStyleBackColor = true;
            this.fonlineAim.CheckedChanged += new System.EventHandler( this.fonlineAim_CheckedChanged );
            // 
            // fonlineArmor
            // 
            this.fonlineArmor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fonlineArmor.AutoSize = true;
            this.fonlineArmor.Location = new System.Drawing.Point( 238, 19 );
            this.fonlineArmor.Name = "fonlineArmor";
            this.fonlineArmor.Size = new System.Drawing.Size( 15, 14 );
            this.fonlineArmor.TabIndex = 1;
            this.fonlineArmor.UseVisualStyleBackColor = true;
            this.fonlineArmor.CheckedChanged += new System.EventHandler( this.fonlineArmor_CheckedChanged );
            // 
            // fonlineRotate
            // 
            this.fonlineRotate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fonlineRotate.AutoSize = true;
            this.fonlineRotate.Location = new System.Drawing.Point( 281, 19 );
            this.fonlineRotate.Name = "fonlineRotate";
            this.fonlineRotate.Size = new System.Drawing.Size( 15, 14 );
            this.fonlineRotate.TabIndex = 2;
            this.fonlineRotate.UseVisualStyleBackColor = true;
            this.fonlineRotate.CheckedChanged += new System.EventHandler( this.fonlineRotate_CheckedChanged );
            // 
            // fonlineWalk
            // 
            this.fonlineWalk.Location = new System.Drawing.Point( 314, 16 );
            this.fonlineWalk.MaximumSize = new System.Drawing.Size( 40, 0 );
            this.fonlineWalk.Name = "fonlineWalk";
            this.fonlineWalk.Size = new System.Drawing.Size( 40, 20 );
            this.fonlineWalk.TabIndex = 15;
            this.fonlineWalk.ValueChanged += new System.EventHandler( this.fonlineWalk_ValueChanged );
            // 
            // fonlineRun
            // 
            this.fonlineRun.Location = new System.Drawing.Point( 360, 16 );
            this.fonlineRun.MaximumSize = new System.Drawing.Size( 40, 0 );
            this.fonlineRun.Name = "fonlineRun";
            this.fonlineRun.Size = new System.Drawing.Size( 40, 20 );
            this.fonlineRun.TabIndex = 16;
            this.fonlineRun.ValueChanged += new System.EventHandler( this.fonlineRun_ValueChanged );
            // 
            // fonlineSteps1
            // 
            this.fonlineSteps1.AutoSize = true;
            this.fonlineSteps1.Location = new System.Drawing.Point( 406, 16 );
            this.fonlineSteps1.Name = "fonlineSteps1";
            this.fonlineSteps1.Size = new System.Drawing.Size( 41, 20 );
            this.fonlineSteps1.TabIndex = 17;
            this.fonlineSteps1.ValueChanged += new System.EventHandler( this.fonlineSteps1_ValueChanged );
            // 
            // fonlineSteps2
            // 
            this.fonlineSteps2.Location = new System.Drawing.Point( 453, 16 );
            this.fonlineSteps2.MaximumSize = new System.Drawing.Size( 40, 0 );
            this.fonlineSteps2.Name = "fonlineSteps2";
            this.fonlineSteps2.Size = new System.Drawing.Size( 40, 20 );
            this.fonlineSteps2.TabIndex = 18;
            this.fonlineSteps2.ValueChanged += new System.EventHandler( this.fonlineSteps2_ValueChanged );
            // 
            // fonlineSteps3
            // 
            this.fonlineSteps3.Location = new System.Drawing.Point( 499, 16 );
            this.fonlineSteps3.MaximumSize = new System.Drawing.Size( 40, 0 );
            this.fonlineSteps3.Name = "fonlineSteps3";
            this.fonlineSteps3.Size = new System.Drawing.Size( 40, 20 );
            this.fonlineSteps3.TabIndex = 19;
            this.fonlineSteps3.ValueChanged += new System.EventHandler( this.fonlineSteps3_ValueChanged );
            // 
            // fonlineSteps4
            // 
            this.fonlineSteps4.Location = new System.Drawing.Point( 545, 16 );
            this.fonlineSteps4.MaximumSize = new System.Drawing.Size( 40, 0 );
            this.fonlineSteps4.Name = "fonlineSteps4";
            this.fonlineSteps4.Size = new System.Drawing.Size( 40, 20 );
            this.fonlineSteps4.TabIndex = 20;
            this.fonlineSteps4.ValueChanged += new System.EventHandler( this.fonlineSteps4_ValueChanged );
            // 
            // fonlineSound
            // 
            this.fonlineSound.Location = new System.Drawing.Point( 591, 16 );
            this.fonlineSound.Name = "fonlineSound";
            this.fonlineSound.Size = new System.Drawing.Size( 100, 20 );
            this.fonlineSound.TabIndex = 23;
            this.fonlineSound.TextChanged += new System.EventHandler( this.fonlineSound_TextChanged );
            // 
            // fonlineComment
            // 
            this.fonlineComment.Location = new System.Drawing.Point( 697, 16 );
            this.fonlineComment.Name = "fonlineComment";
            this.fonlineComment.Size = new System.Drawing.Size( 100, 20 );
            this.fonlineComment.TabIndex = 24;
            this.fonlineComment.TextChanged += new System.EventHandler( this.fonlineComment_TextChanged );
            // 
            // animations
            // 
            this.animations.AutoSize = true;
            this.animations.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.animations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.animations.Location = new System.Drawing.Point( 94, 24 );
            this.animations.Margin = new System.Windows.Forms.Padding( 5 );
            this.animations.Name = "animations";
            this.animations.Padding = new System.Windows.Forms.Padding( 5 );
            this.animations.Size = new System.Drawing.Size( 589, 274 );
            this.animations.TabIndex = 9;
            // 
            // lstCritters
            // 
            this.lstCritters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCritters.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstCritters.FormattingEnabled = true;
            this.lstCritters.Location = new System.Drawing.Point( 5, 24 );
            this.lstCritters.Name = "lstCritters";
            this.lstCritters.ScrollAlwaysVisible = true;
            this.lstCritters.Size = new System.Drawing.Size( 89, 274 );
            this.lstCritters.Sorted = true;
            this.lstCritters.TabIndex = 0;
            this.lstCritters.SelectedValueChanged += new System.EventHandler( this.lstCritters_SelectedValueChanged );
            // 
            // status
            // 
            this.status.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.statusProgress,
            this.statusLabel} );
            this.status.Location = new System.Drawing.Point( 5, 431 );
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size( 678, 22 );
            this.status.TabIndex = 10;
            // 
            // statusProgress
            // 
            this.statusProgress.Name = "statusProgress";
            this.statusProgress.Size = new System.Drawing.Size( 100, 16 );
            this.statusProgress.Visible = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size( 66, 17 );
            this.statusLabel.Text = "statusLabel";
            // 
            // frmChecker
            // 
            this.frmChecker.WorkerReportsProgress = true;
            this.frmChecker.WorkerSupportsCancellation = true;
            this.frmChecker.DoWork += new System.ComponentModel.DoWorkEventHandler( this.frmChecker_DoWork );
            this.frmChecker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler( this.frmChecker_ProgressChanged );
            this.frmChecker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler( this.frmChecker_RunWorkerCompleted );
            // 
            // menuOptions
            // 
            this.menuOptions.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.menuOptionsGeneral,
            this.menuOptionsTarget} );
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size( 61, 20 );
            this.menuOptions.Text = "Options";
            // 
            // menuOptionsGeneral
            // 
            this.menuOptionsGeneral.Name = "menuOptionsGeneral";
            this.menuOptionsGeneral.Size = new System.Drawing.Size( 152, 22 );
            this.menuOptionsGeneral.Text = "General";
            this.menuOptionsGeneral.Click += new System.EventHandler( this.menuOptionsGeneral_Click );
            // 
            // menuOptionsTarget
            // 
            this.menuOptionsTarget.Enabled = false;
            this.menuOptionsTarget.Name = "menuOptionsTarget";
            this.menuOptionsTarget.Size = new System.Drawing.Size( 152, 22 );
            this.menuOptionsTarget.Text = "Current target";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size( 688, 453 );
            this.Controls.Add( this.animations );
            this.Controls.Add( this.lstCritters );
            this.Controls.Add( this.tabGame );
            this.Controls.Add( this.status );
            this.Controls.Add( this.menu );
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding( 5, 0, 5, 0 );
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CritterBrowser";
            this.menu.ResumeLayout( false );
            this.menu.PerformLayout();
            this.tabGame.ResumeLayout( false );
            this.tabFallout.ResumeLayout( false );
            this.tabFallout.PerformLayout();
            this.grpFalloutConfiguration.ResumeLayout( false );
            this.grpFalloutConfiguration.PerformLayout();
            this.flwFalloutConfiguration.ResumeLayout( false );
            this.flwFalloutConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.falloutAlias)).EndInit();
            this.tabFOnline.ResumeLayout( false );
            this.tabFOnline.PerformLayout();
            this.grpFonlineConfiguration.ResumeLayout( false );
            this.grpFonlineConfiguration.PerformLayout();
            this.tblFonlineConfiguration.ResumeLayout( false );
            this.tblFonlineConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineAlias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineMultihex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineWalk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineSteps1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineSteps2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineSteps3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fonlineSteps4)).EndInit();
            this.status.ResumeLayout( false );
            this.status.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog openDirectory;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.TabControl tabGame;
        private System.Windows.Forms.TabPage tabFallout;
        private System.Windows.Forms.TabPage tabFOnline;
        private System.Windows.Forms.Panel animations;
        private System.Windows.Forms.ListBox lstCritters;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TextBox fonlineCritterTypesCfg;
        private System.Windows.Forms.Label txtFonlineCritterTypesCfg;
        private System.Windows.Forms.TextBox falloutCrittersLst;
        private System.Windows.Forms.Label txtFalloutCrittersLst;
        private System.Windows.Forms.GroupBox grpFonlineConfiguration;
        private System.Windows.Forms.NumericUpDown fonlineID;
        private System.Windows.Forms.Label txtFonlineId;
        private System.Windows.Forms.Label txtFonlineAlias;
        private System.Windows.Forms.NumericUpDown fonlineAlias;
        private System.Windows.Forms.Label txtFonlineMultihex;
        private System.Windows.Forms.NumericUpDown fonlineMultihex;
        private System.Windows.Forms.TableLayoutPanel tblFonlineConfiguration;
        private System.Windows.Forms.Label txtFonlineAim;
        private System.Windows.Forms.Label txtFonlineArmor;
        private System.Windows.Forms.Label txtFonlineRotate;
        private System.Windows.Forms.Label txtFonlineWalk;
        private System.Windows.Forms.Label txtFonlineRun;
        private System.Windows.Forms.Label txtFonlineSteps;
        private System.Windows.Forms.CheckBox fonlineAim;
        private System.Windows.Forms.CheckBox fonlineArmor;
        private System.Windows.Forms.CheckBox fonlineRotate;
        private System.Windows.Forms.NumericUpDown fonlineWalk;
        private System.Windows.Forms.NumericUpDown fonlineRun;
        private System.Windows.Forms.NumericUpDown fonlineSteps1;
        private System.Windows.Forms.NumericUpDown fonlineSteps2;
        private System.Windows.Forms.NumericUpDown fonlineSteps3;
        private System.Windows.Forms.NumericUpDown fonlineSteps4;
        private System.Windows.Forms.Label txtFonlineSound;
        private System.Windows.Forms.Label txtFonlineComment;
        private System.Windows.Forms.TextBox fonlineSound;
        private System.Windows.Forms.TextBox fonlineComment;
        private System.Windows.Forms.Label txtFonlineEnabled;
        private System.Windows.Forms.CheckBox fonlineEnabled;
        private System.Windows.Forms.GroupBox grpFalloutConfiguration;
        private System.Windows.Forms.FlowLayoutPanel flwFalloutConfiguration;
        private System.Windows.Forms.Label txtFalloutAlias;
        private System.Windows.Forms.NumericUpDown falloutAlias;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpenDatafile;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpenDirectory;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.ComponentModel.BackgroundWorker frmChecker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuOptions;
        private System.Windows.Forms.ToolStripMenuItem menuOptionsGeneral;
        private System.Windows.Forms.ToolStripMenuItem menuOptionsTarget;
    }
}

