using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.Send
{
    public class ResponseAuth : ASendPacket
    {
        protected internal override void Write()
        {
            WriteH(0);
            WriteH(0);
            WriteH(1); // gender
            WriteH(0);

            WriteH(0);
            WriteH(0);
            WriteH(9);
            WriteH(0);
            WriteH(0);
            WriteH(0);
            WriteH(0); //1

            WriteH(0);
            WriteH(14);
            WriteH(0);
            WriteH(1);
            WriteH(4369);
            WriteH(0);
            WriteH(4369);
            WriteH(0);
            WriteH(4369);

            WriteC(0xff);
            WriteC(0xff);

            WriteD(0);
            WriteD(Funcs.GetRoundedLocal());
            WriteD(28);
            WriteB(Funcs.NextBytes(4));
            WriteH(0x5041);
        }
    }
}
