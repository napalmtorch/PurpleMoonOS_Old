using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleMoon.Types
{
    public static class Cursors
    {
        // cursor data
        public static readonly uint[] arrow = new uint[12 * 20]
        {
             Color.black, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, 
             Color.black, Color.black, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.black, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.white, Color.black, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.white, Color.white, Color.black, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.white, Color.white, Color.white, Color.black, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.white, Color.white, Color.white, Color.white, Color.black, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.black, Color.magenta, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.black, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.black, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.black, Color.magenta,
             Color.black, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.black,
             Color.black, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.black, Color.black, Color.black, Color.black, Color.black,
             Color.black, Color.white, Color.white, Color.white, Color.black, Color.white, Color.white, Color.black, Color.magenta, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.white, Color.black, Color.magenta, Color.black, Color.white, Color.white, Color.black, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.white, Color.black, Color.magenta, Color.magenta, Color.black, Color.white, Color.white, Color.black, Color.magenta, Color.magenta, Color.magenta,
             Color.black, Color.black, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.black, Color.white, Color.white, Color.black, Color.magenta, Color.magenta,
             Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.black, Color.white, Color.white, Color.black, Color.magenta, Color.magenta,
             Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.black, Color.black, Color.magenta, Color.magenta, Color.magenta,
             Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta,
        };
    }

    public enum Cursor
    {
        arrow,
        hand,
        cross,
        loading,
    }
}
