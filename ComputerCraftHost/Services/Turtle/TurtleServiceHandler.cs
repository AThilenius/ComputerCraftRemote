using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerCraft.Core.Rpc;
using ComputerCraftRemote;
using Lidgren.Network;

namespace ComputerCraftHost.Services.Turtle
{
    public static class TurtleServiceHandler
    {
        private static Boolean m_wasInitialized;
        private static QueueHttpServer m_httpServer;

        internal static void Initialize(QueueHttpServer httpServer)
        {
            m_httpServer = httpServer;
            m_wasInitialized = true;
        }

        public static String InvokeCommandOnTurtle_Handler(int computerId, String command)
        {
            if (!m_wasInitialized) 
                throw new Exception("Must call Initialize!");

            return m_httpServer.RunCommand(computerId, command);
        }

        public static String QuickReturn_Handler(String message)
        {
            if (!m_wasInitialized)
                throw new Exception("Must call Initialize!");

            return message;
        }
    }
}
