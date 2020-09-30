using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoon.Math
{
    public struct Vector3
    {
        // values
        public float x, y, z;

        // constructor
        public Vector3(float x = 0, float y = 0, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        // operations
        public void Add(Vector3 p) { x += p.x; y += p.y; z += p.z; }
        public void Subtract(Vector3 p) { x -= p.x; y -= p.y; z -= p.z; }
        public void Multiply(Vector3 p) { x *= p.x; y *= p.y; z *= p.z; }
        public void Divide(Vector3 p) { x /= p.x; y /= p.y; z /= p.z; }
        public bool Equals(Vector3 p) { return x == p.x && y == p.y && z == p.z; }

        // string
        public override string ToString() { return "x=" + x.ToString() + " y=" + y.ToString() + " z=" + z.ToString(); }

        // operators
        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vector3 operator *(Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        public static Vector3 operator /(Vector3 a, Vector3 b) => new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }
}
