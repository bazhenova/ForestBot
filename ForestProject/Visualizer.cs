using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForestProject.Locations;

namespace ForestProject
{
    public class Visualizer
    {
        private static Dictionary<int, char> DisplayDict = new Dictionary<int, char>()
        {
            {3, '*'},
            {4, '♥'},
            {2, '█'},
            {1, ' '},
        };

        private int[,] Map;
        private Player Player;

        public void SetData(Player player, int[,] map)
        {
            Map = map;
            Player = player;
        }

        public void ChangeData(Tuple<Point, int>[] mapChanges, Tuple<int, Point, int> playerChanges)
        {
            foreach (var change in mapChanges)
            {
                Map[change.Item1.Y, change.Item1.X] = change.Item2;
            }
            Player.Hp = playerChanges.Item3;
            Player.StartPosition = playerChanges.Item2;
        }

        public void Update()
        {
            Console.Clear();
            var array = new char[Map.GetLength(0), Map.GetLength(1)];
            for (int row = 0; row < array.GetLength(0); row++)
                for (int col = 0; col < array.GetLength(1); col++)
                    array[row, col] = DisplayDict[Map[row, col]];

            if (Player.Hp != 0)
                array[Player.StartPosition.Y, Player.StartPosition.X] = Player.Nick[0];

            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int col = 0; col < array.GetLength(1); col++)
                    Console.Write(array[row, col]);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("█ - заросли");
            Console.WriteLine("♥ - жизнь");
            Console.WriteLine("*  - капкан");
            Console.WriteLine(Player.Nick[0] + " - лесной житель " + "(кол-во жизней: {0})", Player.Hp);
            if (Player.Hp == 0)
                Console.WriteLine("Цель недостижима по причине смерти лесного жителя");
            else if (Player.StartPosition.Equals(Player.Target))
                Console.WriteLine("Цель достигнута!");
        }
    }
}
