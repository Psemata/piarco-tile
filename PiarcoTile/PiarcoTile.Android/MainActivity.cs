using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;
using Android.Content.Res;
using System.Collections.Generic;
using PiarcoTile.Models;
using System.Text.RegularExpressions;

namespace PiarcoTile.Droid
{
    [Activity(Label = "PiarcoTile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        List<Song> Songs { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Songs = new List<Song>();
            Regex rx = new Regex(@"(\d+)\s(.+)\s-\s(.+)");
            AssetManager assets = this.Assets;
            string[] c = assets.List("Songs/");
            foreach(string s in c)
            {
                Console.WriteLine(s);
                MatchCollection matches = rx.Matches(s);
                GroupCollection groups = matches[0].Groups;
                Song song = new Song(int.Parse(groups[1].Value), groups[2].Value, groups[3].Value, "", "Songs/"+groups[0].Value, assets);
                Songs.Add(song);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}