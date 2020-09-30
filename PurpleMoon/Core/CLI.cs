using System;
using System.Collections.Generic;
using System.Text;
namespace PurpleMoon.Core
{
    public static class CLI
    {
        public static ConsoleColor backColor = ConsoleColor.Black;
        public static ConsoleColor foreColor = ConsoleColor.White;

        public static List<CLICommand> commands = new List<CLICommand>();

        // init
        public static void Initialize()
        {
            // startup message
            Console.Clear();
            WriteLine("PurpleMoon OS", ConsoleColor.Cyan);

            // add commands
            WriteLine("Initializing command library...");
            commands.Add(new CMD_CLEAR());
            commands.Add(new CMD_GUI());
        }

        // get input
        public static void GetInput()
        {
            Write("shell:>");

            string input = Console.ReadLine();
            string[] pos = input.Split(' ');
            bool exec = false;

            for (int i = 0; i < commands.Count; i++)
            {
                for (int j = 0; j < commands[i].names.Length; j++)
                {
                    if (pos[0].ToLower() == commands[i].names[j])
                    {
                        commands[i].Execute(pos);
                        exec = true;
                    }
                }
            }

            if (!exec) { WriteLine("Invalid command!", ConsoleColor.Red); }

            GetInput();
        }

        // write
        public static void WriteLine(string txt) { Console.WriteLine(txt); }
        public static void WriteLine(string txt, ConsoleColor fg) { Console.ForegroundColor = fg; Console.WriteLine(txt); Console.ForegroundColor = foreColor; }
        public static void WriteLine(string txt, ConsoleColor fg, ConsoleColor bg)
        {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.Write(txt);
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
        }
        public static void Write(string txt) { Console.Write(txt); }
        public static void Write(string txt, ConsoleColor fg) { Console.ForegroundColor = fg; Console.Write(txt); Console.ForegroundColor = foreColor; }
        public static void Write(string txt, ConsoleColor fg, ConsoleColor bg)
        {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.Write(txt);
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
        }
        public static void Clear() { Console.Clear(); }
        public static void Clear(ConsoleColor c) { backColor = c; Console.BackgroundColor = backColor; Console.Clear(); }

        // read
        public static int Read() { return Console.Read(); }
        public static string ReadLine() { return Console.ReadLine(); }

        // colors
        public static void SetFG(ConsoleColor fg) { foreColor = fg; }
        public static void SetBG(ConsoleColor bg) { backColor = bg; }
    }
}
