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
        //Song selected
        private Song song;
        //Difficulty selected
        private int difficultyIndex;
        //Index of the notes in the map
        private int index = 0;
        //Stopwatch that counts time since the beginning
        private Stopwatch st;
        //Map that was selected
        private Map chosenMap;
        //Points that the player gets from the notes
        private int points = 0;
        //Number of notes played
        private int notesUsed = 0;
        //Dictionnary of notes that were hit depending on the timing
        private Dictionary<string, int> notes;
        //Private list of tiles that will be displayed
        private ObservableCollection<TileVM> tiles;
        //Public list of tiles that will be displayed
        public ObservableCollection<TileVM> Tiles { get { return this.tiles; } private set { this.tiles = value; } }
        //Event when the song is finished
        public event EventHandler SongFinished;

        public SongVM(Song song, int difficultyIndex) {
            //Wait a second before playing so player will not be surprised
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
            //Event that will fire when song ends
            song.Music.Completion += (d, e) =>
            {
                //Compute accuracy of player
                double accuracy = (points * 100) / (notesUsed * 300);
                EventHandler handler = SongFinished;
                handler?.Invoke(this, new SongFinishedEventArgs(this.notes["Fail"], this.notes["Bad"], this.notes["Good"], this.notes["Excellent"], accuracy));
            };

            SetTimer();
            song.Music.Prepare();
        }
        /// <summary>
        /// Event that will start the stopwatch and the timer that will add and move tiles
        /// </summary>
        private void SetTimer()
        {
            st = new Stopwatch();
            st.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(1000/60), () => 
            {
                //If it is time for the next note to be added
                if (st.ElapsedMilliseconds >= chosenMap.Notes[index].TimeStart)
                {
                    //If the are still notes to play
                    if (index < chosenMap.Notes.Count - 1)
                    {
                        //We copy a note so we do not modify the original in case we replay it
                        Note noteCopy = new Note(chosenMap.Notes[index]);
                        //We create a tile based on the note
                        TileVM toBeAddedTile = new TileVM(noteCopy);
                        //Event for the click
                        toBeAddedTile.TilePressed += HandleTile;
                        //Increment index
                        index++;
                        //Add to the list
                        this.Tiles.Add(toBeAddedTile);
                    }
                    else
                    {
                        //If there is no more notes to play we stop the stopwatch and the timer
                        st.Stop();
                        return false;
                    }
                }
                //Move every note down in Y axis of 10 pixels
                for(int i = 0; i < Tiles.Count; i++) {
                    Tiles[i].PosY += 10;
                    //If the note has passed the size of the screen and is no longer clickable
                    if (Tiles[i].PosY > Application.Current.MainPage.Height) {
                        //we remove it and put a fail on it
                        tiles.Remove(Tiles[i]);
                        this.notes["Fail"] += 1;
                    }
                }
                //Return true so timer continues
                return true;
            });
        }
        /// <summary>
        /// Event that handles the click on a tile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleTile(object sender, EventArgs e) {
            //Get the time when the tile was clicked
            double time = st.ElapsedMilliseconds;
            //Get the object
            TileVM t = sender as TileVM;
            //Get the difference in time in miliseconds when it was hit compared when it needed to be hit
            double timeHit = Math.Abs(t.TimeHit - time);
            //We cannot click on the note if we try to click it half a second too soon
            if (timeHit <= 500)
            {
                //If we can click it we add a note that was used
                notesUsed++;
                //If the time clicked is 0.3s or more
                if (timeHit >= 300) {
                    //We give it 1/6 of the total points of the note
                    points += 50;
                    this.notes["Bad"] += 1;
                }
                //If the time clicked is 0.2s or more
                else if (timeHit >= 200)
                {
                    //We give it 1/3 of the total points of the note
                    points += 100;
                    this.notes["Good"] += 1;
                }
                //If the time clicked is perfect or less than 0.2s
                else
                {
                    //We give full points
                    points += 300;
                    this.notes["Excellent"] += 1;
                }
                tiles.Remove(t);
            }
        }
    }
}
