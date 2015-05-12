using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestProject
{
    public interface IAi
    {
        //Point GetDirection(Func<IHero, Point, bool> func);
        Point GetDirection(Point next);
        void SetPosition(Point position);
        Point GetNextMove();
    }
}
