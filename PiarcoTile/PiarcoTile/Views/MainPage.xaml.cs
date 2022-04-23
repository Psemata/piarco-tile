using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PiarcoTile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
            // Navigation
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void OnSelectionPageButtonClicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new NavigationPage(new SelectionPage()));
        }
    }
}
