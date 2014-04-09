using GameServer.Network.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.Recv
{
    public class RequestInitGame : ARecvPacket
    {
        protected internal override void Read()
        {
            
        }

        protected internal override void Run()
        {
            new ResponseInitGame().Send(_Client);
        }
    }
}
