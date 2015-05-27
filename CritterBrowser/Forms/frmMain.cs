using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using FOCommon.Graphic;

namespace CritterBrowser.Forms
{
    public partial class frmMain : Form
    {
        enum LoadModeType
        {
            None,
            Datafile,
            Directory
        }

        readonly string BaseText;

        // Generated members names prefix
        readonly string AnimCheck = "animCheck";
        readonly string AnimFlow = "animFlow";
        readonly string AnimGroup = "animGroup";
        readonly string AnimLink = "animLink";
        readonly string AnimPanel = "animPanel";

        List<string> ValidAnimations = new List<string>();
        List<string> ValidAnimationsGroups = new List<string>();

        List<CritterType> CritterTypes = new List<CritterType>();
        CritterType CurrentCritterType = null;
        int PrevSelectedCritterIndex = -1;


        LoadModeType LoadMode = LoadModeType.None;

        Color TransparencyFRM = Color.FromArgb(255, 11, 0, 11);

        readonly int ProgressCritter = -1;
        private bool frmCheckerCompleted = true;
        private bool ClosePending = false;

        public frmMain()
        {
            InitializeComponent();

            this.BaseText = this.Text;

            this.InitAnimations();
            this.AutoPlacement();
            this.EnableControls(false);

            statusLabel.Text = "";

            switch (this.StartPosition)
            {
                case FormStartPosition.CenterParent:
                    this.CenterToParent();
                    break;
                case FormStartPosition.CenterScreen:
                    this.CenterToScreen();
                    break;
            }
        }

        // http://stackoverflow.com/a/1732361
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!this.frmCheckerCompleted)
            {
                this.Enabled = false;
                e.Cancel = true;
                this.ClosePending = true;
                frmChecker.CancelAsync();
                return;
            }

            base.OnFormClosing(e);
        }

        void EnableControls(bool enable)
        {
            lstCritters.Enabled =
            grpFalloutConfiguration.Enabled =
            falloutCrittersLst.Enabled =
            grpFonlineConfiguration.Enabled =
            fonlineCritterTypesCfg.Enabled =
            enable;

            if (!enable)
                this.ResetAnimations();
        }

        void ResetAnimations()
        {
            foreach (string animName in this.ValidAnimations)
            {
                CheckBox check = (CheckBox)this.GetControl(this.AnimCheck + animName);
                check.CheckState = CheckState.Unchecked;

                LinkLabel link = (LinkLabel)this.GetControl(this.AnimLink + animName);
                link.Enabled = false;
            }
        }

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

            this.ValidAnimations.Sort();
        }

        // Panel ("animations")
        //   GroupBox (AnimGroupX)
        //     FlowLayoutPanel (AnimFlowX)
        //       Panel (AnimPanelXY)
        //         CheckBox (AnimCheckXY)
        //         LinkLabel (AnimLinkXY)

        void AddAnimationGroup(string animGroup, string description = "" )
        {
            animGroup = animGroup.ToUpper();

            if (animGroup.Length != 1)
                return;
            else if (this.ValidAnimationsGroups.Contains(animGroup))
                return;

            this.ValidAnimationsGroups.Add(animGroup);

            GroupBox group = new GroupBox();
            group.Name = this.AnimGroup + animGroup;

            group.Text = animGroup + "*";
            if (description.Length > 0)
                group.Text += " (" + description + ")";

            group.Margin = new Padding(15);
            group.Padding = new Padding(3);
            group.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            group.AutoSize = true;

            FlowLayoutPanel fpanel = new FlowLayoutPanel();
            fpanel.Name = this.AnimFlow + animGroup;

            fpanel.FlowDirection = FlowDirection.TopDown;
            fpanel.Padding = new Padding(0);
            fpanel.Margin = new Padding(0);
            fpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fpanel.AutoSize = true;
            fpanel.Dock = DockStyle.Fill;

            group.Controls.Add(fpanel);
            animations.Controls.Add(group);
        }

        void AddAnimation(string animName, string description = "")
        {
            animName = animName.ToUpper();

            if (animName.Length != 2)
                return;

            string animGroup = animName.Substring(0, 1);

            if (!this.ValidAnimationsGroups.Contains(animGroup))
                return;
            
            if(this.ValidAnimations.Contains(animName))
                return;

            this.ValidAnimations.Add(animName);

            FlowLayoutPanel fpanel = (FlowLayoutPanel)this.GetControl(AnimFlow + animGroup);

            Panel panel = new Panel();
            panel.Name = this.AnimPanel + animName;
            panel.Margin = panel.Padding = new Padding(0);
            panel.AutoSize = true;

            CheckBox check = new CheckBox();
            check.Name = this.AnimCheck + animName;

            check.Font = new Font(check.Font.FontFamily, 7, FontStyle.Underline);
            check.Margin = new Padding(0);
            check.Padding = new Padding(3);
            check.AutoSize = true;

            LinkLabel link = new LinkLabel();
            link.Name = this.AnimLink + animName;

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

        public void AutoPlacement()
        {
            List<GroupBox> groups = new List<GroupBox>();
            foreach (string animGroup in this.ValidAnimationsGroups)
            {
                groups.Add( (GroupBox)this.GetControl(this.AnimGroup + animGroup));
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

            // 2.0 does not support argument-less Action
            var fixColumnWidth = new Action<int>(width =>
                {
                    foreach (GroupBox group in column)
                    {
                        if (!group.AutoSize)
                            continue;

                        Size oldSize = group.Size;
                        group.AutoSize = false;
                        group.Size = oldSize;
                        group.Width = width;
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
                    fixColumnWidth(w);
                    column.Clear();
                    column.Add(group);

                    x += w;
                    group.Left = x;
                    w = group.Width;
                    h = group.Height;
                }
            }

            fixColumnWidth(w);
        }

        /// <summary>
        /// Deep search for Control with given name
        /// </summary>
        /// <param name="name"></param>
        /// <throws>aaa</throws>
        /// <returns></returns>
        private Control GetControl(string name)
        {
            Control[] controls = this.Controls.Find(name, true);
            if (controls != null && controls.Length == 1)
                return (controls[0]);

            throw new NotSupportedException(); // :)
        }

        private void RefreshFalloutFOnline()
        {
            this.falloutCrittersLst.Text = this.CurrentCritterType.ToFalloutString();

            this.fonlineCritterTypesCfg.Clear();

            this.fonlineCritterTypesCfg.AppendText(this.CurrentCritterType.ToFOnlineString(this.fonlineCritterTypesCfg.Multiline));
        }

        private void menuFileOpenDatafile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFile.ShowDialog(this);
        }

        private void menuFileOpenDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result = openDirectory.ShowDialog(this);

            if (result != DialogResult.OK)
                return;

            this.LoadMode = LoadModeType.Directory;
            this.Text = this.BaseText + " : " + openDirectory.SelectedPath;

            lstCritters.SelectedIndex = this.PrevSelectedCritterIndex = -1;
            lstCritters.Items.Clear();

            this.EnableControls(false);
            menuFileOpen.Enabled = false;

            statusLabel.Text = "Opening " + openDirectory.SelectedPath + "...";

            frmChecker.DoWork -= this.frmChecker_DoWork;

            frmChecker.DoWork +=new DoWorkEventHandler(frmChecker_DoWork);
            frmChecker.RunWorkerAsync(openDirectory.SelectedPath);
        }

        private void lstCritters_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox self = (ListBox)sender;

            if (self.SelectedItem == null)
                return;

            if (self.SelectedIndex == this.PrevSelectedCritterIndex)
                return;
            else
                this.PrevSelectedCritterIndex = self.SelectedIndex;

            this.ResetAnimations();

            string baseName = (string)self.SelectedItem;
            this.CurrentCritterType = this.CritterTypes.Find(cr => cr.Name == baseName);

            this.falloutAlias.Value = this.CurrentCritterType.Alias;

            this.fonlineEnabled.Checked = this.CurrentCritterType.Enabled;
            this.fonlineID.Value = this.CurrentCritterType.ID;
            this.fonlineAlias.Value = this.CurrentCritterType.Alias;
            this.fonlineMultihex.Value = this.CurrentCritterType.Multihex;
            this.fonlineAim.Checked = this.CurrentCritterType.Aim;
            this.fonlineRotate.Checked = this.CurrentCritterType.Rotate;
            this.fonlineWalk.Value = this.CurrentCritterType.Walk;
            this.fonlineRun.Value = this.CurrentCritterType.Run;
            this.fonlineSteps1.Value = this.CurrentCritterType.Step1;
            this.fonlineSteps2.Value = this.CurrentCritterType.Step2;
            this.fonlineSteps3.Value = this.CurrentCritterType.Step3;
            this.fonlineSteps4.Value = this.CurrentCritterType.Step4;
            this.fonlineSound.Text = this.CurrentCritterType.Sound;
            this.fonlineComment.Text = this.CurrentCritterType.Comment;

            this.RefreshFalloutFOnline();

            foreach (CritterAnimation crAnim in this.CurrentCritterType.Animations)
            {
                CheckBox check = (CheckBox)this.GetControl(this.AnimCheck+ crAnim.Name);
                check.CheckState = (crAnim.AllDirs ? CheckState.Checked : CheckState.Indeterminate);

                LinkLabel link = (LinkLabel)this.GetControl(this.AnimLink + crAnim.Name);
                link.Enabled = true;
            }
        }

        void animLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.LoadMode == LoadModeType.None)
                return;

            LinkLabel self = (LinkLabel)sender;
            string animName = (string)self.Tag;

            frmAnimation animWin = new frmAnimation();
            animWin.Text = this.CurrentCritterType.Name + animName;

            if (this.LoadMode == LoadModeType.Directory)
            {
                Bitmap[] frms = new Bitmap[6];
                
                string filename = openDirectory.SelectedPath + Path.DirectorySeparatorChar + this.CurrentCritterType.Name + animName;
                if (File.Exists(filename + ".FRM"))
                {
                    byte[] bytes = File.ReadAllBytes(filename + ".FRM");
                    FalloutFRM frm = FalloutFRMLoader.LoadFRM(bytes, this.TransparencyFRM);

                    for (int d = 0; d <= 5; d++)
                    {
                        frms[d] = frm.GetAnimFrameByDirN(d, 1);
                    }
                }

                for (int d = 0; d <= 5; d++)
                {
                    if (this.CurrentCritterType[animName].Dir[d] && File.Exists(filename + ".FR" + d))
                    {
                        byte[] bytes = File.ReadAllBytes(filename + ".FR" + d);
                        FalloutFRM frm = FalloutFRMLoader.LoadFRM(bytes, this.TransparencyFRM);
                        frms[d] = frm.Frames[0];
                    }
                }

                animWin.anim0.Image = frms[0];
                animWin.anim1.Image = frms[1];
                animWin.anim2.Image = frms[2];
                animWin.anim3.Image = frms[3];
                animWin.anim4.Image = frms[4];
                animWin.anim5.Image = frms[5];
            }

            animWin.Show();
        }

        private void frmChecker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker self = (BackgroundWorker)sender;
            string directory = (string)e.Argument;

            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(directory, "*.FRM", SearchOption.TopDirectoryOnly));
            for (int i = 0; i <= 5; i++)
            {
                files.AddRange(Directory.GetFiles(directory, "*.FR" + i, SearchOption.TopDirectoryOnly));
            }
            files.Sort();

            int currFile = 0;
            foreach (string file in files)
            {
                currFile++;
                int percent = (currFile * 100) / files.Count;

                // if file == C:\Fallout\data\art\critters\HFJMPSAB.FRM

                string name = Path.GetFileName(file).ToUpper(); // HFJMPSAB.FRM
                string nameNoExt = Path.GetFileNameWithoutExtension(name); // HFJMPSAB
                string baseName = nameNoExt.Substring(0, nameNoExt.Length - 2); // HFJMPS
                string animName = nameNoExt.Substring(nameNoExt.Length - 2); // AB
                string ext = Path.GetExtension(name).Substring(1); // FRM

                self.ReportProgress(percent, "Checking " + name + "...");

                if (!this.ValidAnimations.Contains(animName))
                    continue;

                // TODO: should be outside
                CritterType crType = this.CritterTypes.Find(cr => cr.Name == baseName);
                if (crType == null)
                {
                    crType = new CritterType(baseName);
                    this.CritterTypes.Add(crType);
                }

                self.ReportProgress(ProgressCritter, crType.Name);

                // TODO: should be outside
                if (crType[animName] == null)
                {
                    CritterAnimation crAnim = new CritterAnimation(animName);
                    crType.Animations.Add(crAnim);
                }

                if (ext == "FRM")
                {
                    byte[] bytes = File.ReadAllBytes(file);
                    FalloutFRM frm = FalloutFRMLoader.LoadFRM(bytes, this.TransparencyFRM);

                    for (int d = 0; d <= 5; d++)
                    {
                        if (frm.GetAnimFrameByDirN(d, 1) != null)
                            crType[animName].Dir[d] = true;
                    }
                }

                for (int d = 0; d <= 5; d++)
                {
                    if (ext == "FR" + d)
                    {
                        if (FalloutFRMLoader.Load(file, 1, this.TransparencyFRM) != null)
                            crType[animName].Dir[d] = true;
                    }
                }
            }
        }

        private void frmChecker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string text = (string)e.UserState;

            if (e.ProgressPercentage == ProgressCritter && text != null)
            {
                if (!lstCritters.Items.Contains(text))
                    lstCritters.Items.Add(text);
            }
            else if (e.ProgressPercentage >= 0)
            {
                if (!statusProgress.Visible)
                    statusProgress.Visible = true;

                statusProgress.Value = e.ProgressPercentage;
                statusProgress.ToolTipText = e.ProgressPercentage + "%";

                if (text != null)
                    statusLabel.Text = "[" + e.ProgressPercentage + "%] " + text;
            }
        }

        private void frmChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.frmCheckerCompleted = true;

            this.EnableControls(true);
            menuFileOpen.Enabled = true;

            statusProgress.Visible = false;
            statusLabel.Text = "";

            if (this.ClosePending)
                this.Close();
        }

        private void falloutAlias_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            this.CurrentCritterType.Alias = decimal.ToUInt16(self.Value);

            this.fonlineAlias.Value = self.Value;

            this.RefreshFalloutFOnline();
        }

        private void fonlineEnabled_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox self = (CheckBox)sender;
            this.CurrentCritterType.Enabled = self.Checked;

            this.RefreshFalloutFOnline();
        }

        private void fonlineID_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;

            this.CurrentCritterType.ID = (UInt16)self.Value;

            this.RefreshFalloutFOnline();
        }

        private void fonlineAlias_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            this.CurrentCritterType.Alias = (UInt16)self.Value;

            this.falloutAlias.Value = self.Value;

            this.RefreshFalloutFOnline();
        }

        private void fonlineMultihex_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            this.CurrentCritterType.Multihex = (byte)self.Value;

            this.RefreshFalloutFOnline();
        }

        private void fonlineAim_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox self = (CheckBox)sender;
            this.CurrentCritterType.Aim = self.Checked;

            this.RefreshFalloutFOnline();
        }

        private void fonlineArmor_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox self = (CheckBox)sender;
            this.CurrentCritterType.Armor = self.Checked;

            this.RefreshFalloutFOnline();
        }

        private void fonlineRotate_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox self = (CheckBox)sender;
            this.CurrentCritterType.Rotate = self.Checked;

            this.RefreshFalloutFOnline();

        }

        private void fonlineWalk_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            this.CurrentCritterType.Walk = (UInt16)self.Value;

            this.RefreshFalloutFOnline();
        }

        private void fonlineRun_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            this.CurrentCritterType.Run = (UInt16)self.Value;

            this.RefreshFalloutFOnline();
        }

        private void fonlineSteps1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            this.CurrentCritterType.Step1 = (UInt16)self.Value;

            this.RefreshFalloutFOnline();
        }

        private void fonlineSteps2_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            this.CurrentCritterType.Step2 = (UInt16)self.Value;

            this.RefreshFalloutFOnline();
        }

        private void fonlineSteps3_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            this.CurrentCritterType.Step3 = (UInt16)self.Value;

            this.RefreshFalloutFOnline();

        }

        private void fonlineSteps4_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown self = (NumericUpDown)sender;
            this.CurrentCritterType.Step4 = (UInt16)self.Value;

            this.RefreshFalloutFOnline();
        }

        private void fonlineSound_TextChanged(object sender, EventArgs e)
        {
            TextBox self = (TextBox)sender;
            this.CurrentCritterType.Sound = self.Text;

            this.RefreshFalloutFOnline();
        }

        private void fonlineComment_TextChanged(object sender, EventArgs e)
        {
            TextBox self = (TextBox)sender;
            this.CurrentCritterType.Comment = self.Text;

            this.RefreshFalloutFOnline();
        }
    }
}
