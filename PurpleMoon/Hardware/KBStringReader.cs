// system libraries
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

// os libraries
using PurpleMoon.Core;
using PurpleMoon.Math;
using PurpleMoon.Types;
using PurpleMoon.Processes;

namespace PurpleMoon.Hardware
{
    public class KBStringReader
    {
        public string output;
        private bool upperCase = false;
        public bool acceptNewLine = false;

        public void GetInput()
        {
            if (KBPS2.CAPS_LOCK)
            {
                if (KBPS2.SHIFT_DOWN) { upperCase = false; }
                else { upperCase = true; }
            }
            else
            {
                if (KBPS2.SHIFT_DOWN) { upperCase = true; }
                else { upperCase = false; }
            }

            // letters
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.A)) { if (upperCase) { output += "A"; } else { output += "a"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.B)) { if (upperCase) { output += "B"; } else { output += "b"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.C)) { if (upperCase) { output += "C"; } else { output += "c"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D)) { if (upperCase) { output += "D"; } else { output += "d"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.E)) { if (upperCase) { output += "E"; } else { output += "e"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.F)) { if (upperCase) { output += "F"; } else { output += "f"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.G)) { if (upperCase) { output += "G"; } else { output += "g"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.H)) { if (upperCase) { output += "H"; } else { output += "h"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.I)) { if (upperCase) { output += "I"; } else { output += "i"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.J)) { if (upperCase) { output += "J"; } else { output += "j"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.K)) { if (upperCase) { output += "K"; } else { output += "k"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.L)) { if (upperCase) { output += "L"; } else { output += "l"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.M)) { if (upperCase) { output += "M"; } else { output += "m"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.N)) { if (upperCase) { output += "N"; } else { output += "n"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.O)) { if (upperCase) { output += "O"; } else { output += "o"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.P)) { if (upperCase) { output += "P"; } else { output += "p"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Q)) { if (upperCase) { output += "Q"; } else { output += "q"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.R)) { if (upperCase) { output += "R"; } else { output += "r"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.S)) { if (upperCase) { output += "S"; } else { output += "s"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.T)) { if (upperCase) { output += "T"; } else { output += "t"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.U)) { if (upperCase) { output += "U"; } else { output += "u"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.V)) { if (upperCase) { output += "V"; } else { output += "v"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.W)) { if (upperCase) { output += "W"; } else { output += "w"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.X)) { if (upperCase) { output += "X"; } else { output += "x"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Y)) { if (upperCase) { output += "Y"; } else { output += "y"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Z)) { if (upperCase) { output += "Z"; } else { output += "z"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D1)) { if (upperCase) { output += "!"; } else { output += "1"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D2)) { if (upperCase) { output += "@"; } else { output += "2"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D3)) { if (upperCase) { output += "#"; } else { output += "3"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D4)) { if (upperCase) { output += "$"; } else { output += "4"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D5)) { if (upperCase) { output += "%"; } else { output += "5"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D6)) { if (upperCase) { output += "^"; } else { output += "6"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D7)) { if (upperCase) { output += "&"; } else { output += "7"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D8)) { if (upperCase) { output += "*"; } else { output += "8"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D9)) { if (upperCase) { output += "("; } else { output += "9"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D0)) { if (upperCase) { output += ")"; } else { output += "0"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Minus)) { if (upperCase) { output += "_"; } else { output += "-"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Equal)) { if (upperCase) { output += "+"; } else { output += "="; } }

            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.LBracket)) { if (upperCase) { output += "{"; } else { output += "["; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.RBracket)) { if (upperCase) { output += "}"; } else { output += "]"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Backslash)) { if (upperCase) { output += "|"; } else { output += "\\"; } }
            if (KBPS2.IsKeyDown(':')) { output += ":"; } if (KBPS2.IsKeyDown(';')) { output += ";"; }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Apostrophe)) { if (upperCase) { output += "\""; } else { output += "'"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Comma)) { if (upperCase) { output += "<"; } else { output += ","; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Period)) { if (upperCase) { output += ">"; } else { output += "."; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Slash)) { if (upperCase) { output += "?"; } else { output += "/"; } }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Backquote)) { if (upperCase) { output += "~"; } else { output += "`"; } }


            // functions
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Spacebar)) { output += " "; }
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Enter) && acceptNewLine) { output += "\n"; }

            // backspace
            if (KBPS2.IsKeyDown(Sys.ConsoleKeyEx.Backspace))
            {
                if (output.Length > 1) { output = output.Remove(output.Length - 1, 1); }
                else if (output.Length == 1) { output = string.Empty; }
            }
        }
    }
}
