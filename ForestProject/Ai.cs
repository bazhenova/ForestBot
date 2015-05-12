using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForestProject.Locations;

namespace ForestProject
{
    public class Ai : IAi
    {
        private Point CurrentPosition;
        private Point Target;
        private int Width;
        private int Hight;
        private HashSet<Point> Closed;
        private HashSet<Point> Visited;

        public Ai(Point start, Point target, int width, int hight)
        {
            CurrentPosition = start;
            Target = target;
            Width = width;
            Hight = hight;
            Closed = new HashSet<Point>();
            Visited = new HashSet<Point>();
        }

        private IEnumerable<Point> GetNeighbours(Point point)
        {
            var x = 0;
            var y = 0;
            var neigh = new Point(0, 0);
            for (int dx = -1; dx < 2; dx++)
                for (int dy = -1; dy < 2; dy++)
                {
                    if (Math.Abs(dx - dy) == 0 || Math.Abs(dx - dy) == 2 || dx == 0 && dy == 0)
                        continue;
                    x = point.X + dx;
                    y = point.Y + dy;
                    if (x < 0 || x > Width - 1 || y < 0 || y > Hight - 1)
                        continue;
                    if (Closed.Contains(new Point(x, y)))
                        continue;
                    yield return new Point(x, y);
                }       
        }

        public Point GetNextMove()
        {
            if (CurrentPosition.Equals(Target))
                return Target;
            var pathAndFinish = GetNotVisitedCell();
            var path = pathAndFinish.Item1;
            var finish = pathAndFinish.Item2;
            if (finish.Equals(new Point(-1, -1)))
                return new Point(-1, -1);
            var next = path[finish];
            while (!next.Equals(CurrentPosition))
            {
                finish = next;
                next = path[finish];
            }
            return finish;
        }

        private Tuple<Dictionary<Point, Point>, Point> GetNotVisitedCell()
        {
            var queue = new Queue<Point>();
            var path = new Dictionary<Point, Point>();
            var traversed = new HashSet<Point>();
            queue.Enqueue(CurrentPosition);
            Visited.Add(CurrentPosition);
            path[CurrentPosition] = new Point(-1, -1);
            while (queue.Any())
            {
                var curPoint = queue.Dequeue();
                traversed.Add(curPoint);
                foreach (var neigh in GetNeighbours(curPoint))
                {
                    if (!Visited.Contains(neigh))
                    {
                        path[neigh] = curPoint;
                        return new Tuple<Dictionary<Point,Point>,Point>(path, neigh);
                    }
                    path[neigh] = curPoint;
                    if (!traversed.Contains(neigh))
                        queue.Enqueue(neigh);
                }
            }
            return new Tuple<Dictionary<Point, Point>, Point>(path, new Point(-1, -1));
        }

        public Point GetDirection(Point next)
        {
            var direction = new Point(next.X - CurrentPosition.X, next.Y - CurrentPosition.Y);
            return direction;
        }

        public void SetClosedAndVisited(Point coordinates, bool isMove)
        {
            if (!isMove)
                Closed.Add(coordinates);
            Visited.Add(coordinates);
        }

        //public Point GetDirection(Func<IHero, Point, bool> tryMove)
        //{
        //    var next = GetNextMove();
        //    if (next.Equals(new Point(-1, -1)))
        //        next = CurrentPosition;
        //    var delta = new Point(next.X - CurrentPosition.X, next.Y - CurrentPosition.Y);
        //    if (delta.Equals(new Point(0, 0)))
        //        Forest.IsPath = false;
        //    var isMove = tryMove(Man, delta);
        //    if (!isMove)
        //        Closed.Add(next);
        //    Visited.Add(next);
        //    return delta;
        //}

        public void SetPosition(Point position)
        {
            CurrentPosition = position;
        }
    }
}
