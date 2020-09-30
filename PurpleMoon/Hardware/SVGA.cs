using Sys = Cosmos.System;
using Cosmos.HAL.Drivers.PCI.Video;
using PurpleMoon.Core;
using PurpleMoon.Types;
using System.Diagnostics;

namespace PurpleMoon.Hardware
{
    public static class SVGA
    {
        // driver
        public static VMWareSVGAII device;
        public const int width = 512, height = 320;

        // buffer
        public static uint[] buffer { get; private set; }
        public static uint color = Color.FromARGB(86, 34, 72);
        private static bool refresh = false;

        // framerate
        private static int frames;
        public static int fps { get; private set; }
        public static float delta { get; private set; }
        private static int tick;
        public static string fpsString { get; private set; }
        public static string deltaString { get; private set; }
        
        // init
        public static bool Initialize()
        {
            // init device
            device = new VMWareSVGAII();
            device.SetMode(width, height, 32);

            // init buffer
            buffer = new uint[width * height];

            refresh = true;

            // success
            return true;
        }

        // update
        public static void Update()
        {
            //delta = deltaOld;
            // draw buffer
            if (refresh)
            {
                for (int i = 0; i < width * height; i++)
                {
                    int x = i % width;
                    int y = i / width;
                    device.SetPixel((uint)x, (uint)y, buffer[i]);
                }

                // clear buffer
                ClearBuffer(color);
                refresh = false;
            }

            // update device
            device.Update(0, 0, width, height);

            // get framerate
            if (frames > 0) { delta = (float)1000 / (float)frames; }
            int sec = Clock.GetSecond();
            if (tick != sec)
            {
                fpsString = "FPS: " + frames.ToString();
                fps = frames;
                if (delta.ToString().Length > 6)
                { deltaString = "DELTA: " + delta.ToString().Substring(0, 6) + "ms"; }
                else { deltaString = "DELTA: " + delta.ToString() + "ms"; }
                frames = 0;
                tick = sec;
            }
            frames++;
        }

        // force update
        public static void ForceRefresh() { refresh = true; }

        // clear buffer
        public static void ClearBuffer(uint c)
        {
            for (int i = 0; i < width * height; i++)
            {
                if (buffer[i] != c) { buffer[i] = c; }
            }
        }

        // set pixel
        public static bool SetPixel(int x, int y, uint c)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                buffer[x + y * width] = c;
                refresh = true;
                return true;
            }
            else { return false; }
        }

        // set pixel
        public static bool SetPixelAlt(int i, uint c)
        {
            if (i < width * height)
            {
                buffer[i] = c;
                refresh = true;
                return true;
            }
            else { return false; }
        }

        // get pixel
        public static uint GetPixel(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return buffer[x + y * width];
            }
            else { return Color.black; }
        }
    }
}
