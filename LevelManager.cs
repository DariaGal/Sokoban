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
        Dictionary<string, FileInfo> pathToLevels;
        public LevelManager()
        {
            pathToLevels = new Dictionary<string, FileInfo>();
        }

        public void AddLevel(string levelName, FileInfo file)
        {
            pathToLevels.Add(levelName, file);
        }

        public LevelInfo LoadLevel(string levelName)
        {
            var file = pathToLevels[levelName];

            string str = File.ReadAllText(file.FullName);
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

            var mapCreator = new CellMapCreator(width, height, mapString);

            return new LevelInfo(levelName, mapCreator.map, mapCreator.PlayerPosition, mapCreator.Goals);
        }
    }
}
