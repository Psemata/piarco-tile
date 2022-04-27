using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiarcoTile.Models;
using System.Text.RegularExpressions;
using Android.Content.Res;
using Xamarin.Forms;
using PiarcoTile.Interfaces;

[assembly: Dependency(typeof(PiarcoTile.Droid.Models.SongsService))]
namespace PiarcoTile.Droid.Models {
    public class SongsService : ISongService {
        public List<Song> GetSongs() {
            List<Song> Songs = new List<Song>();
            Regex rx = new Regex(@"(\d+)\s(.+)\s-\s(.+)");
            AssetManager assets = Android.App.Application.Context.Assets;
            string[] c = assets.List("Songs/");
            foreach (string s in c) {
                MatchCollection matches = rx.Matches(s);
                GroupCollection groups = matches[0].Groups;
                Song song = new Song(int.Parse(groups[1].Value), groups[2].Value, groups[3].Value, "", "Songs/" + groups[0].Value, assets);
                Songs.Add(song);
            }

            return Songs;
        }
    }
}