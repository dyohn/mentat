using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentat.Domain
{
    /// <summary>
    /// A convenient class for generating bash color decorators that can be used to wrap a string.
    /// </summary>
    public class BashColor
    {
        /// <value>Holds the string containing the color formatting.</value>
        public String Color { get; private set; }
        /// <value>Holds the escape sequence used to start color formatting.</value>
        private const String _escape = "\\e[";
        /// <value>Holds the string for ending a strings formatting.</value>
        public static String End = "\\e[0m";

        /// <value>Text modifiers not involving colors.</value>
        public enum TextModifier
        {
            Reset, Bold, Faint, Italics, Underline
        }

        /// <value>Values for changing the color of the actual text.</value>
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

        /// <value>Values for changing the color of the background of text (highlighting).</value>
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

        /// <summary>
        /// Constructor that generates the decorator string that will be held by the object.
        /// </summary>
        /// <param name="text">The color of the text.</param>
        /// <param name="background">The color of the background.</param>
        /// <param name="modifier">Modifier to apply to the style of the text.</param>
        public BashColor(BashColor.TextColor? text, BashColor.BackgroundColor? background, BashColor.TextModifier? modifier)
        {
            Color += _escape;
            // Check if any params were supplied. If not, generate the end token.
            if (text.HasValue || background.HasValue || modifier.HasValue)
            {
                // Is a modifier being applied? If so, append it. If not, append the default 0.
                if (modifier.HasValue)
                {
                    Color += ((int)modifier.Value);
                }
                else
                {
                    Color += ("0");
                }
                // Apply a delimiter if there are any proceeding values.
                if (background.HasValue || text.HasValue)
                {
                    Color += (";");
                }
                // Append the background color if applicable. If needed, append a delimiter afterwards.
                if (background.HasValue)
                {
                    Color += (int)background.Value;
                    if (text.HasValue)
                    {
                        Color += ";";
                    }
                }
                // Append the text color if applicable.
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
