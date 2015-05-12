using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestProject.Locations
{
    public class Trap : ICell
    {
        public Trap() { }

        public ICell TryMove(IHero man, Point point)
        {
            man.SetCoordinates(point);
            man.ChangeCountOfLife(-1);
            return new Locations.Trap();
        }
    }
}
