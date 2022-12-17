using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public struct PacketHeader
    {
        public int size;
        public int prefix;
        public int id;
    }
}
