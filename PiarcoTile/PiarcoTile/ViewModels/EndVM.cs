using PiarcoTile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiarcoTile.ViewModels {
    public class EndVM {

        // Stats of the song the user finished
        public int Failed { get; set; }
        public int Bad { get; set; }
        public int Good { get; set; }
        public int Excellent { get; set; }
        public double Accuracy { get; set; }
        public Song Song { get; set; }

        public EndVM(int failed, int bad, int good, int excellent, double accuracy, Song song) {
            this.Failed = failed;
            this.Bad = bad;
            this.Good = good;
            this.Excellent = excellent;
            this.Accuracy = accuracy;
            this.Song = song;
        }
    }
}
