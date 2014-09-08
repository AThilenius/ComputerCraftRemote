using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCraftRemote.TurtleAPI
{
    public class TurtlePool
    {
        public String Name { get; private set; }
        public String Owner { get; internal set; }
        public List<Turtle> Turtles = new List<Turtle>();

        public Boolean RequestOwnership()
        {
            if (Turtles.Count == 0)
                return false;

            return Turtles[0]
                .RemoteServer
                .TurtleClient
                .RequestPoolOwnership(Name, Turtles[0].RemoteServer.Username);
        }

        internal TurtlePool(String poolName)
        {
            Name = poolName;
        }

    }
}
