using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCraftRemote
{
    public class ComputerRemoteHandler : ComputerRemote.Iface
    {
        private QueueHttpServer m_httpServer;

        public ComputerRemoteHandler(QueueHttpServer httpServer) : base()
        {
            m_httpServer = httpServer;
        }

        #region Remoting Handler

        public string invokeCommandSync(int computerId, string command)
        {
            return m_httpServer.RunCommand(computerId, command);
        }

        public void invokeCommandAsync(int computerId, string command)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Pooling Handler

        public bool requestPoolOwnership(string poolName, string ownerName)
        {
            lock (m_httpServer.OwnerByPoolName)
            {
                if (!m_httpServer.OwnerByPoolName.ContainsKey(poolName))
                    m_httpServer.OwnerByPoolName.Add(poolName, servicesConstants.DEFAULT_OWNER_NAME);

                if (m_httpServer.OwnerByPoolName[poolName] == servicesConstants.DEFAULT_OWNER_NAME)
                {
                    Console.WriteLine(ownerName + " took ownership of pool " + poolName);
                    m_httpServer.OwnerByPoolName[poolName] = ownerName;
                    return true;
                }

                else
                    return false;
            }
        }

        public void freePool(string poolName)
        {
            lock (m_httpServer.OwnerByPoolName)
            {
                if (m_httpServer.OwnerByPoolName[poolName] != servicesConstants.DEFAULT_POOL_NAME)
                    m_httpServer.OwnerByPoolName[poolName] = servicesConstants.DEFAULT_POOL_NAME;
            }
        }

        public List<TurtleIdPool> getAllTurtles()
        {
            List<TurtleIdPool> turtles = new List<TurtleIdPool>();

            lock (m_httpServer.ComputerBuffersById)
            {
                foreach (var kvp in m_httpServer.ComputerBuffersById)
                    turtles.Add(new TurtleIdPool { TurtleId = kvp.Key, PoolName = kvp.Value.PoolName, StartLocation = kvp.Value.StartingLocation });
            }

            return turtles;
        }

        public List<PoolOwnner> getAllPools()
        {
            List<PoolOwnner> pools = new List<PoolOwnner>();

            lock (m_httpServer.OwnerByPoolName)
            {
                foreach (var kvp in m_httpServer.OwnerByPoolName)
                    pools.Add(new PoolOwnner { PoolName = kvp.Key, OwnerName = kvp.Value });
            }

            return pools;
        }

        #endregion
    }
}
