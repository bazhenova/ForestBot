using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ForestProject
{
    public class Client
    {
        private Socket ClientSocket;
        private string Name;
        private Serializer Serializer = new Serializer();
        private Dictionary<Point, int> Vectors = new Dictionary<Point, int>() {
            {new Point(0, -1), 0},
            {new Point(1, 0), 1},
            {new Point(0, 1), 2},
            {new Point(-1, 0), 3},
        };
        private Point LastMove;
        private Ai Ai;

        public Client(int port, string name)
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ClientSocket.Connect(new IPEndPoint(IPAddress.Loopback, port));
            Name = name;
        }

        public void StartGame()
        {
            SendHello();
            var info = RecieveData<ClientInfo>(ClientSocket, 1024);
            Ai = new Ai(info.StartPosition, info.Target, info.MapSize.X, info.MapSize.Y);
            var endOfGame = false;
            while (!endOfGame)
            {
                try
                {
                    SendMove();
                    var result = RecieveData<MoveResultInfo>(ClientSocket);
                    Console.WriteLine("Answer: {0}", result.Result);
                    var isMove = result.Result == 0 ? false : true;
                    Ai.SetClosedAndVisited(LastMove, isMove);
                    if (result.Result == 1)
                        Ai.SetPosition(LastMove);
                    else if (result.Result == 2)
                        endOfGame = true;
                }
                catch { throw; }
            }
            Console.WriteLine("Конец игры! Цель достигнута!");
            Console.ReadKey();
        }

        private T RecieveData<T>(Socket socket, int length = 1024)
        {
            var data = new byte[length];
            socket.Receive(data);
            T result = Serializer.Deserialize<T>(new MemoryStream(data));
            return result;
        }

        private void SendHello()
        {            
            Hello hello = new Hello() 
            { 
                IsVisualizator = false, 
                Name = Name 
            };            
            ClientSocket.Send(Serializer.Serialize(hello).ToArray());
        }

        private void SendMove()
        {
            var next = Ai.GetNextMove();
            LastMove = next;
            Console.WriteLine("Next move: {0} {1}", next.X, next.Y);
            var delta = Ai.GetDirection(next);
            Move move = new Move()
            {
                Direction = Vectors[delta]
            };
            ClientSocket.Send(Serializer.Serialize(move).ToArray());
        }
    }
}
