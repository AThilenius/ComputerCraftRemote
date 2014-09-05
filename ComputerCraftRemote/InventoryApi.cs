using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCraftRemote
{
    public class InventoryApi
    {
        private Turtle m_turtle;

        internal InventoryApi(Turtle turtle)
        {
            m_turtle = turtle;
        }

        public Boolean Select(Int32 slotNumber)
        {
            String commandString = @"return turtle.select(" + slotNumber + ")";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Int32 GetSelectedSlot()
        {
            String commandString = @"return turtle.getSelectedSlot()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Int32.Parse(retValue);
        }

        public Int32 GetItemCount(Int32 slotNumber)
        {
            String commandString = @"return turtle.getItemCount(" + slotNumber + ")";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Int32.Parse(retValue);
        }

        public Int32 GetItemSpace(Int32 slotNumber)
        {
            String commandString = @"return turtle.getItemSpace(" + slotNumber + ")";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Int32.Parse(retValue);
        }

        public Boolean EquipLeft()
        {
            String commandString = @"return turtle.equipLeft()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

        public Boolean EquipRight()
        {
            String commandString = @"return turtle.equipRight()";
            String retValue = m_turtle.RemoteServer.RemoteingService.invokeCommandSync(m_turtle.TurtleID, commandString);
            return Boolean.Parse(retValue);
        }

    }
}
