using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class CellMapCreator
    {
        public ICell[,] map { get; }
        public int Width { get; }
        public int Height { get; }
        public List<Position> Goals { get; }
        public Position PlayerPosition { get; private set; }
        private int BoxCount;

        public CellMapCreator(int width, int height, char[,] mapString)
        {
            map = new ICell[width,height];
            Width = width;
            Height = height;
            Goals = new List<Position>();
            PlayerPosition = null;
            BoxCount = 0;
            SetMap(mapString);
            if(Goals.Count != BoxCount)
            {
                throw new Exception("Wrong map, goals != boxes");
            }
            if(Goals.Count == 0)
            {
                throw new Exception("Wrong map, goals count can't be zero");
            }
            if(PlayerPosition == null)
            {
                throw new Exception("Wrong map, no player");
            }

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
                            BoxCount++;
                            break;
                        case '*':
                            Goals.Add(new Position(x, y));
                            map[x, y] = new EmptyCell();
                            break;
                        case 'o':
                            if (PlayerPosition == null)
                                PlayerPosition = new Position(x, y);
                            else
                                throw new Exception("Wrong Map, more then 1 player");
                            map[x, y] = new EmptyCell();
                            break;
                    }
                }
            }
        }
    }
}
