using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCraft.Core.Rpc
{
    /// <summary>
    /// Invokes a RpcInvoke request and catches exceptions.
    /// </summary>
    public class RpcInvoker
    {
        // Commands sent as: MethodName
        public RpcResponse Invoke(RpcRequest rpcRequest)
        {
            //try
            //{
                Assembly assembly = Assembly.GetEntryAssembly();
                Type type = assembly.GetType(rpcRequest.TypePath);
                MethodInfo method = type.GetMethod(rpcRequest.Methodpath);
                Object retVal = method.Invoke(null, rpcRequest.Args);

                if (rpcRequest.IsNonCallback)
                    return null;

                // Build a response
                return new RpcResponse
                {
                    CallbackToken = rpcRequest.CallbackToken,
                    DidThrowException = false,
                    ReturnedObject = retVal
                };
            //}
            //catch (Exception ex)
            //{
            //    if (ex.InnerException != null && ex.InnerException.Message != "")
            //    {
            //        Console.WriteLine("Remote invoke exception: " + ex.InnerException.Message);
            //        return new RpcResponse
            //        {
            //            CallbackToken = rpcRequest.CallbackToken,
            //            DidThrowException = true,
            //            ReturnedObject = "[REMOTE_INVOKE_EXCEPTION]: " + ex.InnerException.Message
            //        };
            //    }
            //    else
            //    {
            //        Console.WriteLine("Remote invoke exception: " + ex.Message);
            //        return new RpcResponse
            //        {
            //            CallbackToken = rpcRequest.CallbackToken,
            //            DidThrowException = true,
            //            ReturnedObject = "[REMOTE_INVOKE_EXCEPTION]: " + ex.Message
            //        };
            //    }
            //}

        }
    }
}
