using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoon.Core
{
    public class CMD_CLEAR : CLICommand
    {
        public CMD_CLEAR()
        {
            names = new string[3] { "clear", "cls", "clr" };
            completed = false;
        }

        public override void Execute(string[] args)
        {
            // clear console
            CLI.Clear();

            // execute base
            base.Execute(args);
        }
    }
}
