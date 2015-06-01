using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Compression;

namespace CritterBrowser
{
    public enum CritterAnimationDir
    {
        None,
        Full,
        Partial
    }

    class CritterAnimation
    {
        public readonly string Name;
        public CritterAnimationDir[] Dir = new CritterAnimationDir[6];

        public ZipStorer.ZipFileEntry[] ZipData = new ZipStorer.ZipFileEntry[6];
        public int[] DatData = new int[6];

        public bool Full
        {
            get
            {
                foreach( CritterAnimationDir crAnimType in Dir )
                {
                    if( crAnimType != CritterAnimationDir.Full )
                        return (false);
                }

                return (true);
            }
        }

        public bool Partial
        {
            get
            {
                foreach( CritterAnimationDir crAnimType in Dir )
                {
                    if( crAnimType != CritterAnimationDir.Partial )
                        return (false);
                }

                return (true);
            }
        }

        public bool AllDirs
        {
            get
            {
                foreach( CritterAnimationDir t in Dir )
                {
                    if( t == CritterAnimationDir.None )
                        return (false);
                }

                return (true);
            }
        }

        public bool AnyDir
        {
            get
            {
                foreach( CritterAnimationDir  t in Dir )
                {
                    if( t != CritterAnimationDir.None )
                        return (true);
                }
                return (false);
            }
        }

        public CritterAnimation(string name)
        {
            if( name.Length != 2 )
                throw new ArgumentException( "wrong length", "name" );

            Name = name.ToUpper();
        }
    }

    class CritterType
    {
        public readonly string Name;

        public bool Enabled, Aim, Armor, Rotate;
        public byte Multihex;
        public UInt16 ID, Alias, Walk, Run, Step1, Step2, Step3, Step4;
        public string Sound = "", Comment = "";

        public List<CritterAnimation> Animations = new List<CritterAnimation>();

        public CritterType(string name)
        {
            Name = name.ToUpper();
        }

        public CritterAnimation this[char name]
        {
            get
            {
                return (Animations.Find(anim => anim.Name.StartsWith(name.ToString().ToUpper())));
            }
        }

        public CritterAnimation this[string name]
        {
            get
            {
                return (Animations.Find(anim => anim.Name == name));
            }
        }

        public string ToFalloutString() // is it actually correct?
        {
            string result = Name.ToLower();

            if( Alias > 0 )
            {
                result += "," + Alias;

                if( this["AB"] != null || this["AT"] != null )
                    result += ",1";
            }

            return (result);
        }

        public string ToFOnlineString( bool verbose = false )
        {
            string result = "";

            List<string> args = new List<string>();

            var sbool = new Func<bool, string>( b =>
                {
                    return (b ? "1" : "0");
                }
            );

            var anim = new Func<string, string>( name =>
                {
                    return (this[name] != null ? "1" : "0");
                }
            );

            var animGroup = new Func<char, string>( name =>
                {
                    return (this[name] != null ? "1" : "0");
                }
            );

            // cvet plz, no format changes ;_;

            args.Add( (Enabled ? "@" : "#") );
            args.Add( ID.ToString() );
            args.Add( Name.ToLower() );
            args.Add( Alias.ToString() );
            args.Add( Multihex.ToString() );
            args.Add( "0" ); // Type
            args.Add( anim( "AB" ) ); // Walk
            args.Add( anim( "AT" ) ); // Run
            args.Add( sbool( Aim ) );
            args.Add( sbool( Armor ) );
            args.Add( sbool( Rotate ) );
            args.Add( animGroup( 'A' ) );
            args.Add( animGroup( 'B' ) );
            args.Add( animGroup( 'C' ) );
            args.Add( animGroup( 'D' ) );
            args.Add( animGroup( 'E' ) );
            args.Add( animGroup( 'F' ) );
            args.Add( animGroup( 'G' ) );
            args.Add( animGroup( 'H' ) );
            args.Add( animGroup( 'I' ) );
            args.Add( animGroup( 'J' ) );
            args.Add( anim( "KJ" ) ); // K
            args.Add( animGroup( 'L' ) );
            args.Add( anim( "KL" ) ); // M
            args.Add( animGroup( 'N' ) );
            args.Add( animGroup( 'J' ) ); // O
            args.Add( animGroup( 'P' ) );
            args.Add( animGroup( 'Q' ) );
            args.Add( animGroup( 'R' ) );
            args.Add( animGroup( 'S' ) );
            args.Add( animGroup( 'T' ) );
            args.Add( animGroup( 'U' ) );
            args.Add( animGroup( 'V' ) );
            args.Add( animGroup( 'W' ) );
            args.Add( animGroup( 'X' ) );
            args.Add( animGroup( 'Y' ) );
            args.Add( animGroup( 'Z' ) );
            args.Add( Walk.ToString() );
            args.Add( Run.ToString() );
            args.Add( Step1.ToString() );
            args.Add( Step2.ToString() );
            args.Add( Step3.ToString() );
            args.Add( Step4.ToString() );
            args.Add( (Sound.Length == 0 ? "-" : Sound) );
            args.Add( (Comment.Length > 0) ? " # " + Comment : "" );

            if (!verbose)
            {
                bool first = true;
                foreach (string str in args)
                {
                    if( first )
                        first = false;
                    else
                        result += " ";

                    result += str;
                }
            }
            else
            {
                int len = 5;
                if( Name.Length > len - 1 )
                    len = Name.Length + 1;
                args.Insert(0, "Name");
                string format =
                    "# ID  {0," + -len + "}Alias MH Type Walk Run Aim Armor Rotate  A B C D E F G H I J K L M N O P Q R S T U V W X Y Z  Walk Run Walk steps Sound"
                    + Environment.NewLine
                    + "{1,-2}{2,-4}{3," + -len + "}{4,-6}{5,-3}{6,-5}{7,-5}{8,-4}{9,-4}{10,-6}{11,-8}";

                // A-Z
                for (int a = 12; a <= 37; a++)
                {
                    format += "{" + a + ",-2}";
                }
                
                // walk, run
                format += " {38,-5}{39,-4}";

                // walk steps
                for (int s = 40; s <= 43; s++)
                {
                    format += "{" + s + ",-3}";
                }

                format += "{44}{45}";

                result += string.Format( format, args.ToArray() );
            }

            return (result);
        }
    }
}
