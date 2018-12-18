﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static bool operator ==(Position pos1, Position pos2)
        {
            return pos1.Equals(pos2);
        }
        public static bool operator !=(Position pos1, Position pos2)
        {
            return !pos1.Equals(pos2);
        }
        public override bool Equals(object obj)
        {
            var other = (Position)obj;
            return X == other.X && Y == other.Y;
        }

    }
}
