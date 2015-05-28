using System;
using System.Collections.Generic;
using System.Text;

namespace CritterBrowser
{
    class CritterAnimation
    {
        public readonly string Name;
        public bool[] Dir = new bool[6];

        public bool AllDirs
        {
            get
            {
                foreach (bool d in this.Dir)
                {
                    if (!d)
                        return (false);
                }

                return (true);
            }
        }

        public bool AnyDir
        {
            get
            {
                foreach (bool d in this.Dir)
                {
                    if (d)
                        return (true);
                }
                return (false);
            }
        }

        public CritterAnimation(string name)
        {
            if (name.Length != 2)
                throw new ArgumentException("wrong length", name.ToString());

            this.Name = name.ToUpper();
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
            this.Name = name.ToUpper();
        }

        public CritterAnimation this[char name]
        {
            get
            {
                return (this.Animations.Find(anim => anim.Name.StartsWith(name.ToString().ToUpper())));
            }
        }

        public CritterAnimation this[string name]
        {
            get
            {
                return (this.Animations.Find(anim => anim.Name == name));
            }
        }

        public string ToFalloutString() // is it actually correct?
        {
            string result = this.Name.ToLower();

            if( this.Alias > 0 )
            {
                result += "," + this.Alias;

                if( this["AB"] != null || this["AT"] != null )
                    result += ",1";
            }

            return (result);
        }

        public string ToFOnlineString( bool verbose = false )
        {
            string result = "";

            List<string> args = new List<string>();

            args.Add((this.Enabled ? "@" : "#"));
            args.Add(this.ID.ToString());
            args.Add(this.Name.ToLower());
            args.Add(this.Alias.ToString());
            args.Add(this.Multihex.ToString());
            args.Add("0"); // 3D
            args.Add((this["AB"] != null ? "1" : "0"));
            args.Add((this["AT"] != null ? "1" : "0"));
            args.Add((this.Aim ? "1" : "0"));
            args.Add((this.Armor ? "1" : "0"));
            args.Add((this.Rotate ? "1" : "0"));
            args.Add((this['A'] != null ? "1" : "0"));
            args.Add((this['B'] != null ? "1" : "0"));
            args.Add((this['C'] != null ? "1" : "0"));
            args.Add((this['D'] != null ? "1" : "0"));
            args.Add((this['E'] != null ? "1" : "0"));
            args.Add((this['F'] != null ? "1" : "0"));
            args.Add((this['G'] != null ? "1" : "0"));
            args.Add((this['H'] != null ? "1" : "0"));
            args.Add((this['I'] != null ? "1" : "0"));
            args.Add((this['J'] != null ? "1" : "0"));
            args.Add((this["KJ"] != null ? "1" : "0"));
            args.Add((this['L'] != null ? "1" : "0"));
            args.Add((this["KL"] != null ? "1" : "0")); // M
            args.Add((this['N'] != null ? "1" : "0"));
            args.Add((this['J'] != null ? "1" : "0")); // J -> O
            args.Add((this['P'] != null ? "1" : "0"));
            args.Add((this['Q'] != null ? "1" : "0"));
            args.Add((this['R'] != null ? "1" : "0"));
            args.Add((this['S'] != null ? "1" : "0"));
            args.Add((this['T'] != null ? "1" : "0"));
            args.Add((this['U'] != null ? "1" : "0"));
            args.Add((this['V'] != null ? "1" : "0"));
            args.Add((this['W'] != null ? "1" : "0"));
            args.Add((this['X'] != null ? "1" : "0"));
            args.Add((this['Y'] != null ? "1" : "0"));
            args.Add((this['Z'] != null ? "1" : "0"));
            args.Add(this.Walk.ToString());
            args.Add(this.Run.ToString());
            args.Add(this.Step1.ToString());
            args.Add(this.Step2.ToString());
            args.Add(this.Step3.ToString());
            args.Add(this.Step4.ToString());
            args.Add( (this.Sound.Length == 0 ? "-" : this.Sound));
            args.Add( (this.Comment.Length > 0) ? " # "+this.Comment:"");

            if (!verbose)
            {
                bool first = true;
                foreach (string str in args)
                {
                    if (!first)
                    {
                        result += " ";
                        first = false;
                    }
                    result += str;
                }
            }
            else
            {
                int len = 5;
                if( this.Name.Length > len - 1 )
                    len = this.Name.Length + 1;
                args.Insert(0, "Name");
                string format =
                    "# ID  {0," + -len + "}Alias MH 3D Walk Run Aim Armor Rotate  A B C D E F G H I J K L M N O P Q R S T U V W X Y Z  Walk Run Walk steps Sound"
                    + Environment.NewLine
                    + "{1,-2}{2,-4}{3," + -len + "}{4,-6}{5,-3}{6,-3}{7,-5}{8,-4}{9,-4}{10,-6}{11,-8}";

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

                result += string.Format( format, args.ToArray());
            }

            return (result);
        }
    }
}
