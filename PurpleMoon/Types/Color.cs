using System;
using System.Collections.Generic;
using System.Text;
using PurpleMoon.Math;

namespace PurpleMoon.Types
{
    public class Color
    {
        public int r, g, b;

        // default constructor
        public Color() { this.r = 0; this.g = 0; this.b = 0; }

        // constructor
        public Color(int r, int g, int b) { this.r = r; this.g = g; this.b = b; }

        // conversions
        public Vector3 ToVector3() { return new Vector3(r, g, b); }
        public override string ToString() { return "r=" + r.ToString() + " g=" + g.ToString() + " b=" + b.ToString(); }
        public uint ToUint() { return (uint)(256 * 256 * r + 256 * g + b); }

        public static uint ColorToUint(Color c) { return c.ToUint(); }
        public static uint FromARGB(int rr, int gg, int bb) { return (uint)(256 * 256 * rr + 256 * gg + bb); }
        public static Color ToARGB(uint c)
        {
            byte[] values = BitConverter.GetBytes(c);
            if (!BitConverter.IsLittleEndian) Array.Reverse(values);
            Color myColor = new Color(values[0], values[1], values[2]);
            return myColor;
        }

        // shades
        public const uint black = 0;
        public const uint white = 16777215;
        public const uint gray32 = 2105376;
        public const uint gray64 = 4210752;
        public const uint gray96 = 6316128;
        public const uint gray160 = 10526880;
        public const uint silver = 12829635;

        // red
        public const uint red = 16711680;
        public const uint tomato = 14763560;
        public const uint pink = 14778496;
        public const uint maroon = 8388608;
        public const uint burgundy = 8388640;
        public const uint magenta = 16711935;

        // orange
        public const uint orange = 16744448;
        public const uint darkOrange = 12482560;
        public const uint yellow = 16776960;
        public const uint gold = 16766720;

        // blue
        public const uint blue = 255;
        public const uint darkBlue = 130;
        public const uint lightBlue = 8553215;
        public const uint teal = 32896;

        // brown
        public static uint brown { get { return Color.FromARGB(139, 69, 19); } }
        public static uint saddleBrown { get { return 9127187; } }

        // green
        public const uint green = 3316510;
        public const uint limeGreen = 65280;
    }
}
