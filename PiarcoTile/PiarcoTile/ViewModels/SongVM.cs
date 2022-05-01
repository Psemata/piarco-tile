using System;
using System.Collections.Generic;
using System.Text;
using PiarcoTile.Models;

namespace PiarcoTile.ViewModels {
    public class SongVM {

        private Song song;
        private int difficultyIndex;

        public SongVM(Song song, int difficultyIndex) {
            this.song = song;
            this.difficultyIndex = difficultyIndex;
        }
    }
}
