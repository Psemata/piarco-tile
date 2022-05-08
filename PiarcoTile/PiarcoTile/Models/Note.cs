using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PiarcoTile.Models
{
    public class Note {
        //X position in layout
        public double X { get; set; }
        //Y position in layout
        public double Y { get; set; }
        //Time in miliseconds when note needs to appear
        public int TimeStart { get; set; }
        //Time in miliseconds when note needs to be hit
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
        /// <summary>
        /// Function to compute the time when the note needs to appear depending on the time when it needs to be hit and its speed
        /// </summary>
        /// <returns></returns>
        private int ComputeTimeStart()
        {
            double y0 = -100.0;
            double height = Application.Current.MainPage.Height - 0.4;
            //Size of screen * first part of screen + size of separation line
            double y1 = height*(8.5/11)+0.2;
            int time2 = TimeHit;
            //10 every 1/60 of a second so we move of 600px/s
            // x1 = v*t3 + x0 => t3 = (x1-x0)/v so t0 = t2-t3
            int time1 = (int)(time2 - (y1 - y0) / 600 * 1000);
            return time1;
        }
        /// <summary>
        /// In osu! mania the position of the the 4 columns are based on pixels so we convert it for our positions
        /// </summary>
        /// <param name="x"></param>
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