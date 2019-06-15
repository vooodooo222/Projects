using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardF
{
    class Map
    {
        private int size;
        int[,] map;

        public Map(int size)
        {
            this.size = size;
            map = new int[size,size];
        }

        public void Set(Coord coord, int value)
        {
            if (coord.OnBoard(size))
            {
                map[coord.x, coord.y] = value;
            }
        }

        public int Get(Coord coord)
        {
            if (coord.OnBoard(size))
            {
                return map[coord.x, coord.y];
            }
            return 0;
        }

        public void Swap(Coord dst, Coord src)
        {
            int val = Get(dst);
            Set(dst, Get(src));
            Set(src, val);
        }

    }
}
