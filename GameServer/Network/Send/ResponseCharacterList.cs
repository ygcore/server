using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.Send
{
    public class ResponseCharacterList : ASendPacket
    {
        public ResponseCharacterList()
        {

        }

        protected internal override void Write()
        {
            WriteC(0xff);
        }
    }
}
