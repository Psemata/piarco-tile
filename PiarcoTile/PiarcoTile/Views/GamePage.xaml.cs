using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PiarcoTile.Models;
using PiarcoTile.ViewModels;

namespace PiarcoTile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage {

        private Song song;
        private int difficultyIndex;

        public GamePage(Song song, int difficultyIndex) {
            InitializeComponent();
            // Context
            this.song = song;
            this.difficultyIndex = difficultyIndex;
            SongVM songvm = new SongVM(this.song, this.difficultyIndex);
            songvm.SongFinished += HandleFinishedSong;
            this.BindingContext = songvm;
            // Navigation
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void HandleFinishedSong(object sender, EventArgs e) {
            SongFinishedEventArgs args = e as SongFinishedEventArgs;
            await Navigation.PushModalAsync(new NavigationPage(new EndPage(args.failed, args.bad, args.good, args.excellent, args.accuracy, this.song, this.difficultyIndex)));
        }
    }
}