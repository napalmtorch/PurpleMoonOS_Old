using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoon.Math
{
    public struct Vector2
    {
        // values
        public float x, y;

        // constructor
        public Vector2(float x = 0, float y = 0)
        {
            this.x = x;
            this.y = y;
        }

        // operations
        public void Add(Vector2 p) { x += p.x; y += p.y; }
        public void Subtract(Vector2 p) { x -= p.x; y -= p.y; }
        public void Multiply(Vector2 p) { x *= p.x; y *= p.y; }
        public void Divide(Vector2 p) { x /= p.x; y /= p.y; }
        public bool Equals(Vector2 p) { return x == p.x && y == p.y; }

        // string
        public override string ToString() { return "x=" + x.ToString() + " y=" + y.ToString(); }

        // operators
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
        public static Vector2 operator *(Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);
        public static Vector2 operator /(Vector2 a, Vector2 b) => new Vector2(a.x / b.x, a.y / b.y);
    }
}
