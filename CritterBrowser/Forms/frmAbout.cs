using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace CritterBrowser.Forms
{
    partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();

            this.Text = String.Format( "About {0}", AssemblyTitle );
            this.txtProductName.Text = AssemblyProduct;
            this.txtVersion.Text = String.Format( "Version {0}", AssemblyVersion );
            this.txtCopyright.Text = AssemblyCopyright;
            this.txtCompanyName.Text = AssemblyCompany;
            this.textDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyTitleAttribute ), false );
                if( attributes.Length > 0 )
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if( titleAttribute.Title != "" )
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension( Assembly.GetExecutingAssembly().CodeBase );
            }
        }

        public string AssemblyVersion
        {
            get
            {
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                for( int v = 0; v < 2; v++ )
                {
                    if( version.Substring( version.Length - 2, 2 ) == ".0" )
                        version = version.Substring( 0, version.Length - 2 );
                }

                return (version);
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyDescriptionAttribute ), false );
                if( attributes.Length == 0 )
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyProductAttribute ), false );
                if( attributes.Length == 0 )
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyCopyrightAttribute ), false );
                if( attributes.Length == 0 )
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyCompanyAttribute ), false );
                if( attributes.Length == 0 )
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void btnOK_Click( object sender, EventArgs e )
        {
            Close();
        }
    }
}
