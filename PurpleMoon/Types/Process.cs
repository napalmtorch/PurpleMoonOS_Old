using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoon.Types
{
    public enum ProcessType
    {
        normal,
        system,
        window,
    }

    public abstract class Process
    {
        // properties
        public int id;
        public string name;
        public ProcessPriority priority;
        public ProcessType type;
        public bool exitRequest = false;

        // constructor
        public Process(string name)
        {
            this.name = name;
            this.type = ProcessType.normal;
        }
    }

    public abstract class SystemProcess : Process
    {
        // properties
        public bool topMost;
        public bool onTaskbar;

        public SystemProcess(string name) : base(name)
        {
            this.topMost = true;
            this.onTaskbar = false;
            this.type = ProcessType.system;
        }

        public abstract void Update();
        public abstract void Draw();
    }
}
