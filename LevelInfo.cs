using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class LevelInfo
    {
        public string Name { get; }
        public int MapWidth { get; }
        public int MapHeight { get; }

        private readonly Position PlayerPosition;
        private readonly List<Position> Goals;
        private readonly ICell[,] Map;

        public LevelInfo(string name, ICell[,] map, Position player, List<Position> goals)
        {
            MapWidth = map.GetLength(0);
            MapHeight= map.GetLength(1);
            Name = name;
            Map = map;
            PlayerPosition = player;
            Goals = goals;
        }
        public LevelInfo(LevelInfo level)
        {
            MapWidth = level.MapWidth;
            MapHeight = level.MapHeight;
            Name = level.Name;
            Map = level.Map;
            PlayerPosition = level.PlayerPosition;
            Goals = level.Goals;
        }
        public ICell[,] GetMap()
        {
            return (ICell[,])Map.Clone();
        }
        public List<Position> GetGoals()
        {
            return new List<Position>(Goals);
        }
        public Position GetPlayerPosition()
        {
            return new Position(PlayerPosition.X, PlayerPosition.Y);
        }
    }
}
