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

        public Map(int width, int height, ICell[,] map)
        {
            Width = width;
            Height = height;
            this.map = new ICell[width, height];
            this.map = map;
        }

        public Map(Map other)
        {
            map = other.map;
            Width = other.Width;
            Height = other.Height;
        }

        public void Move(Position from, Position to)
        {
            var cell = GetCell(from);
            SetCell(to,cell);
            SetCell(from, new EmptyCell());
        }

        public ICell GetCell(Position pos)
        {
            return map[pos.X, pos.Y];
        }

        public void SetCell(Position pos, ICell cell)
        {
            map[pos.X, pos.Y] = cell;
        }

    }
}
