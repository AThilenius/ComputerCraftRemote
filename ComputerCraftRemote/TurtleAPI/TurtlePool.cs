using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCraftRemote
{
    public class TurtlePool
    {
        public String Name { get; private set; }
        public String Owner { get; internal set; }
        public List<Turtle> Turtles = new List<Turtle>();

        internal TurtlePool(String poolName)
        {
            Name = poolName;
        }

    }
}
