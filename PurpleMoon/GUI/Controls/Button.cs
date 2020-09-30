// system libraries
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

// os libraries
using PurpleMoon.Core;
using PurpleMoon.Hardware;
using PurpleMoon.Math;
using PurpleMoon.Types;
using PurpleMoon.Processes;

namespace PurpleMoon.GUI
{
    public class Button : Control
    {
        private uint bgCurrent, fgCurrent;
        public TextAlign textAlign = TextAlign.middle;

        public Button(int x, int y, string text) : base(x, y, 92, 22, "button")
        {
            this.text = text;
            this.style = ControlStyles.DARK;
        }

        public override void Update()
        {
            // update base
            base.Update();
        }

        public override void Draw()
        {
            // get style colors
            uint bg = style.C_BG;
            uint fg = style.C_TEXT;
            uint bord = style.C_BORDER;
            uint bordOuter = style.C_BORDER_OUTER;
            uint bordInner = style.C_BORDER_INNER;
            uint fgShadow = style.C_TEXT_SHADOW;
            uint fgHover = style.C_TEXT_HOVER;
            uint fgDown = style.C_TEXT_DOWN;
            uint dis = style.C_DISABLED;
            uint shadow = style.C_SHADOW;
            uint hov = style.C_HOVER;
            uint dwn = style.C_DOWN;

            // get style sizes
            int bordSize = style.SIZE_BORDER;
            int shadowSize = style.SIZE_SHADOW;
            int fgShadowSize = style.SIZE_TEXT_SHADOW;
            int borderStyle = style.BORDER_STYLE;

            // current colors
            if (hover && !down) { bgCurrent = hov; fgCurrent = fgHover; }
            else if (hover && down) { bgCurrent = dwn; fgCurrent = fgDown; }
            else if (!hover && !down) { bgCurrent = bg; fgCurrent = fg; }
            if (!enabled) { bgCurrent = dis; }

            // draw bg
            Graphics2D.FillRectangle(bounds, bgCurrent);

            // draw border - fixed single
            if (borderStyle == 1 && bordSize > 0) { Graphics2D.DrawRectangle(bounds, bordSize, bord); }

            // draw text
            if (text.Length > 0)
            {
                Font f = Fonts.FONT_MONO;
                if (font == 3) { f = Fonts.FONT_SYMBOLS; }

                int textW = f.StringWidth(text);
                int xx = x + 4, yy = y + (height / 2) - (f.characterHeight / 2);
                if (textAlign == TextAlign.middle) { xx = x + (width / 2) - (textW / 2); }
                if (textAlign == TextAlign.left) { xx = x + 4; }
                if (textAlign == TextAlign.right) { xx = x + width - (textW + 4); }

                Graphics2D.DrawString(xx, yy, text, fgCurrent, f); 
            }
        }
    }
}
