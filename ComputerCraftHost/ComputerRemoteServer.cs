using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerCraft.Core.Rpc;
using ComputerCraftRemote;
using Lidgren.Network;

namespace ComputerCraftHost
{
    public class ComputerRemoteServer
    {
        internal NetServer LidgrenNetServer;
        private QueueHttpServer m_httpServer;
        private RpcHost m_rpcPeer;

        public ComputerRemoteServer(QueueHttpServer httpServer)
        {
            m_httpServer = httpServer;
        }

        public void Start()
        {
            // Connect Lidgren
            NetPeerConfiguration config = new NetPeerConfiguration("ComputerCraftRemote");
            config.Port = 9090;
            LidgrenNetServer = new NetServer(config);
            LidgrenNetServer.Start();
            
            // Create RPC host
            m_rpcPeer = new RpcHost(LidgrenNetServer);
            m_rpcPeer.Start();
        }

    }
}
