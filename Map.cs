using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Map
    {
        private ICell[,] map;
        public int Width { get; }
        public int Height { get; }

        public Map(int width, int height, char[,] mapString)
        {
            map = new ICell[width, height];
            Width = width;
            Height = height;
        }

        public void SetCell(Position pos, ICell cell)
        {
            map[pos.X, pos.Y] = cell;
        }
    }
}
