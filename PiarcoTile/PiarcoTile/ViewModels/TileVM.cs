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
        public double PosX { get { return this.note.X; } private set { } }
        public double PosY { get { return this.note.Y; } private set { this.note.Y = value; OnPropertyChanged(); } }

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
