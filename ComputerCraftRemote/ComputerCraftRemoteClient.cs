using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerCraft.Core.Rpc;
using ComputerCraftHost.Services.Turtle;
using ComputerCraftRemote.TurtleAPI;
using Lidgren.Network;

namespace ComputerCraftRemote
{
    public class ComputerCraftRemoteClient
    {
        private Dictionary<int, Turtle> m_knownTurtles = new Dictionary<int, Turtle>();
        private Dictionary<String, TurtlePool> m_knownPools = new Dictionary<String, TurtlePool>();

        private NetClient m_netClient;
        private RpcHost m_rpcHost;
        private NetConnection m_serverTarget;
        private TurtleServiceClient m_turtleClient;

        internal String Username;

        public ComputerCraftRemoteClient(String username, String address, int port)
        {
            Username = username;

            // Connect Lidgren
            NetPeerConfiguration config = new NetPeerConfiguration("ComputerCraftRemote");
            m_netClient = new NetClient(config);
            m_netClient.Start();
            Console.Write("Connecting to " + address + " on port " + port + "... ");
            m_serverTarget = m_netClient.Connect(address, port);
            Console.WriteLine("Connected!");

            // Create an RPC host for the connection
            m_rpcHost = new RpcHost(m_netClient);
            m_rpcHost.Start();

            m_turtleClient = GetTurtleService();
        }

        ~ComputerCraftRemoteClient()
        {
        }

        public TurtleServiceClient GetTurtleService()
        {
            return new TurtleServiceClient(m_rpcHost, m_serverTarget);
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
            List<TurtleIdPool> turtleIdPools = m_turtleClient.GetAllTurtles();
            List<PoolOwner> poolOwners = m_turtleClient.GetAllPools();

            foreach (TurtleIdPool turtleIdPool in turtleIdPools)
            {
                if (!m_knownTurtles.ContainsKey(turtleIdPool.TurtleId))
                {
                    // New Turtle!
                    Turtle turlte = new Turtle(this, turtleIdPool.TurtleId);
                    m_knownTurtles.Add(turtleIdPool.TurtleId, turlte);
                }
            }

            foreach (PoolOwner poolOwner in poolOwners)
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

            foreach (PoolOwner poolOwner in poolOwners)
            {
                TurtlePool pool = m_knownPools[poolOwner.PoolName];
                pool.Owner = poolOwner.OwnerName;
            }

        }

    }
}
