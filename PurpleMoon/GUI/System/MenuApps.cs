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

namespace PurpleMoon.GUI
{
    public class MenuApps : Control
    {
        public List<Button> items = new List<Button>();
        public MenuApps() : base(0, 22, 160, 196, "appsmenu")
        {
            this.visible = false;
            AddButtons();
        }

        private void AddButtons()
        {
          
        }

        public override void Update()
        {
            // update buttons
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Update();
            }

            if (Kernel.shell != null) { this.height = Kernel.shell.menu.height; }

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

            // draw background
            Graphics2D.FillRectangle(bounds, bg);

            // draw border
            Graphics2D.DrawRectangle(bounds, 1, bord);

            // draw buttons
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Draw();
            }
        }
    }
}
