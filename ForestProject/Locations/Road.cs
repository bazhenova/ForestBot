using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestProject.Locations
{
    public class Road : ICell
    {
        public Road() { }

        public ICell TryMove(IHero man, Point point)
        {
            man.SetCoordinates(point);
            return new Locations.Road();
        }        
    }
}
