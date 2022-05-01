using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PiarcoTile.ViewModels;

namespace PiarcoTile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectionPage : ContentPage {
        public SelectionPage() {
            InitializeComponent();
            // Context
            this.BindingContext = new SelectionVM();
            // Navigation
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }

        async void OnPlayButtonClicked(object sender, EventArgs e) {
            if(this.difficulties.SelectedIndex != -1) {
                await Navigation.PushModalAsync(new NavigationPage(new GamePage((this.BindingContext as SelectionVM).CurrentSong, this.difficulties.SelectedIndex)));
            }
        }
    }
}