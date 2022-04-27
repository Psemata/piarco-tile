using PiarcoTile.Interfaces;
using PiarcoTile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

        IAssetService service = DependencyService.Get<IAssetService>();
        private List<Song> songs;
        private int currentIndex;
        private Song currentSong;
        public Song CurrentSong { get { return this.currentSong;  } private set { SetProperty<Song>(ref currentSong, value); } }

        public ICommand NextSong { get; private set; }
        public ICommand PreviousSong { get; private set; }

        public SelectionVM() {
            GetSongs();

            this.currentIndex = 0;
            this.CurrentSong = this.songs[currentIndex];

            this.NextSong = new Command(
                execute:() => {
                    this.currentIndex = (currentIndex + 1) % this.songs.Count;
                    this.CurrentSong = this.songs[currentIndex];
                });
            this.PreviousSong = new Command(
                execute:() => {
                    this.currentIndex = (currentIndex - 1) % this.songs.Count;
                    this.currentIndex = this.currentIndex < 0 ? this.currentIndex + this.songs.Count : this.currentIndex;
                    this.CurrentSong = this.songs[currentIndex];
                });
        }

        public void GetSongs()
        {
            songs = new List<Song>();
            Regex rx = new Regex(@"(\d+)\s(.+)\s-\s(.+)");
            string[] c = service.GetAssetList("Songs/");
            foreach (string s in c)
            {
                MatchCollection matches = rx.Matches(s);
                GroupCollection groups = matches[0].Groups;
                Song song = new Song(int.Parse(groups[1].Value), groups[3].Value, groups[2].Value, "", "Songs/" + groups[0].Value, service);
                songs.Add(song);
            }
        }
    }
}
