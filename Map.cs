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
        private List<GoalCell> goals;

        public int Width { get; }
        public int Height { get; }

        public Map(int width, int height, char[,] mapString)
        {
            map = new ICell[width, height];
            Width = width;
            Height = height;
            SetMap(mapString);
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

        private void SetMap(char[,] mapString)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    switch (mapString[x, y])
                    {
                        case '-':
                            map[x, y] = new EmptyCell();
                            break;
                        case '#':
                            map[x, y] = new Wall();
                            break;
                        case '@':
                            map[x, y] = new Box();
                            break;
                        case '*':
                            map[x, y] = new GoalCell();
                            break;
                        case 'o':
                            map[x, y] = new PlayerCell();
                            break;
                    }
                }
            }
        }
    }
}
