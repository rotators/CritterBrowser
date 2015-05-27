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

        public string ToFalloutString()
        {
            string result = this.Name.ToLower();

            if (this.Alias > 0)
                result += "," + this.Alias;
            if (this["AB"] != null || this["AT"] != null) // is it actually correct?
                result += ",1";

            return (result);
        }

        public string ToFOnlineString()
        {
            string result = (this.Enabled ? "@" : "#");
            result += " " + this.ID;
            result += " " + this.Name.ToLower();
            result += " " + this.Alias;
            result += " " + this.Multihex;
            result += " " + (this["AB"] != null ? "1" : "0");
            result += " " + (this["AT"] != null ? "1" : "0");
            result += " " + (this.Aim ? "1" : "0");
            result += " " + (this.Armor ? "1" : "0");
            result += " " + (this['A'] != null ? "1" : "0");
            result += " " + (this['B'] != null ? "1" : "0");
            result += " " + (this['C'] != null ? "1" : "0");
            result += " " + (this['D'] != null ? "1" : "0");
            result += " " + (this['E'] != null ? "1" : "0");
            result += " " + (this['F'] != null ? "1" : "0");
            result += " " + (this['G'] != null ? "1" : "0");
            result += " " + (this['H'] != null ? "1" : "0");
            result += " " + (this['I'] != null ? "1" : "0");
            result += " " + (this['J'] != null ? "1" : "0");
            result += " " + (this["KJ"] != null ? "1" : "0");
            result += " " + (this['L'] != null ? "1" : "0");
            result += " " + (this["KL"] != null ? "1" : "0"); // M
            result += " " + (this['N'] != null ? "1" : "0");
            result += " " + (this['J'] != null ? "1" : "0"); // J -> O
            result += " " + (this['P'] != null ? "1" : "0");
            result += " " + (this['Q'] != null ? "1" : "0");
            result += " " + (this['R'] != null ? "1" : "0");
            result += " " + (this['S'] != null ? "1" : "0");
            result += " " + (this['T'] != null ? "1" : "0");
            result += " " + (this['U'] != null ? "1" : "0");
            result += " " + (this['V'] != null ? "1" : "0");
            result += " " + (this['W'] != null ? "1" : "0");
            result += " " + (this['X'] != null ? "1" : "0");
            result += " " + (this['Y'] != null ? "1" : "0");
            result += " " + (this['Z'] != null ? "1" : "0");
            result += " " + this.Walk;
            result += " " + this.Run;
            result += " " + this.Step1;
            result += " " + this.Step2;
            result += " " + this.Step3;
            result += " " + this.Step4;
            result += " " + (this.Sound.Length == 0 ? "-" : this.Sound);
            if (this.Comment.Length > 0)
                result += " # " + this.Comment;

            return (result);
        }
    }
}
