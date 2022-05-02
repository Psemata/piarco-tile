using PiarcoTile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiarcoTile.ViewModels {
    /// <summary>
    /// Argument passed when the tile is pressed
    /// </summary>
    public class TileEventArgs : EventArgs {
        public Note note;

        public TileEventArgs(Note note) {
            this.note = note;
        }
    }
}
