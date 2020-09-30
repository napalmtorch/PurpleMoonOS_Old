// system libraries
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.Core;

// os libraries
using PurpleMoon.Hardware;
using PurpleMoon.Math;
using PurpleMoon.Types;
using PurpleMoon.Processes;

namespace PurpleMoon.Core
{
    public class Kernel : Sys.Kernel
    {
        public static string OS_NAME = "PurpleMoon OS";
        public static string OS_VER = "1.1";
        public static string KERNEL_VER = "usrkit_07082020";
        public static string KERNEL_INFO = "This operating system was built using the open\nsource COSMOS kernel.";

        public static bool graphical = false;
        public static bool debugText = false;
        public static float cpuUsage = 0;

        public static Shell shell;
        public static Logon logon;

        protected override void BeforeRun()
        {
            CLI.Initialize();

            if (!SVGA.Initialize()) { CLI.WriteLine("[FATAL ERROR] could not initialize graphics mode!", ConsoleColor.Red); }

            if (!PMFAT.Initialize()) { Console.WriteLine("[FATAL ERROR] could not initialize fat driver!", ConsoleColor.Red); }
        }

        protected override void Run()
        {
            RunGraphical();

            if (!graphical) { CLI.GetInput(); }
        }

        public static void RunGraphical()
        {
            graphical = true;

            // init mouse
            MSPS2.Initialize();

            ProcessManager.Initialize();

            //shell = new Shell();
            //ProcessManager.AddProcess(shell);

            logon = new Logon();
            ProcessManager.AddProcess(logon);

            while (true)
            {
                // update clock
                Clock.Update();

                // update system processes
                ProcessManager.UpdateWindows();
                ProcessManager.UpdateSystemProcesses();

                // debug text
                if (debugText) { shell.DrawDebug(); }

                // update mouse
                MSPS2.Update();

                // update display   
                SVGA.Update();

                // get cpu usage
                cpuUsage = (float)((float)30 / (float)SVGA.fps) * (float)100;
                // manage keyboard
                KBPS2.Update();

                // force text mode
                if (KBPS2.CONTROL_DOWN && KBPS2.ALT_DOWN && KBPS2.IsKeyDown(Sys.ConsoleKeyEx.End))
                {
                    RunTextMode();
                }

                if (KBPS2.CONTROL_DOWN && KBPS2.IsKeyDown(Sys.ConsoleKeyEx.D)) { debugText = !debugText; }

                if (!graphical) { break; }
            }
        }

        public static void RunTextMode()
        {
            SVGA.device.Disable();
            ProcessManager.processes.Clear();
            ProcessManager.windows.Clear();
            graphical = false;
            CLI.GetInput();
        }

        public static int GetFreeRAM()
        {
            return GetTotalRAM() - GetUsedRAM();
        }
        public static int GetUsedRAM()
        {
            int usedRAM = (int)CPU.GetEndOfKernel() + 1024;
            return usedRAM / 1048576;
        }
        public static int GetTotalRAM()
        {
            return (int)CPU.GetAmountOfRAM();
        }
        public static int GetUsedRAMPercent()
        {
            return (GetUsedRAM() * 100) / GetTotalRAM();
        }
        public static int GetFreeRAMPercent()
        {
            return 100 - GetUsedRAMPercent();
        }

    }
}
