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
    public abstract class Control
    {
        // dimensions
        public int x, y, width, height;
        public Rectangle bounds { get { return new Rectangle(x, y, width, height); } }

        // properties
        public int id;
        public string name, text, tag;
        public bool enabled, visible;
        public bool hover { get; protected set; }
        public bool down;
        public bool clicked, focus;
        // style
        public ControlStyle style;
        public int font;

        // constructor
        public Control(int x, int y, int w, int h, string name)
        {
            // dimensions
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;

            // properties
            this.name = name;
            this.text = string.Empty;
            this.tag = string.Empty;
            this.enabled = true;
            this.visible = true;
            this.font = FontIndex.mono7x9;

            // style
            this.style = ControlStyles.DARK;
        }

        // update
        public virtual void Update()
        {
            if (bounds.Contains(MSPS2.position))
            {
                hover = true;
                if (MSPS2.state == Sys.MouseState.Left) { down = true; }
                if (MSPS2.state == Sys.MouseState.None) { down = false; clicked = false; }
            }
            else { hover = false; down = false; clicked = false; }
        }

        // draw
        public abstract void Draw();
    }
}
