using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PiarcoTile.Interfaces {
    /// <summary>
    /// Interface used to communicate with the Android project
    /// </summary>
    public interface IAssetService {
        string[] GetAssetList(string path);

        AssetFileDescriptor OpenFd(string path);

        Stream Open(string path);
    }
}
