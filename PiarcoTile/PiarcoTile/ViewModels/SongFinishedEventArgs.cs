using System;
using System.Collections.Generic;
using System.Text;

namespace PiarcoTile.ViewModels {
    /// <summary>
    /// Event arg used to give the GamePage the information needed
    /// </summary>
    public class SongFinishedEventArgs : EventArgs {
        public int failed;
        public int bad;
        public int good;
        public int excellent;
        public double accuracy;

        public SongFinishedEventArgs(int failed, int bad, int good, int excellent, double accuracy) {
            this.failed = failed;
            this.bad = bad;
            this.good = good;
            this.excellent = excellent;
            this.accuracy = accuracy;
        }
    }
}
