using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoon.Core
{
    public class TERM_CLEAR : TerminalCommand
    {
        public TERM_CLEAR()
        {
            names = new string[3] { "clear", "cls", "clr" };
            completed = false;
        }

        public override void Execute(string[] args, Processes.Terminal term)
        {
            // clear console
            term.Clear();

            // execute base
            base.Execute(args, term);
        }
    }
}
