using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestProject.Locations
{
    public class Jungle : ICell
    {
        public Jungle() { }

        public ICell TryMove(IHero man, Point point)
        {
            return new Locations.Jungle();
        }
    }
}
