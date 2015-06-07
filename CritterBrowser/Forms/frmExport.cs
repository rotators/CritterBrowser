using System;
using System.Windows.Forms;

namespace CritterBrowser.Forms
{
    public partial class frmExport : Form
    {
        public frmExport( string crTypeName )
        {
            InitializeComponent();

            txtHeader.Text = "Exporting " + crTypeName + "...";
            DialogResult = DialogResult.Cancel;
        }

        private void Close( DialogResult result )
        {
            DialogResult = result;

            Close();
        }

        /// <summary>
        /// "OK" button event.
        /// </summary>
        private void btnOK_Click( object sender, EventArgs e )
        {
            Close( DialogResult.OK );
        }

        /// <summary>
        /// "Cancel" button event.
        /// </summary>
        private void btnCancel_Click( object sender, EventArgs e )
        {
            Close( DialogResult.Cancel );
        }
    }
}
