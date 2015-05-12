using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestProject.Locations
{
    public interface ICell
    {
        ICell TryMove(IHero man, Point point);
    }
}
