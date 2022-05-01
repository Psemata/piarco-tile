using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
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
        private Map chosenMap;

        private List<TileVM> tiles;
        public List<TileVM> Tiles { get { return this.tiles; } private set { SetProperty<List<TileVM>>(ref tiles, value); } } // Faux, il faut utiliser une observable

        public SongVM(Song song, int difficultyIndex) {
            this.song = song;
            this.difficultyIndex = difficultyIndex;
            this.chosenMap = song.Maps[difficultyIndex];

            this.tiles = new List<TileVM>();

            // For each note of the chosen map, create a TileVM object and listen to its interaction
            foreach (Note note in chosenMap.Notes) {
                TileVM toBeAddedTile = new TileVM(note);
                toBeAddedTile.TilePressed += HandleTile;
                tiles.Add(toBeAddedTile);
            }
        }

        private void HandleTile(object sender, EventArgs e) {
            Console.WriteLine("Test tile " + ((e as TileEventArgs).note) + " " + ((e as TileEventArgs).score));
        }
    }
}
