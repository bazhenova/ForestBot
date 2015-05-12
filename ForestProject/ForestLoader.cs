using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForestProject.Locations;

namespace ForestProject
{
    public class ForestLoader
    {
        private Dictionary<char, Func<ICell>> Creation = new Dictionary<char, Func<ICell>>
        {
            {'K',() => new Trap()},
            {'0',() => new Road()},
            {'1',() => new Jungle()},
            {'L',() => new Life()}
        };

        private string[] Lines;

        public ForestLoader(params string[] lines)
        {
            Lines = lines;
        }

        public Tuple<ICell[,], Point, Point> LoadData()
        {
            ICell[,] map;
            var sizes = Lines[0].Split(' ').Select(x => int.Parse(x)).ToArray();
            map = new ICell[sizes[0], sizes[1]];
            for (int y = 1; y <= sizes[0]; y++)
            {
                var line = Lines[y];
                for (int x = 0; x < sizes[1]; x++)
                {
                    map[y - 1, x] = Creation[line[x]]();
                }
            }
            var source = new Point(1, 1);
            var target = new Point(1, 1);
            var coords = Lines[Lines.Length - 1].Split(' ').Select(x => int.Parse(x)).ToArray();
            source = new Point(coords[0], coords[1]);
            target = new Point(coords[2], coords[3]);
            return new Tuple<ICell[,], Point, Point>(map, source, target);
        }

        public ICell[,] LoadMap()
        {
            ICell[,] map = new ICell[Lines.Length, Lines[0].Length];
            for (int y = 0; y < Lines.Length; y++)
            {
                var line = Lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    map[y, x] = Creation[line[x]]();
                }
            }
            return map;
        }
    }
}
