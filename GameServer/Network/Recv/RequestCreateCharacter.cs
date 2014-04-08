using GameServer.Model.Character;
using GameServer.Service;

namespace GameServer.Network.Recv
{
    public class RequestCreateCharacter : ARecvPacket
    {
        protected Character Character;
        protected internal override void Read()
        {
            Character = new Character();
            Character.Name = ReadS(15);
            ReadC();
            Character.Class = (CharacterClass)ReadC();
            Character.HairStyle = ReadC();
            Character.HairColor = ReadC();
            Character.Face = ReadC();
            Character.Voice = ReadC();
            ReadC(); // UNK
            Character.Gender = (CharacterGender)ReadC();
            ReadC(); // UNK
            ReadD(); // UNK
        }

        protected internal override void Run()
        {
            CharacterService.GetInstance().CreateCharacter(_Client._Account, Character);
        }
    }
}
