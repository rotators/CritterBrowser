using System;
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

            public frmCheckerConfig( LoadModeType loadMode, string target )
            {
                LoadMode = loadMode;
                Target = target;
                FastCheck = Settings.GetBool( "FastCheckFRM" );
            }
        }

        enum ProgressData : int
        {
            CritterTypeName = -1,
            CritterAnimation = -2,
            ZipEntry = -3
        }

        readonly string BaseText;

        // Generated members names prefix
        readonly string AnimCheck = "animCheck";
        readonly string AnimFlow = "animFlow";
        readonly string AnimGroup = "animGroup";
        readonly string AnimLink = "animLink";
        readonly string AnimPanel = "animPanel";

        // other
        readonly string ArtCritters = "ART" + Path.DirectorySeparatorChar + "CRITTERS" + Path.DirectorySeparatorChar;

        List<string> ValidAnimations = new List<string>();
        List<string> ValidAnimationsGroups = new List<string>();
        List<frmAnimation> AnimationWindows = new List<frmAnimation>();

        List<CritterType> CritterTypes = new List<CritterType>();
        CritterType CurrentCritterType = null;
        int PrevSelectedCritterIndex = -1;

        LoadModeType LoadMode = LoadModeType.None;
        string TargetName;

        Color TransparencyFRM = Color.FromArgb( 11, 0, 11 );

        bool frmCheckerCompleted = true;
        bool ClosePending = false;

        public frmMain()
        {
            InitializeComponent();

            BaseText = Text;

            menuAboutSelf.Text = new frmAbout().AssemblyProduct;

            InitAnimations();
            AutoPlacement();
            EnableControls( false );

            statusLabel.Text = "";

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
        protected override void OnFormClosing(FormClosingEventArgs e)
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

        void EnableControls(bool enable)
        {
            lstCritters.Enabled =
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
        }

        /// <summary>
        /// Initialize animations config
        /// </summary>
        void InitAnimations()
        {
            AddAnimationGroup("A", "unarmed");
            AddAnimation("AA", "idle");
            AddAnimation("AB", "walk");
            AddAnimation("AE", "climb");
            AddAnimation("AK", "pick up");
            AddAnimation("AL", "use");
            AddAnimation("AN", "dodge");
            AddAnimation("AO", "hit front");
            AddAnimation("AP", "hit back");
            AddAnimation("AQ", "punch");
            AddAnimation("AR", "kick");
            AddAnimation("AS", "throw");
            AddAnimation("AT", "run");

            AddAnimationGroup("C", "stand up");
            AddAnimation("CH", "front");
            AddAnimation("CJ", "back");

            AddAnimationGroup("B", "death");
            AddAnimation("BA", "fall back");
            AddAnimation("BB", "fall front");
            AddAnimation("BD", "hole");
            AddAnimation("BE", "fall back burn");
            AddAnimation("BF", "ripped");
            AddAnimation("BG", "perforated");
            AddAnimation("BH", "fall back electric");
            AddAnimation("BI", "cut");
            AddAnimation("BJ", "fall front burn");
            AddAnimation("BK", "ashes");
            AddAnimation("BL", "explosion");
            AddAnimation("BM", "meltdown");
            AddAnimation("BN", "burning dance");
            AddAnimation("BO", "bleed back");
            AddAnimation("BP", "bleed front");

            AddAnimationGroup("D", "knife");
            AddAnimation("DA", "idle");
            AddAnimation("DB", "walk");
            AddAnimation("DC", "pull out");
            AddAnimation("DD", "put in");
            AddAnimation("DE", "dodge");
            AddAnimation("DF", "thrust");
            AddAnimation("DG", "swing");
            AddAnimation("DM", "throw");

            AddAnimationGroup("E", "club");
            AddAnimation("EA", "idle");
            AddAnimation("EB", "walk");
            AddAnimation("EC", "pull out");
            AddAnimation("ED", "put in");
            AddAnimation("EE", "dodge");
            AddAnimation("EF", "thrust");
            AddAnimation("EG", "swing");

            AddAnimationGroup("F", "hammer");
            AddAnimation("FA", "idle");
            AddAnimation("FB", "walk");
            AddAnimation("FC", "pull out");
            AddAnimation("FD", "put in");
            AddAnimation("FE", "dodge");
            AddAnimation("FF", "thrust");
            AddAnimation("FG", "swing");

            AddAnimationGroup("G", "spear");
            AddAnimation("GA", "idle");
            AddAnimation("GB", "walk");
            AddAnimation("GC", "pull out");
            AddAnimation("GD", "put int");
            AddAnimation("GE", "dodge");
            AddAnimation("GF", "thrust");
            AddAnimation("GG", "swing");
            AddAnimation("GM", "throw");

            AddAnimationGroup("H", "pistol");
            AddAnimation("HA", "idle/reload");
            AddAnimation("HB", "walk");
            AddAnimation("HC", "pull out");
            AddAnimation("HD", "put in");
            AddAnimation("HE", "dodge");
            AddAnimation("HH", "aim");
            AddAnimation("HI", "pull down");
            AddAnimation("HJ", "shot");

            AddAnimationGroup("I", "smg");
            AddAnimation("IA", "idle/reload");
            AddAnimation("IB", "walk");
            AddAnimation("IC", "pull out");
            AddAnimation("ID", "put in");
            AddAnimation("IE", "dodge");
            AddAnimation("IH", "aim");
            AddAnimation("II", "pull down");
            AddAnimation("IJ", "shot");
            AddAnimation("IK", "burst");

            AddAnimationGroup("J", "rifle");
            AddAnimation("JA", "idle/reload");
            AddAnimation("JB", "walk");
            AddAnimation("JC", "pull out");
            AddAnimation("JD", "put in");
            AddAnimation("JE", "dodge");
            AddAnimation("JH", "aim");
            AddAnimation("JI", "pull down");
            AddAnimation("JJ", "shot");
            AddAnimation("JK", "burst");

            AddAnimationGroup("M", "rocket launcher");
            AddAnimation("MA", "idle/reload");
            AddAnimation("MB", "walk");
            AddAnimation("MC", "pull out");
            AddAnimation("MD", "put in");
            AddAnimation("ME", "dodge");
            AddAnimation("MH", "aim");
            AddAnimation("MI", "pull down");
            AddAnimation("MJ", "shot");

            AddAnimationGroup("L", "minigun");
            AddAnimation("LA", "idle/reload");
            AddAnimation("LB", "walk");
            AddAnimation("LC", "pull out");
            AddAnimation("LD", "put in");
            AddAnimation("LE", "dodge");
            AddAnimation("LH", "aim");
            AddAnimation("LI", "pull down");
            AddAnimation("LK", "burst");

            AddAnimationGroup("K", "heavy");
            AddAnimation("KA", "idle/reload");
            AddAnimation("KB", "walk");
            AddAnimation("KC", "pull out");
            AddAnimation("KD", "put in");
            AddAnimation("KE", "dodge");
            AddAnimation("KH", "aim");
            AddAnimation("KI", "pull down");
            AddAnimation("KJ", "shot");
            AddAnimation("KK", "burst");
            AddAnimation("KL", "flamer");

            AddAnimationGroup("R", "body");
            AddAnimation("RA", "back");
            AddAnimation("RB", "front");
            AddAnimation("RD", "hole");
            AddAnimation("RE", "burned");
            AddAnimation("RF", "perforated front");
            AddAnimation("RG", "perforated back");
            AddAnimation("RH", "electric");
            AddAnimation("RJ", "cut");
            AddAnimation("RK", "ashes");
            AddAnimation("RL", "explosion");
            AddAnimation("RM", "meltdown");
            AddAnimation("RO", "bleed back");
            AddAnimation("RP", "bleed front");

            AddAnimationGroup("N", "target");
            AddAnimation("NA", "target");

            ValidAnimations.Sort();
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
        void AddAnimationGroup(string animGroup, string description = "" )
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
            if (description.Length > 0)
                group.Text += " (" + description + ")";

            group.Margin = new Padding(15);
            group.Padding = new Padding(3);
            group.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            group.AutoSize = true;

            FlowLayoutPanel fpanel = new FlowLayoutPanel();
            fpanel.Name = AnimFlow + animGroup;

            fpanel.FlowDirection = FlowDirection.TopDown;
            fpanel.Padding = new Padding(0);
            fpanel.Margin = new Padding(0);
            fpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fpanel.AutoSize = true;
            fpanel.Dock = DockStyle.Fill;

            group.Controls.Add(fpanel);
            animations.Controls.Add(group);
        }

        /// <summary>
        /// Adds animation with specified name to supported list.
        /// </summary>
        /// <param name="animName">Animation identifier (required length: 2).</param>
        /// <param name="description">Short information about animation type</param>
        void AddAnimation(string animName, string description = "")
        {
            if( !animName.IsAlpha() )
                return;

            animName = animName.ToUpper();

            if (animName.Length != 2)
                return;

            string animGroup = animName.Substring(0, 1);

            if (!ValidAnimationsGroups.Contains(animGroup))
                return;

            if( ValidAnimations.Contains( animName ) )
                return;

            ValidAnimations.Add( animName );

            FlowLayoutPanel fpanel = (FlowLayoutPanel)GetControl(AnimFlow + animGroup);

            Panel panel = new Panel();
            panel.Name = AnimPanel + animName;
            panel.Margin = panel.Padding = new Padding(0);
            panel.AutoSize = true;

            CheckBox check = new CheckBox();
            check.Name = AnimCheck + animName;

            check.Font = new Font(check.Font.FontFamily, 7, FontStyle.Underline);
            check.Margin = new Padding(0);
            check.Padding = new Padding(3);
            check.AutoSize = true;

            LinkLabel link = new LinkLabel();
            link.Name = AnimLink + animName;

            link.Text = animName;
            if (description.Length > 0)
                link.Text += " (" + description.ToLower() + ")";
            link.AutoSize = true;
            link.Tag = animName;
            link.LinkClicked += new LinkLabelLinkClickedEventHandler(animLink_LinkClicked);

            check.Enabled = link.Enabled = false;

            panel.Controls.Add(check);
            link.Location = check.Location;
            link.Top += 3;
            link.Left = check.Right - 3;
            panel.Controls.Add(link);
            fpanel.Controls.Add(panel);

            fpanel.Invalidate(true);
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
            foreach (GroupBox group in groups)
            {
                if (group.Height > maxH)
                    maxH = group.Height;
            }
            maxH += bonusX;
            lstCritters.Height = maxH;

            int x = 0, w = 0, h = 0;
            List<GroupBox> column = new List<GroupBox>();

            var fixColumnWidth = new Action( () =>
                {
                    foreach (GroupBox group in column)
                    {
                        if (!group.AutoSize)
                            continue;

                        Size oldSize = group.Size;
                        group.AutoSize = false;
                        group.Size = oldSize;
                        group.Width = w;
                    }
                }
            );

            foreach (GroupBox group in groups)
            {
                int currH = group.Height;
                if (currH + h < maxH)
                {
                    group.Top = h;
                    h += currH;
                    group.Left = x;
                    if (group.Width > w)
                        w = group.Width;

                    column.Add(group);
                }
                else
                {
                    fixColumnWidth();
                    column.Clear();
                    column.Add(group);

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
        private Control GetControl(string name)
        {
            Control[] controls = Controls.Find(name, true);
            if( controls != null && controls.Length == 1 )
                return (controls[0]);

            throw new NotSupportedException(); // :)
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
                fonlineAim.CheckState = (crType.Armor? CheckState.Checked : CheckState.Unchecked);
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
            LoadMode = loadMode;
            TargetName = target;
            EnableControls( false );
            foreach( frmAnimation animWin in AnimationWindows )
            {
                animWin.Close();
            }
            AnimationWindows.Clear();

            CritterTypes.Clear();

            Text = BaseText + " : " + target;

            menuFileOpen.Enabled =
            menuOptionsTarget.Enabled =
                false;
            lstCritters.SelectedIndex = PrevSelectedCritterIndex = -1;
            lstCritters.Items.Clear();
            RefreshFalloutFOnline( new CritterType( "" ), true );
            statusLabel.Text = "Opening " + target + "...";

            frmChecker.DoWork -= frmChecker_DoWork;
            frmCheckerCompleted = false;

            return (config);
        }

        /// <summary>
        /// File->Open->Datafile event.
        /// </summary>
        private void menuFileOpenDatafile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog(this);

            if( result != DialogResult.OK )
                return;

            string ext = Path.GetExtension( openFile.SafeFileName ).Substring( 1 ).ToUpper();

            LoadModeType loadMode = LoadModeType.None;
            if( ext == "ZIP" )
                loadMode = LoadModeType.Zip;
            else if( ext == "DAT")
                loadMode = LoadModeType.Dat;
            else
                return;

            frmCheckerConfig config = frmCheckerPrepare( loadMode, openFile.FileName );
            frmChecker.DoWork += new DoWorkEventHandler( frmChecker_DoWork );
            frmChecker.RunWorkerAsync( config );
        }

        /// <summary>
        /// File->Open->Directory event.
        /// </summary>
        private void menuFileOpenDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result = openDirectory.ShowDialog(this);

            if (result != DialogResult.OK)
                return;

            frmCheckerConfig config = frmCheckerPrepare( LoadModeType.Directory, openDirectory.SelectedPath );
            frmChecker.DoWork +=new DoWorkEventHandler(frmChecker_DoWork);
            frmChecker.RunWorkerAsync(config);
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

        private void lstCritters_SelectedValueChanged( object sender, EventArgs e )
        {
            ListBox self = (ListBox)sender;

            if (self.SelectedItem == null)
                return;

            if( self.SelectedIndex == PrevSelectedCritterIndex )
                return;
            else
                PrevSelectedCritterIndex = self.SelectedIndex;

            ResetAnimations();

            string baseName = (string)self.SelectedItem;
            CurrentCritterType = CritterTypes.Find( cr => cr.Name == baseName );

            RefreshFalloutFOnline( CurrentCritterType, true );

            foreach( CritterAnimation crAnim in CurrentCritterType.Animations )
            {
                CheckBox check = (CheckBox)GetControl( AnimCheck + crAnim.Name );
                check.CheckState = (crAnim.AllDirs ? CheckState.Checked : CheckState.Indeterminate);

                LinkLabel link = (LinkLabel)GetControl( AnimLink + crAnim.Name );
                link.Enabled = true;
            }
        }

        void animLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if( LoadMode == LoadModeType.None )
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

            frmAnimation animWin = new frmAnimation();
            animWin.Text = CurrentCritterType.Name + animName;
            string filename = null;
            
            Bitmap[] frms = new Bitmap[6];
            CritterAnimation crAnim = CurrentCritterType[animName];

            switch( LoadMode )
            {
                case LoadModeType.Directory:
                    filename = openDirectory.SelectedPath + Path.DirectorySeparatorChar + CurrentCritterType.Name + animName;
                    if( File.Exists( filename + ".FRM" ) )
                    {
                        FalloutFRM frm = LoadFRM( null, filename, LoadModeType.Directory );

                        for( int d = 0; d <= 5; d++ )
                        {
                            frms[d] = frm.GetAnimFrameByDir( d, 1 );
                        }
                    }

                    for( int d = 0; d <= 5; d++ )
                    {
                        if( CurrentCritterType[animName].Dir[d] && File.Exists( filename + ".FR" + d ) )
                        {
                            FalloutFRM frm = LoadFRM( null, filename + ".FR" + d, LoadModeType.Directory );
                            frms[d] = frm.Frames[0];
                        }
                    }
                    break;

                case LoadModeType.Zip:
                    ZipStorer zip = ZipStorer.Open( TargetName, FileAccess.Read );
                    if( zip == null )
                        return;

                    if( !crAnim.Splitted )
                    {
                        FalloutFRM frm = LoadFRM( zip, crAnim.ZipData[0], LoadModeType.Zip );
                        for( int dir = 0; dir <= 5; dir++ )
                        {
                            if( crAnim.Dir[dir] )
                                frms[dir] = frm.GetAnimFrameByDir( dir, 1 );
                        }
                    }
                    else
                    {
                        for( int dir = 0; dir <= 5; dir++ )
                        {
                            if( crAnim.Dir[dir] )
                            {
                                FalloutFRM frm = LoadFRM( zip, crAnim.ZipData[dir], LoadModeType.Zip );
                                frms[dir] = frm.Frames[0];
                            }
                        }
                    }
                    zip.Close();
                    break;

                case LoadModeType.Dat:
                    DatReaderError status;
                    DAT dat = DATReader.ReadDat( TargetName, out status );
                    if( status.Error != DatError.Success )
                        return;

                    if( !crAnim.Splitted )
                    {
                        DATFile datFile = dat.FileList[crAnim.DatData[0]];
                        FalloutFRM frm = LoadFRM( null, datFile, LoadModeType.Dat );
                        for( int dir = 0; dir <= 5; dir++ )
                        {
                            if( crAnim.Dir[dir] )
                                frms[dir] = frm.GetAnimFrameByDir( dir, 1 );
                        }
                    }
                    else
                    {
                        for( int dir = 0; dir <= 5; dir++ )
                        {
                            if( crAnim.Dir[dir] )
                            {
                                DATFile datFile = dat.FileList[crAnim.DatData[dir]];
                                FalloutFRM frm = LoadFRM( null, datFile, LoadModeType.Dat );
                                frms[dir] = frm.Frames[0];
                            }
                        }
                    }
                    dat.Close();
                    break;
            }

            animWin.anim0.Image = frms[0];
            animWin.anim1.Image = frms[1];
            animWin.anim2.Image = frms[2];
            animWin.anim3.Image = frms[3];
            animWin.anim4.Image = frms[4];
            animWin.anim5.Image = frms[5];

            AnimationWindows.Add( animWin );
            animWin.Show();
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

        FalloutFRM LoadFRM( object datafile, object file, LoadModeType loadMode )
        {
            byte[] bytes = null;
            switch( loadMode )
            {
                case LoadModeType.Directory:
                    string filename = (string)file;
                    if( File.Exists( filename ) )
                        bytes = File.ReadAllBytes( filename );
                    break;

                case LoadModeType.Zip:
                    using( MemoryStream stream = new MemoryStream() )
                    {
                        ZipStorer zip = (ZipStorer)datafile;
                        ZipStorer.ZipFileEntry zipEntry = (ZipStorer.ZipFileEntry)file;
                        zip.ExtractFile( zipEntry, stream );
                        bytes = stream.ToArray();
                    }
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

            switch( config.LoadMode )
            {
                case LoadModeType.Directory:
                    files.AddRange( Directory.GetFiles( config.Target, "*.FR?", SearchOption.TopDirectoryOnly ) );
                    break;

                case LoadModeType.Zip:
                    ZipStorer zip = ZipStorer.Open( config.Target, FileAccess.Read );
                    if( zip == null )
                        return;

                    datafile = zip;
                    foreach( ZipStorer.ZipFileEntry entry in zip.ReadCentralDir() )
                    {
                        if( entry.CompressedSize == 0 )
                            continue;

                        string filename = entry.FilenameInZip.ToUpper()
                            .Replace( '\\', Path.DirectorySeparatorChar )
                            .Replace( '/', Path.DirectorySeparatorChar );

                        if( !filename.StartsWith( ArtCritters ) )
                            continue;

                        string extmp = Path.GetExtension( filename );
                        if( extmp.Length != 4 || extmp.Substring( 1, 2 ) != "FR" )
                            continue;

                        files.Add( entry );
                    }
                    break;
                
                case LoadModeType.Dat:
                    DatReaderError status;
                    DAT dat = DATReader.ReadDat( config.Target, out status );
                    if( status.Error != DatError.Success )
                        return;

                    datafile = dat;
                    long idx = -1;
                    foreach( DATFile entry in dat.FileList )
                    {
                        idx++;
                        string filename = entry.Path.ToUpper()
                            .Replace( '\\', Path.DirectorySeparatorChar )
                            .Replace( '/', Path.DirectorySeparatorChar );

                        if( !filename.StartsWith( ArtCritters ) )
                            continue;

                        string extmp = Path.GetExtension( filename );
                        if( extmp.Length != 4 || extmp.Substring( 1, 2 ) != "FR" )
                            continue;

                        // DATLib doesn't care about FileIndex, so we have to
                        entry.FileIndex = idx;
                        files.Add( entry );
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }

            string baseName = null, animName = null, ext = null;
            int dir = -1;

            List<CritterType> crTypesFound = new List<CritterType>();
            int currFile = 0, lastPercent = -1, filesCount = files.Count;
            foreach (object file in files)
            {
                if( self.CancellationPending )
                    return;

                int percent = (++currFile * 100) / filesCount;
                if( percent != lastPercent )
                {
                    lastPercent = percent;
                    self.ReportProgress( percent, "Checking "+config.Target );
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
                if (crType == null)
                {
                    crType = new CritterType(baseName);
                    crTypesFound.Add(crType);
                    self.ReportProgress( (int)ProgressData.CritterTypeName, crType.Name );
                }
                
                if (crType[animName] == null)
                {
                    CritterAnimation crAnim = new CritterAnimation(animName);
                    crType.Animations.Add(crAnim);
                }

                if( ext == "FRM" )
                {
                    if( config.FastCheck )
                    {
                        for( int d = 0; d <= 5; d++ )
                        {
                            CritterAnimation crAnim = crType[animName];
                            crAnim.Dir[d] = true;
                            if( config.LoadMode == LoadModeType.Zip )
                                crAnim.ZipData[d] = (ZipStorer.ZipFileEntry)file;
                            else if( config.LoadMode == LoadModeType.Dat )
                            {
                                DATFile datFile = (DATFile)file;
                                crAnim.DatData[d] = (int)datFile.FileIndex;
                            }

                        }
                    }
                    else
                    {
                        FalloutFRM frm = LoadFRM( datafile, file, config.LoadMode );

                        for( int d = 0; d <= 5; d++ )
                        {
                            if( frm != null && frm.GetAnimFrameByDir( d, 1 ) != null )
                            {
                                CritterAnimation crAnim = crType[animName];
                                crAnim.Dir[d] = true;
                                if( config.LoadMode == LoadModeType.Zip )
                                    crAnim.ZipData[d] = (ZipStorer.ZipFileEntry)file;
                                else if( config.LoadMode == LoadModeType.Dat )
                                    crAnim.DatData[d] = (int)((DATFile)file).FileIndex;
                            }
                        }
                    }
                }
                else
                {
                    CritterAnimation crAnim = crType[animName];

                    if( config.FastCheck )
                    {
                        crAnim.Dir[dir] = crAnim.Splitted = true;
                        if( config.LoadMode == LoadModeType.Zip )
                            crAnim.ZipData[dir] = (ZipStorer.ZipFileEntry)file;
                        else if( config.LoadMode == LoadModeType.Dat )
                            crAnim.DatData[dir] = (int)((DATFile)file).FileIndex;
                    }
                    else
                    {
                        FalloutFRM frm = LoadFRM( datafile, file, config.LoadMode );
                        if( frm != null && frm.GetAnimFrameByDir( 0, 1 ) != null )
                        {
                            crAnim.Dir[dir] = crAnim.Splitted = true;
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

            if( datafile != null )
            {
                switch( config.LoadMode )
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
        }

        private void frmChecker_ProgressChanged(object sender, ProgressChangedEventArgs e)
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
        }

        private void frmChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frmCheckerCompleted = true;

            EnableControls( true );
            menuFileOpen.Enabled =
            //menuOptionsTarget.Enabled =
            true;

            statusProgress.Visible = false;
            statusLabel.Text = "";

            if (ClosePending)
                Close();
        }

        private void falloutAlias_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Alias = decimal.ToUInt16( self.Value );
            fonlineAlias.Value = self.Value;

            RefreshFalloutFOnline();
        }

        private void fonlineEnabled_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox self = (CheckBox)sender;

            CurrentCritterType.Enabled = self.Checked;

            RefreshFalloutFOnline();
        }

        private void fonlineID_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.ID = (UInt16)self.Value;

            RefreshFalloutFOnline();
        }

        private void fonlineAlias_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Alias = (UInt16)self.Value;
            falloutAlias.Value = self.Value;

            RefreshFalloutFOnline();
        }

        private void fonlineMultihex_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Multihex = (byte)self.Value;

            RefreshFalloutFOnline();
        }

        private void fonlineAim_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox self = (CheckBox)sender;

            CurrentCritterType.Aim = self.Checked;

            RefreshFalloutFOnline();
        }

        private void fonlineArmor_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox self = (CheckBox)sender;

            CurrentCritterType.Armor = self.Checked;

            RefreshFalloutFOnline();
        }

        private void fonlineRotate_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox self = (CheckBox)sender;

            CurrentCritterType.Rotate = self.Checked;

            RefreshFalloutFOnline();

        }

        private void fonlineWalk_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Walk = (UInt16)self.Value;

            RefreshFalloutFOnline();
        }

        private void fonlineRun_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Run = (UInt16)self.Value;

            RefreshFalloutFOnline();
        }

        private void fonlineSteps1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Step1 = (UInt16)self.Value;

            RefreshFalloutFOnline();
        }

        private void fonlineSteps2_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Step2 = (UInt16)self.Value;

            RefreshFalloutFOnline();
        }

        private void fonlineSteps3_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Step3 = (UInt16)self.Value;

            RefreshFalloutFOnline();

        }

        private void fonlineSteps4_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            CurrentCritterType.Step4 = (UInt16)self.Value;

            RefreshFalloutFOnline();
        }

        private void fonlineSound_TextChanged(object sender, EventArgs e)
        {
            TextBox self = (TextBox)sender;

            CurrentCritterType.Sound = self.Text;

            RefreshFalloutFOnline();
        }

        private void fonlineComment_TextChanged(object sender, EventArgs e)
        {
            TextBox self = (TextBox)sender;

            CurrentCritterType.Comment = self.Text;

            RefreshFalloutFOnline();
        }
    }
}
