using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PiarcoTile.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(PiarcoTile.Droid.Models.AssetService))]
namespace PiarcoTile.Droid.Models
{
    class AssetService : IAssetService
    {
        AssetManager assets = Android.App.Application.Context.Assets;

        public string[] GetAssetList(string path)
        {
            return assets.List(path);
        }

        public AssetFileDescriptor OpenFd(string path)
        {
            return assets.OpenFd(path);
        }

        public string GetAssetString(string path)
        {
            throw new NotImplementedException();
        }

        public Stream Open(string path)
        {
            return assets.Open(path);
        }
    }
}