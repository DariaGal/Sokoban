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
        Position PlayerPosition { get; }
        public List<Position> Goals { get; }

        public LevelInfo(string name, ICell[,] map, Position player, List<Position> goals)
        {
            Name = name;
            Map = map;
            PlayerPosition = player;
            Goals = goals;
        }
    }
}
