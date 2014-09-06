using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerCraftRemote;

namespace ComputerCraftHost.Services.Pooling
{
    public static class PoolingServiceHandler
    {
        private static Boolean m_wasInitialized;
        private static QueueHttpServer m_httpServer;

        internal static void Initialize(QueueHttpServer httpServer)
        {
            m_httpServer = httpServer;
            m_wasInitialized = true;
        }

        //public static bool RequestPoolOwnership(string poolName, string ownerName)
        //{
        //    lock (m_httpServer.OwnerByPoolName)
        //    {
        //        if (!m_httpServer.OwnerByPoolName.ContainsKey(poolName))
        //            m_httpServer.OwnerByPoolName.Add(poolName, servicesConstants.DEFAULT_OWNER_NAME);

        //        if (m_httpServer.OwnerByPoolName[poolName] == servicesConstants.DEFAULT_OWNER_NAME)
        //        {
        //            Console.WriteLine(ownerName + " took ownership of pool " + poolName);
        //            m_httpServer.OwnerByPoolName[poolName] = ownerName;
        //            return true;
        //        }

        //        else
        //            return false;
        //    }
        //}

        //public static void FreePool(string poolName)
        //{
        //    lock (m_httpServer.OwnerByPoolName)
        //    {
        //        if (m_httpServer.OwnerByPoolName[poolName] != servicesConstants.DEFAULT_POOL_NAME)
        //            m_httpServer.OwnerByPoolName[poolName] = servicesConstants.DEFAULT_POOL_NAME;
        //    }
        //}

        //public static List<TurtleIdPool> getAllTurtles()
        //{
        //    List<TurtleIdPool> turtles = new List<TurtleIdPool>();

        //    lock (m_httpServer.ComputerBuffersById)
        //    {
        //        foreach (var kvp in m_httpServer.ComputerBuffersById)
        //            turtles.Add(new TurtleIdPool { TurtleId = kvp.Key, PoolName = kvp.Value.PoolName, StartLocation = kvp.Value.StartingLocation });
        //    }

        //    return turtles;
        //}

        //public static List<PoolOwnner> getAllPools()
        //{
        //    List<PoolOwnner> pools = new List<PoolOwnner>();

        //    lock (m_httpServer.OwnerByPoolName)
        //    {
        //        foreach (var kvp in m_httpServer.OwnerByPoolName)
        //            pools.Add(new PoolOwnner { PoolName = kvp.Key, OwnerName = kvp.Value });
        //    }

        //    return pools;
        //}
    }
}
