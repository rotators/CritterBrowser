using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CritterBrowser.Forms
{
    public partial class frmOptionsGeneral : Form
    {
        public frmOptionsGeneral()
        {
            InitializeComponent();
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            // TODO
            // DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
