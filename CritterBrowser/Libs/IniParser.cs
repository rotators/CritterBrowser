#define INIPARSER_CONTROLS

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#if INIPARSER_CONTROLS
using System.Drawing;
using System.Windows.Forms;
#endif

/// <remarks>Based on https://github.com/rotators/fo2238/tree/master/Tools/FO2238Config/ by Atom/Rotators</remarks>
public class IniParser
{
    #region Static members

    public readonly static String RootSection = "ROOT";
    public readonly static Char[] CommentChars = { '#', ';' };
    public readonly static Char[] SectionChars = { '[', ']' };
    public readonly static Char VarSeparator = '=';
    public readonly static String EmptySetting = "0";
    public readonly static String[] FalseStrings = { "false", "disabled", "no", "0" };
    public readonly static String[] TrueStrings = { "true", "enabled", "yes", "1" };

    public static String Comment { get { return (CommentChars[0].ToString()); } }

    public static String SectionOpen { get { return (SectionChars[0].ToString()); } }
    public static String SectionClose { get { return (SectionChars[1].ToString()); } }

    public static String FalseString { get { return (FalseStrings[0]); } }
    public static String TrueString { get { return (TrueStrings[0]); } }

    #endregion // Static members

    private Hashtable keyPairs = new Hashtable();
    private String iniFilePath;

    private struct SectionPair
    {
        public String Section;
        public String Key;
    }

    public readonly bool Loaded = false;

    /// <summary>
    /// Defines file header.
    /// All elements will added at beginning of saved file with default comment character.
    /// Note that all comments are ignored when opening file; list must filled before each
    /// save or it will be lost.
    /// </summary>
    /// <seealso cref="HeaderText"/>
    public List<String> Header = new List<string>();

    /// <summary>
    /// Returns file header as a text.
    /// </summary>
    /// <seealso cref="Header"/>
    public String HeaderText
    {
        get
        {
            string result = String.Empty;

            if( Header.Count > 0 )
            {
                foreach( String line in Header )
                {
                    result += Comment + " " + line + Environment.NewLine;
                }
            }

            return (result);
        }

    }

    /// <summary>
    /// Returns current sections, keys pairs as text.
    /// </summary>
    public String ConfigurationText
    {
        get
        {
            ArrayList rootSection = new ArrayList();
            ArrayList sections = new ArrayList();
            String tmpValue = "";
            String result = "";

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
                        tmpValue = VarSeparator + tmpValue;

                    result += (sectionPair.Key + tmpValue + Environment.NewLine);
                }

                result += Environment.NewLine;
            }

            foreach( String section in sections )
            {
                result += (SectionOpen + section + SectionClose + Environment.NewLine);

                foreach( SectionPair sectionPair in keyPairs.Keys )
                {
                    if( sectionPair.Section == section )
                    {
                        tmpValue = (String)keyPairs[sectionPair];

                        if( tmpValue != null )
                            tmpValue = VarSeparator + tmpValue;

                        result += (sectionPair.Key + tmpValue + Environment.NewLine);
                    }
                }

                result += Environment.NewLine;
            }

            return (result);
        }
    }

    public string AsText
    {
        get
        {
            String result = "";

            String header = HeaderText;
            if( header != String.Empty )
            {
                result += header;
                result += Environment.NewLine;
            }

            result += ConfigurationText;

            return (result);
        }
    }

    public IniParser()
    {
    }

    /// <summary>
    /// Opens the INI file at the given path and enumerates the values in the IniParser.
    /// </summary>
    /// <param name="iniPath">Full path to INI file.</param>
    public IniParser( String iniPath )
    {
        iniFilePath = iniPath;

        if( File.Exists( iniPath ) )
        {
            TextReader iniFile = null;

            try
            {
                iniFile = new StreamReader( iniPath );
                Read( iniFile );

                Loaded = true;
            }
            catch( Exception ex )
            {
                throw ex;
            }
            finally
            {
                if( iniFile != null )
                    iniFile.Close();
            }
        }
    }

    /// <summary>
    /// Reads a Stream and enumerates the values in the IniParser.
    /// </summary>
    /// <param name="stream">Stream to process.</param>
    public IniParser( Stream stream )
    {
        if( stream.CanRead )
        {
            TextReader readStream = null;

            try
            {
                readStream = new StreamReader( stream );
                Read( readStream );
                Loaded = true;
            }
            catch( Exception ex )
            {
                throw ex;
            }
            finally
            {
                if( readStream != null )
                    readStream.Close();
            }
        }
    }

    protected void Read( TextReader text )
    {
        String currentRoot = null;
        String[] keyPair = null;

        String strLine = text.ReadLine();

        while( strLine != null )
        {
            strLine = strLine.Trim().TrimStart( new char[] { '\t' } );

            if( strLine != "" )
            {
                if( strLine.StartsWith( SectionOpen ) && strLine.EndsWith( SectionClose ) )
                {
                    currentRoot = strLine.Substring( 1, strLine.Length - 2 );
                }
                else if( CommentChars.Length > 0 && strLine.IndexOfAny( CommentChars ) == 0 )
                {
                    // comment
                }
                else
                {
                    keyPair = strLine.Split( new char[] { VarSeparator }, 2 );

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

            strLine = text.ReadLine();
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
        if( (ret == null) || ret.Length == 0 || !keyPairs.Contains( sectionPair ) ) return EmptySetting;

        return ret;
    }

    public Byte GetSettingByte( String sectionName, String settingName )
    {
        Byte result = 0;
        String resultStr = GetSetting( sectionName, settingName );
        Byte.TryParse( resultStr, out result );

        return (result);
    }

    public Int32 GetSettingInt32( String sectionName, String settingName )
    {
        Int32 result = 0;
        String resultStr = GetSetting( sectionName, settingName );
        Int32.TryParse( resultStr, out result );

        return (result);
    }

    public UInt16 GetSettingUInt16( String sectionName, String settingName )
    {
        UInt16 result = 0;
        String resultStr = GetSetting( sectionName, settingName );
        UInt16.TryParse( resultStr, out result );

        return (result);
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

    public String[] EnumSections()
    {
        ArrayList sections = new ArrayList();

        foreach( SectionPair sectionPair in keyPairs.Keys )
        {
            if( sectionPair.Section == RootSection )
                continue;
            else if( !sections.Contains( sectionPair.Section ) )
                sections.Add( sectionPair.Section );
        }

        return ((String[])sections.ToArray( typeof( String ) ));
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
                tmpArray.Add( pair.Key + VarSeparator + keyPairs[pair] );
        }

        return (String[])tmpArray.ToArray( typeof( String ) );
    }

    /// <summary>
    /// Enumerates all keys for given section.
    /// </summary>
    /// <param name="sectionName">Section to enum.</param>
    public String[] EnumSectionKeys( String sectionName )
    {
        ArrayList tmpArray = new ArrayList();

        foreach( SectionPair pair in keyPairs.Keys )
        {
            if( pair.Section == sectionName )
                tmpArray.Add( pair.Key );
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
        if( sectionName.Contains( SectionOpen ) || sectionName.Contains( SectionClose ) )
            throw new ArgumentException( "Invalid section name", "sectionName" );

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

    public void AddNewSetting( String sectionName, String settingName, UInt16 settingValue )
    {
        AddNewSetting( sectionName, settingName, settingValue.ToString() );
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
        try
        {
            TextWriter tw = new StreamWriter( newFilePath );
            tw.Write( AsText );
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
        if( iniFilePath == null || iniFilePath == String.Empty )
            throw new InvalidOperationException( "INI file path not set, probably object initialized from Stream" );

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
