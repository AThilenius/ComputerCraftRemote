using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerCraft.Core.Rpc;
using Lidgren.Network;

namespace ComputerCraftHost.Services.Turtle
{
    public class CCServiceClient
    {
        private RpcHost m_host;
        private NetConnection m_target;

        public CCServiceClient(RpcHost host, NetConnection target)
        {
            m_host = host;
            m_target = target;
        }

        public String InvokeCommandOnTurtle(int computerId, String command)
        {
            return m_host.Invoke<String>(m_target,
                    typeof(CCServiceHandler).GetMethod("InvokeCommandOnTurtle_Handler"),
                    computerId,
                    command);
        }

        public bool RequestPoolOwnership(string poolName, string ownerName)
        {
            return m_host.Invoke<bool>(m_target,
                    typeof(CCServiceHandler).GetMethod("RequestPoolOwnership_Handler"),
                    poolName,
                    ownerName);
        }

        public void FreePool(string poolName)
        {
            m_host.Invoke<String>(m_target,
                    typeof(CCServiceHandler).GetMethod("FreePool_Handler"),
                    poolName);
        }

        public List<TurtleIdPool> GetAllTurtles()
        {
            return m_host.Invoke<List<TurtleIdPool>>(m_target,
                    typeof(CCServiceHandler).GetMethod("GetAllTurtles_Handler"),
                    null);
        }

        public List<PoolOwner> GetAllPools()
        {
            return m_host.Invoke<List<PoolOwner>>(m_target,
                    typeof(CCServiceHandler).GetMethod("GetAllPools_Handler"),
                    null);
        }

        public String QuickReturn(String message)
        {
            return m_host.Invoke<String>(m_target,
                    typeof(CCServiceHandler).GetMethod("QuickReturn_Handler"),
                    message);
        }
    }
}
