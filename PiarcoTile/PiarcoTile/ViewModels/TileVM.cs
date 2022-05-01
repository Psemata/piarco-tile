using PiarcoTile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PiarcoTile.ViewModels {
    public class TileVM {

        private Note note;
        public ICommand PlayTile { get; private set; }

        public event EventHandler TilePressed;

        public TileVM(Note note) {
            this.note = note;
            this.PlayTile = new Command(
                execute: () => {
                    EventHandler handler = TilePressed;
                    handler?.Invoke(this, new TileEventArgs(this.note, 10));
                }
            );
        }
    }
}
