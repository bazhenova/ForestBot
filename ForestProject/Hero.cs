using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestProject
{
    public class Hero : IHero
    {
        public int CountOfLife { get; private set; }
        public Point Target { get; private set; }
        private string Name;
        private Point Coordinates;

        public Hero(int count, string name, Point target)
        {
            CountOfLife = count;
            Name = name;
            Target = target;
        }

        public void ChangeCountOfLife(int delta)
        {
            CountOfLife += delta;
        }

        public Point GetCoordinates()
        {
            return Coordinates;
        }

        public void SetCoordinates(Point curPlace)
        {
            Coordinates = curPlace;
        }

        public char GetNameSymbol()
        {
            return Name[0];
        }
    }
}
