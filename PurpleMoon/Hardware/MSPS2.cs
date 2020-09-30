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

namespace PurpleMoon.Hardware
{
    public static class MSPS2
    {
        // dimensions
        public static ushort x { get { return (ushort)Sys.MouseManager.X; } }
        public static ushort y { get { return (ushort)Sys.MouseManager.Y; } }
        public static Point position { get { return new Point((ushort)Sys.MouseManager.X, (ushort)Sys.MouseManager.Y); } }
        public static Point positionOld;
        public static bool moving { get; private set; }

        // state
        public static Sys.MouseState state { get { return Sys.MouseManager.MouseState; } }
        public static Sys.MouseState stateOld { get; private set; }
        public static Cursor cursor = Cursor.arrow;

        // init
        public static bool Initialize()
        {
            Sys.MouseManager.ScreenWidth = (uint)SVGA.width;
            Sys.MouseManager.ScreenHeight = (uint)SVGA.height;

            return true;
        }

        public static void Update()
        {
            // previous state
            if (stateOld != state) { stateOld = state; }

            // get cursor type
            uint[] cursorData = Cursors.arrow;
            if (cursor == Cursor.arrow) { cursorData = Cursors.arrow; }

            if (position.x != positionOld.x && position.y != positionOld.y) { moving = true;  }
            else { moving = false; }

            positionOld.x = position.x;
            positionOld.y = position.y;

            // draw cursor
            Graphics2D.DrawBitmap(new Rectangle(x, y, 12, 20), cursorData, Color.magenta, true);
        }
    }
}
