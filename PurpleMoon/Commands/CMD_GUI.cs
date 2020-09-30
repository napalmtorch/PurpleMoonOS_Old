using PurpleMoon.Hardware;
using PurpleMoon.Processes;

namespace PurpleMoon.Core
{
    public class CMD_GUI : CLICommand
    {
        public CMD_GUI()
        {
            names = new string[1] { "gui" };
            completed = false;
        }

        public override void Execute(string[] args)
        {
            if (!SVGA.Initialize())
            {
                Kernel.graphical = false;
                CLI.WriteLine("[FATAL ERROR] could not switch graphics mode!");
            }
            else { Kernel.RunGraphical(); }

            // execute base
            base.Execute(args);
        }
    }
}
