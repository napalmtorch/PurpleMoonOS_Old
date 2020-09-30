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
    public class MenuMain : Control
    {
        public List<Button> items = new List<Button>();
        public Button btnApps, btnFiles, btnSettings, btnTerminal, btnLogoff, btnRestart, btnOff, btnCLI, btnAbout;

        public MenuMain() : base(0, 22, 160, 196, "mainmenu")
        {
            this.visible = false;
            AddButtons();
        }

        private void AddButtons()
        {
            // applications button
            btnApps = new Button(x + 6, y + 6, "Applications       >");
            btnApps.width = width - 12; btnApps.height = 24;
            btnApps.style.SIZE_BORDER = 0;
            btnApps.textAlign = TextAlign.left;
            items.Add(btnApps);

            // file browser button
            btnFiles = new Button(x + 6, btnApps.y + btnApps.height, "File Browser");
            btnFiles.width = width - 12; btnFiles.height = 24;
            btnFiles.style.SIZE_BORDER = 0;
            btnFiles.textAlign = TextAlign.left;
            items.Add(btnFiles);

            // settings button
            btnSettings = new Button(x + 6, btnFiles.y + btnFiles.height, "Settings");
            btnSettings.width = width - 12; btnSettings.height = 24;
            btnSettings.style.SIZE_BORDER = 0;
            btnSettings.textAlign = TextAlign.left;
            items.Add(btnSettings);

            // terminal button
            btnTerminal = new Button(x + 6, btnSettings.y + btnSettings.height, "Terminal");
            btnTerminal.width = width - 12; btnTerminal.height = 24;
            btnTerminal.style.SIZE_BORDER = 0;
            btnTerminal.textAlign = TextAlign.left;
            items.Add(btnTerminal);

            // about button
            btnAbout = new Button(x + 6, btnTerminal.y + btnTerminal.height, "About...");
            btnAbout.width = width - 12; btnAbout.height = 24;
            btnAbout.style.SIZE_BORDER = 0;
            btnAbout.textAlign = TextAlign.left;
            items.Add(btnAbout);

            // cli button
            btnCLI = new Button(x + 6, btnAbout.y + btnAbout.height, "Return to CLI");
            btnCLI.width = width - 12; btnCLI.height = 24;
            btnCLI.style.SIZE_BORDER = 0;
            btnCLI.textAlign = TextAlign.left;
            items.Add(btnCLI);

            // log off button
            btnLogoff = new Button(x + 6, btnCLI.y + btnCLI.height, "Log Off...");
            btnLogoff.width = width - 12; btnLogoff.height = 24;
            btnLogoff.style.SIZE_BORDER = 0;
            btnLogoff.textAlign = TextAlign.left;
            items.Add(btnLogoff);

            // restart button
            btnRestart = new Button(x + 6, btnLogoff.y + btnLogoff.height, "Restart");
            btnRestart.width = width - 12; btnRestart.height = 24;
            btnRestart.style.SIZE_BORDER = 0;
            btnRestart.textAlign = TextAlign.left;
            items.Add(btnRestart);

            // shut down button
            btnOff = new Button(x + 6, btnRestart.y + btnRestart.height, "Shut Down");
            btnOff.width = width - 12; btnOff.height = 24;
            btnOff.style.SIZE_BORDER = 0;
            btnOff.textAlign = TextAlign.left;
            items.Add(btnOff);

        }

        public override void Update()
        {
            // update buttons
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Update();
            }

            this.height = (items.Count * 24) + 12;

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
