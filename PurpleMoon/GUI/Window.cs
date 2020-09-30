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
using PurpleMoon.Processes;

namespace PurpleMoon.GUI
{
    public enum WindowState
    {
        normal,
        maximized,
        minimized,
    }

    public abstract class Window : Process
    {
        // dimensions
        public int x, xOld, y, yOld, width, widthOld, height, heightOld;
        public Rectangle bounds { get { return new Rectangle(x, y, width, height); } }
        public Rectangle boundsUsable { get { return new Rectangle(x, y + 20, width, height - 20); } }

        // properties
        public string title, tag;
        public bool enabled;
        public uint backColor = Color.gray64;

        private Button btnClose, btnMax, btnMin;
        private Rectangle tbBounds;

        // movement
        private Point startPosMS;
        private Point startPosWin;
        private bool clicked = false;
        public WindowState state = WindowState.normal;
        public WindowState prevState = WindowState.normal;
        public bool moving;
        public bool exitBox, minimizeBox, maximizeBox;
        public bool dialog = false;

        public Window(string name, int x, int y, int w, int h) : base(name)
        {
            this.exitBox = true;
            this.minimizeBox = true;
            this.maximizeBox = true;
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
            xOld = x; yOld = y; widthOld = width; heightOld = height;

            this.priority = ProcessPriority.high;
            this.type = ProcessType.window;
            this.title = name;

            // init buttons
            btnClose = new Button(999, 999, "1") { width = 20, height = 20 };
            btnClose.style.SIZE_BORDER = 0; btnClose.style.SIZE_TEXT_SHADOW = 0;
            btnClose.style.C_HOVER = Color.red; btnClose.style.C_DOWN = Color.maroon;
            btnClose.font = 3;

            btnMax = new Button(999, 999, "0") { width = 20, height = 20 };
            btnMax.style.SIZE_BORDER = 0; btnMax.style.SIZE_TEXT_SHADOW = 0;
            btnMax.font = 3;

            btnMin = new Button(999, 999, "/") { width = 20, height = 20 };
            btnMin.style.SIZE_BORDER = 0; btnMin.style.SIZE_TEXT_SHADOW = 0;
            btnMin.style.C_HOVER = Color.gold; btnMin.style.C_DOWN = Color.FromARGB(168, 128, 0);
            btnMin.font = 3;

            UpdateWindow();
        }

        public virtual void Update()
        {
           
        }

        public abstract void Draw();

        public void UpdateWindow()
        {
            if (state != WindowState.minimized && ProcessManager.selWindowID == id)
            {
                // get bounds
                tbBounds = new Rectangle(x, y, width, 20);

                int btnX = tbBounds.x + tbBounds.width - 20;
                if (exitBox)
                {
                    btnClose.Update();
                    btnClose.x = btnX;
                    btnClose.y = tbBounds.y;
                    btnX -= 20;
                    if (btnClose.down && !moving) { exitRequest = true; }
                }

                if (maximizeBox)
                {
                    btnMax.Update();
                    btnMax.x = btnX;
                    btnMax.y = tbBounds.y;
                    btnX -= 20;
                    if (btnMax.down && !btnMax.clicked && !moving)
                    {
                        if (state == WindowState.normal)
                        {
                            SetState(WindowState.maximized);
                            btnMax.text = "-";
                        }
                        else if (state == WindowState.maximized)
                        {
                            SetState(WindowState.normal);
                            btnMax.text = "0";
                        }
                        btnMax.clicked = true;
                    }
                }

                if (minimizeBox)
                {
                    btnMin.Update();
                    if (maximizeBox) { btnMin.x = btnMax.x - 20; }
                    else { btnMin.x = btnClose.x - 20; }
                    btnMin.y = tbBounds.y;
                    if (btnMin.down && !moving) { SetState(WindowState.minimized); }
                }

                // move click
                if (state != WindowState.maximized)
                {
                    if (tbBounds.Contains(MSPS2.position) && !btnClose.hover && !btnMax.hover && !btnMin.hover)
                    {
                        if (MSPS2.state == Sys.MouseState.Left)
                        {
                            if (!clicked)
                            {
                                startPosMS = new Point((int)MSPS2.x - x, (int)MSPS2.y - y);
                                startPosWin = new Point(x, y);
                                clicked = true;
                            }
                            moving = true;
                        }
                    }

                    // move window
                    int newX = x, newY = y;
                    if (moving)
                    {
                        newX = (int)MSPS2.x - startPosMS.x;
                        newY = (int)MSPS2.y - startPosMS.y;
                        x = newX;
                        y = newY;
                    }

                    if (x < -(width - 24)) { x = -(width - 24); }
                    if (y < 22) { y = 22; }

                    // move release
                    if (MSPS2.state == Sys.MouseState.None && moving) { xOld = x; yOld = y; widthOld = width; heightOld = height; }
                    if (MSPS2.state == Sys.MouseState.None) { moving = false; clicked = false; }
                }
            }
        }

        public void DrawWindow()
        {
            if (state == WindowState.maximized) { }

            if (state != WindowState.minimized)
            {
                // draw bg
                Graphics2D.FillRectangle(boundsUsable, backColor);
                Graphics2D.DrawRectangle(new Rectangle(boundsUsable.x, boundsUsable.y, boundsUsable.width, boundsUsable.height + 1), 1, Color.silver);

                // draw title bar
                Graphics2D.FillRectangle(tbBounds, Color.gray32);

                // draw title
                Graphics2D.DrawString(x + 8, y + 6, title, Color.white, Fonts.FONT_MONO);

                // draw buttons
                if (exitBox) { btnClose.Draw(); }
                if (maximizeBox) { btnMax.Draw(); }
                if (minimizeBox) { btnMin.Draw(); }

                // draw title bar border
                Graphics2D.DrawRectangle(tbBounds, 1, Color.silver);
            }
        }

        public void SetState(WindowState s)
        {
            if (s == WindowState.normal)
            {
                if (state != WindowState.minimized) { prevState = state; }
                xOld = x; yOld = y; widthOld = width; heightOld = height;
                state = s;
            }
            else if (s == WindowState.maximized)
            {
                if (state != WindowState.minimized) { prevState = state; }
                if (state == WindowState.normal) { xOld = x; yOld = y; widthOld = width; heightOld = height; }
                state = s;
                x = 0; y = 22; width = (int)SVGA.width; height = (int)SVGA.height - 23;
            }
            else if (s == WindowState.minimized)
            {
                if (state == WindowState.normal || state == WindowState.maximized) { xOld = x; yOld = y; widthOld = width; heightOld = height; }
                prevState = state;
                state = s;
            }

            UpdateWindow();
        }
    }
}
