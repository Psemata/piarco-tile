using System;
using System.Collections.Generic;
using System.Text;

namespace PiarcoTile.Models
{
    public class Note
    {
        int X { get; set; }
        int Y { get; set; }
        int Time { get; set; }

        public Note(int x, int y, int time)
        {
            this.X = x;
            this.Y = y;
            this.Time = time;
        }
    }
}