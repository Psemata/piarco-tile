using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using PiarcoTile.Models;

namespace PiarcoTile.ViewModels {
    public class SongVM : INotifyPropertyChanged {

        #region INotifyPropertyChanged
        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private Song song;
        private int difficultyIndex;
        private Timer aTimer;
        private Map chosenMap;

        private ObservableCollection<TileVM> tiles;
        public ObservableCollection<TileVM> Tiles { get { return this.tiles; } private set { this.tiles = value; } }

        public SongVM(Song song, int difficultyIndex) {
            this.song = song;
            this.difficultyIndex = difficultyIndex;
            this.chosenMap = song.Maps[difficultyIndex];

            this.Tiles = new ObservableCollection<TileVM>();

            // For each note of the chosen map, create a TileVM object and listen to its interaction
            foreach (Note note in chosenMap.Notes)
            {
                Note noteCopy = new Note(note);
                TileVM toBeAddedTile = new TileVM(noteCopy);
                toBeAddedTile.TilePressed += HandleTile;
                this.Tiles.Add(toBeAddedTile);
            }

            //SetTimer();
        }

        private void SetTimer()
        {
            // Create a timer with a sixtieth of a second interval.
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

        private void HandleTile(object sender, EventArgs e) {
            Console.WriteLine("Test tile " + ((e as TileEventArgs).note));
        }
    }
}
