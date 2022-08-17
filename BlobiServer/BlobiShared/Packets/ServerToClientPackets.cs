using System;
using System.Collections.Generic;
using System.Text;

namespace BlobiShared.Packets
{
    public class PrintPacket
    {
        public string Message { get; set; }
    }

    public class JoinPacket
    {
        public uint PlayerId { get; set; }
    }

    public class SetPlayerNamePacket
    {
        public uint PlayerId { get; set; }
        public string PlayerName { get; set; }
    }
}
