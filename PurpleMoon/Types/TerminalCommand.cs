using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoon.Core
{
    public abstract class TerminalCommand
    {
        public string[] names;
        public bool completed;

        public virtual void Execute(string[] args, Processes.Terminal term) { completed = true; }
    }
}
