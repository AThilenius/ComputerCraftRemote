using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerCraft.Core.Rpc;
using Lidgren.Network;

namespace ComputerCraftHost.Services.Turtle
{
    public class TurtleServiceClient
    {
        private RpcHost m_host;
        private NetConnection m_target;

        public TurtleServiceClient(RpcHost host, NetConnection target)
        {
            m_host = host;
            m_target = target;
        }

        public String InvokeCommandOnTurtle(int computerId, String command)
        {
            return m_host.Invoke<String>(m_target,
                    typeof(TurtleServiceHandler).GetMethod("InvokeCommandOnTurtle_Handler"),
                    computerId,
                    command);
        }

        public String QuickReturn(String message)
        {
            return m_host.Invoke<String>(m_target,
                    typeof(TurtleServiceHandler).GetMethod("QuickReturn_Handler"),
                    message);
        }
    }
}
