using GameServer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.Recv
{
    public class RequestEntergame : ARecvPacket
    {
        protected int charIndex;
        protected internal override void Read()
        {
            charIndex = ReadC();
        }

        protected internal override void Run()
        {
            _Client._Char = _Client._Account._Characters[charIndex];
            CharacterService.GetInstance().CharacterEnterGame(_Client);
        }
    }
}
