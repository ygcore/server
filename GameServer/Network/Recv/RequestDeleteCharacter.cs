using GameServer.Service;

namespace GameServer.Network.Recv
{
    public class RequestDeleteCharacter : ARecvPacket
    {
        protected string deletepw;
        protected string charname;

        protected internal override void Read()
        {
            ReadD();
            long unk1 = ReadQ();
            deletepw = ReadS(10);
            ReadB(30);
            charname = ReadS(16).Replace("\0", "");
        }

        protected internal override void Run()
        {
            CharacterService.GetInstance().DeleteCharacter(_Client._Account, deletepw, charname);
        }
    }
}
