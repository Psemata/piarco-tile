using PiarcoTile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiarcoTile.ViewModels {
    public class TileEventArgs : EventArgs {
        public Note note;
        public int score;

        public TileEventArgs(Note note, int score) {
            this.note = note;
            this.score = score;
        }
    }
}
