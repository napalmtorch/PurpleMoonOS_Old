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
    public class Label : Control
    {
        private Point textSize;
        public uint backColor { get { return this.style.C_BG; } set { this.style.C_BG = value; } }
        public uint textColor { get { return this.style.C_TEXT; } set { this.style.C_TEXT = value; } }

        public Label(int x, int y, string txt) : base(x, y, 9, 9, "label")
        {
            this.text = txt;
            this.style.SIZE_BORDER = 0;
            this.backColor = SVGA.color;
        }

        public override void Update()
        {
            int lines = 1;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n') { lines++; }
            }

            int h = lines * (Fonts.FONT_MONO.characterHeight + 1);

            textSize = new Point(Fonts.FONT_MONO.StringWidth(text), h);
            this.width = textSize.x / 2;
            this.height = textSize.y;

            // update base
            base.Update();
        }

        public override void Draw()
        {
            // draw bg
            if (backColor != SVGA.color) { Graphics2D.FillRectangle(bounds, backColor); }

            // draw border
            int bs = this.style.SIZE_BORDER;
            uint bord = this.style.C_BORDER;
            if (bs > 0) { Graphics2D.DrawRectangle(bounds, bs, bord); }

            // draw text
            int shadSize = this.style.SIZE_TEXT_SHADOW;
            uint shadCol = this.style.C_TEXT_SHADOW;
            if (text.Length > 0)
            {
                if (shadSize > 0 && shadCol != SVGA.color) { Graphics2D.DrawStringShadow(x, y, text, textColor, shadCol, shadSize, Fonts.FONT_MONO); }
                else { Graphics2D.DrawString(x, y, text, textColor, Fonts.FONT_MONO); }
            }
        }
    }
}
