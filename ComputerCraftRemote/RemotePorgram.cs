using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ComputerCraftHost.Services.Turtle;

namespace ComputerCraftRemote
{
    public class RemotePorgram
    {
        public static void Main(String[] args)
        {
            ComputerCraftRemoteClient client = new ComputerCraftRemoteClient("Alec", "localhost", 9090);

            TurtleServiceClient turtleService = client.GetTurtleService();
            
            //for(int i = 0; i < 100; i++)
            //{
            //    Thread thread = new Thread((turtleNum) =>
            //        {
            //            Console.WriteLine("Thread: " + (int)turtleNum);
            //            Stopwatch timer = Stopwatch.StartNew();
            //            int count = 500;
            //            for (int c = 0; c < count; c++)
            //                turtleService.QuickReturn("Hello World!");
            //            timer.Stop();
            //            Console.WriteLine("Thread: [" + (int)turtleNum + "] took [" + timer.Elapsed + "], or [" + ((float)count) / timer.Elapsed.TotalSeconds + "] Ops/Second");
            //        });
            //    thread.Name = "Pusher: " + i;
            //    thread.Start(i);
            //}

            //for (int i = 0; i < 10; i++)
            //{
            //    Thread thread = new Thread((turtleNum) =>
            //    {
            //        Console.WriteLine("Thread: " + (int)turtleNum);
            //        while (true)
            //            turtleService.InvokeCommandOnTurtle((int)turtleNum, @"return turtle.turnLeft()");
            //    });
            //    thread.Name = "Pusher: " + i;
            //    thread.Start(i);
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
        }
    }
}
