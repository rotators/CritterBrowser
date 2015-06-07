using System;
using System.IO;
using System.Windows.Forms;

using CritterBrowser.Forms;

namespace CritterBrowser
{
    public static class Settings
    {
        public static IniParser Config;

        public static string Get( string name )
        {
            return (Config.GetSetting( IniParser.RootSection, name ));
        }

        public static bool GetBool( string name )
        {
            return (Config.GetSettingBool( IniParser.RootSection, name ));
        }

        public static void Set( string name, bool value )
        {
            Config.AddSetting( IniParser.RootSection, name, value );
        }
    }

    static class CritterBrowser
    {
        public static readonly string Self = Application.ProductName;
        public static readonly string ConfigDirectory = Path.GetDirectoryName( Application.ExecutablePath ) + Path.DirectorySeparatorChar;
        public static readonly string ConfigName = Self + ".cfg";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Settings.Config = new IniParser( ConfigDirectory + "CritterBrowser.cfg" );
            if( !Settings.Config.Loaded )
            {
                Settings.Config.AddSetting( IniParser.RootSection, "AutoSaveTarget", true );
                Settings.Config.AddSetting( IniParser.RootSection, "FastCheckFRM", false );
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new frmMain() );
        }
    }
}
