using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ComputerCraft.Core.Rpc
{
    public class CallbackFuture
    {
        public long Key;
        public Object LockObject;
        public Object FutureValue;

        public Object AwaitFuture()
        {
            lock (LockObject)
                Monitor.Wait(LockObject);

            return FutureValue;
        }

        public void Notify(Object futureValue)
        {
            FutureValue = futureValue;
            lock (LockObject)
                Monitor.PulseAll(LockObject);
        }
    }

    public class RpcCallbackSyncronizer
    {
        private long m_nextKey;
        private ConcurrentBag<CallbackFuture> m_freeKeys = new ConcurrentBag<CallbackFuture>();
        private List<CallbackFuture> m_allFutures;

        public RpcCallbackSyncronizer(int initalPoolSize = 100)
        {
            m_allFutures = new List<CallbackFuture>(initalPoolSize);

            for (int i = 0; i < initalPoolSize; i++)
            {
                CallbackFuture future = new CallbackFuture { Key = m_nextKey++, LockObject = new Object() };
                m_freeKeys.Add(future);
                m_allFutures.Add(future);
            }
        }

        public CallbackFuture GetFuture()
        {
            CallbackFuture future = null;

            // Get key, double size if needed
            while (!m_freeKeys.TryTake(out future))
            {
                for (int i = 0; i < m_nextKey * 2; i++)
                {
                    CallbackFuture newFuture = new CallbackFuture { Key = m_nextKey++, LockObject = new Object() };
                    m_freeKeys.Add(newFuture);
                    m_allFutures.Add(newFuture);
                }
            }

            return future;
        }

        public void ReturnKey(CallbackFuture future)
        {
            future.FutureValue = null;
            m_freeKeys.Add(future);
        }

        public void NotifyFutureID(long id, Object message)
        {
            CallbackFuture future = m_allFutures[(int)id];
            future.Notify(message);
        }

    }
}
