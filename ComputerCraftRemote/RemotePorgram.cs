﻿using System;
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
        public static CCServiceProvider TurtleService;

        public static void Main(string[] args)
        {
            TurtleService = new CCServiceProvider(@"Alec", @"localhost", 9090);
            TurtleService.Terminal.Info("Welcome to the debug console!");

            WhileLoop();
            //WhileAndForLoop();
            //DoWhileAndIfStatment();
            //WRONGMoreComplex();
            //CORRECTMoreComplex();


            //Task.Factory.StartNew(WhileLoop);
            //Task.Factory.StartNew(WhileAndForLoop);
            //Task.Factory.StartNew(DoWhileAndIfStatment);
            //Task.Factory.StartNew(CORRECTMoreComplex);
            //Console.ReadLine();
        }

        public static void WhileLoop()
        {
            Turtle greenTurtle = TurtleService.GetTurtleById(11);
            TurtleService.Terminal.Info("Spinning green turtle in circles!");

            while (true)
                greenTurtle.Movement.TurnRight();
        }

        public static void WhileAndForLoop()
        {
            Turtle blackTurtle = TurtleService.GetTurtleById(20);
            TurtleService.Terminal.Info("Back turtle doing the dishwasher!");
            
            while (true)
            {
                for (int i = 0; i < 4; i++)
                    blackTurtle.Movement.TurnLeft();

                Thread.Sleep(1000);

                for (int i = 0; i < 4; i++)
                    blackTurtle.Movement.TurnRight();

                Thread.Sleep(1000);
            }
        }

        public static void DoWhileAndIfStatment()
        {
            Turtle purpleTurtle = TurtleService.GetTurtleById(10);
            TurtleService.Terminal.Info("Purple running the loop!");

            Boolean moveSucceeded = false; 
            do
            {
                moveSucceeded = purpleTurtle.Movement.Forward();
            } while(moveSucceeded);

            TurtleService.Terminal.Info("Hit a wall, turn around");
            purpleTurtle.Movement.TurnLeft();
            purpleTurtle.Movement.TurnLeft();

            do
            {
            } while (purpleTurtle.Movement.Forward());

            TurtleService.Terminal.Info("Hit start wall, turn around again");
            purpleTurtle.Movement.TurnLeft();
            purpleTurtle.Movement.TurnLeft();
        }

        public static void WRONGMoreComplex()
        {
            Turtle blueTurtle = TurtleService.GetTurtleById(9);
            TurtleService.Terminal.Info("Starting MoreComplex");

            // Go forward, checking left each time
            while (blueTurtle.Movement.Forward())
                if (!OurFirstFunction_CheckLeft(blueTurtle))
                    break;

            // Hit end, turn around
            TurtleService.Terminal.Info("Hit end, turning around.");
            blueTurtle.Movement.TurnLeft();
            blueTurtle.Movement.TurnLeft();

            while (blueTurtle.Movement.Forward())
                if (!OurSecondFunction_CheckRight(blueTurtle))
                    break;

            // Back at start, turn around
            TurtleService.Terminal.Info("Back at beginning. Turning around.");
            blueTurtle.Movement.TurnLeft();
            blueTurtle.Movement.TurnLeft();
        }

        public static void CORRECTMoreComplex()
        {
            Turtle blueTurtle = TurtleService.GetTurtleById(9);

            // Go forward, checking left each time
            while (OurFirstFunction_CheckLeft(blueTurtle) || blueTurtle.Movement.Forward())
            {
            }

            // Hit end, turn around
            blueTurtle.Movement.TurnLeft();
            blueTurtle.Movement.TurnLeft();

            while (OurSecondFunction_CheckRight(blueTurtle) || blueTurtle.Movement.Forward())
            {
            }

            // Back at start, turn around
            blueTurtle.Movement.TurnLeft();
            blueTurtle.Movement.TurnLeft();
        }

        public static Boolean OurFirstFunction_CheckLeft(Turtle blueTurtle)
        {
            blueTurtle.Movement.TurnLeft();
            bool isThereAHole = blueTurtle.Movement.Forward();
            if (!isThereAHole)
                blueTurtle.Movement.TurnRight();

            return isThereAHole;
        }

        public static Boolean OurSecondFunction_CheckRight(Turtle blueTurtle)
        {
            blueTurtle.Movement.TurnRight();
            bool isThereAHole = blueTurtle.Movement.Forward();
            if (!isThereAHole)
                blueTurtle.Movement.TurnLeft();

            return isThereAHole;
        }

    }
}
