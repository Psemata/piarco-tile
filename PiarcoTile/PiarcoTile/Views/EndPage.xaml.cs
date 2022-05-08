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

        public EndPage(int failed, int bad, int good, int excellent, double accuracy, Song song) {
            InitializeComponent();

            // Context
            this.BindingContext = new EndVM(failed, bad, good, excellent, accuracy, song);
            // Navigation
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }
        
        /// <summary>
        /// The user wishes to select another song
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnSelectionPageButtonClicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new NavigationPage(new SelectionPage()));
        }
    }
}