using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ComputerCraftHost.Services.Turtle;
using ComputerCraftRemote.TurtleAPI;

namespace ComputerCraftRemote
{
    public class RemotePorgram
    {
        public static void Main(String[] args)
        {
            Console.Write("User name: ");
            String username = Console.ReadLine();

            Console.Write("Remote Address: ");
            String address = Console.ReadLine();

            ComputerCraftRemoteClient client = new ComputerCraftRemoteClient(username, address, 9090);


            Console.WriteLine("Getting Free Turtle...");
            Turtle freeTurtle = client.GetFreeTurtle();


            Console.WriteLine("\nAll Turtles");
            foreach (Turtle turtle in client.GetAllTurtles())
                Console.WriteLine("Turtle: [" + turtle.TurtleID + "] in Pool: [" + turtle.Pool.Name + "] at location [" + turtle.StartLocation + "]");

            Console.WriteLine("\nAll Pools");
            foreach (TurtlePool pool in client.GetAllPools())
                Console.WriteLine("Pool: [" + pool.Name + "] owned by: [" + pool.Owner + "]");

            Console.WriteLine("\n\nOwned Turtles");
            foreach (Turtle turtle in client.GetAllOwnedTurtles())
                Console.WriteLine("Turtle: [" + turtle.TurtleID + "] in Pool: [" + turtle.Pool.Name + "] at location [" + turtle.StartLocation + "]");

            Console.WriteLine("\nOwned Pools");
            foreach (TurtlePool pool in client.GetAllOwnedPools())
                Console.WriteLine("Pool: [" + pool.Name + "] owned by: [" + pool.Owner + "]");


            freeTurtle.Pool.RequestOwnership();

            while (true)
                freeTurtle.Movement.TurnRight();
            
            Console.ReadLine();

        }
    }
}
