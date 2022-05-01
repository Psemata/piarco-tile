using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using PiarcoTile.Models;

namespace PiarcoTile.ViewModels {
    public class SongVM {

        private Song song;
        private int difficultyIndex;
        private System.Timers.Timer aTimer;

        public SongVM(Song song, int difficultyIndex) {
            this.song = song;
            this.difficultyIndex = difficultyIndex;
            SetTimer();
        }

        private void SetTimer()
        {
            // Create a timer with a sixtyth of a second interval.
            aTimer = new Timer(1000/60);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }
    }
}
