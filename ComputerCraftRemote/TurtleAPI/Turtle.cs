using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerCraftRemote.TurtleAPI;

namespace ComputerCraftRemote.TurtleAPI
{
    public class Turtle
    {
        internal CCServiceProvider RemoteServer; 

        // Public API
        public int TurtleID { get; internal set; }
        public TurtlePool Pool { get; internal set; }
        public String Owner { get { return Pool.Owner; } }
        public Location StartLocation { get; internal set; }
        public Boolean HasOwnership { get { return RemoteServer.Username == Owner; } }

        public Movement Movement { get; internal set; }
        public Inventory Inventory { get; internal set; }
        public Terminal Terminal { get; internal set; }

        internal Turtle(CCServiceProvider remoteServer, int id)
        {
            RemoteServer = remoteServer;
            TurtleID = id;

            Movement = new Movement(this);
            Inventory = new Inventory(this);
            Terminal = new Terminal(this);
        }



    }
}
