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
    public class CheckBox : Control
    {
        private int txtWidth;
        private Rectangle box;
        public bool toggled;
        private uint bgCurrent, bordCurrent;
        private bool checkToggle;

        public CheckBox(int x, int y, string txt) : base(x, y, 13, 13, "checkbox")
        {
            this.text = txt;
        }

        public override void Update()
        {
            if (font == FontIndex.mono7x9)
            {
                txtWidth = Fonts.FONT_MONO.StringWidth(text);
                this.width = txtWidth + 16;
                this.height = Fonts.FONT_MONO.characterHeight;
            }
            else
            {
                txtWidth = Fonts.FONT_MONO.StringWidth(text);
                this.width = txtWidth + 16;
                this.height = Fonts.FONT_MONO.characterHeight;
            }

            box = new Rectangle(x, y, 13, 13);

            if (hover && down && !checkToggle) { toggled = !toggled; checkToggle = true; }
            if (!hover || !down) { checkToggle = false; }

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
            if (hover && !down) { bgCurrent = hov; bordCurrent = fgHover;  }
            else if (hover && down) { bgCurrent = dwn; bordCurrent = fgDown; }
            else if (!hover && !down)
            {
                if (!toggled) { bgCurrent = bg; }
                else { bgCurrent = bord; }
                bordCurrent = bord; 
            }
            if (!enabled) { bgCurrent = dis; }

            // draw box
            Graphics2D.FillRectangle(box, bgCurrent);

            // draw border
            Graphics2D.DrawRectangle(box, 1, bordCurrent);

            // draw text
            if (text.Length > 0 && this.txtWidth > 0)
            {
                if (fgShadowSize > 0)
                {
                    if (font == FontIndex.mono7x9) { Graphics2D.DrawStringShadow(x + box.width + 8, y + 2, text, fg, fgShadow, fgShadowSize, Fonts.FONT_MONO); }
                }
                else
                {
                    if (font == FontIndex.mono7x9) { Graphics2D.DrawString(x + box.width + 8, y + 2, text, fg, Fonts.FONT_MONO); }
                }
            }
        }
    }
}
