using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerCraftHost.Services.Turtle;

namespace ComputerCraftRemote.TurtleAPI
{
    public class Movement
    {
        private Turtle m_turtle;
        private TurtleServiceClient m_turtleService;

        internal Movement(Turtle turtle)
        {
            m_turtle = turtle;
            m_turtleService = turtle.RemoteServer.GetTurtleService();
        }

        public Boolean Forward()
        {
            String commandString = @"return turtle.forward()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean Back()
        {
            String commandString = @"return turtle.back()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean Up()
        {
            String commandString = @"return turtle.up()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean Down()
        {
            String commandString = @"return turtle.down()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean TurnLeft()
        {
            String commandString = @"return turtle.turnLeft()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean TurnRight()
        {
            String commandString = @"return turtle.turnRight()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean Dig()
        {
            String commandString = @"return turtle.dig()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean DigUp()
        {
            String commandString = @"return turtle.digUp()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean DigDown()
        {
            String commandString = @"return turtle.digDown()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

    }
}
