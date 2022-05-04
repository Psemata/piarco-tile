using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        private int index = 0;
        private Timer aTimer;
        private Stopwatch st;
        private Map chosenMap;

        private ObservableCollection<TileVM> tiles;
        public ObservableCollection<TileVM> Tiles { get { return this.tiles; } private set { this.tiles = value; } }

        public SongVM(Song song, int difficultyIndex) {
            this.song = song;
            this.difficultyIndex = difficultyIndex;
            this.chosenMap = song.Maps[difficultyIndex];

            this.Tiles = new ObservableCollection<TileVM>();

            // For each note of the chosen map, create a TileVM object and listen to its interaction
            //foreach (Note note in chosenMap.Notes)
            //{
            //    Note noteCopy = new Note(note);
            //    TileVM toBeAddedTile = new TileVM(noteCopy);
            //    toBeAddedTile.TilePressed += HandleTile;
            //    this.Tiles.Add(toBeAddedTile);
            //}

            for(int i = 0; i < 10; i++)
            {
                Note noteCopy = new Note(chosenMap.Notes[i]);
                noteCopy.Y -= 500 * i;
                TileVM toBeAddedTile = new TileVM(noteCopy);
                toBeAddedTile.TilePressed += HandleTile;
                this.Tiles.Add(toBeAddedTile);
            }

            SetTimer();
        }

        private void SetTimer()
        {
            st = new Stopwatch();
            // Create a timer with a sixtieth of a second interval.
            aTimer = new Timer(1000/60);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += MoveTiles;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            st.Start();
        }

        private void MoveTiles(Object source, ElapsedEventArgs e)
        {
            if (st.ElapsedMilliseconds >= chosenMap.Notes[index].TimeStart)
            {
                if (index < chosenMap.Notes.Count - 1 && index < 10)
                {
                    //Console.WriteLine(index);
                    Note noteCopy = new Note(chosenMap.Notes[index]);
                    TileVM toBeAddedTile = new TileVM(noteCopy);
                    toBeAddedTile.TilePressed += HandleTile;
                    index++;
                    //this.Tiles.Add(toBeAddedTile);
                }
            }

            //Console.WriteLine(tiles.Count);
            foreach (TileVM t in tiles)
            {
                //Console.WriteLine(t.PosY);
                t.PosY += 10;
            }
        }

        private void HandleTile(object sender, EventArgs e) {
            //Console.WriteLine("b");
            tiles.Remove(sender as TileVM);
        }
    }
}
