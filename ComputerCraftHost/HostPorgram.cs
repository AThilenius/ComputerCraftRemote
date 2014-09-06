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

            TurtleServiceHandler.Initialize(httpServer);

            ComputerRemoteServer server = new ComputerRemoteServer(httpServer);
            server.Start();

            while (true)
            {
                Console.Write("> ");
                String command = Console.ReadLine();
                int id = int.Parse(command.Split()[0]);
                command = command.Remove(0, 2);
                Console.Write("[" + command + "]: ");
                String results = httpServer.RunCommand(id, command);
                Console.WriteLine(results);
            }

        }
    }
}
