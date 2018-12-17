using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class LevelManager
    {
        Map LoadLevel(string fileName)
        {
            string str = File.ReadAllText(fileName);
            var split = str.Split('\n');
            int width = split.Length;
            int height = split[0].Length;
            var mapString = new char[width, height];
            for(int x=0;x < width; x++)
            {
                for(int y = 0; y< height; y++)
                {
                    mapString[x, y] = split[x][y];
                }
            }
            return new Map(width, height, mapString);
        }
    }
}
