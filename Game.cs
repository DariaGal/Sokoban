using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Game
    {
        private Map map;

        public Game(int width, int height, char[,] mapString)
        {
            map = new Map(width, height, mapString);
        }
    }
}
