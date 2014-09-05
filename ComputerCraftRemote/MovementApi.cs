using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCraftRemote
{
    public class MovementApi
    {
        private Turtle m_turtle;

        internal MovementApi(Turtle turtle)
        {
            m_turtle = turtle;
        }

        public Boolean Forward()
        {
            String commandString = @"return turtle.forward()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean Back()
        {
            String commandString = @"return turtle.back()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean Up()
        {
            String commandString = @"return turtle.up()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean Down()
        {
            String commandString = @"return turtle.down()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean TurnLeft()
        {
            String commandString = @"return turtle.turnLeft()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean TurnRight()
        {
            String commandString = @"return turtle.turnRight()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean Dig()
        {
            String commandString = @"return turtle.dig()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean DigUp()
        {
            String commandString = @"return turtle.digUp()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean DigDown()
        {
            String commandString = @"return turtle.digDown()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

    }
}
