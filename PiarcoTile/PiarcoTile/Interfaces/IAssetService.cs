using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PiarcoTile.Interfaces {
    public interface IAssetService {
        string[] GetAssetList(string path);

        AssetFileDescriptor OpenFd(string path);

        Stream Open(string path);
    }
}
