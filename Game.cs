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
        private readonly List<Position> GoalPosition;

        public Game(LevelInfo levelInfo)
        {
            playerPosition = null;
            GoalPosition = new List<Position>();
            map = new Map(levelInfo.MapWidth,levelInfo.MapHeight,levelInfo.GetMap());
            playerPosition = levelInfo.GetPlayerPosition();
            GoalPosition = levelInfo.GetGoals();
        }

        public bool TryMakeStep(Directions direction)
        {
            if (direction != Directions.None)
            {
                var newPos = GetNewPosition(playerPosition, direction);

                if (map.GetCell(newPos) is Wall || IsOutOfMap(newPos))
                {
                    newPos = playerPosition;
                }
                else if (map.GetCell(newPos) is Box)
                {
                    if (TryMoveBox(direction, newPos))
                    {
                        var newBoxPos = GetNewPosition(newPos, direction);
                        map.Move(newPos, newBoxPos);
                    }
                    else
                    {
                        newPos = playerPosition;
                    }
                }
                if (playerPosition == newPos)
                    return false;
                playerPosition = newPos;
                return true;
            }
            return false;
        }

        bool TryMoveBox(Directions direction, Position pos)
        {
            Position newPos = GetNewPosition(pos, direction);
            if (map.GetCell(newPos) is Wall || IsOutOfMap(newPos) || map.GetCell(newPos) is Box)
                return false;
            return true;
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
           
            if (cell is Wall) return "Wall";
            if (cell is Box) return "Box";

            if (playerPosition == pos && GoalPosition.Contains(pos))
                return "PlayerOnGoal";
            else if (playerPosition == pos)
                return "Player";
            else if (GoalPosition.Contains(pos))
                return "Goal";

            return "Empty";
        }

        public bool IsWin()
        {
            foreach(var pos in GoalPosition)
            {
                if(!(map.GetCell(pos) is Box))
                {
                    return false;
                }
            }
            return true;
        }
        

        bool IsOutOfMap(Position pos)
        {
            return pos.X < 0 || pos.X >= map.Width || pos.Y < 0 || pos.Y >= map.Height;
        }
    }
}
