using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoon.Types
{
    public struct ControlStyle
    {
        // colors
        public uint C_BG;
        public uint C_TEXT;
        public uint C_TEXT_HOVER;
        public uint C_TEXT_DOWN;
        public uint C_TEXT_SHADOW;
        public uint C_HOVER;
        public uint C_DOWN;
        public uint C_DISABLED;
        public uint C_BORDER;
        public uint C_BORDER_OUTER;
        public uint C_BORDER_INNER;
        public uint C_SHADOW;

        // properties
        public int SIZE_BORDER;
        public int SIZE_TEXT_SHADOW;
        public int SIZE_SHADOW;
        public int BORDER_STYLE;
        public string NAME;

        // default constructor
        public ControlStyle(string NAME = "dark")
        {
            this.NAME = NAME;
            ControlStyle DARK = ControlStyles.DARK;
            C_BG = DARK.C_BG;
            C_TEXT = DARK.C_TEXT;
            C_TEXT_HOVER = DARK.C_TEXT_HOVER;
            C_TEXT_DOWN = DARK.C_TEXT_DOWN;
            C_TEXT_SHADOW = DARK.C_TEXT_SHADOW;
            C_HOVER = DARK.C_HOVER;
            C_DOWN = DARK.C_TEXT_DOWN;
            C_DISABLED = DARK.C_DISABLED;
            C_BORDER = DARK.C_BORDER;
            C_BORDER_OUTER = DARK.C_BORDER_OUTER;
            C_BORDER_INNER = DARK.C_BORDER_INNER;
            C_SHADOW = DARK.C_SHADOW;

            SIZE_BORDER = DARK.SIZE_BORDER;
            SIZE_TEXT_SHADOW = DARK.SIZE_TEXT_SHADOW;
            SIZE_SHADOW = DARK.SIZE_SHADOW;
            BORDER_STYLE = DARK.BORDER_STYLE;
        }
    }

    public static class ControlStyles
    {
        // dark
        public static ControlStyle DARK = new ControlStyle()
        {
            C_BG = Color.FromARGB(32, 32, 32),
            C_TEXT = Color.white,
            C_TEXT_HOVER = Color.black,
            C_TEXT_DOWN = Color.white,
            C_TEXT_SHADOW = Color.black,
            C_HOVER = Color.darkOrange,
            C_DOWN = Color.brown,
            C_DISABLED = Color.gray64,
            C_BORDER = Color.silver,
            C_BORDER_INNER = Color.black,
            C_BORDER_OUTER = Color.white,
            C_SHADOW = Color.black,

            SIZE_BORDER = 1,
            SIZE_TEXT_SHADOW = 1,
            SIZE_SHADOW = 0,
            BORDER_STYLE = 1,
        };

        // light
        public static ControlStyle LIGHT = new ControlStyle()
        {
            C_BG = Color.FromARGB(191, 184, 191),
            C_TEXT = Color.black,
            C_TEXT_HOVER = Color.white,
            C_TEXT_DOWN = Color.black,
            C_TEXT_SHADOW = Color.black,
            C_HOVER = Color.blue,
            C_DOWN = Color.darkBlue,
            C_DISABLED = Color.gray64,
            C_BORDER = Color.black,
            C_BORDER_INNER = Color.gray96,
            C_BORDER_OUTER = Color.white,
            C_SHADOW = Color.black,

            SIZE_BORDER = 1,
            SIZE_TEXT_SHADOW = 1,
            SIZE_SHADOW = 0,
            BORDER_STYLE = 1,
        };
    }
}
