using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoon.Math
{
    public struct Rectangle
    {
        // values
        public int x, y, width, height;

        // constructor
        public Rectangle(int x = 0, int y = 0, int w = 1, int h = 1)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
        }

        // collisions
        public bool Contains(Point pos) { return pos.x >= x && pos.y >= y && pos.x < x + width && pos.y < y + height; }
        public bool Intersects(Rectangle rect) { return rect.x >= x && rect.y >= y && rect.x < x + width && rect.y < y + height; }
    }
}
