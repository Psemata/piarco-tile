using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PiarcoTile.Models
{
    class Map
    {
        string Name { get; set; }
        List<Note> Notes { get; set; }

        public Map(string name, Stream map)
        {
            this.Name = name;
            Notes = new List<Note>();
            ReadMap(map);
        }

        public void ReadMap(Stream map)
        {
            bool read = false;
            using (var reader = new StreamReader(map, Encoding.UTF8))
            {
                while (reader.Peek() >= 0)
                {
                    string value = reader.ReadLine();
                    if (read)
                    {
                        Regex rx = new Regex(@"(\d+),(\d+),(\d+)");
                        MatchCollection matches = rx.Matches(value);
                        GroupCollection groups = matches[0].Groups;
                        Note n = new Note(int.Parse(groups[1].Value), int.Parse(groups[2].Value), int.Parse(groups[3].Value));
                        Notes.Add(n);
                    }
                    if (value.Contains("HitObjects"))
                        read = true;
                }
            }
        }
    }
}
