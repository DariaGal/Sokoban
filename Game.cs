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
        private Position playerPosition;

        public Game(int width, int height, char[,] mapString)
        {
            playerPosition = null;
            map = new Map(width, height, mapString);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map.GetCell(new Position(x, y)) is PlayerCell)
                    {
                        map.SetCell(new Position(x, y), new EmptyCell());
                        playerPosition = new Position(x, y);
                        break;
                    }
                }
                if (playerPosition != null) break;
            }
        }

        public void MakeStep(Directions direction)
        {
            if (direction != Directions.None)
            {
                var newPos = GetNewPosition(playerPosition, direction);

                if (map.GetCell(newPos) is Wall || IsOutOfMap(newPos))
                    newPos = playerPosition;
                else if (map.GetCell(newPos) is Box)
                {
                    if(TryMoveBox(direction, newPos))
                    {
                        var newBoxPos = GetNewPosition(newPos, direction);
                        map.Move(newPos, newBoxPos);
                    }
                    else
                    {
                        newPos = playerPosition;
                    }
                }
                playerPosition = newPos;
            }
        }

        bool TryMoveBox(Directions direction, Position pos)
        {
            Position newPos = GetNewPosition(pos, direction);
            if (map.GetCell(newPos) is Wall || IsOutOfMap(newPos) || map.GetCell(newPos) is Box)
                return false;
            return true;
        }

        public Map GetMap()
        {
            return new Map(map);
        }
        private Position GetNewPosition(Position pos, Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    return new Position(pos.X, pos.Y - 1);
                case Directions.Down:
                    return new Position(pos.X, pos.Y + 1);
                case Directions.Left:
                    return new Position(pos.X - 1, pos.Y);
                case Directions.Right:
                    return new Position(pos.X + 1, pos.Y);
            }
            return null;
        }

        public string GetNameCell(Position pos)
        {
            var cell = map.GetCell(pos);
            if (cell is EmptyCell)
            {
                if (playerPosition.X == pos.X && playerPosition.Y == pos.Y)
                    return "Player";
                else return "Empty";
            }
            if (cell is GoalCell)
            {
                if (playerPosition.X == pos.X && playerPosition.Y == pos.Y)
                    return "PlayerOnGoal";
                else return "Goal";
            }
            if (cell is Wall) return "Wall";
            if (cell is Box) return "Box";
            if (cell is PlayerCell) return "Player";

            return string.Empty;
        }
        

        bool IsOutOfMap(Position pos)
        {
            return pos.X < 0 || pos.X >= map.Width || pos.Y < 0 || pos.Y >= map.Height;
        }
    }
}
