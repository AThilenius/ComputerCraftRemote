using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ComputerCraft.Core.Rpc;
using ComputerCraftHost;
using ComputerCraftHost.Services.Turtle;
using Lidgren.Network;

namespace ComputerCraftRemote
{
    public class HostProgram
    {
        public static void Main(String[] args)
        {
            QueueHttpServer httpServer = new QueueHttpServer(80);
            Task.Factory.StartNew(httpServer.listen);

            CCServiceHandler.Initialize(httpServer);

            ComputerRemoteServer server = new ComputerRemoteServer(httpServer);
            server.Start();

            Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        Console.Title = "Lidgren Connections: [" + server.LidgrenNetServer.Connections.Count + "]  Minecraft Turtles: [" + httpServer.ComputerBuffersById.Count + "]";
                        Thread.Sleep(100);
                    }
                });

            Console.WriteLine("[ID] [Times] [Command]");
            while (true)
            {
                Console.Write("> ");
                String command = Console.ReadLine();
                String[] tokens = command.Split();

                if (tokens.Length < 3)
                {
                    Console.WriteLine("Invalid command format");
                    continue;
                }

                int id = int.Parse(tokens[0]);
                int times = int.Parse(tokens[1]);


                String turtleCommand = "";
                for (int i = 2; i < tokens.Length; i++)
                    turtleCommand = turtleCommand + tokens[i];

                Console.WriteLine("[" + command + "] [" + times + "] times returned: ");
                for (int i = 0; i < times; i++)
                {
                    String results = httpServer.RunCommand(id, turtleCommand);
                    Console.WriteLine(results);
                }
            }

        }
    }
}
