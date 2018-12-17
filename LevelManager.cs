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
        public char[,] LoadLevel(string fileName)
        {
            var file = new FileInfo(fileName);

            string str = File.ReadAllText(fileName);
            var split = str.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int width = split[0].Length;
            int height = split.Length;

            var mapString = new char[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    mapString[x, y] = split[y][x];
                }
            }
            return mapString;
        }
    }
}
