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

            Settings.Config.ToControl( autosaveTarget, IniParser.RootSection, "AutoSaveTarget" );
            Settings.Config.ToControl( fastFRM, IniParser.RootSection, "FastCheckFRM" );
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            Settings.Config.FromControl( autosaveTarget, IniParser.RootSection, "AutoSaveTarget" );
            Settings.Config.FromControl( fastFRM, IniParser.RootSection, "FastCheckFRM" );

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
