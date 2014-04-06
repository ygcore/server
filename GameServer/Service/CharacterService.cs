using GameServer.Model.Account;
using GameServer.Network.Send;

namespace GameServer.Service
{
    public class CharacterService
    {
        private static CharacterService Instance;

        internal void SendCharacterList(Account account)
        {
            account._Client.SendPacket(new ResponseCharacterList());
        }

        internal static CharacterService GetInstance()
        {
            return (Instance != null) ? Instance : Instance = new CharacterService();
        }
    }
}
