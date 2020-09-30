// system libraries
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

// os libraries
using PurpleMoon.Hardware;
using PurpleMoon.Math;
using PurpleMoon.Types;
using PurpleMoon.GUI;

namespace PurpleMoon.Core
{
    public static class ProcessManager
    {
        public static List<SystemProcess> processes = new List<SystemProcess>();
        public static List<Window> windows = new List<Window>();
        public static int tick { get; private set; } = 0;
        public static bool update = true;
        public static int moveID = 999;
        public static int selWindowID = 999;

        public static void Initialize() { }

        public static void UpdateSystemProcesses()
        {
            if (update)
            {
                tick++;

                for (int i = 0; i < processes.Count; i++)
                {
                    processes[i].id = i;
                    if (processes[i].priority == ProcessPriority.low) { if (tick > 2) { processes[i].Update(); } }
                    if (processes[i].priority == ProcessPriority.normal) { if (tick <= 1) { processes[i].Update(); } }
                    if (processes[i].priority == ProcessPriority.high) { processes[i].Update(); }

                    // draw
                    if (!processes[i].topMost) { processes[i].Draw(); }

                    if (tick > 2) { tick = 0; }
                }

                for (int i = 0; i < processes.Count; i++)
                {
                    if (processes[i].topMost) { processes[i].Draw(); }

                    // check exit
                    if (processes[i].exitRequest)
                    {
                        processes[i].exitRequest = false;
                        processes.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public static void UpdateWindows()
        {
            moveID = 10000;
            if (selWindowID > windows.Count) { selWindowID = windows.Count; }
            if (update)
            {
                if (selWindowID >= 0 && selWindowID < windows.Count && windows.Count > 0)
                {
                    if (!windows[selWindowID].moving && moveID == selWindowID) { moveID = 10000; }
                    else if (windows[selWindowID].moving && moveID != selWindowID) { moveID = selWindowID; }
                }

                for (int i = 0; i < windows.Count; i++)
                {
                    windows[i].id = i;

                    // check moving window
                    if (windows[i].bounds.Contains(MSPS2.position))
                    {
                        if (MSPS2.state == Sys.MouseState.Left)
                        {
                            if (selWindowID >= 0 && selWindowID < windows.Count)
                            {
                                if (!windows[selWindowID].bounds.Contains(MSPS2.position)) { selWindowID = i; }
                            }
                        }
                    }

                    if (selWindowID != i) { windows[i].moving = false; }

                    if (moveID >= 999)
                    {
                        if (windows[i].priority == ProcessPriority.low) { if (tick > 2) { windows[i].Update(); } }
                        if (windows[i].priority == ProcessPriority.normal) { if (tick <= 1) { windows[i].Update(); } }
                        if (windows[i].priority == ProcessPriority.high) { windows[i].Update(); }
                    }
                    else if (moveID >= 0 && moveID < windows.Count) { windows[moveID].Update(); }

                    // draw
                    if (i != selWindowID) { windows[i].Draw(); }

                    // check exit
                    if (windows[i].exitRequest)
                    {
                        windows[i].exitRequest = false;
                        windows.RemoveAt(i);
                        i--;
                        if (selWindowID > 0) { selWindowID--; }
                    }
                }
            }

            if (selWindowID >= 0 && selWindowID < windows.Count && windows.Count > 0)
            {
                windows[selWindowID].Draw();
            }
        }

        public static void EndProcess(uint i) { processes.RemoveAt((int)i); }
        public static void EndProcess(SystemProcess p) { processes.Remove(p); }

        public static void CloseWindow(int i) { windows.RemoveAt(i); }
        public static void CloseWindow(Window win) { windows.Remove(win); }

        public static void AddProcess(SystemProcess proc) { processes.Add(proc); }
        public static void AddWindow(Window win) { windows.Add(win); selWindowID = windows.Count - 1; }

        public static int GetCount() { return processes.Count + windows.Count; }

        public static bool IsInstanceOpen(string name)
        {
            bool value = false;
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].name == name)
                {
                    value = true;
                    break;
                }
                else { value = false; }
            }

            return value;
        }
    }
}
