using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentat.Domain
{
    public class BashColor
    {
        public String Color { get; private set; }
        private const String _escape = "\\e[";
        public static String End = "\\e[0m";


        public enum TextModifier
        {
            Reset, Bold, Faint, Italics, Underline
        }

        public enum TextColor
        {
            Black = 30,
            Red = 31,
            Green = 32,
            Yellow = 33,
            Blue = 34,
            Magenta = 35,
            Cya = 36,
            LightGray = 37,
            Gray = 90,
            LightRed = 91,
            LightGreen = 92,
            LightYellow = 93,
            LightBlue = 94,
            LightMagenta = 95,
            LightCyan = 96,
            White = 97
        }

        public enum BackgroundColor
        {
            Black = 40,
            Red = 41,
            Green = 42,
            Yellow = 43,
            Blue = 44,
            Magenta = 45,
            Cya = 46,
            LightGray = 47,
            Gray = 100,
            LightRed = 101,
            LightGreen = 102,
            LightYellow = 103,
            LightBlue = 104,
            LightMagenta = 105,
            LightCyan = 106,
            White = 107
        }

        public BashColor(BashColor.TextColor? text, BashColor.BackgroundColor? background, BashColor.TextModifier? modifier)
        {
            Color += _escape;
            if (text.HasValue || background.HasValue || modifier.HasValue)
            {
                if (modifier.HasValue)
                {
                    Color += ((int)modifier.Value);
                }
                else
                {
                    Color += ("0");
                }
                if (background.HasValue || text.HasValue)
                {
                    Color += (";");
                }
                if (background.HasValue)
                {
                    Color += (int)background.Value;
                    if (text.HasValue)
                    {
                        Color += ";";
                    }
                }
                if (text.HasValue)
                {
                    Color += (int)text.Value;
                }
            }
            else
            {
                Color += "0";
            }
            Color += "m";
        }
    }
}
