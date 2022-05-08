using PiarcoTile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PiarcoTile.ViewModels {
    public class TileVM : INotifyPropertyChanged {

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

        private Note note;
        //Time in miliseconds when the note needs to be hit
        public double TimeHit { get { return this.note.TimeHit; } private set { } }
        //X position of the note
        public double PosX { get { return this.note.X; } private set { } }
        //Y position of the note
        public double PosY { get { return this.note.Y; } set { this.note.Y = value; OnPropertyChanged(); } }

        // ICommand used when the tile is pressed
        public ICommand PlayTile { get; private set; }

        public event EventHandler TilePressed;

        public TileVM(Note note) {
            this.note = note;
            this.PlayTile = new Command(
                execute: () => {
                    EventHandler handler = TilePressed;
                    handler?.Invoke(this, new TileEventArgs(this.note));
                }
            );
        }
    }
}
