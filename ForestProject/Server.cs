using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ForestProject.Locations;
using Newtonsoft.Json;

namespace ForestProject
{
    public class Server
    {
        private int PlayersCount;
        private Socket Listener;
        private Socket VisSocket;
        private ICell[,] Map;
        private int[,] IntMap;
        private List<IHero> Players;
        private int Hp = 5;
        private Serializer Serializer = new Serializer();
        private List<Socket> Sockets;
        private Dictionary<int, Point> Vectors = new Dictionary<int, Point>() 
        {
            {0, new Point(0, -1)},
            {1, new Point(1, 0)},
            {2, new Point(0, 1)},
            {3, new Point(-1, 0)},
        };
        private Point Target;
        private Point Source;
        private bool EndOfGame = false;
        private Dictionary<Type, int> ConverterToInt = new Dictionary<Type, int>()
        {
            {typeof(Trap), 3},
            {typeof(Life), 4},
            {typeof(Jungle), 2},
            {typeof(Road), 1},
        };
        private Dictionary<int, Func<ICell>> ConverterToICell = new Dictionary<int, Func<ICell>>()
        {
            {3, () => new Trap()},
            {4, () => new Life()},
            {2, () => new Jungle()},
            {1, () => new Road()},
        };

        public Server(int port)
        {
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Listener.Bind(new IPEndPoint(IPAddress.Any, port));
            Listener.Listen(7);
            LoadData();            
            Players = new List<IHero>();
            Sockets = new List<Socket>();
        }

        public void CreateConnection()
        {
            var isVis = false;
            try
            {
                while (true)
                {
                    var clientSocket = Listener.Accept();
                    var hello = RecieveData<Hello>(clientSocket);
                    Console.WriteLine("Соединение принято от {0}", clientSocket.RemoteEndPoint);
                    if (!hello.Name.Equals("Visualizer"))
                    {
                        var hero = new Hero(Hp, hello.Name, Target);
                        Players.Add(hero);
                        Sockets.Add(clientSocket);
                    }
                    else
                    {
                        isVis = true;
                        VisSocket = clientSocket;
                    }

                    Console.WriteLine(hello.Name);
                    if (Players.Count == PlayersCount && isVis)
                    {
                        PlayGame();
                        break;
                    }
                }
            }
            catch { throw; }
            foreach (var socket in Sockets)
                socket.Close();
            Listener.Close();
            Console.ReadKey();
        }

        private void PlayGame()
        {
            var forest = new Forest(Map);
            forest.SetHero(Players[0], Source);
            SendClientInfo(Players[0], Source, Target, Sockets[0]);
            var gamer = new Player(0, Players[0].GetNameSymbol().ToString(), Source, Target, Hp);
            SendWorldInfo(gamer);
            while (!EndOfGame)
            {
                try
                {
                    System.Threading.Thread.Sleep(150);
                    var move = RecieveData<Move>(Sockets[0]);
                    var direction = Vectors[move.Direction];
                    var prev = forest.Hero.GetCoordinates();
                    var result = forest.TryMove(Players[0], direction);
                    var currentCoords = forest.Hero.GetCoordinates();
                    if (currentCoords.Equals(Target))
                        EndOfGame = true;
                    if (result)
                    {
                        Map = forest.Map;
                        Hp = forest.Hero.CountOfLife;
                        SendLastMoveInfo(prev, currentCoords);
                    }
                    SendMoveResultInfo(result, Sockets[0]);
                }
                catch { throw; }
            }
        }

        private T RecieveData<T>(Socket socket, int length = 1024)
        {
            var data = new byte[length];
            socket.Receive(data);
            T result = Serializer.Deserialize<T>(new MemoryStream(data));
            return result;
        }
      
        private void SendClientInfo(IHero hero, Point start, Point target, Socket socket)
        {
            ClientInfo info = new ClientInfo()
            {
                MapSize = new Point(Map.GetLength(1), Map.GetLength(0)),
                Hp = Hp,
                StartPosition = start,
                Target = target
            };
            socket.Send(Serializer.Serialize(info).ToArray());
        }

        private void SendMoveResultInfo(bool result, Socket socket)
        {
            var res = 0;
            if (EndOfGame)
            {
                res = 2;
            }
            else
                res = result ? 1 : 0;
            MoveResultInfo info = new MoveResultInfo()
            {                
                Result = res
            };
            socket.Send(Serializer.Serialize(info).ToArray());
        }

        private void SendLastMoveInfo(Point last, Point now)
        {
            var first = new Tuple<Point, int>(last, ConverterToInt[Map[last.Y, last.X].GetType()]);
            var second = new Tuple<Point, int>(now, ConverterToInt[Map[now.Y, now.X].GetType()]);
            LastMoveInfo info = new LastMoveInfo()
            {
                ChangedCells = new Tuple<Point,int>[] { first, second },
                PlayerChangedPosition = new Tuple<int,Point,int>(0, now, Hp),
                IsEnd = EndOfGame
            };
            VisSocket.Send(Serializer.Serialize(info).ToArray());
            var answer = RecieveData<Answer>(VisSocket);
        }

        private void SendWorldInfo(Player player)
        {
            WorldInfo info = new WorldInfo()
            {
                Player = player,
                Map = Convert()
            };
            VisSocket.Send(Serializer.Serialize(info).ToArray());
            var answer = RecieveData<Answer>(VisSocket);
        }

        private int[,] Convert()
        {
            var map = new int[Map.GetLength(0), Map.GetLength(1)];
            for (int y = 0; y < Map.GetLength(0); y++)
                for (int x = 0; x < Map.GetLength(1); x++)
                    map[y, x] = ConverterToInt[Map[y, x].GetType()];
            return map;
        }        

        private void LoadData()
        {
            string data = File.ReadAllText("config.txt");
            Config info = JsonConvert.DeserializeObject<Config>(data);
            PlayersCount = info.PlayersCount;
            var file = info.FileWithMap;
            string map = File.ReadAllText("test.txt");
            IntMap = JsonConvert.DeserializeObject<int[,]>(map);
            Source = info.StartAndFinish.Item1;
            Target = info.StartAndFinish.Item2;
            Map = new ICell[IntMap.GetLength(0), IntMap.GetLength(1)];
            for (int y = 0; y < IntMap.GetLength(0); y++)
                for (int x = 0; x < IntMap.GetLength(1); x++)
                    Map[y, x] = ConverterToICell[IntMap[y, x]]();
        }
    }
}
