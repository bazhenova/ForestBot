using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestProject
{
    public interface IHero
    {
        void ChangeCountOfLife(int delta);
        void SetCoordinates(Point currentCoordinates);
        Point GetCoordinates();
        char GetNameSymbol();
        int CountOfLife { get; }
        Point Target { get; }
    }
}
