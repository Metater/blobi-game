using System;
using System.Collections.Generic;
using System.Text;

namespace BlobiShared.Packets
{
    public class PrintPacket
    {
        public string Message { get; set; }
    }

    public class JoinAcceptPacket
    {
        public uint PlayerId { get; set; }
    }
}
