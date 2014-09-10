using ComputerCraftHost.Services.Turtle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCraftRemote.TurtleAPI
{
    public class Terminal
    {
        private Turtle m_turtle;
        private CCServiceClient m_turtleService;

        private const String newLineCommand = 
@"
monitor = peripheral.wrap(""back"")
local _, cY= monitor.getCursorPos()
monitor.setCursorPos(1, cY+1)
";

        internal Terminal(Turtle turtle)
        {
            m_turtle = turtle;
            m_turtleService = turtle.RemoteServer.GetTurtleService();
        }

        public void SetTextScale(int value)
        {
            String commandString = String.Join(
                Environment.NewLine,
                "monitor = peripheral.wrap(\"back\")",
                "monitor.setTextScale(" + value.ToString() + ")",
                newLineCommand
            );
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
        }

        public void Info(String text)
        {
            String commandString = String.Join(
                Environment.NewLine,
                "monitor = peripheral.wrap(\"back\")",
                "monitor.setTextColor(colors.green)",
                "monitor.write(\"[INFO] \")",
                "monitor.setTextColor(colors.white)",
                "monitor.write(\"" + text + "\")",
                newLineCommand
            );
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
        }
        	
        public void Warning(String text)
        {
            String commandString = String.Join(
                Environment.NewLine,
                "monitor = peripheral.wrap(\"back\")",
                "monitor.setTextColor(colors.yellow)",
                "monitor.write(\"[WRNG] \")",
                "monitor.setTextColor(colors.white)",
                "monitor.write(\"" + text + "\")",
                newLineCommand
            );
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
        }

        public void Error(String text)
        {
            String commandString = String.Join(
                Environment.NewLine,
                "monitor = peripheral.wrap(\"back\")",
                "monitor.setTextColor(colors.red)",
                "monitor.write(\"[ERROR] \")",
                "monitor.write(\"" + text + "\")",
                "monitor.setTextColor(colors.white)",
                newLineCommand
            );
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
        }

        public void Clear()
        {
            String commandString = String.Join(
                Environment.NewLine,
                "monitor = peripheral.wrap(\"back\")",
                "monitor.clear()",
                "monitor.setCursorPos(1,1)"
            );
            String retValue = m_turtleService.InvokeCommandOnTurtle(m_turtle.TurtleID, commandString);
        }

    }
}
