using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardF
{
    public class Game
    {
        int size;
        public bool IsGameStarted { get; private set; }
        Map map;
        Coord spaceCoord;

        public int currentStepsCount { get; private set; }
        
        public Game(int size)
        {
            this.IsGameStarted = false;
            this.size = size;
            map = new Map(size);
        }
        
        // запускает игру и перемешивает
        public void Start(int stepsCount = 0)
        {
            this.IsGameStarted = true;

            int digit = 0;
            foreach( Coord newCoord in new Coord().YieldCoord(size) )
                map.Set(newCoord, ++digit);

            spaceCoord = new Coord(size);
            map.Set(spaceCoord, 0);

            // перемешивание
            if (stepsCount > 0)
            {
                Shuffle(stepsCount);
            }

            currentStepsCount = 0;
        }

        private void Shuffle(int stepsCount)
        {
            Random random = new Random(stepsCount);
            for (int i = 0; i < stepsCount; i++)
            {
                PressAt(random.Next(size), random.Next(size));
            }
        }

        // return how much steps was make
        public int PressAt( int x, int y )
        {
            return PressAt( new Coord( x, y ) );
        }

        // internal kitchen of heandling coords
        // return how much steps was make
        int PressAt(Coord currentCoord)
        {
            if (spaceCoord.Equals(currentCoord)) 
                return 0;

            // check press by diagonal
            if (currentCoord.x != spaceCoord.x &&
                currentCoord.y != spaceCoord.y) 
                return 0;

            int steps = Math.Abs(currentCoord.x - spaceCoord.x) +
                        Math.Abs(currentCoord.y - spaceCoord.y);

            while (currentCoord.x != spaceCoord.x)
            {
                Shift(Math.Sign(currentCoord.x - spaceCoord.x), 0);
            }

            while (currentCoord.y != spaceCoord.y)
            {
                Shift(0, Math.Sign(currentCoord.y - spaceCoord.y));
            }

            currentStepsCount += steps;

            return steps;
        }

        void Shift(int shiftX, int shiftY)
        {
            Coord nextSpaceCoord = spaceCoord.Add(shiftX, shiftY);
            map.Swap(spaceCoord, nextSpaceCoord);
            spaceCoord = nextSpaceCoord;
        }

        public int GetDigitalAt( int x, int y )
        {
            return GetDigitalAt(new Coord(x, y));
        }

        int GetDigitalAt(Coord currentCoord)
        {
            return map.Get(currentCoord);
        }

        // solution check 
        public bool Solved()
        {
            if (!spaceCoord.Equals(new Coord(size)))
                return false;

            int digit = 0;
            foreach (Coord currentCoord in new Coord().YieldCoord(size))
                if (map.Get(currentCoord) != ++digit)
                    return spaceCoord.Equals(currentCoord);

            this.IsGameStarted = false;
            return true;
        }
    }
}
