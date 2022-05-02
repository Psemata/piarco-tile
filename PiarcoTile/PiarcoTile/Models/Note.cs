using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PiarcoTile.Models
{
    public class Note {
        public double X { get; set; }
        public double Y { get; set; }
        public int Time { get; set; }

        public Note(int x, int y, int time) {
            FormatPosX(x);
            this.Y = y;
            this.Time = time;
        }

        public Note(Note note) {
            this.X = note.X;
            this.Y = note.Y;
            this.Time = note.Time;
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