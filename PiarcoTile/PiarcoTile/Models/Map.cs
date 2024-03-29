﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PiarcoTile.Models
{
    public class Map
    {
        //Name of the difficulty
        public string Name { get; set; }
        //List of notes of the map
        public List<Note> Notes { get; set; }

        public Map(string name, Stream map)
        {
            this.Name = name;
            Notes = new List<Note>();
            ReadMap(map);
        }
        /// <summary>
        /// Function to read .osu file to create note objects
        /// </summary>
        /// <param name="map"></param>
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
        public override string ToString()
        {
            return Name;
        }
    }
}
