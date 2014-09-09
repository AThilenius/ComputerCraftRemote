using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerCraft.Core.Rpc;
using Lidgren.Network;
using ComputerCraftRemote;

namespace ComputerCraftHost.Services.Turtle
{
    public class TurtleIdPool
    {
        public int TurtleId;
        public string PoolName;
        public string StartLocation;
    }

    public class PoolOwner
    {
        public String PoolName;
        public String OwnerName;
    }

    public static class CCServiceHandler
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

        public static bool RequestPoolOwnership_Handler(string poolName, string ownerName)
        {
            if (!m_httpServer.OwnerByPoolName.ContainsKey(poolName))
                m_httpServer.OwnerByPoolName.Add(poolName, "None");

            if (m_httpServer.OwnerByPoolName[poolName] == "None")
            {
                Console.WriteLine(ownerName + " took ownership of pool " + poolName);
                m_httpServer.OwnerByPoolName[poolName] = ownerName;
                return true;
            }

            else
                return false;
        }

        public static void FreePool_Handler(string poolName)
        {
            lock (m_httpServer.OwnerByPoolName)
            {
                if (m_httpServer.OwnerByPoolName[poolName] != "Unassigned")
                    m_httpServer.OwnerByPoolName[poolName] = "Unassigned";
            }
        }

        public static List<TurtleIdPool> GetAllTurtles_Handler()
        {
            List<TurtleIdPool> turtles = new List<TurtleIdPool>();

            lock (m_httpServer.ComputerBuffersById)
            {
                foreach (var kvp in m_httpServer.ComputerBuffersById)
                    turtles.Add(new TurtleIdPool { TurtleId = kvp.Key, PoolName = kvp.Value.PoolName, StartLocation = kvp.Value.StartingLocation });
            }

            return turtles;
        }

        public static List<PoolOwner> GetAllPools_Handler()
        {
            List<PoolOwner> pools = new List<PoolOwner>();

            lock (m_httpServer.OwnerByPoolName)
            {
                foreach (var kvp in m_httpServer.OwnerByPoolName)
                    pools.Add(new PoolOwner { PoolName = kvp.Key, OwnerName = kvp.Value });
            }

            return pools;
        }

        public static String QuickReturn_Handler(String message)
        {
            if (!m_wasInitialized)
                throw new Exception("Must call Initialize!");

            return message;
        }
    }
}
