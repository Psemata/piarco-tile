﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PiarcoTile.ViewModels;
using PiarcoTile.Models;

namespace PiarcoTile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tile : ContentView {
        public Tile() {
            InitializeComponent();
        }
    }
}