using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PiarcoTile.ViewModels {
    public class SelectionVM : INotifyPropertyChanged {

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

        public class Song {
            public string name { get; set; }
            public string length { get; set; }
            public List<string> difficulties { get; set; }

            public Song(string name, string length, List<string> difficulties) {
                this.name = name;
                this.length = length;
                this.difficulties = difficulties;
            }
        }

        private List<Song> songs;
        private int currentIndex;
        private Song currentSong;
        public Song CurrentSong { get { return this.currentSong;  } private set { SetProperty<Song>(ref currentSong, value); } }

        public ICommand NextSong { get; private set; }
        public ICommand PreviousSong { get; private set; }

        public SelectionVM() {
            this.songs = new List<Song>();
            this.songs.Add(new Song("Libéré, délivré", "3:16", new List<string> { "Facile", "Moyen", "Difficile", "NTM" }));
            this.songs.Add(new Song("Ce rêve bleu", "4:20", new List<string> { "Suce", "Echo", "Yo", "Hello" }));
            this.songs.Add(new Song("Comme un homme", "2:25", new List<string> { "Disney", "C'est", "Des", "Putes" }));

            this.CurrentSong = this.songs[0];

            this.NextSong = new Command(
                execute:() => {
                    this.currentIndex = (currentIndex + 1) % this.songs.Count;
                    this.CurrentSong = this.songs[currentIndex];
                });
            this.PreviousSong = new Command(
                execute:() => {
                    this.currentIndex = (currentIndex - 1) % this.songs.Count;
                    this.CurrentSong = this.songs[currentIndex];
                });
        }
    }
}
