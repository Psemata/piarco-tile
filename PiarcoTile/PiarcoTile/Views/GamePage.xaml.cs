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
        public GamePage(Song song, int difficultyIndex) {
            InitializeComponent();
            // Context
            this.BindingContext = new SongVM(song, difficultyIndex);
            // Navigation
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}