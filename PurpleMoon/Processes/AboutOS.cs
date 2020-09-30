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
using PurpleMoon.GUI;

namespace PurpleMoon.Processes
{
    public class AboutOS : Window
    {
        private Button btnExit = new Button(0, 0, "Close");

        public AboutOS(int x, int y) : base("AboutOS", x, y, 334, 160)
        {
            this.title = "About";
            this.x = (SVGA.width / 2) - (width / 2);
            this.y = (SVGA.height / 2) - (height / 2);
            this.maximizeBox = false;
        }

        public override void Update()
        {
            UpdateWindow();
            base.Update();

            btnExit.Update();
            btnExit.x = x + width - btnExit.width - 8;
            btnExit.y = y + height - btnExit.height - 8;
            if (btnExit.down) { exitRequest = true; }
        }

        public override void Draw()
        {
            if (state != WindowState.minimized)
            {
                DrawWindow();

                // draw icon
                Graphics2D.DrawBitmap(new Rectangle(x + 4, y + 26, 32, 32), Bitmap.LOGO_32, Color.magenta, true);

                // draw title
                Graphics2D.DrawStringShadow(x + 40, y + 39, Kernel.OS_NAME, Color.white, Color.black, 1, Fonts.FONT_MONO_BIG);

                // draw version
                Graphics2D.DrawStringShadow(x + 4, y + 62, "Alpha " + Kernel.OS_VER, Color.silver, Color.black, 1, Fonts.FONT_MONO);

                // draw kernel version
                Graphics2D.DrawStringShadow(x + 4, y + 74, "Kernel: " + Kernel.KERNEL_VER, Color.silver, Color.black, 1, Fonts.FONT_MONO);

                // draw info
                Graphics2D.DrawStringShadow(x + 4, y + 90, Kernel.KERNEL_INFO, Color.white, Color.black, 1, Fonts.FONT_MONO);

                btnExit.Draw();
            }
        }
    }
}
