using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using KB = Cosmos.System.KeyboardManager;
using KEY = Cosmos.System.KeyEvent;


namespace PurpleMoon.Hardware
{
    public static class KBPS2
    {
        public static bool CAPS_LOCK = false;
        public static bool SHIFT_DOWN = false;
        public static bool ENTER_DOWN = false;
        public static bool ESCAPE_DOWN = false;
        public static bool CONTROL_DOWN = false;
        public static bool ALT_DOWN = false;
        public static KEY currentKey;
        public static KEY previousKey;

        public static void Update()
        {
            // get current key down
            if (KB.KeyAvailable)
            {
                if (KB.TryReadKey(out currentKey))
                {
                    previousKey = currentKey;
                }
            }
            else { currentKey = null; }

            // check enter
            if (currentKey.Key == Sys.ConsoleKeyEx.Enter) { ENTER_DOWN = true; } else { ENTER_DOWN = false; }

            // check control keys
            CAPS_LOCK = KB.CapsLock;
            SHIFT_DOWN = KB.ShiftPressed;
            CONTROL_DOWN = KB.ControlPressed;
            ALT_DOWN = KB.AltPressed;
        }

        public static bool IsKeyDown(Sys.ConsoleKeyEx key)
        {
            if (currentKey.Key == key) { return true; }
            else { return false; }
        }

        public static bool IsKeyDown(char key)
        {
            if (currentKey.KeyChar == key) { return true; }
            else { return false; }
        }

        public static bool IsKeyUp(Sys.ConsoleKeyEx key)
        {
            if (currentKey.Key != key) { return true; }
            else { return false; }
        }
    }
}
