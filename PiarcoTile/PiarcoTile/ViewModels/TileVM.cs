using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PiarcoTile.ViewModels {
    public class TileVM {

        public ICommand PlayTile { get; private set; }

        public TileVM() {
            this.PlayTile = new Command(
                execute: () => {
                    Console.WriteLine("Eh yow");
                });
        }
    }
}
