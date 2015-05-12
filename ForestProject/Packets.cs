using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForestProject.Locations;

namespace ForestProject
{
    public class Hello
    {
        public bool IsVisualizator;
        public string Name;
    }

    #region Работа с игроком

    public class ClientInfo
    {
        public Point MapSize; // x - witdh, y - height
        public int Hp;
        public Point StartPosition;
        public Point Target;
        //public int[,] VisibleMap; // видимая часть карты в начале игры.
    }

    public class Move
    {
        public int Direction;
    }

    public class MoveResultInfo
    {
        public int Result; // 2 -- GameOver.
        //public int[,] VisibleMap;
    }

    #endregion

    #region Работа с визуализатором


    public class WorldInfo
    {
        public Player Player;
        public int[,] Map;
    }

    public class Answer
    {
        public int AnswerCode;
    }

    public class LastMoveInfo
    {
        //public int WinnerId;
        public bool IsEnd;
        public Tuple<Point, int>[] ChangedCells;
        //public Tuple<int, Point, int>[] PlayersChangedPosition; // <id, new position, new hp>
        public Tuple<int, Point, int> PlayerChangedPosition;
    }


    #endregion

    public class Player
    {
        public Player(int id, string nick, Point startPos, Point target, int hp)
        {
            Id = id;
            Nick = nick;
            StartPosition = startPos;
            Target = target;
            Hp = hp;
        }

        public int Id;
        public string Nick;
        public int Hp;
        public Point StartPosition;
        public Point Target;
    }

}