using PiarcoTile.Models;
using PiarcoTile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PiarcoTile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EndPage : ContentPage {

        private Song song;
        private int difficultyIndex;

        public EndPage(int failed, int bad, int good, int excellent, double accuracy, Song song, int difficultyIndex) {
            InitializeComponent();

            this.song = song;
            this.difficultyIndex = difficultyIndex;

            // Context
            this.BindingContext = new EndVM(failed, bad, good, excellent, accuracy, song);
            // Navigation
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }

        async void OnReplayButtonClicked(object sender, EventArgs e) {
           await Navigation.PushModalAsync(new NavigationPage(new GamePage(this.song, this.difficultyIndex)));
        }

        async void OnSelectionPageButtonClicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new NavigationPage(new SelectionPage()));
        }
    }
}