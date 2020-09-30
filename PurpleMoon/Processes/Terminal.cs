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
using PurpleMoon.GUI;

namespace PurpleMoon.Processes
{
    public class Terminal : Window
    {
        public static int xCount = 53;
        public static int yCount = 19;
        public TerminalChar[] output = new TerminalChar[xCount * yCount];

        // cursor
        public int cursorX = 0;
        public int cursorY = 0;
        private bool cursor = false;
        private int tick = 0;
        private int prevSec;

        // input
        public bool reading = false;
        public KBStringReader kbReader = new KBStringReader();
        public string input = "";
        private string inputOld;
        private int lastX;
        public List<TerminalCommand> commands = new List<TerminalCommand>();
        private WindowState oldState;

        public Terminal(int x, int y) : base("Terminal", x, y, 384, 224)
        {
            this.title = "Terminal";
            this.backColor = Color.black;
            WriteLine("PurpleMoon Terminal", Color.FromARGB(0, 255, 255));
            Write("shell:>", Color.yellow);
            lastX = cursorX;

            commands.Add(new TERM_CLEAR());
        }

        public override void Update()
        {
            // update base
            UpdateWindow();
            base.Update();

            if (state != WindowState.minimized)
            {
                if (ProcessManager.selWindowID == id)
                {
                    tick = Clock.GetSecond();
                    if (tick != prevSec)
                    {
                        cursor = !cursor;
                        prevSec = tick;
                    }
                }

                if (oldState != state)
                {
                    int newX = (width - 16) / 7;
                    int newY = (height - 20 - 16) / 11;
                    TerminalChar[] termNew = new TerminalChar[newX * newY];

                    if (state == WindowState.maximized)
                    {
                        for (int xx = 0; xx < xCount; xx++)
                        {
                            for (int yy = 0; yy < yCount; yy++)
                            {
                                termNew[xx + (yy * newX)] = output[xx + (yy * xCount)];
                            }
                        }
                    }
                    else if (state == WindowState.normal)
                    {
                        for (int xx = 0; xx < newX; xx++)
                        {
                            for (int yy = 0; yy < newY; yy++)
                            {
                                termNew[xx + (yy * newX)] = output[xx + (yy * xCount)];
                            }
                        }
                    }
    
                    xCount = newX;
                    yCount = newY;
                    output = termNew;
                    oldState = state;
                }

                if (reading)
                {
                    // get input
                    kbReader.GetInput();
                    input = kbReader.output;

                    // white input to char array
                    if (input != inputOld)
                    {
                        cursorX = lastX;
                        for (int nx = cursorX; nx < xCount; nx++) { output[nx + (cursorY * xCount)] = null; }
                        foreach (char c in input)
                        {
                            PutCharNext(c, Color.white);
                        }
                    }

                    // max length
                    if (input.Length >= xCount - 7) { input = input.Remove(input.Length - 1, 1); kbReader.output = input; }

                    // set old input
                    inputOld = input;

                    // check enter press
                    if (KBPS2.ENTER_DOWN)
                    {
                        ParseCommand(input);
                        kbReader.output = "";
                        input = "";
                    }
                }
                else
                {
                    kbReader.output = "";
                    input = "";
                }
            }
        }

        public override void Draw()
        {
            if (state != WindowState.minimized)
            {
                DrawWindow();

                for (int xx = 0; xx < xCount; xx++)
                {
                    for (int yy = 0; yy < yCount; yy++)
                    {
                        int i = xx + (yy * xCount);
                        if (output[i] != null)
                        {
                            int dx = x + 8 + (xx * (Fonts.FONT_MONO.characterWidth + Graphics2D.FONT_SPACING));
                            int dy = y + 28 + (yy * (Fonts.FONT_MONO.characterHeight + 1));
                            Graphics2D.DrawChar(dx, dy, output[i].character, output[i].foreColor, Fonts.FONT_MONO);
                        }
                    }
                }

                if (cursor)
                {
                    int cx = x + 8, cy = y + 28;
                    if (cursorX > 0) { cx = x + 8 + (cursorX * (Fonts.FONT_MONO.characterWidth + Graphics2D.FONT_SPACING)); }
                    if (cursorY > 0) { cy = y + 28 + (cursorY * (Fonts.FONT_MONO.characterHeight + 1)); }
                    Graphics2D.DrawChar(cx, cy, '_', Color.white, Fonts.FONT_MONO);
                }
            }
        }

        public void ParseCommand(string cmd)
        {
            reading = false;
            cursorX = 0;
            cursorY++;
            if (cursorY >= yCount) { Scroll(); }


            string[] pos = cmd.Split(' ');
            bool exec = false;

            for (int i = 0; i < commands.Count; i++)
            {
                for (int j = 0; j < commands[i].names.Length; j++)
                {
                    if (pos[0].ToLower() == commands[i].names[j])
                    {
                        commands[i].Execute(pos, this);
                        exec = true;
                    }
                }
            }

            if (!exec) { WriteLine("Invalid command!", Color.tomato); }

            // reset input position
            Write("shell:>", Color.yellow);
            lastX = cursorX;
            if (cursorY >= yCount) { Scroll(); }
            reading = true;
        }

        public void Clear()
        {
            for (int xx = 0; xx < xCount; xx++)
            {
                for (int yy = 0; yy < yCount; yy++)
                {
                    output[xx + (yy * xCount)] = null;
                }
            }
            cursorX = 0;
            cursorY = 0;
        }

        public void WriteLine(string txt, uint fg)
        {
            foreach (char c in txt)
            {
                TerminalChar ch = new TerminalChar() { character = c, backColor = backColor, foreColor = fg };
                output[cursorX + (cursorY * xCount)] = ch;
                cursorX++;
                if (cursorX >= xCount) { cursorX = 0; cursorY++; }
            }
            cursorX = 0;
            cursorY++;
            if (cursorY >= yCount) { Scroll(); }
            reading = true;
        }
        public void Write(string txt, uint fg)
        {
            foreach (char c in txt)
            {
                TerminalChar ch = new TerminalChar() { character = c, backColor = backColor, foreColor = fg };
                output[cursorX + (cursorY * xCount)] = ch;
                cursorX++;
                if (cursorX >= xCount) { cursorX = 0; cursorY++; }
            }
            if (cursorY >= yCount) { Scroll(); }
            reading = true;
        }
        public void PutCharNext(char c, uint fg)
        {
            TerminalChar ch = new TerminalChar() { character = c, backColor = backColor, foreColor = fg };
            output[cursorX + (cursorY * xCount)] = ch;
            cursorX++;
            if (cursorY >= yCount) { Scroll(); }
        }
        public void PutString(int x, int y, string txt, uint fg)
        {
            int ox = cursorX, oy = cursorY;
            cursorX = x;
            cursorY = y;
            Write(txt, fg);
            cursorX = ox;
            cursorY = oy;
        }

        public void Scroll()
        {
            TerminalChar[] newOutput = new TerminalChar[xCount * yCount];
            cursorY = yCount - 1;

            for (int xx = 0; xx < xCount; xx++)
            {
                for (int yy = 0; yy < yCount; yy++)
                {
                    newOutput[xx + (yy * xCount)] = output[xx + (((yy + 1) % yCount) * xCount)];
                }
            }
            output = newOutput;
        }
    }
}
