using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PiarcoTile.Models
{
    public class Song
    {
        string Name { get; set; }
        string Artist { get; set; }
        int ID { get; set; }
        string Music { get; set; }
        List<Map> Maps { get; set; }

        public Song(int id, string name, string artist, string music, string path, AssetManager assets)
        {
            this.ID = id;
            this.Name = name;
            this.Artist = artist;
            this.Music = music;
            this.Maps = new List<Map>();
            GenerateMaps(path, assets);
        }

        private void GenerateMaps(string path, AssetManager assets)
        {
            string[] c = assets.List(path);
            Regex rx = new Regex(@"[(.+)]");
            foreach (string s in c)
            {
                Console.WriteLine(s);
                if (s.Contains(".osu"))
                {
                    MatchCollection matches = rx.Matches(s);
                    GroupCollection groups = matches[0].Groups;
                    Map m = new Map(groups[1].Value,assets.Open(path+"/"+s));
                    Maps.Add(m);
                }
            }
        }
    }
}
