using ComputerCraftHost.Services.Turtle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCraftRemote.TurtleAPI
{
    public class Inventory
    {
        private Turtle m_turtle;
        private CCServiceClient m_turtleService;

        internal Inventory(Turtle turtle)
        {
            m_turtle = turtle;
            m_turtleService = turtle.RemoteServer.GetTurtleService();
        }

        public Boolean Select(int slotNumber)
        {
            String commandString = @"return turtle.select(" + slotNumber + ")";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public int GetSelectedSlot()
        {
            String commandString = @"return turtle.getSelectedSlot()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return int.Parse(retValue);
        }

        public int GetItemCount(int slotNumber)
        {
            String commandString = @"return turtle.getItemCount(" + slotNumber + ")";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return int.Parse(retValue);
        }

        public int GetItemSpace(int slotNumber)
        {
            String commandString = @"return turtle.getItemSpace(" + slotNumber + ")";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return int.Parse(retValue);
        }

        public Boolean EquipLeft()
        {
            String commandString = @"return turtle.equipLeft()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean EquipRight()
        {
            String commandString = @"return turtle.equipRight()";
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

    }
}
