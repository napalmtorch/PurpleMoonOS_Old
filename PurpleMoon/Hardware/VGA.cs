// system libraries
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

// os libraries
using PurpleMoon.Core;
using PurpleMoon.Math;
using PurpleMoon.Types;
using COL = System.Drawing.Color;

namespace PurpleMoon.Hardware
{
    public static class VGA
    {
        // driver
        public static Cosmos.HAL.VGADriver device;
        public static int width = 320, height = 200;

        // buffer
        public static Color[] buffer = new Color[width * height];
        public static bool refresh = false;

        // framerate
        private static int frames;
        public static float delta { get; private set; }
        private static int tick;
        public static string fpsString { get; private set; }
        public static string deltaString { get; private set; }
        private static int moveX = 0;

        public static void Initialize()
        {
            device = new Cosmos.HAL.VGADriver();
            device.SetGraphicsMode(Cosmos.HAL.VGADriver.ScreenSize.Size320x200, Cosmos.HAL.VGADriver.ColorDepth.BitDepth8);
            ClearBuffer(new Color(0, 0, 0));
            refresh = true;
        }

        public static void Update()
        {
            // draw buffer
            if (refresh)
            {
                for (int i = 0; i < width * height; i++)
                {
                    int x = i % width;
                    int y = i / width;
                    SetPixelRaw(x, y, buffer[i]);
                }
                ClearBuffer(new Color(128, 128, 128));
                refresh = false;
            }

            // get framerate
            if (frames > 0) { delta = (float)1000 / (float)frames; }
            int sec = Clock.GetSecond();
            if (tick != sec)
            {
                fpsString = "FPS: " + frames.ToString();
                if (delta.ToString().Length > 6)
                { deltaString = "DELTA: " + delta.ToString().Substring(0, 6) + "ms"; }
                else { deltaString = "DELTA: " + delta.ToString() + "ms"; }
                frames = 0;
                tick = sec;
            }
            frames++;
        }

        public static void ClearBuffer(Color c)
        {
            for (int i = 0; i < width * height; i++)
            {
                buffer[i] = c;
            }
        }

        public static bool SetPixel(int xx, int yy, Color c)
        {
            if (xx >= 0 && xx < width && yy >= 0 && yy < height)
            {
                buffer[xx + yy * width] = c;
                refresh = true;
                return true;
            }
            else { return false; }
        }

        public static void SetPixelRaw(int xx, int yy, Color c)
        {
            if (xx >= 0 && xx < width && yy >= 0 && yy < height)
            {
                device.SetPixel((uint)xx, (uint)yy, COL.FromArgb(c.r, c.g, c.b));
            }
        }
    }
}
