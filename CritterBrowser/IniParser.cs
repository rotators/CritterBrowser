#define INIPARSER_CONTROLS

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#if INIPARSER_CONTROLS
using System.Drawing;
using System.Windows.Forms;
#endif

//
// based on https://github.com/rotators/fo2238/Tools/FO2238Config/ by Atom/Rotators
//

public class IniParser
{
    private Hashtable keyPairs = new Hashtable();
    private String iniFilePath;

    private struct SectionPair
    {
        public String Section;
        public String Key;
    }

    public readonly bool Loaded = false;

    public readonly static String RootSection = "ROOT";
    public readonly static Char[] CommentChars = { '#', ';' };
    public readonly static String[] FalseStrings = { "false", "disabled", "no", "0" };
    public readonly static String[] TrueStrings = { "true", "enabled", "yes", "1" };

    public static String FalseString { get { return (FalseStrings[0]); } }
    public static String TrueString { get { return (TrueStrings[0]); } }

    /// <summary>
    /// Opens the INI file at the given path and enumerates the values in the IniParser.
    /// </summary>
    /// <param name="iniPath">Full path to INI file.</param>
    public IniParser( String iniPath )
    {
        TextReader iniFile = null;
        String strLine = null;
        String currentRoot = null;
        String[] keyPair = null;

        iniFilePath = iniPath;

        if( File.Exists( iniPath ) )
        {
            try
            {
                iniFile = new StreamReader( iniPath );

                strLine = iniFile.ReadLine();

                while( strLine != null )
                {
                    strLine = strLine.Trim().TrimStart( new char[] { '\t' } );

                    if( strLine != "" )
                    {
                        if( strLine.StartsWith( "[" ) && strLine.EndsWith( "]" ) )
                        {
                            currentRoot = strLine.Substring( 1, strLine.Length - 2 );
                        }
                        else if( CommentChars.Length > 0 && strLine.IndexOfAny( CommentChars ) == 0 )
                        {
                            // comment
                        }
                        else
                        {
                            keyPair = strLine.Split( new char[] { '=' }, 2 );

                            SectionPair sectionPair;
                            String value = "";

                            if( currentRoot == null )
                                currentRoot = RootSection;

                            sectionPair.Section = currentRoot;
                            sectionPair.Key = keyPair[0];

                            if( keyPair.Length > 1 )
                                value = keyPair[1];

                            keyPairs.Add( sectionPair, value );
                        }
                    }

                    strLine = iniFile.ReadLine();
                }
            }
            catch( Exception ex )
            {
                throw ex;
            }
            finally
            {
                if( iniFile != null )
                {
                    iniFile.Close();
                    Loaded = true;
                }
            }
        }
    }

    /// <summary>
    /// Returns the value for the given section, key pair.
    /// </summary>
    /// <param name="sectionName">Section name.</param>
    /// <param name="settingName">Key name.</param>
    public String GetSetting( String sectionName, String settingName )
    {
        SectionPair sectionPair;
        sectionPair.Section = sectionName;
        sectionPair.Key = settingName;
        String ret = (String)keyPairs[sectionPair];
        if( (ret == null) || ret.Length == 0 || !keyPairs.Contains( sectionPair ) ) return "0";

        return ret;
    }

    /// <summary>
    /// Returns the value of the given section, key pair.
    /// </summary>
    /// <param name="sectionName">Section name.</param>
    /// <param name="settingName">Key name.</param>
    public Boolean GetSettingBool( String sectionName, String settingName )
    {
        String result = GetSetting( sectionName, settingName ).ToLower();
        if( ContainsNoCase( TrueStrings, result ) )
            return (true);

        return (false);
    }

    /// <summary>
    /// Enumerates all lines for given section.
    /// </summary>
    /// <param name="sectionName">Section to enum.</param>
    public String[] EnumSectionLines( String sectionName )
    {
        ArrayList tmpArray = new ArrayList();

        foreach( SectionPair pair in keyPairs.Keys )
        {
            if( pair.Section == sectionName )
                tmpArray.Add( pair.Key + "=" + keyPairs[pair] );
        }

        return (String[])tmpArray.ToArray( typeof( String ) );
    }

    public List<KeyValuePair<String, String>> EnumSection( String sectionName )
    {
        List<KeyValuePair<String, String>> tmpArray = new List<KeyValuePair<String, String>>();

        foreach( SectionPair pair in keyPairs.Keys )
        {
            if( pair.Section == sectionName )
                tmpArray.Add( new KeyValuePair<String, String>( pair.Key, keyPairs[pair].ToString() ) );
        }

        return tmpArray;
    }

    /// <summary>
    /// Adds or replaces a setting to the table to be saved.
    /// </summary>
    /// <param name="sectionName">Section to add under.</param>
    /// <param name="settingName">Key name to add.</param>
    /// <param name="settingValue">Value of key.</param>
    public void AddSetting( String sectionName, String settingName, String settingValue )
    {
        SectionPair sectionPair;
        sectionPair.Section = sectionName;
        sectionPair.Key = settingName;

        if( keyPairs.ContainsKey( sectionPair ) )
            keyPairs.Remove( sectionPair );

        keyPairs.Add( sectionPair, settingValue );
    }

    public void AddSetting( String sectionName, String settingName, Boolean settingValue )
    {
        AddSetting( sectionName, settingName, settingValue ? TrueString : FalseString );
    }

    /// <summary>
    /// Adds or replaces a setting to the table to be saved with a null value.
    /// </summary>
    /// <param name="sectionName">Section to add under.</param>
    /// <param name="settingName">Key name to add.</param>
    public void AddSetting( String sectionName, String settingName )
    {
        AddSetting( sectionName, settingName, null );
    }

    /// <summary>
    /// Remove a setting.
    /// </summary>
    /// <param name="sectionName">Section to add under.</param>
    /// <param name="settingName">Key name to add.</param>
    public void DeleteSetting( String sectionName, String settingName )
    {
        SectionPair sectionPair;
        sectionPair.Section = sectionName;
        sectionPair.Key = settingName;

        if( keyPairs.ContainsKey( sectionPair ) )
            keyPairs.Remove( sectionPair );
    }

    public bool IsSetting( String sectionName, String settingName )
    {
        SectionPair sectionPair;
        sectionPair.Section = sectionName;
        sectionPair.Key = settingName;

        return keyPairs.ContainsKey( sectionPair );
    }

    public void AddNewSetting( String sectionName, String settingName, String settingValue )
    {
        if( !IsSetting( sectionName, settingName ) )
            AddSetting( sectionName, settingName, settingValue );
    }

    public void AddNewSetting( String sectionName, String settingName, Boolean settingValue )
    {
        AddNewSetting( sectionName, settingName, settingValue ? TrueString : FalseString );
    }

    public void DeleteSection( String sectionName )
    {
        ArrayList pairs = new ArrayList();
        foreach( SectionPair pair in keyPairs.Keys )
            if( pair.Section == sectionName ) pairs.Add( pair );
        foreach( SectionPair pair in pairs ) keyPairs.Remove( pair );
    }

    /// <summary>
    /// Save settings to new file.
    /// </summary>
    /// <param name="newFilePath">New file path.</param>
    public void SaveSettings( String newFilePath )
    {
        ArrayList rootSection = new ArrayList();
        ArrayList sections = new ArrayList();
        String tmpValue = "";
        String strToSave = "";

        foreach( SectionPair sectionPair in keyPairs.Keys )
        {
            if( sectionPair.Section == RootSection )
                rootSection.Add( sectionPair );
            else if( !sections.Contains( sectionPair.Section ) )
                sections.Add( sectionPair.Section );
        }

        sections.Sort();

        if( rootSection.Count > 0 )
        {
            foreach( SectionPair sectionPair in rootSection )
            {
                tmpValue = (String)keyPairs[sectionPair];

                if( tmpValue != null )
                    tmpValue = "=" + tmpValue;

                strToSave += (sectionPair.Key + tmpValue + Environment.NewLine);
            }

            strToSave += Environment.NewLine;
        }

        foreach( String section in sections )
        {
            strToSave += ("[" + section + "]" + Environment.NewLine);

            foreach( SectionPair sectionPair in keyPairs.Keys )
            {
                if( sectionPair.Section == section )
                {
                    tmpValue = (String)keyPairs[sectionPair];

                    if( tmpValue != null )
                        tmpValue = "=" + tmpValue;

                    strToSave += (sectionPair.Key + tmpValue + Environment.NewLine);
                }
            }

            strToSave += Environment.NewLine;
        }

        try
        {
            TextWriter tw = new StreamWriter( newFilePath );
            tw.Write( strToSave );
            tw.Close();
        }
        catch( Exception ex )
        {
            throw ex;
        }
    }

    /// <summary>
    /// Save settings back to ini file.
    /// </summary>
    public void SaveSettings()
    {
        SaveSettings( iniFilePath );
    }

    private bool ContainsNoCase( string[] list, string value )
    {
        value = value.ToLower();
        foreach( string search in list )
        {
            if( search.ToLower() == value )
                return (true);
        }

        return (false);
    }

#if INIPARSER_CONTROLS

    /// <summary>
    /// Update control state basing on current settings.
    /// </summary>
    /// <param name="box">Target control.</param>
    /// <param name="section">Section name.</param>
    /// <param name="key">Key name.</param>
    public void ToControl( CheckBox box, String section, String key )
    {
        box.Checked = GetSettingBool( section, key );
    }

    /// <summary>
    /// Update control state basing on current settings.
    /// </summary>
    /// <param name="num">Target control.</param>
    /// <param name="section">Section name.</param>
    /// <param name="key">Key name.</param>
    public void ToControl( NumericUpDown num, String section, String key )
    {
        num.Value = Clamp( ParseInt( GetSetting( section, key ) ), (int)num.Minimum, (int)num.Maximum );
    }

    /// <summary>
    /// Update control state basing on current settings.
    /// </summary>
    /// <param name="box">Target control.</param>
    /// <param name="section">Section name.</param>
    /// <param name="key">Key name.</param>
    public void ToControl( ComboBox box, String section, String key )
    {
        box.SelectedIndex = Clamp( ParseInt( GetSetting( section, key ) ), 0, box.Items.Count - 1 );
    }

    /// <summary>
    /// Update control state basing on current settings.
    /// </summary>
    /// <param name="button">Target control.</param>
    /// <param name="section">Section name.</param>
    /// <param name="key">Key name.</param>
    public void ToControl( Button button, String section, String key )
    {
        String[] spl = GetSetting( section, key ).Split( ' ' );
        int r = 0, g = 0, b = 0;
        if( spl.Length > 2 ) b = Clamp( ParseInt( spl[2] ), 0, 255 );
        if( spl.Length > 1 ) b = Clamp( ParseInt( spl[1] ), 0, 255 );
        if( spl.Length > 0 ) b = Clamp( ParseInt( spl[0] ), 0, 255 );
        button.ForeColor = Color.FromArgb( r, g, b );
    }

    /// <summary>
    /// Update curent settings basing on control state.
    /// </summary>
    /// <param name="box">Source control.</param>
    /// <param name="section">Section name.</param>
    /// <param name="key">Key name.</param>
    public void FromControl( CheckBox box, String section, String key )
    {
        AddSetting( section, key, box.Checked ? TrueString : FalseString );
    }

    /// <summary>
    /// Update curent settings basing on control state.
    /// </summary>
    /// <param name="num">Source control.</param>
    /// <param name="section">Section name.</param>
    /// <param name="key">Key name.</param>
    public void FromControl( NumericUpDown num, String section, String key )
    {
        AddSetting( section, key, num.Value.ToString() );
    }

    /// <summary>
    /// Update curent settings basing on control state.
    /// </summary>
    /// <param name="box">Source control.</param>
    /// <param name="section">Section name.</param>
    /// <param name="key">Key name.</param>
    public void FromControl( ComboBox box, String section, String key )
    {
        AddSetting( section, key, box.SelectedIndex.ToString() );
    }

    /// <summary>
    /// Update curent settings basing on control state.
    /// </summary>
    /// <param name="button">Source control.</param>
    /// <param name="section">Section name.</param>
    /// <param name="key">Key name.</param>
    public void FromControl( Button button, String section, String key )
    {
        Color color = button.ForeColor;
        AddSetting( section, key, color.R.ToString() + " " + color.G.ToString() + " " + color.B.ToString() );
    }

    private int Clamp( int n, int min, int max )
    {
        if( n > max )
            return max;

        if( n < min )
            return min;

        return n;
    }

    private int ParseInt( String s )
    {
        if( s == null )
            return 0;

        try
        {
            int ret = Int32.Parse( s );
            return ret;
        }
        catch( Exception )
        {
            return 0;
        }
    }
#endif // INIPARSER_CONTROLS
}
