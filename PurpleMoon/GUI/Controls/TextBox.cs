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
    public class TextBox : Control
    {
        public KBStringReader kbReader { get; private set; }
        public int textW;
        private bool cursor = true;
        private int tick = 0;
        private int prevSec;
        public bool passwordFilter = false;

        public TextBox(int x, int y, int w) : base(x, y, w, 22, "textbox")
        {
            kbReader = new KBStringReader();
        }

        public override void Update()
        {
            textW = Fonts.FONT_MONO.StringWidth(text);
            if (textW < 0) { textW = 0; }

            // get focus
            if (hover && down) { focus = true; }
            if (!hover && MSPS2.state == Sys.MouseState.Left) { focus = false; }

            // when focused
            if (focus)
            {
                tick = Clock.GetSecond();
                if (tick != prevSec)
                {
                    cursor = !cursor;
                    prevSec = tick;
                }

                if (textW < width - 8) { kbReader.GetInput(); }
                else
                {
                    int len = kbReader.output.Length;
                    if (len > 0) { kbReader.output = kbReader.output.Remove(len - 1, 1); }
                    text = kbReader.output;
                }
            }
            else { tick = 0; cursor = false; }

            text = kbReader.output;

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

            // draw bg
            Graphics2D.FillRectangle(bounds, bg);

            // draw border
            Graphics2D.DrawRectangle(bounds, bordSize, bord);

            // text
            if (text.Length > 0)
            {
                if (!passwordFilter) { Graphics2D.DrawString(x + 4, y + (height / 2) - 4, text, fg, Fonts.FONT_MONO); }
                else
                {
                    int sx = x + 4;
                    for (int i = 0; i < text.Length; i++)
                    {
                        Graphics2D.DrawChar(sx, y + (height / 2) - 4, '*', fg, Fonts.FONT_MONO);
                        sx += Fonts.FONT_MONO.characterWidth + Graphics2D.FONT_SPACING;
                    }
                }
            }

            if (cursor)
            {
                int xadd = textW;
                if (text.Length == 0) { xadd = 2; }
                Graphics2D.DrawChar(x + xadd + 2, y + (height / 2) - 4, '|', fg, Fonts.FONT_MONO);
            }
        }
    }
}
