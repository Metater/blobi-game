using System;
using System.Collections.Generic;
using System.Text;

namespace BlobiShared.Utils
{
    public class IdGen
    {
        private uint nextId = 0;

        public IdGen(uint firstId = 0)
        {
            nextId = firstId;
        }

        public uint GetNext()
        {
            return nextId++;
        }
    }
}
