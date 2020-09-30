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
    public class Shell : SystemProcess
    {
        public ControlManager ctrlMgr;
        public Taskbar taskbar;
        public MenuMain menu;
        private Button menuBtn;
        private Label timeLabel;
        private Dialog shutdownDialog = new Dialog("Shutdown?", "Are you sure you want to shut down your PC?", DialogResult.yesNo);
        private Dialog restartDialog = new Dialog("Restart?", "Are you sure you want to restart your PC?", DialogResult.yesNo);
        private Dialog logoffDialog = new Dialog("Log Off?", "Are you sure you want to log out?", DialogResult.yesNo);

        public Shell() : base("desktop")
        {
            this.priority = ProcessPriority.high;
            this.topMost = true;
            this.onTaskbar = false;

            ctrlMgr = new ControlManager();

            shutdownDialog.name = "Shutdown";
            restartDialog.name = "Restart";
            logoffDialog.name = "Logoff";

            AddControls();
        }

        private void AddControls()
        {
            // taskbar
            taskbar = new Taskbar();
            ctrlMgr.Add(taskbar);

            // menu button
            menuBtn = new Button(taskbar.x + 2, taskbar.y + 2, "");
            menuBtn.width = 18;
            menuBtn.height = 18;
            ctrlMgr.Add(menuBtn);

            // menu
            menu = new MenuMain();
            ctrlMgr.Add(menu);

            // time label
            int w = Fonts.FONT_MONO.StringWidth("12:00 AM");
            timeLabel = new Label(taskbar.width - w - 8, 7, Clock.FormattedTime);
            timeLabel.textColor = Color.white;
            ctrlMgr.Add(timeLabel);
        }

        public override void Update()
        {
            timeLabel.text = Clock.FormattedTime;

            // toggle main menu
            if (menuBtn.down && !menuBtn.clicked) { menu.visible = !menu.visible; menuBtn.clicked = true; }

            // terminal
            if (menu.btnTerminal.down && !menu.btnTerminal.clicked) { ProcessManager.AddWindow(new Processes.Terminal(64, 64)); menu.btnTerminal.clicked = true; menu.visible = false; }

            // about
            if (menu.btnAbout.down && !menu.btnAbout.clicked) { ProcessManager.AddWindow(new Processes.AboutOS(64, 64)); menu.btnAbout.clicked = true; menu.visible = false; }

            // cli
            if (menu.btnCLI.down) { Kernel.RunTextMode(); }

            // shutdown result
            if (shutdownDialog.result == DialogResult.yes) { Sys.Power.Shutdown(); }
            if (shutdownDialog.result == DialogResult.no) { shutdownDialog.result = DialogResult.none; shutdownDialog.exitRequest = true; }

            // restart result
            if (restartDialog.result == DialogResult.yes) { Sys.Power.Reboot(); }
            if (restartDialog.result == DialogResult.no) { restartDialog.result = DialogResult.none; restartDialog.exitRequest = true; }

            // logoff dialog
            if (logoffDialog.result == DialogResult.yes)
            {
                ProcessManager.windows.Clear();
                Kernel.logon = new Logon();
                ProcessManager.AddProcess(Kernel.logon);
                exitRequest = true;
            }
            if (logoffDialog.result == DialogResult.no) { logoffDialog.result = DialogResult.none; logoffDialog.exitRequest = true; }

            // confirm logoff
            if (menu.btnLogoff.down && !menu.btnLogoff.clicked)
            {
                if (!ProcessManager.IsInstanceOpen(logoffDialog.name) && !ProcessManager.IsInstanceOpen(restartDialog.name) && !ProcessManager.IsInstanceOpen(shutdownDialog.name))
                {
                    logoffDialog.exitRequest = false;
                    logoffDialog.moving = false;
                    logoffDialog.result = DialogResult.none;
                    ProcessManager.AddWindow(logoffDialog);
                }
                menu.btnLogoff.clicked = true;
                menu.visible = false;
            }

            // confirm restart
            if (menu.btnRestart.down && !menu.btnRestart.clicked)
            {
                if (!ProcessManager.IsInstanceOpen(logoffDialog.name) && !ProcessManager.IsInstanceOpen(restartDialog.name) && !ProcessManager.IsInstanceOpen(shutdownDialog.name))
                {
                    restartDialog.exitRequest = false;
                    restartDialog.moving = false;
                    restartDialog.result = DialogResult.none;
                    ProcessManager.AddWindow(restartDialog);
                }
                menu.btnRestart.clicked = true;
                menu.visible = false;
            }

            // confirm shutdown
            if (menu.btnOff.down && !menu.btnOff.clicked)
            {
                if (!ProcessManager.IsInstanceOpen(logoffDialog.name) && !ProcessManager.IsInstanceOpen(restartDialog.name) && !ProcessManager.IsInstanceOpen(shutdownDialog.name))
                {
                    shutdownDialog.exitRequest = false;
                    shutdownDialog.moving = false;
                    shutdownDialog.result = DialogResult.none;
                    ProcessManager.AddWindow(shutdownDialog);
                }
                menu.btnOff.clicked = true;
                menu.visible = false;
            }

            // hide menu on click-away
            if (!menu.bounds.Contains(MSPS2.position) && !menuBtn.hover && !menu.hover)
            {
                if (MSPS2.state == Sys.MouseState.Left) { menu.visible = false; }
            }
        }

        public override void Draw()
        {
            // update/draw controls
            ctrlMgr.Update();

            // draw menu btn icon
            Graphics2D.DrawBitmap(new Rectangle(4, 4, 14, 14), Bitmap.MENU_ICON, Color.magenta, true);
        }

        public void DrawDebug()
        {
            int dx = 4, dy = SVGA.height - 13;

            Graphics2D.DrawString(dx, dy, SVGA.fpsString, Color.white, Fonts.FONT_MONO);
            dy -= 10;

            Graphics2D.DrawString(dx, dy, SVGA.deltaString, Color.white, Fonts.FONT_MONO);
            dy -= 10;

            Graphics2D.DrawString(dx, dy, "SEL_WIN: " + ProcessManager.selWindowID.ToString() + "  MOVE_WIN: " + ProcessManager.moveID.ToString(), Color.white, Fonts.FONT_MONO);
            dy -= 10;

            string cpu = Kernel.cpuUsage.ToString();
            if (cpu.Length > 5) { cpu = cpu.Substring(0, 5); }
            Graphics2D.DrawString(dx, dy, "CPU: " + cpu + "%   PROCESSES: " + ProcessManager.GetCount().ToString(), Color.white, Fonts.FONT_MONO);
            dy -= 10;

            Graphics2D.DrawString(dx, dy, "RAM TOTAL: " + Kernel.GetTotalRAM().ToString() + "MB", Color.white, Fonts.FONT_MONO);
            dy -= 10;

            string mem = "RAM: " + Kernel.GetUsedRAM().ToString() + "/" + Kernel.GetFreeRAM().ToString() + "MB";
            Graphics2D.DrawString(dx, dy, mem, Color.white, Fonts.FONT_MONO);
            dy -= 10;
        }
    }
}
