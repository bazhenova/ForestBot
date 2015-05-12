using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForestProject.Locations;

namespace ForestProject
{
    public interface IForest
    {
        void SetHero(IHero man, Point point);
        void AddBot(IAi ai);
        void DoAllMoves();
        ICell[,] Map { get; }
        IHero Hero { get; }
        bool IsPath { get; set; }
    }
}
