using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class LevelInfo
    {
        public string Name { get; }
        public Map Map { get; }
        Position Player { get; }
        public List<Position> Goal { get; }
    }
}
