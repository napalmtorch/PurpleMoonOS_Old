// system libraries
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

// os libraries
using PurpleMoon.Hardware;
using PurpleMoon.Math;
using PurpleMoon.Types;
using PurpleMoon.Processes;

namespace PurpleMoon.GUI
{
    public class ControlManager
    {
        public List<Control> controls = new List<Control>();
        private int tabIndex = 0;

        public void Update()
        {
            for (int i = 0; i < controls.Count; i++)
            {
                // update
                if (controls[i].enabled && controls[i].visible) { controls[i].Update(); }

                // draw
                if (controls[i].visible) { controls[i].Draw(); }
            }
        }

        // toggle control usability
        public void DisableAll()
        {
            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].enabled = false;
                controls[i].Update();
            }
        }
        public void EnableAll()
        {
            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].enabled = true;
                controls[i].Update();
            }
        }

        // sets
        public void Add(Control ctrl) { controls.Add(ctrl); }
        public void Remove(Control ctrl) { controls.Remove(ctrl); }
        public void RemoveAt(int i) { controls.RemoveAt(i); }
        public void Clear() { controls.Clear(); }

        // gets
        public int GetCount() { return controls.Count; }
    }
}
