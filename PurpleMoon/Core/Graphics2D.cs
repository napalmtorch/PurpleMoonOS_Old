// system libraries
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

// os libraries
using PurpleMoon.Hardware;
using PurpleMoon.Math;
using PurpleMoon.Types;

namespace PurpleMoon.Core
{
    public static class Graphics2D
    {
        public static int FONT_SPACING = 0;
        public static int driverMode = 0;

        // fill rectangle
        public static void FillRectangle(Rectangle bounds, uint c)
        {
            for (int i = 0; i < bounds.width * bounds.height; i++)
            {
                int xx = i % bounds.width;
                int yy = i / bounds.width;
                if (bounds.width > 0 && bounds.height > 0)
                {
                    if (driverMode == 0) { SVGA.SetPixel(bounds.x + xx, bounds.y + yy, c); }
                    else if (driverMode == 1) { VGA.SetPixel(bounds.x + xx, bounds.y + yy, Color.ToARGB(c)); }
                }
            }
        }
        public static void FillRectangle(int x, int y, int w, int h, uint c) { FillRectangle(new Rectangle(x, y, w, h), c); }

        // draw rectangle
        public static void DrawRectangle(Rectangle bounds, int s, uint c)
        {
            int size = (int)s;
            if (size > 0)
            {
                // horizontal
                FillRectangle(bounds.x, bounds.y, bounds.width, size, c);
                FillRectangle(bounds.x, bounds.y + bounds.height - size, bounds.width, size, c);
                // vertical
                FillRectangle(bounds.x, bounds.y + size, size, bounds.height - (size * 2), c);
                FillRectangle(bounds.x + bounds.width - size, bounds.y + size, size, bounds.height - (size * 2), c);
            }
        }

        public static void DrawRectangle(int x, int y, int w, int h, int size, uint c) { DrawRectangle(new Rectangle(x, y, w, h), size, c); }

        // draw line
        public static void DrawLine(Point p0, Point p1, uint c)
        {
            int x0 = p0.x;
            int y0 = p0.y;
            int x1 = p1.x;
            int y1 = p1.y;

            var dx = System.Math.Abs(x1 - x0);
            var dy = System.Math.Abs(y1 - y0);
            var sx = (x0 < x1) ? 1 : -1;
            var sy = (y0 < y1) ? 1 : -1;
            var err = dx - dy;

            while (true)
            {
                FillRectangle(x0, y0, 1, 1, c);

                if ((x0 == x1) && (y0 == y1)) break;
                var e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x0 += (int)sx; }
                if (e2 < dx) { err += dx; y0 += (int)sy; }
            }
        }

        // draw char
        public static void DrawChar(int x, int y, char c, uint col, Font font)
        {
            Rectangle b = new Rectangle(x, y, font.characterWidth, font.characterHeight);
            if (c == '!') { DrawBitmap(b, font.characters[FontCharIndex.exclamation], col, 0, true); }
            if (c == '"') { DrawBitmap(b, font.characters[FontCharIndex.quotation], col, 0, true); }
            if (c == '#') { DrawBitmap(b, font.characters[FontCharIndex.numberSign], col, 0, true); }
            if (c == '$') { DrawBitmap(b, font.characters[FontCharIndex.dollarSign], col, 0, true); }
            if (c == '%') { DrawBitmap(b, font.characters[FontCharIndex.percent], col, 0, true); }
            if (c == '&') { DrawBitmap(b, font.characters[FontCharIndex.ampersand], col, 0, true); }
            if (c == '\'') { DrawBitmap(b, font.characters[FontCharIndex.apostrophe], col, 0, true); }
            if (c == '(') { DrawBitmap(b, font.characters[FontCharIndex.bracketLeft], col, 0, true); }
            if (c == ')') { DrawBitmap(b, font.characters[FontCharIndex.bracketRight], col, 0, true); }
            if (c == '*') { DrawBitmap(b, font.characters[FontCharIndex.multiply], col, 0, true); }
            if (c == '+') { DrawBitmap(b, font.characters[FontCharIndex.add], col, 0, true); }
            if (c == ',') { DrawBitmap(b, font.characters[FontCharIndex.comma], col, 0, true); }
            if (c == '-') { DrawBitmap(b, font.characters[FontCharIndex.minus], col, 0, true); }
            if (c == '.') { DrawBitmap(b, font.characters[FontCharIndex.period], col, 0, true); }
            if (c == '/') { DrawBitmap(b, font.characters[FontCharIndex.slash], col, 0, true); }
            if (c == '1') { DrawBitmap(b, font.characters[FontCharIndex.n1], col, 0, true); }
            if (c == '2') { DrawBitmap(b, font.characters[FontCharIndex.n2], col, 0, true); }
            if (c == '3') { DrawBitmap(b, font.characters[FontCharIndex.n3], col, 0, true); }
            if (c == '4') { DrawBitmap(b, font.characters[FontCharIndex.n4], col, 0, true); }
            if (c == '5') { DrawBitmap(b, font.characters[FontCharIndex.n5], col, 0, true); }
            if (c == '6') { DrawBitmap(b, font.characters[FontCharIndex.n6], col, 0, true); }
            if (c == '7') { DrawBitmap(b, font.characters[FontCharIndex.n7], col, 0, true); }
            if (c == '8') { DrawBitmap(b, font.characters[FontCharIndex.n8], col, 0, true); }
            if (c == '9') { DrawBitmap(b, font.characters[FontCharIndex.n9], col, 0, true); }
            if (c == '0') { DrawBitmap(b, font.characters[FontCharIndex.n0], col, 0, true); }
            if (c == ':') { DrawBitmap(b, font.characters[FontCharIndex.colon], col, 0, true); }
            if (c == ';') { DrawBitmap(b, font.characters[FontCharIndex.semiColon], col, 0, true); }
            if (c == '<') { DrawBitmap(b, font.characters[FontCharIndex.arrowLeft], col, 0, true); }
            if (c == '=') { DrawBitmap(b, font.characters[FontCharIndex.equals], col, 0, true); }
            if (c == '>') { DrawBitmap(b, font.characters[FontCharIndex.arrowRight], col, 0, true); }
            if (c == '?') { DrawBitmap(b, font.characters[FontCharIndex.question], col, 0, true); }
            if (c == '@') { DrawBitmap(b, font.characters[FontCharIndex.at], col, 0, true); }
            if (c == 'A') { DrawBitmap(b, font.characters[FontCharIndex.capA], col, 0, true); }
            if (c == 'B') { DrawBitmap(b, font.characters[FontCharIndex.capB], col, 0, true); }
            if (c == 'C') { DrawBitmap(b, font.characters[FontCharIndex.capC], col, 0, true); }
            if (c == 'D') { DrawBitmap(b, font.characters[FontCharIndex.capD], col, 0, true); }
            if (c == 'E') { DrawBitmap(b, font.characters[FontCharIndex.capE], col, 0, true); }
            if (c == 'F') { DrawBitmap(b, font.characters[FontCharIndex.capF], col, 0, true); }
            if (c == 'G') { DrawBitmap(b, font.characters[FontCharIndex.capG], col, 0, true); }
            if (c == 'H') { DrawBitmap(b, font.characters[FontCharIndex.capH], col, 0, true); }
            if (c == 'I') { DrawBitmap(b, font.characters[FontCharIndex.capI], col, 0, true); }
            if (c == 'J') { DrawBitmap(b, font.characters[FontCharIndex.capJ], col, 0, true); }
            if (c == 'K') { DrawBitmap(b, font.characters[FontCharIndex.capK], col, 0, true); }
            if (c == 'L') { DrawBitmap(b, font.characters[FontCharIndex.capL], col, 0, true); }
            if (c == 'M') { DrawBitmap(b, font.characters[FontCharIndex.capM], col, 0, true); }
            if (c == 'N') { DrawBitmap(b, font.characters[FontCharIndex.capN], col, 0, true); }
            if (c == 'O') { DrawBitmap(b, font.characters[FontCharIndex.capO], col, 0, true); }
            if (c == 'P') { DrawBitmap(b, font.characters[FontCharIndex.capP], col, 0, true); }
            if (c == 'Q') { DrawBitmap(b, font.characters[FontCharIndex.capQ], col, 0, true); }
            if (c == 'R') { DrawBitmap(b, font.characters[FontCharIndex.capR], col, 0, true); }
            if (c == 'S') { DrawBitmap(b, font.characters[FontCharIndex.capS], col, 0, true); }
            if (c == 'T') { DrawBitmap(b, font.characters[FontCharIndex.capT], col, 0, true); }
            if (c == 'U') { DrawBitmap(b, font.characters[FontCharIndex.capU], col, 0, true); }
            if (c == 'V') { DrawBitmap(b, font.characters[FontCharIndex.capV], col, 0, true); }
            if (c == 'W') { DrawBitmap(b, font.characters[FontCharIndex.capW], col, 0, true); }
            if (c == 'X') { DrawBitmap(b, font.characters[FontCharIndex.capX], col, 0, true); }
            if (c == 'Y') { DrawBitmap(b, font.characters[FontCharIndex.capY], col, 0, true); }
            if (c == 'Z') { DrawBitmap(b, font.characters[FontCharIndex.capZ], col, 0, true); }
            if (c == '[') { DrawBitmap(b, font.characters[FontCharIndex.sqBracketL], col, 0, true); }
            if (c == '\\') { DrawBitmap(b, font.characters[FontCharIndex.backSlash], col, 0, true); }
            if (c == ']') { DrawBitmap(b, font.characters[FontCharIndex.sqBracketR], col, 0, true); }
            if (c == '^') { DrawBitmap(b, font.characters[FontCharIndex.upArrow], col, 0, true); }
            if (c == '_') { DrawBitmap(b, font.characters[FontCharIndex.underscore], col, 0, true); }
            if (c == '`') { DrawBitmap(b, font.characters[FontCharIndex.tilde], col, 0, true); }
            if (c == 'a') { DrawBitmap(b, font.characters[FontCharIndex.a], col, 0, true); }
            if (c == 'b') { DrawBitmap(b, font.characters[FontCharIndex.b], col, 0, true); }
            if (c == 'c') { DrawBitmap(b, font.characters[FontCharIndex.c], col, 0, true); }
            if (c == 'd') { DrawBitmap(b, font.characters[FontCharIndex.d], col, 0, true); }
            if (c == 'e') { DrawBitmap(b, font.characters[FontCharIndex.e], col, 0, true); }
            if (c == 'f') { DrawBitmap(b, font.characters[FontCharIndex.f], col, 0, true); }
            if (c == 'g') { DrawBitmap(b, font.characters[FontCharIndex.g], col, 0, true); }
            if (c == 'h') { DrawBitmap(b, font.characters[FontCharIndex.h], col, 0, true); }
            if (c == 'i') { DrawBitmap(b, font.characters[FontCharIndex.i], col, 0, true); }
            if (c == 'j') { DrawBitmap(b, font.characters[FontCharIndex.j], col, 0, true); }
            if (c == 'k') { DrawBitmap(b, font.characters[FontCharIndex.k], col, 0, true); }
            if (c == 'l') { DrawBitmap(b, font.characters[FontCharIndex.l], col, 0, true); }
            if (c == 'm') { DrawBitmap(b, font.characters[FontCharIndex.m], col, 0, true); }
            if (c == 'n') { DrawBitmap(b, font.characters[FontCharIndex.n], col, 0, true); }
            if (c == 'o') { DrawBitmap(b, font.characters[FontCharIndex.o], col, 0, true); }
            if (c == 'p') { DrawBitmap(b, font.characters[FontCharIndex.p], col, 0, true); }
            if (c == 'q') { DrawBitmap(b, font.characters[FontCharIndex.q], col, 0, true); }
            if (c == 'r') { DrawBitmap(b, font.characters[FontCharIndex.r], col, 0, true); }
            if (c == 's') { DrawBitmap(b, font.characters[FontCharIndex.s], col, 0, true); }
            if (c == 't') { DrawBitmap(b, font.characters[FontCharIndex.t], col, 0, true); }
            if (c == 'u') { DrawBitmap(b, font.characters[FontCharIndex.u], col, 0, true); }
            if (c == 'v') { DrawBitmap(b, font.characters[FontCharIndex.v], col, 0, true); }
            if (c == 'w') { DrawBitmap(b, font.characters[FontCharIndex.w], col, 0, true); }
            if (c == 'x') { DrawBitmap(b, font.characters[FontCharIndex.x], col, 0, true); }
            if (c == 'y') { DrawBitmap(b, font.characters[FontCharIndex.y], col, 0, true); }
            if (c == 'z') { DrawBitmap(b, font.characters[FontCharIndex.z], col, 0, true); }
            if (c == '{') { DrawBitmap(b, font.characters[FontCharIndex.crBracketL], col, 0, true); }
            if (c == '|') { DrawBitmap(b, font.characters[FontCharIndex.div], col, 0, true); }
            if (c == '}') { DrawBitmap(b, font.characters[FontCharIndex.crBracketR], col, 0, true); }
            if (c == '~') { DrawBitmap(b, font.characters[FontCharIndex.squiggle], col, 0, true); }
        }

        public static void DrawString(int x, int y, string txt, uint col, Font font)
        {
            if (txt.Length > 0 && font != null)
            {
                int xx = x, yy = y;
                foreach (char c in txt)
                {
                    DrawChar(xx, yy, c, col, font);

                    if (c == '\n') { yy += font.characterHeight + 1; xx = x; }
                    else { xx += font.characterWidth + FONT_SPACING; }
                }
            }
        }

        public static void DrawStringShadow(int x, int y, string txt, uint fg, uint bg, int dist, Font font)
        {
            DrawString(x + dist, y + dist, txt, bg, font);
            DrawString(x, y, txt, fg, font);
        }

        // draw bitmap
        public static void DrawBitmap(Rectangle bounds, Bitmap bitmap, uint transCol, bool transparency)
        {
            DrawBitmap(bounds, bitmap.data, transCol, transparency);
        }

        // draw bitmap - array
        public static void DrawBitmap(Rectangle bounds, uint[] data, uint transCol, bool transparency)
        {
            for (int i = 0; i < bounds.width * bounds.height; i++)
            {
                int xx = bounds.x + (i % bounds.width);
                int yy = bounds.y + (i / bounds.width);
                if (transparency)
                {
                    if (data[i] != transCol) 
                    {
                        if (driverMode == 0) { SVGA.SetPixel(xx, yy, data[i]); }
                        else if (driverMode == 1) { VGA.SetPixel(xx, yy, Color.ToARGB(data[i])); }
                    }
                }
                else 
                {
                    if (driverMode == 0) { SVGA.SetPixel(xx, yy, data[i]); }
                    else if (driverMode == 1) { VGA.SetPixel(xx, yy, Color.ToARGB(data[i])); }
                }
            }
        }

        // draw bitmap - colored
        public static void DrawBitmap(Rectangle bounds, uint[] data, uint color, uint transCol, bool transparency)
        {
            for (int i = 0; i < bounds.width * bounds.height; i++)
            {
                int xx = bounds.x + (i % bounds.width);
                int yy = bounds.y + (i / bounds.width);
                if (transparency)
                {
                    if (data[i] != transCol)
                    {
                        if (driverMode == 0) { SVGA.SetPixel(xx, yy, color); }
                        else if (driverMode == 1) { VGA.SetPixel(xx, yy, Color.ToARGB(color)); }
                    }
                }
                else { SVGA.SetPixel(xx, yy, color); }
            }
        }
    }
}
