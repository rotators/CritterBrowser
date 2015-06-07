namespace CritterBrowser.Forms
{
    partial class frmCompletion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpBBCode = new System.Windows.Forms.GroupBox();
            this.flowBBCode = new System.Windows.Forms.FlowLayoutPanel();
            this.radioNormal = new System.Windows.Forms.RadioButton();
            this.radioRu = new System.Windows.Forms.RadioButton();
            this.flowBBCodeColumns = new System.Windows.Forms.FlowLayoutPanel();
            this.lblColumns = new System.Windows.Forms.Label();
            this.numColumns = new System.Windows.Forms.NumericUpDown();
            this.textOut = new System.Windows.Forms.TextBox();
            this.checkFull = new System.Windows.Forms.CheckBox();
            this.groupGeneral = new System.Windows.Forms.GroupBox();
            this.flowGeneral = new System.Windows.Forms.FlowLayoutPanel();
            this.radioText = new System.Windows.Forms.RadioButton();
            this.radioForum = new System.Windows.Forms.RadioButton();
            this.radioPicture = new System.Windows.Forms.RadioButton();
            this.flowOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.grpPicture = new System.Windows.Forms.GroupBox();
            this.picOut = new System.Windows.Forms.PictureBox();
            this.flowOutput = new System.Windows.Forms.FlowLayoutPanel();
            this.flowMain = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveText = new System.Windows.Forms.Button();
            this.btnSavePicture = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.saveText = new System.Windows.Forms.SaveFileDialog();
            this.grpBBCode.SuspendLayout();
            this.flowBBCode.SuspendLayout();
            this.flowBBCodeColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numColumns)).BeginInit();
            this.groupGeneral.SuspendLayout();
            this.flowGeneral.SuspendLayout();
            this.flowOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOut)).BeginInit();
            this.flowOutput.SuspendLayout();
            this.flowMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBBCode
            // 
            this.grpBBCode.AutoSize = true;
            this.grpBBCode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpBBCode.Controls.Add( this.flowBBCode );
            this.grpBBCode.Location = new System.Drawing.Point( 141, 3 );
            this.grpBBCode.Name = "grpBBCode";
            this.grpBBCode.Size = new System.Drawing.Size( 107, 97 );
            this.grpBBCode.TabIndex = 0;
            this.grpBBCode.TabStop = false;
            this.grpBBCode.Text = "BBCode options";
            // 
            // flowBBCode
            // 
            this.flowBBCode.AutoSize = true;
            this.flowBBCode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowBBCode.Controls.Add( this.radioNormal );
            this.flowBBCode.Controls.Add( this.radioRu );
            this.flowBBCode.Controls.Add( this.flowBBCodeColumns );
            this.flowBBCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowBBCode.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowBBCode.Location = new System.Drawing.Point( 3, 16 );
            this.flowBBCode.Name = "flowBBCode";
            this.flowBBCode.Size = new System.Drawing.Size( 101, 78 );
            this.flowBBCode.TabIndex = 2;
            // 
            // radioNormal
            // 
            this.radioNormal.AutoSize = true;
            this.radioNormal.Checked = true;
            this.radioNormal.Location = new System.Drawing.Point( 3, 3 );
            this.radioNormal.Name = "radioNormal";
            this.radioNormal.Size = new System.Drawing.Size( 93, 17 );
            this.radioNormal.TabIndex = 0;
            this.radioNormal.TabStop = true;
            this.radioNormal.Text = "SMF, vBulletin";
            this.radioNormal.UseVisualStyleBackColor = true;
            this.radioNormal.CheckedChanged += new System.EventHandler( this.event_forumGeneric );
            // 
            // radioRu
            // 
            this.radioRu.AutoSize = true;
            this.radioRu.Location = new System.Drawing.Point( 3, 26 );
            this.radioRu.Name = "radioRu";
            this.radioRu.Size = new System.Drawing.Size( 68, 17 );
            this.radioRu.TabIndex = 1;
            this.radioRu.Text = "fonline.ru";
            this.radioRu.UseVisualStyleBackColor = true;
            this.radioRu.CheckedChanged += new System.EventHandler( this.event_forumGeneric );
            // 
            // flowBBCodeColumns
            // 
            this.flowBBCodeColumns.AutoSize = true;
            this.flowBBCodeColumns.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowBBCodeColumns.Controls.Add( this.lblColumns );
            this.flowBBCodeColumns.Controls.Add( this.numColumns );
            this.flowBBCodeColumns.Location = new System.Drawing.Point( 3, 49 );
            this.flowBBCodeColumns.Name = "flowBBCodeColumns";
            this.flowBBCodeColumns.Size = new System.Drawing.Size( 95, 26 );
            this.flowBBCodeColumns.TabIndex = 3;
            // 
            // lblColumns
            // 
            this.lblColumns.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point( 3, 6 );
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size( 47, 13 );
            this.lblColumns.TabIndex = 3;
            this.lblColumns.Text = "Columns";
            // 
            // numColumns
            // 
            this.numColumns.Location = new System.Drawing.Point( 56, 3 );
            this.numColumns.Maximum = new decimal( new int[] {
            10,
            0,
            0,
            0} );
            this.numColumns.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.numColumns.Name = "numColumns";
            this.numColumns.Size = new System.Drawing.Size( 36, 20 );
            this.numColumns.TabIndex = 2;
            this.numColumns.Value = new decimal( new int[] {
            5,
            0,
            0,
            0} );
            this.numColumns.ValueChanged += new System.EventHandler( this.event_forumGeneric );
            // 
            // textOut
            // 
            this.textOut.Location = new System.Drawing.Point( 3, 3 );
            this.textOut.Multiline = true;
            this.textOut.Name = "textOut";
            this.textOut.ReadOnly = true;
            this.textOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textOut.Size = new System.Drawing.Size( 283, 200 );
            this.textOut.TabIndex = 1;
            // 
            // checkFull
            // 
            this.checkFull.AutoSize = true;
            this.checkFull.Location = new System.Drawing.Point( 3, 72 );
            this.checkFull.Name = "checkFull";
            this.checkFull.Size = new System.Drawing.Size( 120, 17 );
            this.checkFull.TabIndex = 4;
            this.checkFull.Text = "Include missing sets";
            this.checkFull.UseVisualStyleBackColor = true;
            this.checkFull.CheckedChanged += new System.EventHandler( this.checkFull_CheckedChanged );
            // 
            // groupGeneral
            // 
            this.groupGeneral.AutoSize = true;
            this.groupGeneral.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupGeneral.Controls.Add( this.flowGeneral );
            this.groupGeneral.Location = new System.Drawing.Point( 3, 3 );
            this.groupGeneral.Name = "groupGeneral";
            this.groupGeneral.Size = new System.Drawing.Size( 132, 111 );
            this.groupGeneral.TabIndex = 2;
            this.groupGeneral.TabStop = false;
            this.groupGeneral.Text = "General";
            // 
            // flowGeneral
            // 
            this.flowGeneral.AutoSize = true;
            this.flowGeneral.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowGeneral.Controls.Add( this.radioText );
            this.flowGeneral.Controls.Add( this.radioForum );
            this.flowGeneral.Controls.Add( this.radioPicture );
            this.flowGeneral.Controls.Add( this.checkFull );
            this.flowGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowGeneral.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowGeneral.Location = new System.Drawing.Point( 3, 16 );
            this.flowGeneral.Name = "flowGeneral";
            this.flowGeneral.Size = new System.Drawing.Size( 126, 92 );
            this.flowGeneral.TabIndex = 2;
            // 
            // radioText
            // 
            this.radioText.AutoSize = true;
            this.radioText.Location = new System.Drawing.Point( 3, 3 );
            this.radioText.Name = "radioText";
            this.radioText.Size = new System.Drawing.Size( 46, 17 );
            this.radioText.TabIndex = 0;
            this.radioText.TabStop = true;
            this.radioText.Text = "Text";
            this.radioText.UseVisualStyleBackColor = true;
            this.radioText.CheckedChanged += new System.EventHandler( this.radioText_CheckedChanged );
            // 
            // radioForum
            // 
            this.radioForum.AutoSize = true;
            this.radioForum.Location = new System.Drawing.Point( 3, 26 );
            this.radioForum.Name = "radioForum";
            this.radioForum.Size = new System.Drawing.Size( 64, 17 );
            this.radioForum.TabIndex = 1;
            this.radioForum.TabStop = true;
            this.radioForum.Text = "BBCode";
            this.radioForum.UseVisualStyleBackColor = true;
            this.radioForum.CheckedChanged += new System.EventHandler( this.event_forumGeneric );
            // 
            // radioPicture
            // 
            this.radioPicture.AutoSize = true;
            this.radioPicture.Enabled = false;
            this.radioPicture.Location = new System.Drawing.Point( 3, 49 );
            this.radioPicture.Name = "radioPicture";
            this.radioPicture.Size = new System.Drawing.Size( 58, 17 );
            this.radioPicture.TabIndex = 5;
            this.radioPicture.TabStop = true;
            this.radioPicture.Text = "Picture";
            this.radioPicture.UseVisualStyleBackColor = true;
            // 
            // flowOptions
            // 
            this.flowOptions.AutoSize = true;
            this.flowOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowOptions.Controls.Add( this.groupGeneral );
            this.flowOptions.Controls.Add( this.grpBBCode );
            this.flowOptions.Controls.Add( this.grpPicture );
            this.flowOptions.Location = new System.Drawing.Point( 3, 3 );
            this.flowOptions.Name = "flowOptions";
            this.flowOptions.Size = new System.Drawing.Size( 457, 117 );
            this.flowOptions.TabIndex = 3;
            // 
            // grpPicture
            // 
            this.grpPicture.Location = new System.Drawing.Point( 254, 3 );
            this.grpPicture.Name = "grpPicture";
            this.grpPicture.Size = new System.Drawing.Size( 200, 100 );
            this.grpPicture.TabIndex = 3;
            this.grpPicture.TabStop = false;
            this.grpPicture.Text = "Picture options";
            this.grpPicture.Visible = false;
            // 
            // picOut
            // 
            this.picOut.Location = new System.Drawing.Point( 292, 3 );
            this.picOut.Name = "picOut";
            this.picOut.Size = new System.Drawing.Size( 283, 200 );
            this.picOut.TabIndex = 4;
            this.picOut.TabStop = false;
            // 
            // flowOutput
            // 
            this.flowOutput.AutoSize = true;
            this.flowOutput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowOutput.Controls.Add( this.textOut );
            this.flowOutput.Controls.Add( this.picOut );
            this.flowOutput.Location = new System.Drawing.Point( 3, 126 );
            this.flowOutput.Name = "flowOutput";
            this.flowOutput.Size = new System.Drawing.Size( 578, 206 );
            this.flowOutput.TabIndex = 5;
            // 
            // flowMain
            // 
            this.flowMain.AutoSize = true;
            this.flowMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowMain.Controls.Add( this.flowOptions );
            this.flowMain.Controls.Add( this.flowOutput );
            this.flowMain.Controls.Add( this.flowLayoutPanel1 );
            this.flowMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMain.Location = new System.Drawing.Point( 0, 0 );
            this.flowMain.Name = "flowMain";
            this.flowMain.Size = new System.Drawing.Size( 684, 548 );
            this.flowMain.TabIndex = 6;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add( this.btnSaveText );
            this.flowLayoutPanel1.Controls.Add( this.btnSavePicture );
            this.flowLayoutPanel1.Controls.Add( this.btnClose );
            this.flowLayoutPanel1.Location = new System.Drawing.Point( 331, 338 );
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size( 250, 29 );
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // btnSaveText
            // 
            this.btnSaveText.AutoSize = true;
            this.btnSaveText.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSaveText.Location = new System.Drawing.Point( 3, 3 );
            this.btnSaveText.Name = "btnSaveText";
            this.btnSaveText.Size = new System.Drawing.Size( 71, 23 );
            this.btnSaveText.TabIndex = 2;
            this.btnSaveText.Text = "Save text...";
            this.btnSaveText.UseVisualStyleBackColor = true;
            this.btnSaveText.Click += new System.EventHandler( this.btnSaveText_Click );
            // 
            // btnSavePicture
            // 
            this.btnSavePicture.AutoSize = true;
            this.btnSavePicture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSavePicture.Location = new System.Drawing.Point( 80, 3 );
            this.btnSavePicture.Name = "btnSavePicture";
            this.btnSavePicture.Size = new System.Drawing.Size( 86, 23 );
            this.btnSavePicture.TabIndex = 1;
            this.btnSavePicture.Text = "Save picture...";
            this.btnSavePicture.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point( 172, 3 );
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size( 75, 23 );
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler( this.btnClose_Click );
            // 
            // saveText
            // 
            this.saveText.DefaultExt = "txt";
            this.saveText.Filter = "Text file|.txt";
            this.saveText.RestoreDirectory = true;
            this.saveText.SupportMultiDottedExtensions = true;
            // 
            // frmCompletion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size( 684, 548 );
            this.Controls.Add( this.flowMain );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCompletion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Completion report";
            this.grpBBCode.ResumeLayout( false );
            this.grpBBCode.PerformLayout();
            this.flowBBCode.ResumeLayout( false );
            this.flowBBCode.PerformLayout();
            this.flowBBCodeColumns.ResumeLayout( false );
            this.flowBBCodeColumns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numColumns)).EndInit();
            this.groupGeneral.ResumeLayout( false );
            this.groupGeneral.PerformLayout();
            this.flowGeneral.ResumeLayout( false );
            this.flowGeneral.PerformLayout();
            this.flowOptions.ResumeLayout( false );
            this.flowOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOut)).EndInit();
            this.flowOutput.ResumeLayout( false );
            this.flowOutput.PerformLayout();
            this.flowMain.ResumeLayout( false );
            this.flowMain.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout( false );
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBBCode;
        private System.Windows.Forms.FlowLayoutPanel flowBBCode;
        private System.Windows.Forms.RadioButton radioNormal;
        private System.Windows.Forms.RadioButton radioRu;
        private System.Windows.Forms.FlowLayoutPanel flowBBCodeColumns;
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.NumericUpDown numColumns;
        private System.Windows.Forms.CheckBox checkFull;
        private System.Windows.Forms.TextBox textOut;
        private System.Windows.Forms.GroupBox groupGeneral;
        private System.Windows.Forms.FlowLayoutPanel flowGeneral;
        private System.Windows.Forms.RadioButton radioText;
        private System.Windows.Forms.RadioButton radioForum;
        private System.Windows.Forms.FlowLayoutPanel flowOptions;
        private System.Windows.Forms.PictureBox picOut;
        private System.Windows.Forms.FlowLayoutPanel flowOutput;
        private System.Windows.Forms.RadioButton radioPicture;
        private System.Windows.Forms.FlowLayoutPanel flowMain;
        private System.Windows.Forms.GroupBox grpPicture;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSaveText;
        private System.Windows.Forms.Button btnSavePicture;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SaveFileDialog saveText;
    }
}