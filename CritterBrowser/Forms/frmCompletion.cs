using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace CritterBrowser.Forms
{
    public partial class frmCompletion : Form
    {
        frmMain main;
        CritterType CurrentCritterType;

        public frmCompletion( frmMain main, CritterType crType = null )
        {
            this.main = main;

            if( crType == null )
                return;

            InitializeComponent();
            CurrentCritterType = crType;

            radioText.Checked = true;
        }

        internal string CompletionText( CritterType crType, bool part = true )
        {
            string result = crType.Name + Environment.NewLine;

            var groupComplete = new Func<char, bool>( ( groupName ) =>
            {
                List<string> animList = main.ValidAnimations.FindAll( name => name.StartsWith( groupName.ToString() ) );
                foreach( string anim in animList )
                {
                    CritterAnimation crAnim = crType[anim];

                    if( crAnim == null || !crAnim.Full )
                        return (false);
                }

                return (true);
            } );

            var animComplete = new Func<CritterAnimation, bool>( ( crAnim ) =>
            {
                foreach( CritterAnimationDir dir in crAnim.Dir )
                {
                    if( dir == CritterAnimationDir.None )
                        return (false);
                }

                return (true);
            } );

            List<string> complete = new List<string>();
            List<string> partial = new List<string>();
            List<string> missing = new List<string>();

            bool firstPartial = true;
            foreach( string groupNameStr in main.ValidAnimationsGroups )
            {
                char groupNameChar = groupNameStr[0];
                string groupName = main.GetControl( main.AnimGroup + groupNameChar ).Text;

                if( groupComplete( groupNameChar ) )
                {
                    complete.Add( " " + groupName );
                }
                else
                {
                    if( crType[groupNameChar] == null )
                    {
                        if( !part )
                            missing.Add( " " + groupName );

                        continue;
                    }

                    if( firstPartial )
                        firstPartial = false;
                    else
                        partial.Add( "" );
                    partial.Add( " " + groupName + ":" );

                    List<CritterAnimation> list = crType.Animations.FindAll( cr => cr.Name.StartsWith( groupNameChar.ToString() ) );
                    foreach( CritterAnimation crAnim in list )
                    {
                        string animName = " - " + main.GetControl( main.AnimLink + crAnim.Name ).Text;

                        if( !animComplete( crAnim ) )
                        {
                            string[] dirName = { "NE", "E", "SE", "SW", "W", "NW" };
                            List<string> dirDone = new List<string>();

                            for( int dir = 0; dir <= 5; dir++ )
                            {
                                if( crAnim.Dir[dir] == CritterAnimationDir.None )
                                    continue;

                                dirDone.Add( dirName[dir] );
                            }
                            animName += " (" + string.Join( ",", dirDone.ToArray() ) + ")";
                        }
                        partial.Add( animName );
                    }
                }
            }

            if( complete.Count > 0 )
            {
                result += Environment.NewLine;
                result += "Completed sets:" + Environment.NewLine;
                foreach( string name in complete )
                {
                    result += name + Environment.NewLine;
                }
            }

            if( partial.Count > 0 )
            {
                result += Environment.NewLine;
                result += "Partial sets:" + Environment.NewLine;
                foreach( string name in partial )
                {
                    result += name + Environment.NewLine;
                }
            }

            if( missing.Count > 0 )
            {
                result += Environment.NewLine;
                result += "Missing sets:" + Environment.NewLine;
                foreach( string name in missing )
                {
                    result += name + Environment.NewLine;
                }
            }

            return (result);
        }

        internal string CompletionBBCode( CritterType crType, bool partial = false, int columns = 5, bool fonlineRu = false )
        {
            string newLine = string.Empty;

            string tableStart = !fonlineRu ? "[table]" : "[xtable]{tbody}";
            string tableEnd = !fonlineRu ? "[/table]" : "{/tbody}[/xtable]";

            char tagOpen = !fonlineRu ? '[' : '{';
            char tagClose = !fonlineRu ? ']' : '}';

            var rawtag = new Func<string, string, string>( ( id, text ) =>
            {
                string bb = id.Split( new char[] { '=' }, 2 )[0];
                return ('[' + id + ']' + text + "[/" + bb + ']');
            } );

            var tag = new Func<string, string, string>( ( id, text ) =>
            {
                string bb = id.Split( new char[] { '=' }, 2 )[0];
                return (tagOpen + id + tagClose + text + tagOpen + '/' + bb + tagClose);
            } );

            var td = new Func<string, string>( text =>
            {
                return (tag( "td", text ));
            } );

            var tr = new Func<string, string>( text =>
            {
                return (tag( "tr", text ));
            } );

            var animComplete = new Func<CritterAnimation, bool>( ( crAnim ) =>
            {
                foreach( CritterAnimationDir dir in crAnim.Dir )
                {
                    if( dir == CritterAnimationDir.None )
                        return (false);
                }

                return (true);
            } );

            string result = tableStart + newLine;

            List<List<string>> groups = new List<List<string>>();

            foreach( string groupNameStr in main.ValidAnimationsGroups )
            {
                char groupNameChar = groupNameStr[0];
                string groupName = main.GetControl( main.AnimGroup + groupNameChar ).Text;

                if( partial && crType[groupNameChar] == null )
                    continue;

                List<string> group = new List<string>();
                group.Add( td( rawtag( "b", groupName ) ) );

                List<string> animList = main.ValidAnimations.FindAll( anim => anim.StartsWith( groupNameStr ) );

                foreach( string anim in animList )
                {
                    string animName = main.GetControl( main.AnimLink + anim ).Text;

                    CritterAnimation crAnim = crType[anim];
                    if( crAnim == null )
                        group.Add( td( rawtag( "color=red", animName ) ) );
                    else if( animComplete( crAnim ) )
                        group.Add( td( rawtag( "color=green", animName ) ) );
                    else
                    {
                        string[] dirName = { "NE", "E", "SE", "SW", "W", "NW" };
                        List<string> dirDone = new List<string>();

                        for( int dir = 0; dir <= 5; dir++ )
                        {
                            if( crAnim.Dir[dir] == CritterAnimationDir.None )
                                continue;

                            dirDone.Add( dirName[dir] );
                        }
                        animName += " (" + string.Join( ",", dirDone.ToArray() ) + ")";

                        group.Add( td( rawtag( "color=yellow", animName ) ) );
                    }

                }
                groups.Add( group );
            }

            int curr = 0;
            List<string> lines = new List<string>(), rlines = new List<string>();
            int linesH = 0;
            foreach( List<string> group in groups )
            {
                if( ++curr > columns )
                {
                    foreach( string line in lines )
                    {
                        result += tr( line ) + newLine;
                    }
                    lines.Clear();
                    curr = 1;
                    linesH = 0;
                }

                if( linesH < group.Count )
                    linesH = group.Count;

                if( lines.Count == 0 )
                    lines.AddRange( group );
                else
                {
                    for( int l = 0; l < linesH; l++ )
                    {
                        if( l >= lines.Count )
                        {
                            string line = string.Empty;
                            for( int x = 1; x < curr; x++ )
                            {
                                line += td( "" );
                            }
                            lines.Add( line );
                        }
                        if( l >= group.Count )
                        {
                            group.Add( td( "" ) );
                        }
                        lines[l] += group[l];
                    }
                }
            }

            foreach( string line in lines )
            {
                result += tr( line ) + newLine;
            }

            result += tableEnd + newLine;

            return (result);
        }

        void VerifyControls()
        {
            grpBBCode.Enabled = radioForum.Checked;
            grpPicture.Enabled = radioPicture.Checked;

            checkFull.Enabled = !radioPicture.Checked;

            if( radioPicture.Checked )
                textOut.Visible = false;
            if( radioText.Checked || radioForum.Checked )
                picOut.Visible = false;

            if( radioText.Checked || radioForum.Checked )
                textOut.Visible = true;
            if( radioPicture.Checked )
                picOut.Visible = true;

            btnSaveText.Visible = radioText.Checked || radioForum.Checked;
            btnSavePicture.Visible = radioPicture.Checked;

            Update();
        }

        private void radioText_CheckedChanged( object sender, EventArgs e )
        {
            textOut.Text = CompletionText( CurrentCritterType, !checkFull.Checked );

            VerifyControls();
        }

        private void radioPicture_CheckedChanged( object sender, EventArgs e )
        {
            Bitmap bmp = new Bitmap( main.animations.ClientRectangle.Width, main.animations.ClientRectangle.Height );
            main.animations.DrawToBitmap( bmp, main.animations.ClientRectangle );

            picOut.Image = bmp;

            VerifyControls();
        }

        private void checkFull_CheckedChanged( object sender, EventArgs e )
        {
            if( radioText.Checked )
                radioText_CheckedChanged( sender, e );
            else if( radioForum.Checked )
                event_forumGeneric( sender, e );
        }

        private void event_forumGeneric( object sender, EventArgs e )
        {
            textOut.Text = CompletionBBCode( CurrentCritterType, !checkFull.Checked, (int)numColumns.Value, radioRu.Checked );
        }

        private void btnSaveText_Click( object sender, EventArgs e )
        {
            saveText.FileName = CurrentCritterType.Name + ".txt";

            DialogResult result = saveText.ShowDialog( this );
            if( result != DialogResult.OK )
                return;

            File.WriteAllText( saveText.FileName, textOut.Text );
        }

        private void btnSavePicture_Click( object sender, EventArgs e )
        {
            savePicture.FileName = CurrentCritterType.Name + ".png";

            DialogResult result = savePicture.ShowDialog(this);
            if( result!=DialogResult.OK)
                return;

            Bitmap bmp = new Bitmap( main.animations.ClientRectangle.Width, main.animations.ClientRectangle.Height );
            main.animations.DrawToBitmap( bmp, main.animations.ClientRectangle );

            bmp.Save( savePicture.FileName, ImageFormat.Png );
        }

        private void btnClose_Click( object sender, EventArgs e )
        {
            Close();
        }
    }
}
