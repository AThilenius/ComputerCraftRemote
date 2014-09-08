using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Newtonsoft.Json;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace ComputerCraft.Core.Rpc
{
    public class RemoteExecutionException : Exception
    {
        public NetConnection PeerConnection;

        public RemoteExecutionException(NetConnection peer, String message)
            : base(message)
        {
        }
    }
    
    public class RpcHost
    {
        private struct ResponseFuture
        {
            public Object WaitObject;
            public Object Response;
        }
        private NetPeer m_peer;
        private RpcInvoker m_invoker = new RpcInvoker();
        private RpcCallbackSyncronizer m_callbackSyncronizer = new RpcCallbackSyncronizer();
        private BlockingCollection<RpcMessage> m_sendQueue = new BlockingCollection<RpcMessage>(new ConcurrentStack<RpcMessage>());
        private Stopwatch m_stopwatch = new Stopwatch();
        private JsonSerializerSettings m_jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        // DEBUG
        int readCount;
        int SentCount;

        public RpcHost(NetPeer peer)
        {
            m_peer = peer;
            peer.RegisterReceivedCallback(RecieveMessages, new SynchronizationContext());
        }

        public void Start()
        {
            Task.Factory.StartNew(SendMessages);
        }

        public T Invoke<T>(NetConnection targetPeer, MethodInfo info, params Object[] args)
        {
            return (T)Invoke(targetPeer, info, args);
        }

        public Object Invoke(NetConnection targetPeer, MethodInfo info, params Object[] args)
        {
            Boolean isNonCallback = info.ReturnType == typeof(void);
            
            CallbackFuture future = null;
            if (!isNonCallback)
                future = m_callbackSyncronizer.GetFuture();

            RpcRequest request = new RpcRequest
            {
                CallbackToken = isNonCallback ? 0 : future.Key,
                IsNonCallback = isNonCallback,
                TypePath = info.DeclaringType.FullName,
                Methodpath = info.Name,
                Args = args
            };
            RpcMessage message = new RpcMessage
            {
                PeerConnection = targetPeer,
                RpcObject = request
            };

            // EnQueue for sending
            m_sendQueue.Add(message);

            if (isNonCallback)
                return null;

            // Wait for our future
            Object returnValue = future.AwaitFuture();

            // Return key
            m_callbackSyncronizer.ReturnKey(future);

            return returnValue;
        }

        private void SendMessages()
        {
            while (true)
            {
                RpcMessage message = m_sendQueue.Take();
                NetOutgoingMessage sendMsg = m_peer.CreateMessage();
                String messageContents = JsonConvert.SerializeObject(message.RpcObject, m_jsonSettings);
                sendMsg.Write(messageContents);
                m_peer.SendMessage(sendMsg, message.PeerConnection, NetDeliveryMethod.ReliableOrdered);
                SentCount++;
            }
        }

        private void RecieveMessages(Object peer)
        {
            NetPeer netPeer = (NetPeer)peer;
            NetIncomingMessage msg = netPeer.ReadMessage();

            switch (msg.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                case NetIncomingMessageType.VerboseDebugMessage:
                case NetIncomingMessageType.DebugMessage:
                case NetIncomingMessageType.WarningMessage:
                    break;
                case NetIncomingMessageType.ErrorMessage:
                    Console.WriteLine(msg.ReadString());
                    break;
                case NetIncomingMessageType.Data:
                {
                    String sourceText = msg.ReadString();
                    Rpc rpc = JsonConvert.DeserializeObject<Rpc>(sourceText, m_jsonSettings);
                    ProcessRpc(msg.SenderConnection, rpc);
                    readCount++;
                    break;
                }
                default:
                    Console.WriteLine("Unhanded type: " + msg.MessageType);
                    break;
            }
            m_peer.Recycle(msg);
        }

        private void ProcessRpc(NetConnection from, Rpc rpc)
        {
            if (rpc is RpcRequest)
            {
                RpcRequest request = (RpcRequest) rpc;

                // Workaround for JSON bug: http://json.codeplex.com/workitem/20832
                if (request.Args != null)
                {
                    for (int i = 0; i < request.Args.Length; i++)
                        if (request.Args[i] is long)
                            request.Args[i] = (Int32)(Int64)request.Args[i];
                }

                // Invoke the RPC, if a return is needed then enqueue the return
                RpcResponse response = m_invoker.Invoke(request);
                if (response != null)
                    m_sendQueue.Add(new RpcMessage { PeerConnection = from, RpcObject = response });
            }

            else if (rpc is RpcResponse)
            {
                RpcResponse response = (RpcResponse) rpc;

                // Remote exception?
                if (response.DidThrowException)
                    throw new RemoteExecutionException(from, (String)response.ReturnedObject);

                // Notify waiting thread of the response
                m_callbackSyncronizer.NotifyFutureID(response.CallbackToken, response.ReturnedObject);
            }

            else
                throw new NotSupportedException("Unknown RPC type");
        }

    }
}
