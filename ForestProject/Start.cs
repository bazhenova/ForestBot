using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ForestProject
{
    class Start
    {
        static void Main(string[] args)
        {
            Server server;
            Client gamer;
            ClientVis visualizer;
            int port = 30765;
            int id = int.Parse(Console.ReadLine());
            if (!File.Exists("config.txt"))
                WriteData();
            var name = "Visualizer";

            if (id == 0)
            {
                server = new Server(port);
                server.CreateConnection();
            }
            else if (id == 1)
            {
                name = Console.ReadLine();
                gamer = new Client(port, name);
                gamer.StartGame();
            }
            else
            {
                visualizer = new ClientVis(port);
                visualizer.Start();
            }
        }

        private static void WriteData()
        {
            Config config = new Config()
            {
                PlayersCount = 1,
                FileWithMap = "test.txt",
                StartAndFinish = new Tuple<Point, Point>(new Point(1, 1), new Point(7, 5))
            };
            var data = JsonConvert.SerializeObject(config);
            File.WriteAllText("config.txt", data);
        }
    }
}
