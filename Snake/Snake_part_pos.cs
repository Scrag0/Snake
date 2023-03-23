using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal struct Snake_part_pos
    {
        private int _i;
        private int _j;

        public Snake_part_pos(int i, int j)
        {
            _i = i;
            _j = j;
        }

        public int I
        {
            get { return _i; }
            set { _i = value; }
        }

        public int J
        {
            get { return _j; }
            set { _j = value; }
        }
    }
}
