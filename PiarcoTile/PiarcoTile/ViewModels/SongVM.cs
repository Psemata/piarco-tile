using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Android.Media;
using PiarcoTile.Models;
using Xamarin.Forms;

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
        private Stopwatch st;
        private Map chosenMap;
        private int points = 0;
        private int notesUsed = 0;
        private Dictionary<string, int> notes;

        private ObservableCollection<TileVM> tiles;
        public ObservableCollection<TileVM> Tiles { get { return this.tiles; } private set { this.tiles = value; } }

        public event EventHandler SongFinished;

        public SongVM(Song song, int difficultyIndex) {
            Thread.Sleep(1000);
            this.song = song;
            this.difficultyIndex = difficultyIndex;
            this.chosenMap = song.Maps[difficultyIndex];
            this.notes = new Dictionary<string, int>();
            this.notes.Add("Fail", 0);
            this.notes.Add("Bad", 0);
            this.notes.Add("Good", 0);
            this.notes.Add("Excellent", 0);

            this.Tiles = new ObservableCollection<TileVM>();

            song.Music.Completion += (d, e) =>
            {
                double accuracy = (points * 100) / (notesUsed * 300);
                EventHandler handler = SongFinished;
                handler?.Invoke(this, new SongFinishedEventArgs(this.notes["Fail"], this.notes["Bad"], this.notes["Good"], this.notes["Excellent"], accuracy));
            };

            SetTimer();
            song.Music.Prepare();
        }

        private void SetTimer()
        {
            st = new Stopwatch();
            st.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(1000/60), () => 
            {
                if (st.ElapsedMilliseconds >= chosenMap.Notes[index].TimeStart)
                {
                    if (index < chosenMap.Notes.Count - 1)
                    {
                        Note noteCopy = new Note(chosenMap.Notes[index]);
                        TileVM toBeAddedTile = new TileVM(noteCopy);
                        toBeAddedTile.TilePressed += HandleTile;
                        index++;
                        this.Tiles.Add(toBeAddedTile);
                    }
                    else
                    {
                        st.Stop();
                        return false;
                    }
                }

                for(int i = 0; i < Tiles.Count; i++) {
                    Tiles[i].PosY += 10;
                    if (Tiles[i].PosY > Application.Current.MainPage.Height) {
                        tiles.Remove(Tiles[i]);
                        this.notes["Fail"] += 1;
                    }
                }
                return true;
            });
        }

        private void HandleTile(object sender, EventArgs e) {
            double time = st.ElapsedMilliseconds;
            TileVM t = sender as TileVM;
            double timeHit = t.TimeHit - time;
            if (timeHit <= 500)
            {
                notesUsed++;
                if (timeHit >= 300) {
                    //1/6 de l'acc
                    points += 50;
                    this.notes["Bad"] += 1;
                }
                else if (timeHit >= 200)
                {
                    //1/3 de l'acc
                    points += 100;
                    this.notes["Good"] += 1;
                }
                else
                {
                    //1/1 de l'acc
                    points += 300;
                    this.notes["Excellent"] += 1;
                }
                tiles.Remove(t);
            }
        }
    }
}
