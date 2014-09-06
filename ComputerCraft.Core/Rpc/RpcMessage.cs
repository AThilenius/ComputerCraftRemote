using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace ComputerCraft.Core.Rpc
{
    // Not sent over the network
    public class RpcMessage
    {
        public NetConnection PeerConnection;
        public Object RpcObject;
        public String RpcObjectSource;
    }

    public class Rpc
    {
        public long CallbackToken;
    }

    public class RpcRequest : Rpc
    {
        public Boolean IsNonCallback;
        public String TypePath;
        public String Methodpath;
        public Object[] Args;
    }

    public class RpcResponse : Rpc
    {
        public Boolean DidThrowException;
        public Object ReturnedObject;
    }
}
