using System;
using System.Collections.Generic;
using System.Text;

namespace PiarcoTile.Models
{
    class Song
    {
        string Name { get; set; }
        string Artist { get; set; }
        int ID { get; set; }
        string Music { get; set; }
        List<Map> Maps { get; set; }

        public Song(int id, string name, string artist, string music, string path)
        {
            this.ID = id;
            this.Name = name;
            this.Artist = artist;
            this.Music = music;
            GenerateMaps(path);
        }

        private void GenerateMaps(string path)
        {

        }
    }
}
