using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoon.Math
{
    public struct Point
    {
        // values
        public int x, y;

        // constructor
        public Point(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }

        // operations
        public void Add(Point p) { x += p.x; y += p.y; }
        public void Subtract(Point p) { x -= p.x; y -= p.y; }
        public void Multiply(Point p) { x *= p.x; y *= p.y; }
        public void Divide(Point p) { x /= p.x; y /= p.y; }
        public bool Equals(Point p) { return x == p.x && y == p.y; }

        // string
        public override string ToString() { return "x=" + x.ToString() + " y=" + y.ToString(); }

        // operators
        public static Point operator +(Point a, Point b) => new Point(a.x + b.x, a.y + b.y);
        public static Point operator -(Point a, Point b) => new Point(a.x - b.x, a.y - b.y);
        public static Point operator *(Point a, Point b) => new Point(a.x * b.x, a.y * b.y);
        public static Point operator /(Point a, Point b) => new Point(a.x / b.x, a.y / b.y);
        public static Point operator &(Point a, Point b) => new Point(a.x & b.x, a.y & b.y);
    }
}
