using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Lidgren.Network;
using System.Threading;

namespace ComputerCraft.Core.Rpc
{
    public class ThreadedQueueProcessor
    {
        private BlockingCollection<RpcMessage> m_sendPendingSerialize
            = new BlockingCollection<RpcMessage>(new ConcurrentQueue<RpcMessage>());
        private BlockingCollection<RpcMessage> m_sendQueue
            = new BlockingCollection<RpcMessage>(new ConcurrentQueue<RpcMessage>());

        private BlockingCollection<RpcMessage> m_recievedPendingDeserialize
            = new BlockingCollection<RpcMessage>(new ConcurrentQueue<RpcMessage>());

        private RpcInvoker m_invoker = new RpcInvoker();
        private RpcCallbackSyncronizer m_callbackSyncronizer;
        private JsonSerializerSettings m_jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public ThreadedQueueProcessor(RpcCallbackSyncronizer syncronizer)
        {
            m_callbackSyncronizer = syncronizer;

            for (int i = 0; i < 4; i++)
            {
                new Thread(SerializeThread).Start();
                new Thread(DeSerializeThread).Start();
            }
        }

        private void SerializeThread()
        {
            while (true)
            {
                RpcMessage message = m_sendPendingSerialize.Take();
                message.RpcObjectSource = JsonConvert.SerializeObject(message.RpcObject, m_jsonSettings);
                message.RpcObject = null;
                m_sendQueue.Add(message);
            }
        }

        private void DeSerializeThread()
        {
            while (true)
            {
                RpcMessage message = m_recievedPendingDeserialize.Take();
                message.RpcObject = JsonConvert.DeserializeObject<Rpc>(message.RpcObjectSource, m_jsonSettings);
                message.RpcObjectSource = null;

                if (message.RpcObject is RpcRequest)
                {
                    RpcRequest request = (RpcRequest) message.RpcObject;

                    // Workaround for JSON bug: http://json.codeplex.com/workitem/20832
                    for (int i = 0; i < request.Args.Length; i++)
                        if (request.Args[i] is long)
                            request.Args[i] = (Int32)(Int64)request.Args[i];

                    // Invoke the RPC, if a return is needed then enqueue the return
                    RpcResponse response = m_invoker.Invoke(request);

                    // Reuse the same message object, serializing here
                    message.RpcObjectSource = JsonConvert.SerializeObject(response, m_jsonSettings);
                    message.RpcObject = null;

                    if (response != null)
                        m_sendQueue.Add(message);
                }

                else if (message.RpcObject is RpcResponse)
                {
                    RpcResponse response = (RpcResponse) message.RpcObject;

                    // Remote exception?
                    if (response.DidThrowException)
                        throw new Exception((String)response.ReturnedObject);

                    // Notify waiting thread of the response
                    m_callbackSyncronizer.NotifyFutureID(response.CallbackToken, response.ReturnedObject);
                }

                else
                    throw new NotSupportedException("Unknown RPC type");

            }
        }

        public void SendingEnqueue(RpcMessage message) { m_sendPendingSerialize.Add(message); }
        public RpcMessage SendingDeQueue() { return m_sendQueue.Take(); }
        public void RecieveingEnqueue(RpcMessage source) { m_recievedPendingDeserialize.Add(source); }

    }
}
