using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public class GameState
    {
        public int ElementSize = 32;
        public CellAnimation[,] Animations;
        private LevelInfo levelInfo;
        public int MapWidth { get; }
        public int MapHeight { get; }
        private Game game;
        private Directions direction;
        public int StepCount;

        public GameState(LevelInfo lInfo)
        {
            levelInfo = new LevelInfo(lInfo);
            MapWidth = levelInfo.MapWidth;
            MapHeight = levelInfo.MapHeight;
            direction = Directions.None;
            StepCount = 0;
            Animations = new CellAnimation[MapWidth, MapHeight];
            InitGame();
        }

        private void SetAnimations()
        {
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    Animations[x, y].Name = game.GetNameCell(new Position(x, y));
                }
            }
        }

        private void InitGame()
        {
            StepCount = 0;
            game = new Game(levelInfo);
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    Animations[x, y] = new CellAnimation
                    {
                        Name = game.GetNameCell(new Position(x, y)),
                        Location = new Point(x * ElementSize, y * ElementSize + ElementSize)
                    };
                }
            }
        }
        public void RestartGame()
        {
            InitGame();
        }

        public void SetDirection(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    direction = Directions.Up;
                    break;
                case Keys.Down:
                    direction = Directions.Down;
                    break;
                case Keys.Left:
                    direction = Directions.Left;
                    break;
                case Keys.Right:
                    direction = Directions.Right;
                    break;
                case Keys.W:
                    direction = Directions.Up;
                    break;
                case Keys.S:
                    direction = Directions.Down;
                    break;
                case Keys.A:
                    direction = Directions.Left;
                    break;
                case Keys.D:
                    direction = Directions.Right;
                    break;
                default:
                    direction = Directions.None;
                    break;
            }

        }

        public void Act()
        {
            if(game.TryMakeStep(direction))
            {
                StepCount++;
            }
            SetAnimations();
            direction = Directions.None;
        }
        public bool EndGame()
        {
            return game.IsWin();
        }
    }
}
