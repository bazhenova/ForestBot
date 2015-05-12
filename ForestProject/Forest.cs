using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForestProject.Locations;

namespace ForestProject
{
    public class Forest : IForest
    {
        public ICell[,] Map { get; private set; }
        public IHero Hero { get; private set; }
        private IAi ai;
        public bool IsPath { get; set; }

        public Forest(ICell[,] map)
        {
            Map = map;
        }

        public void SetHero(IHero man, Point point)
        {
            Hero = man;
            Hero.SetCoordinates(point);
        }

        public void DoAllMoves()
        {
        //    IsPath = true;
        //    while (true)
        //    {
        //        ai.Move(TryMove);
        //        if (OnChange != null) OnChange();
        //        else if (!IsPath)
        //            break;
        //        else if (Hero.GetCoordinates().Equals(Hero.Target))
        //            break;
                    
        //    }
        }

        public bool TryMove(IHero man, Point delta)
        {
            var lastCoordinate = man.GetCoordinates();
            var newCoord = new Point(lastCoordinate.X + delta.X, lastCoordinate.Y + delta.Y);
            var result = Map[newCoord.Y, newCoord.X].TryMove(man, newCoord);
            Map[newCoord.Y, newCoord.X] = result;
            return !lastCoordinate.Equals(Hero.GetCoordinates());
        }

        public void AddBot(IAi ai)
        {
            this.ai = ai;
        }

        public delegate void ActionMethod();
        public event ActionMethod OnChange;        
    }
}
