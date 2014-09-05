using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCraftRemote
{
    public class Turtle
    {
        internal ComputerCraftRemoteServer RemoteServer; 

        // Public API
        public Int32 TurtleID { get; internal set; }
        public TurtlePool Pool { get; internal set; }
        public String Owner { get { return Pool.Owner; } }
        public Location StartLocation { get; internal set; }
        public Boolean HasOwnership { get { return RemoteServer.Username == Owner; } }

        public MovementApi Movement { get; internal set; }


        internal Turtle(ComputerCraftRemoteServer remoteServer, int id)
        {
            RemoteServer = remoteServer;
            TurtleID = id;

            Movement = new MovementApi(this);
        }



    }
}
