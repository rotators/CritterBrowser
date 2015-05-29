namespace CritterBrowser.Forms
{
    partial class frmOptionsGeneral
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
            this.autosaveTarget = new System.Windows.Forms.CheckBox();
            this.fastFRM = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // autosaveTarget
            // 
            this.autosaveTarget.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autosaveTarget.AutoSize = true;
            this.autosaveTarget.Enabled = false;
            this.autosaveTarget.Location = new System.Drawing.Point( 8, 53 );
            this.autosaveTarget.Name = "autosaveTarget";
            this.autosaveTarget.Size = new System.Drawing.Size( 204, 17 );
            this.autosaveTarget.TabIndex = 0;
            this.autosaveTarget.Text = "Autosave target-specific configuration";
            this.autosaveTarget.UseVisualStyleBackColor = true;
            // 
            // fastFRM
            // 
            this.fastFRM.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.fastFRM.AutoSize = true;
            this.fastFRM.Location = new System.Drawing.Point( 8, 156 );
            this.fastFRM.Name = "fastFRM";
            this.fastFRM.Size = new System.Drawing.Size( 159, 17 );
            this.fastFRM.TabIndex = 1;
            this.fastFRM.Text = "Simple animation verification";
            this.fastFRM.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.Controls.Add( this.fastFRM, 0, 1 );
            this.tableLayoutPanel1.Controls.Add( this.autosaveTarget, 0, 0 );
            this.tableLayoutPanel1.Controls.Add( this.label1, 1, 0 );
            this.tableLayoutPanel1.Controls.Add( this.label2, 1, 1 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 0, 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding( 5, 10, 5, 10 );
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 437, 226 );
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 221, 42 );
            this.label1.MaximumSize = new System.Drawing.Size( 200, 0 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 170, 39 );
            this.label1.TabIndex = 2;
            this.label1.Text = "Update target configuration file automagically when changing any settings.";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 221, 145 );
            this.label2.MaximumSize = new System.Drawing.Size( 200, 0 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 193, 39 );
            this.label2.TabIndex = 3;
            this.label2.Text = "Skip loading of animations files when processing datafile/directory. Initial info" +
                " will be set basing on file existence.";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add( this.btnCancel );
            this.flowLayoutPanel1.Controls.Add( this.btnOK );
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point( 0, 226 );
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding( 0, 10, 0, 0 );
            this.flowLayoutPanel1.Size = new System.Drawing.Size( 437, 39 );
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point( 359, 13 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 75, 23 );
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point( 278, 13 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 75, 23 );
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
            // 
            // frmOptionsGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size( 437, 265 );
            this.Controls.Add( this.tableLayoutPanel1 );
            this.Controls.Add( this.flowLayoutPanel1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmOptionsGeneral";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General options";
            this.tableLayoutPanel1.ResumeLayout( false );
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox autosaveTarget;
        private System.Windows.Forms.CheckBox fastFRM;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;

    }
}