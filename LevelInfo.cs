using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class LevelInfo
    {
        public string Name { get; }
        public ICell[,] Map { get; }
        public int MapWidth { get; }
        public int MapHeight { get; }
        Position PlayerPosition { get; }
        public List<Position> Goals { get; }

        public LevelInfo(string name, ICell[,] map, Position player, List<Position> goals)
        {
            MapWidth = map.GetLength(0);
            MapHeight= map.GetLength(1);
            Name = name;
            Map = map;
            PlayerPosition = player;
            Goals = goals;
        }
    }
}
