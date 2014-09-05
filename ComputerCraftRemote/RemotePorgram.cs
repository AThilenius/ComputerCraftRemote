using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

namespace ComputerCraftRemote
{
    public class RemotePorgram
    {
        public static void Main(String[] args)
        {
            ComputerCraftRemoteServer server = new ComputerCraftRemoteServer("Alec", "localhost", 9090);
            
            //while (true)
            //{
            //    turtle6.Movement.Forward();
            //    turtle6.Movement.TurnRight();
            //}

            //Task.Factory.StartNew(() =>
            //    {
            //        while (true)
            //        {
            //            Console.Clear();
            //            Console.WriteLine("All Turtles");
            //            foreach (Turtle turtle in server.GetAllTurtles())
            //                Console.WriteLine("Turtle: [" + turtle.TurtleID + "] in Pool: [" + turtle.Pool.Name + "] at location [" + turtle.StartLocation + "]");

            //            Console.WriteLine("\nAll Pools");
            //            foreach (TurtlePool pool in server.GetAllPools())
            //                Console.WriteLine("Pool: [" + pool.Name + "] owned by: [" + pool.Owner + "]");

            //            Console.WriteLine("\n\nOwned Turtles");
            //            foreach (Turtle turtle in server.GetAllOwnedTurtles())
            //                Console.WriteLine("Turtle: [" + turtle.TurtleID + "] in Pool: [" + turtle.Pool.Name + "] at location [" + turtle.StartLocation + "]");

            //            Console.WriteLine("\nOwned Pools");
            //            foreach (TurtlePool pool in server.GetAllOwnedPools())
            //                Console.WriteLine("Pool: [" + pool.Name + "] owned by: [" + pool.Owner + "]");

            //            Thread.Sleep(500);
            //        }
            //    });

            Console.ReadLine();
            while(true)
                foreach (Turtle turtle in server.GetAllTurtles())
                //Task.Factory.StartNew(() =>
                    //{
                        //while (true)
                            turtle.Movement.TurnRight();
                    //});

            Console.ReadLine();
        }
    }
}
