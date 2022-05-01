using System;
using System.Collections.Generic;
using System.Text;

namespace PiarcoTile.Models
{
    public class Note
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Time { get; set; }

        public Note(int x, int y, int time)
        {
            this.X = x;
            this.Y = y;
            this.Time = time;
        }
    }
}