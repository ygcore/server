using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.LoginServer.Send
{
    public class GSP_RegistServer : LSASendPacket
    {
        public GSP_RegistServer()
        {
            //todo write server info and channel info list to send to login server
        }

        protected internal override void Write()
        {
            
        }
    }
}
