using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PiarcoTile.Models
{
    public class Note {
        public double X { get; set; }
        public double Y { get; set; }
        public int TimeStart { get; set; }
        public int TimeHit { get; set; }

        public Note(int x, int y, int time) {
            FormatPosX(x);
            this.Y = -100;
            this.TimeHit = time;
            this.TimeStart = ComputeTimeStart();
        }

        public Note(Note note) {
            this.X = note.X;
            this.Y = note.Y;
            this.TimeHit = note.TimeHit;
            this.TimeStart = note.TimeStart;
        }

        private int ComputeTimeStart()
        {
            double y0 = -100.0;
            double height = Application.Current.MainPage.Height - 0.4;
            double y1 = height*(8.5/11)+0.2;
            int time2 = TimeHit;
            //10 toutes les 1/60 secondes donc on se déplace de 600px/s
            int time1 = (int)(time2 - (y1 - y0) / 600 * 1000);
            return time1+2000;
        }

        private void FormatPosX(int x) {
            double width = Application.Current.MainPage.Width;
            double tileWidth = width / 4;
            switch (x) {
                case 64:
                    this.X = 0.0;
                    break;
                case 192:
                    this.X = tileWidth;
                    break;
                case 320:
                    this.X = tileWidth * 2;
                    break;
                case 448:
                    this.X = tileWidth * 3;
                    break;
                default:
                    break;
            }
        }
    }
}