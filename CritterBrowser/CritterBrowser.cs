using System;
using System.Windows.Forms;

using CritterBrowser.Forms;

namespace CritterBrowser
{
    static class CritterBrowser
    {
        static IniParser Config;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
