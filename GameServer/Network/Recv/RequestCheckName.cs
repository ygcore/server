using GameServer.Service;
using System.Text;

namespace GameServer.Network.Recv
{
    public class RequestCheckName : ARecvPacket
    {
        protected string Name;

        protected internal override void Read()
        {
            Name = Encoding.Default.GetString(ReadB(15));
        }

        protected internal override void Run()
        {
            CharacterService.GetInstance().SendCheckName(_Client._Account, Name);
        }
    }
}
