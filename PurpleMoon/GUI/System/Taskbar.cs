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
    public class Taskbar : Control
    {
        private bool btnClick;
        private int tick = 0;
        uint inCol = Color.white;

        public Taskbar() : base(0, 0, SVGA.width, 22, "taskbar")
        {

        }

        public override void Update()
        {
            // update base
            base.Update();
        }

        public override void Draw()
        {
            // draw bg
            Graphics2D.FillRectangle(bounds, Color.gray32);

            // draw input indicator
            tick++;
            if (tick > 40)
            {
                if (!MSPS2.moving && KBPS2.currentKey == null) { inCol = Color.white; }
                tick = 0;
            }

            if (MSPS2.moving && KBPS2.currentKey == null) { inCol = Color.orange; }
            if (!MSPS2.moving && KBPS2.currentKey != null) { inCol = Color.limeGreen; }
            if (MSPS2.moving && KBPS2.currentKey != null) { inCol = Color.teal; }

            Graphics2D.DrawChar(width - 84, 7, '!', inCol, Fonts.FONT_SYMBOLS);

            // draw running apps
            int xx = 26, yy = 2, ww = 76, hh = 18;
            for (int i = 0; i < ProcessManager.windows.Count; i++)
            {
                Window proc = ProcessManager.windows[i];

                int tw = Fonts.FONT_MONO.StringWidth(proc.name) + 16;
                if (ww < tw) { ww = tw; }
                Rectangle btn = new Rectangle(xx, yy, ww, hh);
                uint ic = Color.silver;

                if (btn.Contains(MSPS2.position))
                {
                    ic = Color.darkOrange;
                    if (MSPS2.state == Cosmos.System.MouseState.Left && proc.minimizeBox)
                    {
                        if (!btnClick)
                        {
                            if (ProcessManager.selWindowID != proc.id)
                            {
                                ProcessManager.selWindowID = proc.id;
                            }
                            else
                            {
                                if (proc.state != WindowState.minimized) { proc.SetState(WindowState.minimized); }
                                else { proc.SetState(proc.prevState); }
                            }

                            btnClick = true;
                        }
                        ic = Color.brown;
                    }
                }
                if (MSPS2.state == Cosmos.System.MouseState.None) { btnClick = false; }
                // draw btn
                Graphics2D.DrawRectangle(btn, 1, ic);

                string txt = ProcessManager.windows[i].title;
                if (txt.Length > 9) { txt = txt.Substring(0, 7) + ".."; }

                Graphics2D.DrawString(xx + 8, yy + 4, ProcessManager.windows[i].title, ic, Fonts.FONT_MONO);

                // inc pos
                xx += ww + 4;
            }
        }
    }
}
