using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PiarcoTile.Interfaces {
    public interface IAssetService {
        string[] GetAssetList(string path);
        string GetAssetString(string path);
        Stream Open(string path);
    }
}
