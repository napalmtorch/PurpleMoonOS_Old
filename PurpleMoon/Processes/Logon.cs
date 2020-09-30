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
    public class Logon : SystemProcess
    {
        public ControlManager ctrlMgr;
        public Dialog invalidDialog = new Dialog("ERROR", "You have entered an invalid username or password!", DialogResult.ok);

        public static bool loggedOn = false;
        public static string username = "root", password = "0000";

        private LogonWindow logonWindow;

        public Logon() : base("logon")
        {
            this.priority = ProcessPriority.high;
            this.topMost = true;
            this.onTaskbar = false;

            // init control manager
            ctrlMgr = new ControlManager();

            // get login data
            if (PMFAT.FileExists(@"0:\config.pmc"))
            {
                string fileData = PMFAT.ReadText(@"0:\config.pmc");
                string[] lines = fileData.Split('\n');

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] data = lines[i].Split(',');

                    if (data[0] == "user") { if (data.Length > 1) { username = data[1]; } }
                    if (data[0] == "pass") { if (data.Length > 1) { password = data[1]; } }
                }
            }

            username = username.Remove(username.Length - 1, 1);

            // init login window
            logonWindow = new LogonWindow();
            ProcessManager.AddWindow(logonWindow);
            logonWindow.txtUser.kbReader.output = username;

            // init invalid dialog
            invalidDialog.name = "InvalidLogin";
        }

        public override void Update()
        {
            if (invalidDialog != null)
            {
                if (ProcessManager.IsInstanceOpen(invalidDialog.name))
                {
                    logonWindow.moving = false;
                }
            }

            // restart result
            if (invalidDialog.btnOK.down)
            {
                logonWindow.txtPass.kbReader.output = "";
                logonWindow.txtPass.text = "";
                invalidDialog.result = DialogResult.none; 
                invalidDialog.exitRequest = true;
                invalidDialog.btnOK.down = false;
            }
        }

        public override void Draw()
        {
            // update/draw controls
            ctrlMgr.Update();

            // draw menu btn icon
            Graphics2D.DrawBitmap(new Rectangle(4, 4, 14, 14), Bitmap.MENU_ICON, Color.magenta, true);
            Graphics2D.DrawStringShadow(24, 6, "PurpleMoon OS", Color.white, Color.black, 1, Fonts.FONT_MONO_BIG);
        }

        public void InvalidLogin()
        {
            if (invalidDialog == null)
            {
                invalidDialog = new Dialog("ERROR", "You have entered an invalid username or password!", DialogResult.ok);
            }

            if (!ProcessManager.IsInstanceOpen(invalidDialog.name))
            {
                invalidDialog.exitRequest = false;
                invalidDialog.moving = false;
                invalidDialog.result = DialogResult.none;
                ProcessManager.AddWindow(invalidDialog);
            }
        }
    }
}
