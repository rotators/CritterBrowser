﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using DATLib;
using FOCommon.Graphic;

namespace CritterBrowser.Forms
{
    public partial class frmMain : Form
    {
        enum LoadModeType
        {
            None,
            Directory,
            Zip,
            Dat
        }

        struct frmCheckerConfig
        {
            public readonly LoadModeType LoadMode;
            public readonly string Target;
            public readonly bool FastCheck;

            public string[] TargetCrittersLst;
            public IniParser TargetConfig;
            public string ShowCompletion;

            public frmCheckerConfig( LoadModeType loadMode, string target )
            {
                LoadMode = loadMode;
                Target = target;
                FastCheck = Settings.GetBool( "FastCheckFRM" );

                TargetCrittersLst = null;
                TargetConfig = null;
                ShowCompletion = null;
            }
        }

        enum ProgressData : int
        {
            CritterTypeName = -1,
            CritterAnimation = -2,
            CritterPreview = -3,
            ErrorMessage = Int32.MaxValue,
            Finish = Int32.MinValue
        }

        readonly string BaseText;

        // Generated members names prefix
        internal readonly string AnimCheck = "animCheck";
        internal readonly string AnimFlow = "animFlow";
        internal readonly string AnimGroup = "animGroup";
        internal readonly string AnimLink = "animLink";
        internal readonly string AnimPanel = "animPanel";

        // other
        readonly string ArtCritters = "ART" + Path.DirectorySeparatorChar + "CRITTERS" + Path.DirectorySeparatorChar;
        readonly string ArtCrittersZip;

        internal readonly List<string> ValidAnimations = new List<string>();
        internal readonly List<string> ValidAnimationsGroups = new List<string>();
        readonly int ValidAnimationsNumber;

        List<frmAnimation> AnimationWindows = new List<frmAnimation>();

        List<CritterType> CritterTypes = new List<CritterType>();
        CritterType CurrentCritterType = null;
        int PrevSelectedCritterIndex = -1;

        LoadModeType LoadedMode = LoadModeType.None;
        string LoadedTarget;
        bool LoadedFast;
        IniParser LoadedConfig;

        Color TransparencyFRM = Color.FromArgb( 11, 0, 11 );

        bool frmCheckerCompleted = true;
        bool ClosePending = false;

        public frmMain()
        {
            InitializeComponent();

            BaseText = Text;

            ArtCrittersZip = ArtCritters.Replace( '\\', '/' ).ToLower();

            menuAboutSelf.Text = new frmAbout().AssemblyProduct;

            InitAnimations();
            AutoPlacement();
            ValidAnimationsGroups.Sort();
            ValidAnimations.Sort();
            ValidAnimationsNumber = ValidAnimations.Count * 6;
            if( ValidAnimations.Contains( "NA" ) )
                ValidAnimationsNumber -= 5;

            EnableControls( false );

            statusLabel.Text = "";
            tblGeneral.AutoSize = true;

            switch( StartPosition )
            {
                case FormStartPosition.CenterParent:
                    CenterToParent();
                    break;
                case FormStartPosition.CenterScreen:
                    CenterToScreen();
                    break;
            }
        }

        /// <summary>
        /// Preprare form to be closed even if any BackgroundWorker is running.
        /// </summary>
        /// <remarks>http://stackoverflow.com/a/1732361</remarks>
        protected override void OnFormClosing( FormClosingEventArgs e )
        {
            if( !frmCheckerCompleted )
            {
                ClosePending = true;
                frmChecker.CancelAsync();
                Enabled = false;
                e.Cancel = true;
                return;
            }

            base.OnFormClosing( e );
        }

        void EnableControls( bool enable )
        {
            lstCritters.Enabled =
            tblGeneral.Enabled =
            grpFalloutConfiguration.Enabled =
            falloutCrittersLst.Enabled =
            grpFonlineConfiguration.Enabled =
            fonlineCritterTypesCfg.Enabled =
                enable;

            if( !enable )
                ResetAnimations();
        }

        /// <summary>
        /// Reset all animation-related controls to initial state.
        /// </summary>
        void ResetAnimations()
        {
            foreach( string animName in ValidAnimations )
            {
                CheckBox check = (CheckBox)GetControl( AnimCheck + animName );
                check.CheckState = CheckState.Unchecked;

                LinkLabel link = (LinkLabel)GetControl( AnimLink + animName );
                link.Enabled = false;
            }

            animPreview.Hide();
            animPreview.Update();
            animPreview.Image = null;
            animPreview.Size = new Size( 0, 0 );
        }

        /// <summary>
        /// Initialize animations config
        /// </summary>
        void InitAnimations()
        {
            AddAnimationGroup( "A", "unarmed" );
            AddAnimation( "AA", "idle" );
            AddAnimation( "AB", "walk" );
            AddAnimation( "AE", "climb" );
            AddAnimation( "AK", "pick up" );
            AddAnimation( "AL", "use" );
            AddAnimation( "AN", "dodge" );
            AddAnimation( "AO", "hit front" );
            AddAnimation( "AP", "hit back" );
            AddAnimation( "AQ", "punch" );
            AddAnimation( "AR", "kick" );
            AddAnimation( "AS", "throw" );
            AddAnimation( "AT", "run" );

            AddAnimationGroup( "C", "stand up" );
            AddAnimation( "CH", "front" );
            AddAnimation( "CJ", "back" );

            AddAnimationGroup( "B", "death" );
            AddAnimation( "BA", "fall back" );
            AddAnimation( "BB", "fall front" );
            AddAnimation( "BD", "hole" );
            AddAnimation( "BE", "fall back burn" );
            AddAnimation( "BF", "ripped" );
            AddAnimation( "BG", "perforated" );
            AddAnimation( "BH", "fall back electric" );
            AddAnimation( "BI", "cut" );
            AddAnimation( "BJ", "fall front burn" );
            AddAnimation( "BK", "ashes" );
            AddAnimation( "BL", "explosion" );
            AddAnimation( "BM", "meltdown" );
            AddAnimation( "BN", "burning dance" );
            AddAnimation( "BO", "bleed back" );
            AddAnimation( "BP", "bleed front" );

            AddAnimationGroup( "D", "knife" );
            AddAnimation( "DA", "idle" );
            AddAnimation( "DB", "walk" );
            AddAnimation( "DC", "pull out" );
            AddAnimation( "DD", "put in" );
            AddAnimation( "DE", "dodge" );
            AddAnimation( "DF", "thrust" );
            AddAnimation( "DG", "swing" );
            AddAnimation( "DM", "throw" );

            AddAnimationGroup( "E", "club" );
            AddAnimation( "EA", "idle" );
            AddAnimation( "EB", "walk" );
            AddAnimation( "EC", "pull out" );
            AddAnimation( "ED", "put in" );
            AddAnimation( "EE", "dodge" );
            AddAnimation( "EF", "thrust" );
            AddAnimation( "EG", "swing" );

            AddAnimationGroup( "F", "hammer" );
            AddAnimation( "FA", "idle" );
            AddAnimation( "FB", "walk" );
            AddAnimation( "FC", "pull out" );
            AddAnimation( "FD", "put in" );
            AddAnimation( "FE", "dodge" );
            AddAnimation( "FF", "thrust" );
            AddAnimation( "FG", "swing" );

            AddAnimationGroup( "G", "spear" );
            AddAnimation( "GA", "idle" );
            AddAnimation( "GB", "walk" );
            AddAnimation( "GC", "pull out" );
            AddAnimation( "GD", "put int" );
            AddAnimation( "GE", "dodge" );
            AddAnimation( "GF", "thrust" );
            AddAnimation( "GG", "swing" );
            AddAnimation( "GM", "throw" );

            AddAnimationGroup( "H", "pistol" );
            AddAnimation( "HA", "idle/reload" );
            AddAnimation( "HB", "walk" );
            AddAnimation( "HC", "pull out" );
            AddAnimation( "HD", "put in" );
            AddAnimation( "HE", "dodge" );
            AddAnimation( "HH", "aim" );
            AddAnimation( "HI", "pull down" );
            AddAnimation( "HJ", "shot" );

            AddAnimationGroup( "I", "smg" );
            AddAnimation( "IA", "idle/reload" );
            AddAnimation( "IB", "walk" );
            AddAnimation( "IC", "pull out" );
            AddAnimation( "ID", "put in" );
            AddAnimation( "IE", "dodge" );
            AddAnimation( "IH", "aim" );
            AddAnimation( "II", "pull down" );
            AddAnimation( "IJ", "shot" );
            AddAnimation( "IK", "burst" );

            AddAnimationGroup( "J", "rifle" );
            AddAnimation( "JA", "idle/reload" );
            AddAnimation( "JB", "walk" );
            AddAnimation( "JC", "pull out" );
            AddAnimation( "JD", "put in" );
            AddAnimation( "JE", "dodge" );
            AddAnimation( "JH", "aim" );
            AddAnimation( "JI", "pull down" );
            AddAnimation( "JJ", "shot" );
            AddAnimation( "JK", "burst" );

            AddAnimationGroup( "M", "rocket launcher" );
            AddAnimation( "MA", "idle/reload" );
            AddAnimation( "MB", "walk" );
            AddAnimation( "MC", "pull out" );
            AddAnimation( "MD", "put in" );
            AddAnimation( "ME", "dodge" );
            AddAnimation( "MH", "aim" );
            AddAnimation( "MI", "pull down" );
            AddAnimation( "MJ", "shot" );

            AddAnimationGroup( "L", "minigun" );
            AddAnimation( "LA", "idle/reload" );
            AddAnimation( "LB", "walk" );
            AddAnimation( "LC", "pull out" );
            AddAnimation( "LD", "put in" );
            AddAnimation( "LE", "dodge" );
            AddAnimation( "LH", "aim" );
            AddAnimation( "LI", "pull down" );
            AddAnimation( "LK", "burst" );

            AddAnimationGroup( "K", "heavy" );
            AddAnimation( "KA", "idle/reload" );
            AddAnimation( "KB", "walk" );
            AddAnimation( "KC", "pull out" );
            AddAnimation( "KD", "put in" );
            AddAnimation( "KE", "dodge" );
            AddAnimation( "KH", "aim" );
            AddAnimation( "KI", "pull down" );
            AddAnimation( "KJ", "shot" );
            AddAnimation( "KK", "burst" );
            AddAnimation( "KL", "flamer" );

            AddAnimationGroup( "R", "body" );
            AddAnimation( "RA", "back" );
            AddAnimation( "RB", "front" );
            AddAnimation( "RD", "hole" );
            AddAnimation( "RE", "burned" );
            AddAnimation( "RF", "perforated front" );
            AddAnimation( "RG", "perforated back" );
            AddAnimation( "RH", "electric" );
            AddAnimation( "RJ", "cut" );
            AddAnimation( "RK", "ashes" );
            AddAnimation( "RL", "explosion" );
            AddAnimation( "RM", "meltdown" );
            AddAnimation( "RO", "bleed back" );
            AddAnimation( "RP", "bleed front" );

            AddAnimationGroup( "N", "target" );
            AddAnimation( "NA", "target" );
        }

        //
        // Animations panel structure; use string Anim* when searching for specific control.
        //
        // X  - animation group identifier
        // XY - animation type identifier
        //
        // Panel ("animations")
        //   GroupBox (AnimGroup+X)
        //     FlowLayoutPanel (AnimFlow+X)
        //       Panel (AnimPanel+XY)
        //         CheckBox (AnimCheck+XY)
        //         LinkLabel (AnimLink+XY)
        //

        /// <summary>
        /// Adds animation group to supported list.
        /// </summary>
        /// <param name="animGroup">(required length: 1)</param>
        /// <param name="description">Short information about animation group.</param>
        void AddAnimationGroup( string animGroup, string description = "" )
        {
            if( !animGroup.IsAlpha() )
                return;

            animGroup = animGroup.ToUpper();

            if( animGroup.Length != 1 )
                return;
            else if( ValidAnimationsGroups.Contains( animGroup ) )
                return;

            ValidAnimationsGroups.Add( animGroup );

            GroupBox group = new GroupBox();
            group.Name = AnimGroup + animGroup;

            group.Text = animGroup + "*";
            if( description.Length > 0 )
                group.Text += " (" + description + ")";

            group.Margin = new Padding( 15 );
            group.Padding = new Padding( 3 );
            group.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            group.AutoSize = true;

            FlowLayoutPanel fpanel = new FlowLayoutPanel();
            fpanel.Name = AnimFlow + animGroup;

            fpanel.FlowDirection = FlowDirection.TopDown;
            fpanel.Padding = new Padding( 0 );
            fpanel.Margin = new Padding( 0 );
            fpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fpanel.AutoSize = true;
            fpanel.Dock = DockStyle.Fill;

            group.Controls.Add( fpanel );
            animations.Controls.Add( group );
        }

        /// <summary>
        /// Adds animation with specified name to supported list.
        /// </summary>
        /// <param name="animName">Animation identifier (required length: 2).</param>
        /// <param name="description">Short information about animation type</param>
        void AddAnimation( string animName, string description = "" )
        {
            if( !animName.IsAlpha() )
                return;

            animName = animName.ToUpper();

            if( animName.Length != 2 )
                return;

            string animGroup = animName.Substring( 0, 1 );

            if( !ValidAnimationsGroups.Contains( animGroup ) )
                return;

            if( ValidAnimations.Contains( animName ) )
                return;

            ValidAnimations.Add( animName );

            FlowLayoutPanel fpanel = (FlowLayoutPanel)GetControl( AnimFlow + animGroup );

            Panel panel = new Panel();
            panel.Name = AnimPanel + animName;
            panel.Margin = panel.Padding = new Padding( 0 );
            panel.AutoSize = true;

            CheckBox check = new CheckBox();
            check.Name = AnimCheck + animName;

            check.Font = new Font( check.Font.FontFamily, 7, FontStyle.Underline );
            check.Margin = new Padding( 0 );
            check.Padding = new Padding( 3 );
            check.AutoSize = true;

            LinkLabel link = new LinkLabel();
            link.Name = AnimLink + animName;

            link.Text = animName;
            if( description.Length > 0 )
                link.Text += " (" + description.ToLower() + ")";

            link.AutoSize = true;
            link.Tag = animName;
            link.LinkClicked += new LinkLabelLinkClickedEventHandler( animLink_LinkClicked );

            check.Enabled = link.Enabled = false;

            panel.Controls.Add( check );
            link.Location = check.Location;
            link.Top += 3;
            link.Left = check.Right - 3;
            panel.Controls.Add( link );
            fpanel.Controls.Add( panel );

            fpanel.Invalidate( true );
        }

        /// <summary>
        /// Updates positions of animation-related controls.
        /// </summary>
        public void AutoPlacement()
        {
            List<GroupBox> groups = new List<GroupBox>();
            foreach( string animGroup in ValidAnimationsGroups )
            {
                groups.Add( (GroupBox)GetControl( AnimGroup + animGroup ) );
            }

            const int bonusX = 115;
            int maxH = -1;
            foreach( GroupBox group in groups )
            {
                if( group.Height > maxH )
                    maxH = group.Height;
            }
            maxH += bonusX;
            lstCritters.Height = maxH;

            int x = 0, w = 0, h = 0;
            List<GroupBox> column = new List<GroupBox>();

            var fixColumnWidth = new Action( () =>
            {
                foreach( GroupBox group in column )
                {
                    if( !group.AutoSize )
                        continue;

                    Size oldSize = group.Size;
                    group.AutoSize = false;
                    group.Size = oldSize;
                    group.Width = w;
                }
            } );

            foreach( GroupBox group in groups )
            {
                int currH = group.Height;
                if( currH + h < maxH )
                {
                    group.Top = h;
                    h += currH;
                    group.Left = x;
                    if( group.Width > w )
                        w = group.Width;

                    column.Add( group );
                }
                else
                {
                    fixColumnWidth();
                    column.Clear();
                    column.Add( group );

                    x += w;
                    group.Left = x;
                    w = group.Width;
                    h = group.Height;
                }
            }

            fixColumnWidth();
        }

        /// <summary>
        /// Deep search for Control with given name
        /// </summary>
        /// <param name="name">Name of Control (unique)</param>
        /// <returns>Control object</returns>
        /// <exception cref="NotSupportedException">Thrown when control has not been found, or there is more than one with given name</exception>
        /// <example>Label lbl = (Label)GetControl( "labelName" );</example>
        internal Control GetControl( string name )
        {
            Control[] controls = Controls.Find( name, true );
            if( controls != null && controls.Length == 1 )
                return (controls[0]);

            throw new NotSupportedException(); // :)
        }

        bool OpenDatafile( ref object datafile, string targetName, LoadModeType loadMode )
        {
            switch( loadMode )
            {
                case LoadModeType.Directory:
                    if( !Directory.Exists( targetName ) )
                        return (false);
                    datafile = targetName;
                    break;

                case LoadModeType.Zip:
                    ZipStorer zip = ZipStorer.Open( targetName, FileAccess.Read );
                    if( zip == null )
                        return (false);
                    datafile = zip;
                    break;

                case LoadModeType.Dat:
                    DatReaderError status;
                    DAT dat = DATReader.ReadDat( targetName, out status );
                    if( status.Error != DatError.Success )
                        return (false);
                    datafile = dat;
                    break;
            }

            return (true);
        }

        void CloseDatafile( ref object datafile, LoadModeType loadMode )
        {
            switch( loadMode )
            {
                case LoadModeType.Zip:
                    ZipStorer zip = (ZipStorer)datafile;
                    zip.Close();
                    break;
                case LoadModeType.Dat:
                    DAT dat = (DAT)datafile;
                    dat.Close();
                    break;
            }
        }

        bool OpenCurrentDatafile( ref object datafile )
        {
            return (OpenDatafile( ref datafile, LoadedTarget, LoadedMode ));
        }

        void CloseCurrentDatafile( ref object datafile )
        {
            CloseDatafile( ref datafile, LoadedMode );
        }

        /// <summary>
        /// Refreshs text for falloutCrittersLst and fonlineCritterTypesCfg
        /// </summary>
        private void RefreshFalloutFOnline( CritterType crType = null, bool controls = false )
        {
            if( crType == null )
            {
                if( CurrentCritterType != null )
                    crType = CurrentCritterType;
                else
                    return;
            }

            if( controls )
            {
                falloutAlias.Value = crType.Alias;

                fonlineEnabled.CheckState = (crType.Enabled ? CheckState.Checked : CheckState.Unchecked);
                fonlineID.Value = crType.ID;
                fonlineAlias.Value = crType.Alias;
                fonlineMultihex.Value = crType.Multihex;
                fonlineAim.CheckState = (crType.Aim ? CheckState.Checked : CheckState.Unchecked);
                fonlineAim.CheckState = (crType.Armor ? CheckState.Checked : CheckState.Unchecked);
                fonlineRotate.CheckState = (crType.Rotate ? CheckState.Checked : CheckState.Unchecked);
                fonlineWalk.Value = crType.Walk;
                fonlineRun.Value = crType.Run;
                fonlineSteps1.Value = crType.Step1;
                fonlineSteps2.Value = crType.Step2;
                fonlineSteps3.Value = crType.Step3;
                fonlineSteps4.Value = crType.Step4;
                fonlineSound.Text = crType.Sound;
                fonlineComment.Text = crType.Comment;
            }

            falloutCrittersLst.Text = crType.ToFalloutString();

            fonlineCritterTypesCfg.Text = crType.ToFOnlineString( fonlineCritterTypesCfg.Multiline );
        }

        /// <summary>
        /// Prepares form to open a new target.
        /// </summary>
        /// <param name="loadMode">Target type.</param>
        /// <param name="target">Target name.</param>
        /// <returns>Configuration for frmChecker</returns>
        private frmCheckerConfig frmCheckerPrepare( LoadModeType loadMode, string target )
        {
            frmCheckerConfig config = new frmCheckerConfig( loadMode, target );
            foreach( frmAnimation animWin in AnimationWindows )
            {
                animWin.Close();
            }
            AnimationWindows.Clear();
            CritterTypes.Clear();

            menuFileOpen.Enabled =
            menuFileImport.Enabled =
            menuFileExport.Enabled =
            menuOptionsTarget.Enabled =
                false;
            lstCritters.SelectedIndex = PrevSelectedCritterIndex = -1;
            lstCritters.Items.Clear();
            EnableControls( false );
            RefreshFalloutFOnline( new CritterType( "" ), true );
            statusLabel.Text = "Opening " + target + "...";

            frmCheckerCompleted = false;

            return (config);
        }

        /// <summary>
        /// File->Open->Datafile event.
        /// </summary>
        private void menuFileOpenDatafile_Click( object sender, EventArgs e )
        {
            DialogResult result = dlgDatafile.ShowDialog( this );

            if( result != DialogResult.OK )
                return;

            string ext = Path.GetExtension( dlgDatafile.SafeFileName ).Substring( 1 ).ToUpper();

            LoadModeType loadMode = LoadModeType.None;
            if( ext == "ZIP" )
                loadMode = LoadModeType.Zip;
            else if( ext == "DAT" )
                loadMode = LoadModeType.Dat;
            else
                return;

            frmCheckerConfig config = frmCheckerPrepare( loadMode, dlgDatafile.FileName );
            frmChecker.RunWorkerAsync( config );
        }

        /// <summary>
        /// File->Open->Directory event.
        /// </summary>
        private void menuFileOpenDirectory_Click( object sender, EventArgs e )
        {
            DialogResult result = dlgDirectory.ShowDialog( this );

            if( result != DialogResult.OK )
                return;

            frmCheckerConfig config = frmCheckerPrepare( LoadModeType.Directory, dlgDirectory.SelectedPath );
            frmChecker.RunWorkerAsync( config );
        }

        /// <summary>
        /// File->Import settings event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileImport_Click( object sender, EventArgs e )
        {
            if( LoadedMode == LoadModeType.None )
                return;

            dlgImport.FileName = "";

            DialogResult result = dlgImport.ShowDialog( this );
            if( result != DialogResult.OK )
                return;

            if( dlgImport.SafeFileName.ToUpper() == "CRITTERS.LST" )
            {
                StreamReader text = new StreamReader( dlgImport.FileName );

                string line = null;

                while( (line = text.ReadLine()) != null )
                {
                    string[] vals = line.Split( new char[] { ',' }, 3 );
                    if( vals.Length < 2 )
                        continue;

                    CritterType crType = CritterTypes.Find( cr => cr.Name == vals[1].ToUpper() );
                    if( crType == null )
                        continue;

                    UInt16 alias = 0;
                    if( UInt16.TryParse( vals[1], out alias ) )
                    {
                        crType.Alias = alias;
                    }
                }
            }
            else if( dlgImport.SafeFileName.ToUpper() == "CRITTERTYPES.CFG" )
            {
            }
            else
                return;

            lstCritters.Select();
        }

        /// <summary>
        /// File->Export critter event.
        /// </summary>
        private void menuFileExport_Click( object sender, EventArgs e )
        {
            if( CurrentCritterType == null )
                return;

            DialogResult result = DialogResult.Cancel;

            frmExport export = new frmExport( CurrentCritterType.Name );

            result = export.ShowDialog( this );
            if( result != DialogResult.OK )
                return;

            dlgExport.FileName = CurrentCritterType.Name + ".zip";
            result = dlgExport.ShowDialog( this );
            if( result != DialogResult.OK )
                return;

            ZipStorer zip = ZipStorer.Create( dlgExport.FileName, "" );

            IniParser cfg = new IniParser();

            CurrentCritterType.Save( cfg, CurrentCritterType.Name );
            zip.AddText( ZipStorer.Compression.Deflate, cfg.AsText, CritterBrowser.ConfigName, DateTime.Now, "" );

            if( export.checkCompletion.Checked )
            {
                frmCompletion completion = new frmCompletion( this );
                zip.AddText( ZipStorer.Compression.Deflate, completion.CompletionText( CurrentCritterType, true ), CurrentCritterType.Name + ".txt", DateTime.Now, "" );
            }

            AddCritterType( zip, CurrentCritterType );

            zip.Close();
        }

        /// <summary>
        /// File->Close event.
        /// </summary>
        private void menuFileExit_Click( object sender, EventArgs e )
        {
            Close();
        }

        /// <summary>
        /// Options->General event.
        /// </summary>
        private void menuOptionsGeneral_Click( object sender, EventArgs e )
        {
            frmOptionsGeneral optionsGeneral = new frmOptionsGeneral();

            DialogResult result = optionsGeneral.ShowDialog( this );

            if( result != DialogResult.OK )
                return;

            Settings.Config.SaveSettings();
        }

        private void menuAboutSelf_Click( object sender, EventArgs e )
        {
            frmAbout about = new frmAbout();
            about.ShowDialog( this );
        }

        private void menuAboutRepo_Click( object sender, EventArgs e )
        {
            Process.Start( "https://github.com/rotators/CritterBrowser/" );
        }

        void AddCritterType( ZipStorer zip, string crTypeName )
        {
            CritterType crType = CritterTypes.Find( cr => crTypeName.ToUpper() == cr.Name );
            if( crType == null )
            {
                return;
            }

            AddCritterType( zip, crType );
        }

        void AddCritterType( ZipStorer zip, CritterType crType )
        {
            object datafile = null;
            if( !OpenCurrentDatafile( ref datafile ) )
                return;

            List<CritterAnimationPacked> zipFiles = new List<CritterAnimationPacked>();

            foreach( CritterAnimation crAnim in crType.Animations )
            {
                List<string> nameList = new List<string>();
                List<byte[]> bytesList = new List<byte[]>();
                List<DateTime> dateList = new List<DateTime>();

                string crName = crType.Name + crAnim.Name;

                for( int d = 0; d <= 5; d++ )
                {
                    if( crAnim.Dir[d] == CritterAnimationDir.None )
                        continue;

                    string ext = ".FR" + (crAnim.Full ? "M" : d.ToString());

                    switch( LoadedMode )
                    {
                        case LoadModeType.Directory:
                            string filename = dlgDirectory.SelectedPath + Path.DirectorySeparatorChar + crName + ext;
                            if( File.Exists( filename ) )
                            {
                                zipFiles.Add( new CritterAnimationPacked(
                                    ArtCrittersZip + crName + ext,
                                    File.ReadAllBytes( filename ),
                                    File.GetLastWriteTime( filename )
                                 ) );
                            }
                            break;

                        case LoadModeType.Zip:
                            ZipStorer zipDatafile = (ZipStorer)datafile;
                            MemoryStream stream = new MemoryStream();
                            zipDatafile.ExtractFile( crAnim.ZipData[d], stream );
                            zipFiles.Add( new CritterAnimationPacked(
                                ArtCrittersZip + crName + ext,
                                stream.ToArray(),
                                crAnim.ZipData[d].ModifyTime ) );
                            break;

                        case LoadModeType.Dat:
                            DAT dat = (DAT)datafile;
                            zipFiles.Add( new CritterAnimationPacked(
                                ArtCrittersZip + crName + ext,
                                dat.FileList[crAnim.DatData[d]].GetData(),
                                DateTime.Now ) );
                            break;
                    }

                    if( crAnim.Full )
                        break;
                }
            }

            CloseCurrentDatafile( ref datafile );

            foreach( CritterAnimationPacked crAnimPacked in zipFiles )
            {
                MemoryStream stream = new MemoryStream( crAnimPacked.Bytes, false );
                zip.AddStream( ZipStorer.Compression.Deflate, crAnimPacked.Filename, stream, crAnimPacked.Date, "" );
            }
        }

        private void lstCritters_SelectedValueChanged( object sender, EventArgs e )
        {
            ListBox self = (ListBox)sender;

            if( self.SelectedItem == null )
                return;

            if( self.SelectedIndex == PrevSelectedCritterIndex )
                return;
            else
                PrevSelectedCritterIndex = self.SelectedIndex;

            //ResetAnimations();

            string baseName = (string)self.SelectedItem;
            CurrentCritterType = CritterTypes.Find( cr => cr.Name == baseName );

            foreach( string crAnimName in ValidAnimations )
            {
                CheckState state = CheckState.Unchecked;
                CritterAnimation crAnim = CurrentCritterType[crAnimName];
                if( crAnim != null )
                {
                    if( crAnim.AllDirs )
                        state = CheckState.Checked;
                    else
                        state = CheckState.Indeterminate;
                }

                CheckBox check = (CheckBox)GetControl( AnimCheck + crAnimName );
                check.CheckState = state;

                LinkLabel link = (LinkLabel)GetControl( AnimLink + crAnimName );
                link.Enabled = state != CheckState.Unchecked;
            }

            RefreshFalloutFOnline( CurrentCritterType, true );

            if( CurrentCritterType.Preview != null )
            {
                animPreview.Size = new Size( CurrentCritterType.Preview.Width, CurrentCritterType.Preview.Height );
                animPreview.Image = CurrentCritterType.Preview;
                animPreview.Show();
                animPreview.Update();
            }
            else
            {
                animPreview.Hide();
                animPreview.Update();
                animPreview.Size = Size.Empty;
                animPreview.Image = null;
            }
        }

        void animLink_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            if( LoadedMode == LoadModeType.None )
                return;

            LinkLabel self = (LinkLabel)sender;
            string animName = (string)self.Tag;

            foreach( frmAnimation prevAnimWin in AnimationWindows )
            {
                if( prevAnimWin.Text == CurrentCritterType.Name + animName )
                {
                    prevAnimWin.BringToFront();
                    return;
                }
            }

            object datafile = null;
            if( !OpenCurrentDatafile( ref datafile ) )
            {
                MessageBox.Show( "Error opening " + LoadedTarget, BaseText, MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            frmAnimation animWin = new frmAnimation();
            animWin.Text = CurrentCritterType.Name + animName;

            Bitmap[] frms = LoadFRM( datafile, CurrentCritterType, animName, LoadedMode );
            CloseCurrentDatafile( ref datafile );

            animWin.anim0.Image = frms[0];
            if( animName == "NA" )
            {
                for( int c = 1; c <= 5; c++ )
                {
                    animWin.tblAnimation.ColumnStyles[c].SizeType = SizeType.Absolute;
                    animWin.tblAnimation.ColumnStyles[c].Width = 0;
                }
            }
            else
            {
                animWin.anim1.Image = frms[1];
                animWin.anim2.Image = frms[2];
                animWin.anim3.Image = frms[3];
                animWin.anim4.Image = frms[4];
                animWin.anim5.Image = frms[5];
            }

            animWin.Show();
            AnimationWindows.Add( animWin );
        }

        private bool ValidNameFRM( string filename, ref string baseName, ref string animName, ref int dir, ref string ext )
        {
            // if file == C:\Fallout\data\art\critters\HFJMPSAB.FRM

            string name = Path.GetFileName( filename ).ToUpper(); // HFJMPSAB.FRM
            string nameNoExt = Path.GetFileNameWithoutExtension( name ); // HFJMPSAB

            if( nameNoExt.Length < 3 )
                return (false);

            string baseName_ = nameNoExt.Substring( 0, nameNoExt.Length - 2 ); // HFJMPS
            string animName_ = nameNoExt.Substring( nameNoExt.Length - 2 ); // AB

            if( !animName_.IsAlpha() )
                return (false);

            if( !ValidAnimationsGroups.Contains( animName_.Substring( 0, 1 ) ) )
                return (false);

            if( !ValidAnimations.Contains( animName_ ) )
                return (false);

            string ext_ = Path.GetExtension( name ).Substring( 1 ); // FRM

            if( ext_.Length != 3 )
                return (false);

            if( ext_.Substring( 0, 2 ) != "FR" )
                return (false);

            int dir_ = -1;
            if( ext_.Substring( 2, 1 ) != "M" && !(int.TryParse( ext_.Substring( 2, 1 ), out dir_ ) && dir_ >= 0 && dir_ <= 5) )
                return (false);

            baseName = baseName_;
            animName = animName_;
            ext = ext_;
            if( dir_ >= 0 )
                dir = dir_;

            return (true);
        }

        Bitmap[] LoadFRM( object datafile, CritterType crType, string animName, LoadModeType loadMode )
        {
            CritterAnimation crAnim = crType[animName];

            object[] files = new object[crAnim.Full ? 1 : 6];

            for( uint d = 0; d <= 5; d++ )
            {
                if( crAnim.Dir[d] == CritterAnimationDir.None )
                    continue;

                switch( loadMode )
                {
                    case LoadModeType.Directory:
                        string directory = (string)datafile;
                        files[d] = crType.Name + animName + ".FR" + (crAnim.Dir[d] == CritterAnimationDir.Full ? "M" : d.ToString());
                        break;

                    case LoadModeType.Zip:
                        files[d] = crAnim.ZipData[d];
                        break;

                    case LoadModeType.Dat:
                        DAT dat = (DAT)datafile;
                        files[d] = dat.FileList[crAnim.DatData[d]];
                        break;
                }

                if( crAnim.Full )
                    break;
            }

            Bitmap[] result = new Bitmap[6];

            if( files.Length == 1 && files[0] != null )
            {
                FalloutFRM frm = LoadFRM( datafile, files[0], loadMode );

                if( frm != null )
                {
                    for( int d = 0; d <= 5; d++ )
                    {
                        result[d] = frm.GetAnimFrameByDir( d, 1 );
                    }
                }
            }
            else
            {
                for( int d = 0; d <= 5; d++ )
                {
                    if( files[d] == null )
                        continue;

                    FalloutFRM frm = LoadFRM( datafile, files[d], loadMode );
                    if( frm == null )
                        continue;

                    foreach( Bitmap bmp in frm.Frames )
                    {
                        if( bmp != null )
                        {
                            result[d] = bmp;
                            break;
                        }
                    }
                }
            }

            return (result);
        }

        FalloutFRM LoadFRM( object datafile, object file, LoadModeType loadMode )
        {
            byte[] bytes = null;
            switch( loadMode )
            {
                case LoadModeType.Directory:
                    string filename = (string)datafile + Path.DirectorySeparatorChar + (string)file;
                    if( File.Exists( filename ) )
                        bytes = File.ReadAllBytes( filename );
                    break;

                case LoadModeType.Zip:
                    MemoryStream stream = new MemoryStream();
                    ZipStorer zip = (ZipStorer)datafile;
                    ZipStorer.ZipFileEntry zipEntry = (ZipStorer.ZipFileEntry)file;
                    zip.ExtractFile( zipEntry, stream );
                    bytes = stream.ToArray();
                    break;

                case LoadModeType.Dat:
                    DATFile datFile = (DATFile)file;
                    try
                    {
                        bytes = datFile.GetData();
                    }
                    catch
                    {
                        // sit and cry
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }

            if( bytes != null && bytes.Length > 0 )
                return (FalloutFRMLoader.LoadFRM( bytes, TransparencyFRM ));

            return (null);
        }

        private void frmChecker_DoWork( object sender, DoWorkEventArgs e )
        {
            BackgroundWorker self = (BackgroundWorker)sender;
            frmCheckerConfig config = (frmCheckerConfig)e.Argument;

            List<object> files = new List<object>();
            object datafile = null;
            if( !OpenDatafile( ref datafile, config.Target, config.LoadMode ) )
            {
                self.ReportProgress( (int)ProgressData.ErrorMessage, "Error opening " + config.Target );
                return;
            }

            string[] CrittersLst = null;
            IniParser TargetCfg = null;

            switch( config.LoadMode )
            {
                case LoadModeType.Directory:
                    string[] tmpfiles = Directory.GetFiles( config.Target, "*.FR?", SearchOption.TopDirectoryOnly );
                    foreach( string file in tmpfiles )
                    {
                        files.Add( Path.GetFileName( file ) );
                    }

                    if( File.Exists( config.Target + Path.DirectorySeparatorChar + "CRITTERS.LST" ) )
                        CrittersLst = File.ReadAllLines( config.Target + Path.DirectorySeparatorChar + "CRITTERS.LST" );

                    string oldCfgName = config.Target + Path.DirectorySeparatorChar + "CritterBrowser.cfg";
                    TargetCfg = new IniParser( oldCfgName );
                    if( !TargetCfg.Loaded )
                        TargetCfg = null;

                    break;

                case LoadModeType.Zip:
                    ZipStorer zip = (ZipStorer)datafile;
                    foreach( ZipStorer.ZipFileEntry entry in zip.ReadCentralDir() )
                    {
                        if( entry.CompressedSize == 0 )
                            continue;

                        string filename = entry.FilenameInZip.ToUpper()
                            .Replace( '\\', Path.DirectorySeparatorChar )
                            .Replace( '/', Path.DirectorySeparatorChar );

                        if( !filename.StartsWith( ArtCritters ) )
                        {
                            if( filename == CritterBrowser.ConfigName.ToUpper() )
                            {
                                MemoryStream stream = new MemoryStream();
                                if( zip.ExtractFile( entry, stream ) )
                                {
                                    stream.Seek( 0, SeekOrigin.Begin );
                                    TargetCfg = new IniParser( stream );
                                    if( !TargetCfg.Loaded )
                                        TargetCfg = null;
                                }
                            }
                            continue;
                        }
                        else if( Path.GetFileName( filename ).ToUpper() == "CRITTERS.LST" )
                        {
                            MemoryStream stream = new MemoryStream();
                            if( zip.ExtractFile( entry, stream ) )
                            {
                                stream.Seek(0,SeekOrigin.Begin);
                                StreamReader streamRead = new StreamReader( stream );
                                CrittersLst = streamRead.ReadToEnd().Replace( "\r", "" ).Split( '\n' );
                            }

                            continue;
                        }


                        string extmp = Path.GetExtension( filename ).ToUpper();
                        if( extmp.Length != 4 || extmp.Substring( 1, 2 ) != "FR" )
                            continue;

                        files.Add( entry );
                    }
                    break;

                case LoadModeType.Dat:
                    DAT dat = (DAT)datafile;
                    int idx = -1;
                    foreach( DATFile entry in dat.FileList )
                    {
                        idx++;

                        string filename = entry.Path.ToUpper()
                            .Replace( '\\', Path.DirectorySeparatorChar )
                            .Replace( '/', Path.DirectorySeparatorChar );

                        if( !filename.StartsWith( ArtCritters ) )
                        {
                            if( filename == CritterBrowser.ConfigName.ToUpper() )
                            {
                                byte[] bytes = entry.GetData();
                                MemoryStream stream = new MemoryStream( bytes );
                                TargetCfg = new IniParser( stream );
                                if( !TargetCfg.Loaded )
                                    TargetCfg = null;
                            }
                            continue;
                        }
                        else if( Path.GetFileName( filename ).ToUpper() == "CRITTERS.LST" )
                        {
                            MemoryStream stream = new MemoryStream( entry.GetData() );
                            if( stream.Length > 0 )
                            {
                                StreamReader streamRead = new StreamReader( stream );
                                CrittersLst = streamRead.ReadToEnd().Replace( "\r", "" ).Split( '\n' );
                            }

                            continue;
                        }

                        string extmp = Path.GetExtension( filename ).ToUpper();
                        if( extmp.Length != 4 || extmp.Substring( 1, 2 ) != "FR" )
                            continue;

                        // DATLib doesn't care about FileIndex, so we have to

                        if( idx >= int.MaxValue )
                        {
                            self.ReportProgress( (int)ProgressData.ErrorMessage, "Too many files" );
                            CloseDatafile( ref datafile, config.LoadMode );
                            return;
                        }

                        entry.FileIndex = idx;
                        files.Add( entry );
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }

            if( files.Count == 0 )
            {
                self.ReportProgress( (int)ProgressData.ErrorMessage, "No critter animations found" );
                CloseDatafile( ref datafile, config.LoadMode );
                return;
            }

            string baseName = null, animName = null, ext = null;
            int dir = -1;

            List<CritterType> crTypesFound = new List<CritterType>();
            int currFile = 0, lastPercent = int.MinValue, filesCount = files.Count;
            foreach( object file in files )
            {
                if( self.CancellationPending )
                {
                    CloseDatafile( ref datafile, config.LoadMode );
                    return;
                }

                int percent = (++currFile * 100) / filesCount;
                if( percent != lastPercent )
                {
                    lastPercent = percent;
                    self.ReportProgress( percent, "Checking " + config.Target );
                }

                string filename = null;

                switch( config.LoadMode )
                {
                    case LoadModeType.Directory:
                        filename = (string)file;
                        break;
                    case LoadModeType.Zip:
                        ZipStorer.ZipFileEntry zipEntry = (ZipStorer.ZipFileEntry)file;
                        filename = zipEntry.FilenameInZip;
                        break;
                    case LoadModeType.Dat:
                        DATFile datEntry = (DATFile)file;
                        filename = datEntry.FileName;
                        break;
                    default:
                        throw new NotSupportedException();
                }

                if( !ValidNameFRM( filename, ref baseName, ref animName, ref dir, ref ext ) )
                    continue;

                CritterType crType = crTypesFound.Find( cr => cr.Name == baseName );
                if( crType == null )
                {
                    crType = new CritterType( baseName );
                    crTypesFound.Add( crType );
                    self.ReportProgress( (int)ProgressData.CritterTypeName, crType.Name );
                }

                if( crType[animName] == null )
                {
                    CritterAnimation crAnim = new CritterAnimation( animName );
                    crType.Animations.Add( crAnim );
                }

                if( ext == "FRM" )
                {
                    if( config.FastCheck )
                    {
                        for( int d = 0; d <= 5; d++ )
                        {
                            CritterAnimation crAnim = crType[animName];
                            crAnim.Dir[d] = CritterAnimationDir.Full;

                            if( config.LoadMode == LoadModeType.Zip )
                                crAnim.ZipData[d] = (ZipStorer.ZipFileEntry)file;
                            else if( config.LoadMode == LoadModeType.Dat )
                            {
                                DATFile datFile = (DATFile)file;
                                crAnim.DatData[d] = (int)datFile.FileIndex;
                            }

                        }
                    }
                    else // !config.FastCheck
                    {
                        FalloutFRM frm = LoadFRM( datafile, file, config.LoadMode );

                        for( int d = 0; d <= 5; d++ )
                        {
                            if( frm != null && frm.GetAnimFrameByDir( d, 1 ) != null )
                            {
                                CritterAnimation crAnim = crType[animName];
                                if( crAnim.Dir[d] != CritterAnimationDir.Partial )
                                    crAnim.Dir[d] = CritterAnimationDir.Full;

                                if( config.LoadMode == LoadModeType.Zip )
                                    crAnim.ZipData[d] = (ZipStorer.ZipFileEntry)file;
                                else if( config.LoadMode == LoadModeType.Dat )
                                    crAnim.DatData[d] = (int)((DATFile)file).FileIndex;
                            }
                        }
                    }
                }
                else // ext != "FRM";
                {
                    CritterAnimation crAnim = crType[animName];

                    if( config.FastCheck )
                    {
                        crAnim.Dir[dir] = CritterAnimationDir.Partial;

                        if( config.LoadMode == LoadModeType.Zip )
                            crAnim.ZipData[dir] = (ZipStorer.ZipFileEntry)file;
                        else if( config.LoadMode == LoadModeType.Dat )
                            crAnim.DatData[dir] = (int)((DATFile)file).FileIndex;
                    }
                    else // !config.FastCheck
                    {
                        FalloutFRM frm = LoadFRM( datafile, file, config.LoadMode );
                        if( frm != null && frm.GetAnimFrameByDir( 0, 1 ) != null )
                        {
                            crAnim.Dir[dir] = CritterAnimationDir.Partial;

                            if( config.LoadMode == LoadModeType.Zip )
                                crAnim.ZipData[dir] = (ZipStorer.ZipFileEntry)file;
                            else if( config.LoadMode == LoadModeType.Dat )
                                crAnim.DatData[dir] = (int)((DATFile)file).FileIndex;
                        }
                    }
                }

                object[] data = { baseName, animName, crType[animName] };
                self.ReportProgress( (int)ProgressData.CritterAnimation, data );
            }

            currFile = 0;
            lastPercent = -1;
            filesCount = crTypesFound.Count;
            foreach( CritterType crType in crTypesFound )
            {
                int percent = (++currFile * 100) / filesCount;
                if( percent != lastPercent )
                {
                    self.ReportProgress( 100 - percent, "Caching critters preview..." );
                }

                foreach( string anim in ValidAnimations )
                {
                    bool found = false;

                    CritterAnimation crAnim = crType[anim];
                    if( crAnim != null )
                    {
                        Bitmap[] frm = LoadFRM( datafile, crType, crAnim.Name, config.LoadMode );
                        if( frm == null )
                            continue;
                        int[] dirs = { 3, 2, 4, 1, 5, 0 };
                        foreach( int d in dirs )
                        {
                            if( frm[d] != null )
                            {
                                object[] data = { crType.Name, frm[d] };
                                self.ReportProgress( (int)ProgressData.CritterPreview, data );
                                found = true;
                                break;
                            }
                        }

                        if( found )
                            break;
                    }
                }
            }

            CloseDatafile( ref datafile, config.LoadMode );

            config.TargetCrittersLst = CrittersLst;
            config.TargetConfig = TargetCfg;
            self.ReportProgress( (int)ProgressData.Finish, config );
        }

        private void frmChecker_ProgressChanged( object sender, ProgressChangedEventArgs e )
        {
            if( e.ProgressPercentage == (int)ProgressData.CritterTypeName )
            {
                string text = (string)e.UserState;
                if( text != null )
                {
                    CritterType crType = CritterTypes.Find( cr => cr.Name == text );
                    if( crType == null )
                    {
                        crType = new CritterType( text );
                        CritterTypes.Add( crType );
                    }

                    if( !lstCritters.Items.Contains( text ) )
                    {
                        lstCritters.Items.Add( text );
                        lstCritters.Update();
                    }
                }
            }
            else if( e.ProgressPercentage == (int)ProgressData.CritterAnimation )
            {
                object[] data = (object[])e.UserState;

                string baseName = (string)data[0];
                string animName = (string)data[1];
                CritterAnimation anim = (CritterAnimation)data[2];

                CritterType crType = CritterTypes.Find( cr => cr.Name == baseName );
                crType.Animations.RemoveAll( a => a.Name == anim.Name );
                crType.Animations.Add( anim );

            }
            else if( e.ProgressPercentage == (int)ProgressData.CritterPreview )
            {
                object[] data = (object[])e.UserState;

                string baseName = (string)data[0];
                Bitmap preview = (Bitmap)data[1];

                CritterType crType = CritterTypes.Find( cr => cr.Name == baseName );
                crType.Preview = preview;
            }
            else if( e.ProgressPercentage == (int)ProgressData.ErrorMessage )
            {
                string text = (string)e.UserState;
                if( text != null && text.Length > 0 )
                {
                    MessageBox.Show( text, BaseText, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
            else if( e.ProgressPercentage == (int)ProgressData.Finish )
            {
                frmCheckerConfig config = (frmCheckerConfig)e.UserState;

                
                Text = BaseText + " : " + config.Target;
                LoadedMode = config.LoadMode;
                string LoadedConfigName = LoadedTarget = config.Target;
                LoadedFast = config.FastCheck;

                LoadedConfigName += LoadedMode == LoadModeType.Directory ? Path.DirectorySeparatorChar : '.';
                LoadedConfigName += CritterBrowser.ConfigName;

                LoadedConfig = new IniParser( LoadedConfigName );

                if( config.TargetConfig != null )
                {
                    foreach( string section in config.TargetConfig.EnumSections() )
                    {
                        foreach( string key in config.TargetConfig.EnumSectionKeys( section ) )
                        {
                            LoadedConfig.AddNewSetting( section, key, config.TargetConfig.GetSetting( section, key ) );
                        }
                    }
                }

                if( config.TargetCrittersLst != null )
                {
                    foreach( string line in config.TargetCrittersLst )
                    {
                        string[] args = line.Split( new char[] { ',' }, 3 );
                        if( args.Length < 2 )
                            continue;

                        string basename = args[0].ToUpper();
                        UInt16 value = 0;
                        if( UInt16.TryParse( args[1], out value ) && value > 0 )
                            LoadedConfig.AddNewSetting( args[0], "Alias", value );
                    }
                }

                foreach( string section in LoadedConfig.EnumSections())
                {
                    CritterType crType = CritterTypes.Find( cr => cr.Name.ToUpper() == section.ToUpper() );
                    if( crType == null )
                        continue;

                    crType.Load( LoadedConfig, section );
                }

                menuFileImport.Enabled =
                menuFileExport.Enabled =
                menuOptionsTarget.Enabled =
                    true;

                if( config.ShowCompletion != null )
                {
                    if( lstCritters.Items.Contains( config.ShowCompletion ) )
                    {
                        lstCritters.SelectedItem = config.ShowCompletion;
                        button1_Click( null, null );
                    }
                    else
                    {
                        MessageBox.Show( "Cannot find '" + config.ShowCompletion + "' critter" );
                        lstCritters.SelectedIndex = 0;
                    }
                }
                else
                    lstCritters.SelectedIndex = 0;

                EnableControls( true );
            }
            else if( e.ProgressPercentage >= 0 )
            {
                if( !statusProgress.Visible )
                    statusProgress.Visible = true;

                statusProgress.Value = e.ProgressPercentage;
                statusProgress.ToolTipText = e.ProgressPercentage + "%";
                statusLabel.Text = "[" + e.ProgressPercentage + "%]";

                string text = (string)e.UserState;
                if( text != null )
                    statusLabel.Text += " " + text;

                status.Update();
            }
            else
            {
                MessageBox.Show( "Unknown progress type : " + e.ProgressPercentage );
            }
        }

        private void frmChecker_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
        {
            frmCheckerCompleted = true;

            menuFileOpen.Enabled = true;

            statusProgress.Visible = false;
            statusLabel.Text = "";

            if( ClosePending )
                Close();
        }

        void SaveTargetConfig()
        {
            CurrentCritterType.Save( LoadedConfig, CurrentCritterType.Name );
            LoadedConfig.SaveSettings();
        }

        private void falloutAlias_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Alias = decimal.ToUInt16( self.Value );
            fonlineAlias.Value = self.Value;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineEnabled_CheckedChanged( object sender, EventArgs e )
        {
            CheckBox self = (CheckBox)sender;

            CurrentCritterType.Enabled = self.Checked;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineID_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.ID = (UInt16)self.Value;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineAlias_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Alias = (UInt16)self.Value;
            falloutAlias.Value = self.Value;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineMultihex_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Multihex = (byte)self.Value;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineAim_CheckedChanged( object sender, EventArgs e )
        {
            CheckBox self = (CheckBox)sender;

            CurrentCritterType.Aim = self.Checked;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineArmor_CheckedChanged( object sender, EventArgs e )
        {
            CheckBox self = (CheckBox)sender;

            CurrentCritterType.Armor = self.Checked;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineRotate_CheckedChanged( object sender, EventArgs e )
        {
            CheckBox self = (CheckBox)sender;

            CurrentCritterType.Rotate = self.Checked;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineWalk_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Walk = (UInt16)self.Value;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineRun_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Run = (UInt16)self.Value;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineSteps1_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Step1 = (UInt16)self.Value;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineSteps2_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Step2 = (UInt16)self.Value;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineSteps3_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Step3 = (UInt16)self.Value;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineSteps4_ValueChanged( object sender, EventArgs e )
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Step4 = (UInt16)self.Value;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineSound_TextChanged( object sender, EventArgs e )
        {
            TextBox self = (TextBox)sender;

            CurrentCritterType.Sound = self.Text;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void fonlineComment_TextChanged( object sender, EventArgs e )
        {
            TextBox self = (TextBox)sender;

            CurrentCritterType.Comment = self.Text;

            RefreshFalloutFOnline();
            SaveTargetConfig();
        }

        private void button1_Click( object sender, EventArgs e )
        {
            if( CurrentCritterType == null )
                return;

            if( LoadedFast )
            {
                DialogResult result = MessageBox.Show(
                    "Current target has been opened with simple animation verification" + Environment.NewLine
                    + Environment.NewLine
                    + "It is advised to disable that option before generating" + Environment.NewLine
                    + "completion report or it may contain invalid informations" + Environment.NewLine
                    + Environment.NewLine
                    + "Do you wish to turn off simple verification and reopen current target?",
                    CritterBrowser.Self,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1 );

                if( result == DialogResult.Yes )
                {
                    Settings.Set( "FastCheckFRM", false );
                    frmCheckerConfig config = frmCheckerPrepare( LoadedMode, LoadedTarget );
                    config.ShowCompletion = CurrentCritterType.Name;
                    frmChecker.RunWorkerAsync( config );

                    return;
                }
            }

            frmCompletion completion = new frmCompletion( this, CurrentCritterType );
            completion.ShowDialog( this );
        }
    }
}
