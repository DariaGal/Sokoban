using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public class GameState
    {
        public int ElementSize = 32;
        public int MapWidth;
        public int MapHeight;
        public List<CellAnimation> Animations = new List<CellAnimation>();
        private Game game;

        public GameState(int width, int height, char[,] map)
        {
            MapWidth = width;
            MapHeight = height;
            game = new Game(width, height, map);
        }

        public void BeginAct()
        {

        }
        public void EndAct()
        {

        }
    }
}
