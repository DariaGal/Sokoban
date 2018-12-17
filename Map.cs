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

        public Map(int width, int height)
        {
            map = new ICell[width, height];
            Width = width;
            Height = height;
        }
    }
}
