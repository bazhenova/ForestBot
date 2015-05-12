using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ForestProject
{
    class Config
    {
        public int PlayersCount;
        public string FileWithMap;
        public Tuple<Point, Point> StartAndFinish;
    }
}
