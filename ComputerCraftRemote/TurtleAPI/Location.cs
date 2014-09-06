using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCraftRemote
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Location
    {
        public int X;
        public int Y;
        public int Z;

        public Location(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Location(String location)
        {
            location = location.Trim();
            String[] components = location.Split(',');
            Debug.Assert(components.Length == 3, "Invalid Location string format. Should be of form 1,2,3");
            X = int.Parse(components[0]);
            Y = int.Parse(components[1]);
            Z = int.Parse(components[2]);
        }

        public override string ToString()
        {
            return X + ":" + Y + ":" + Z;
        }
    }
}
