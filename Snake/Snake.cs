using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Snake
    {
        private List<Snake_part_pos> _parts = new List<Snake_part_pos> { new Snake_part_pos(5, 3), new Snake_part_pos(5, 4), new Snake_part_pos(5, 5) };
        private ConsoleKeyInfo move_to;

        public List<Snake_part_pos> Parts 
        { 
            get { return _parts; }
            set { _parts = value; }
        }
        public ConsoleKeyInfo Move
        {
            get { return move_to; }
            set { move_to = value; }
        }
    };
}
