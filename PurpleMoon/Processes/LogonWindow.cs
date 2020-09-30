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
    public class LogonWindow : Window
    {
        public Button btnLogon, btnRestart, btnShutdown;
        public TextBox txtUser, txtPass;

        public LogonWindow() : base("Logon", 0, 0, 308, 140)
        {
            // init window
            this.x = (SVGA.width / 2) - (width / 2);
            this.y = 64;
            this.title = "PurpleMoon OS - Login";
            this.maximizeBox = false;
            this.minimizeBox = false;
            this.exitBox = false;

            // init buttons
            btnLogon = new Button(0, 0, "Log On");
            btnRestart = new Button(0, 0, "Restart");
            btnShutdown = new Button(0, 0, "Shutdown");

            // username textbox
            txtUser = new TextBox(0, 0, width - 96);

            // password textbox
            txtPass = new TextBox(0, 0, width - 96);
            txtPass.passwordFilter = true;
        }

        public override void Update()
        {
            // update base
            UpdateWindow();
            base.Update();

            // update button positions
            int btnX = x + width - (btnLogon.width * 3) - 24;
            btnLogon.x = btnX; btnX += 100;
            btnLogon.y = y + height - btnLogon.height - 8;

            btnRestart.x = btnX; btnX += 100;
            btnRestart.y = y + height - btnRestart.height - 8;

            btnShutdown.x = btnX;
            btnShutdown.y = y + height - btnShutdown.height - 8;

            txtUser.x = x + 88;
            txtUser.y = y + 30;

            txtPass.x = x + 88;
            txtPass.y = y + 64;

            // update buttons
            if (!ProcessManager.IsInstanceOpen(Kernel.logon.invalidDialog.name))
            {
                btnLogon.Update();
                btnRestart.Update();
                btnShutdown.Update();
                txtUser.Update();
                txtPass.Update();
            }

            // power options
            if (btnRestart.down) { Sys.Power.Reboot(); }
            if (btnShutdown.down) { Sys.Power.Shutdown(); }

            // attempt login
            if (btnLogon.down && !btnLogon.clicked)
            {
                if (txtUser.text == Logon.username && txtPass.text == Logon.password)
                {
                    Kernel.shell = new Shell();
                    ProcessManager.AddProcess(Kernel.shell);
                    Kernel.logon.exitRequest = true;
                    exitRequest = true;
                }
                else
                {
                    Kernel.logon.InvalidLogin();
                }
                btnLogon.clicked = true;
            }
        }

        public override void Draw()
        {
            // draw base
            DrawWindow();

            // draw buttons
            btnLogon.Draw();
            btnRestart.Draw();
            btnShutdown.Draw();
            txtUser.Draw();
            txtPass.Draw();

            // draw username text
            Graphics2D.DrawString(x + 12, y + 36, "Username:", Color.white, Fonts.FONT_MONO);

            // draw password text
            Graphics2D.DrawString(x + 12, y + 70, "Password:", Color.white, Fonts.FONT_MONO);
        }
    }
}
