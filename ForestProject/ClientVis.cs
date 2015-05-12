using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ForestProject
{
    class ClientVis
    {
        private Visualizer Visualizer;
        private Socket SocketVis;
        private Serializer Serializer = new Serializer();

        public ClientVis(int port)
        {
            SocketVis = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketVis.Connect(new IPEndPoint(IPAddress.Loopback, port));
            Visualizer = new Visualizer();
        }

        public void Start()
        {
            try
            {
                SendHello();
                var endOfGame = false;
                var info = RecieveData<WorldInfo>();
                Visualizer.SetData(info.Player, info.Map);
                Visualizer.Update();
                var answer = new Answer() { AnswerCode = 0 };
                SocketVis.Send(Serializer.Serialize(answer).ToArray());
                while (!endOfGame)
                {
                    var moveInfo = RecieveData<LastMoveInfo>();
                    Visualizer.ChangeData(moveInfo.ChangedCells, moveInfo.PlayerChangedPosition);
                    Visualizer.Update();
                    endOfGame = moveInfo.IsEnd;
                    answer = new Answer() { AnswerCode = 0 };
                    SocketVis.Send(Serializer.Serialize(answer).ToArray());
                }
            }
            catch { throw; }
            Console.ReadKey();
        }

        private void SendHello()
        {
            Hello hello = new Hello()
            {
                IsVisualizator = true,
                Name = "Visualizer"
            };
            SocketVis.Send(Serializer.Serialize(hello).ToArray());
        }

        private T RecieveData<T>(int length = 1024)
        {
            var data = new byte[length];
            SocketVis.Receive(data);
            T result = Serializer.Deserialize<T>(new MemoryStream(data));
            return result;
        }
    }
}
