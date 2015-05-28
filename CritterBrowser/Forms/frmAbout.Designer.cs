namespace CritterBrowser.Forms
{
    partial class frmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( frmAbout ) );
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.txtProductName = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.Label();
            this.txtCopyright = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.Label();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 33F ) );
            this.tableLayoutPanel.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 67F ) );
            this.tableLayoutPanel.Controls.Add( this.logoPictureBox, 0, 0 );
            this.tableLayoutPanel.Controls.Add( this.txtProductName, 1, 0 );
            this.tableLayoutPanel.Controls.Add( this.txtVersion, 1, 1 );
            this.tableLayoutPanel.Controls.Add( this.txtCopyright, 1, 2 );
            this.tableLayoutPanel.Controls.Add( this.txtCompanyName, 1, 3 );
            this.tableLayoutPanel.Controls.Add( this.textDescription, 1, 4 );
            this.tableLayoutPanel.Controls.Add( this.btnOK, 1, 5 );
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point( 9, 9 );
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel.Size = new System.Drawing.Size( 417, 265 );
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject( "logoPictureBox.Image" )));
            this.logoPictureBox.Location = new System.Drawing.Point( 3, 3 );
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan( this.logoPictureBox, 6 );
            this.logoPictureBox.Size = new System.Drawing.Size( 131, 259 );
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // txtProductName
            // 
            this.txtProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductName.Location = new System.Drawing.Point( 143, 0 );
            this.txtProductName.Margin = new System.Windows.Forms.Padding( 6, 0, 3, 0 );
            this.txtProductName.MaximumSize = new System.Drawing.Size( 0, 17 );
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size( 271, 17 );
            this.txtProductName.TabIndex = 19;
            this.txtProductName.Text = "Product Name";
            this.txtProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVersion
            // 
            this.txtVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVersion.Location = new System.Drawing.Point( 143, 26 );
            this.txtVersion.Margin = new System.Windows.Forms.Padding( 6, 0, 3, 0 );
            this.txtVersion.MaximumSize = new System.Drawing.Size( 0, 17 );
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size( 271, 17 );
            this.txtVersion.TabIndex = 0;
            this.txtVersion.Text = "Version";
            this.txtVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCopyright
            // 
            this.txtCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCopyright.Location = new System.Drawing.Point( 143, 52 );
            this.txtCopyright.Margin = new System.Windows.Forms.Padding( 6, 0, 3, 0 );
            this.txtCopyright.MaximumSize = new System.Drawing.Size( 0, 17 );
            this.txtCopyright.Name = "txtCopyright";
            this.txtCopyright.Size = new System.Drawing.Size( 271, 17 );
            this.txtCopyright.TabIndex = 21;
            this.txtCopyright.Text = "Copyright";
            this.txtCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCompanyName.Location = new System.Drawing.Point( 143, 78 );
            this.txtCompanyName.Margin = new System.Windows.Forms.Padding( 6, 0, 3, 0 );
            this.txtCompanyName.MaximumSize = new System.Drawing.Size( 0, 17 );
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size( 271, 17 );
            this.txtCompanyName.TabIndex = 22;
            this.txtCompanyName.Text = "Company Name";
            this.txtCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textDescription
            // 
            this.textDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textDescription.Location = new System.Drawing.Point( 143, 107 );
            this.textDescription.Margin = new System.Windows.Forms.Padding( 6, 3, 3, 3 );
            this.textDescription.Multiline = true;
            this.textDescription.Name = "textDescription";
            this.textDescription.ReadOnly = true;
            this.textDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textDescription.Size = new System.Drawing.Size( 271, 126 );
            this.textDescription.TabIndex = 23;
            this.textDescription.TabStop = false;
            this.textDescription.Text = "Description";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point( 339, 239 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 75, 23 );
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
            // 
            // frmAbout
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 435, 283 );
            this.Controls.Add( this.tableLayoutPanel );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.Padding = new System.Windows.Forms.Padding( 9 );
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmAbout";
            this.tableLayoutPanel.ResumeLayout( false );
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label txtProductName;
        private System.Windows.Forms.Label txtVersion;
        private System.Windows.Forms.Label txtCopyright;
        private System.Windows.Forms.Label txtCompanyName;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.Button btnOK;
    }
}
