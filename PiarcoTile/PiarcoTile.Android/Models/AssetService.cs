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
        /// <summary>
        /// Function that returns a list of string of the files in a folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] GetAssetList(string path)
        {
            return assets.List(path);
        }
        /// <summary>
        /// Open an uncompressed asset and gives an AssetFileDescriptor useful to get different information on the file. Primarly used for mp3 files
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public AssetFileDescriptor OpenFd(string path)
        {
            return assets.OpenFd(path);
        }
        /// <summary>
        /// Opens an asset given the path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Stream Open(string path)
        {
            return assets.Open(path);
        }
    }
}