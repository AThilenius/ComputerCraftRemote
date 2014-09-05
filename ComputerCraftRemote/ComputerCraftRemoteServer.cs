using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;

namespace ComputerCraftRemote
{
    public class ComputerCraftRemoteServer
    {
        private Dictionary<int, Turtle> m_knownTurtles = new Dictionary<int, Turtle>();
        private Dictionary<String, TurtlePool> m_knownPools = new Dictionary<String, TurtlePool>();

        internal String Username;
        internal ComputerRemote.Client RemoteingService;
        private TTransport m_transport;

        public ComputerCraftRemoteServer(String username, String address, Int32 port)
        {
            Username = username;
            m_transport = new TSocket(address, port);
            TProtocol protocol = new TBinaryProtocol(m_transport);

            // Create Remoteing service
            RemoteingService = new ComputerRemote.Client(protocol);

            // Open the transport
            m_transport.Open();
        }

        ~ComputerCraftRemoteServer()
        {
            if (m_transport != null)
            {
                m_transport.Close();
                m_transport = null;
            }
        }

        public Turtle[] GetAllTurtles()
        {
            UpdateKnown();
            return m_knownTurtles.Values.ToArray();
        }

        public TurtlePool[] GetAllPools()
        {
            UpdateKnown();
            return m_knownPools.Values.ToArray();
        }

        public Turtle[] GetAllOwnedTurtles()
        {
            UpdateKnown();
            return m_knownTurtles.Values
                .Where(turtle => turtle.Owner == Username)
                .ToArray();
        }

        public TurtlePool[] GetAllOwnedPools()
        {
            UpdateKnown();
            return m_knownPools.Values
                .Where(pool => pool.Owner == Username)
                .ToArray();
        }

        private void UpdateKnown()
        {
            List<TurtleIdPool> turtleIdPools = RemoteingService.getAllTurtles();
            List<PoolOwnner> poolOwners = RemoteingService.getAllPools();

            foreach (TurtleIdPool turtleIdPool in turtleIdPools)
            {
                if (!m_knownTurtles.ContainsKey(turtleIdPool.TurtleId))
                {
                    // New Turtle!
                    Turtle turlte = new Turtle(this, turtleIdPool.TurtleId);
                    m_knownTurtles.Add(turtleIdPool.TurtleId, turlte);
                }
            }

            foreach (PoolOwnner poolOwner in poolOwners)
            {
                if (!m_knownPools.ContainsKey(poolOwner.PoolName))
                {
                    // New Pool!
                    TurtlePool pool = new TurtlePool(poolOwner.PoolName);
                    m_knownPools.Add(poolOwner.PoolName, pool);
                }
            }

            foreach (TurtlePool pool in m_knownPools.Values)
                pool.Turtles.Clear();

            // Stitch them back together
            foreach (TurtleIdPool turtleIdPool in turtleIdPools)
            {
                Turtle turtle = m_knownTurtles[turtleIdPool.TurtleId];
                TurtlePool pool = m_knownPools[turtleIdPool.PoolName];
                turtle.Pool = pool;
                turtle.StartLocation = new Location(turtleIdPool.StartLocation);
                pool.Turtles.Add(turtle);
            }

            foreach (PoolOwnner poolOwner in poolOwners)
            {
                TurtlePool pool = m_knownPools[poolOwner.PoolName];
                pool.Owner = poolOwner.OwnerName;
            }

        }

    }
}
