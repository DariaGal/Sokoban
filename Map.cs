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
            SetMap(mapString);
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
        public void SetCell(Position pos, ICell cell)
        {
            map[pos.X, pos.Y] = cell;
        }
    }
}
