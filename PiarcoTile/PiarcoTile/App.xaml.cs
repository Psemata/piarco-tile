using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PiarcoTile.Views;

namespace PiarcoTile {
    public partial class App : Application {
        public App() {
            InitializeComponent();
            // Start page
            this.MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
