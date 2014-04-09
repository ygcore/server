using Common.Utilities;
using GameServer.Config;
using GameServer.Database;
using GameServer.Model.Account;
using GameServer.Model.Character;
using GameServer.Network.Send;
using System;

namespace GameServer.Service
{
    public class CharacterService
    {
        private static CharacterService Instance;

        internal void SendCharacterList(Account account)
        {
            account._Characters = MdbCharacter.GetInstance().GetAccountCharacter(account.Name);

            if (account._Characters == null)
                account._Client.SendPacket(new ResponseCharacterList());
            else
                foreach (var character in account._Characters)
                    account._Client.SendPacket(new ResponseCharacterList(character));
        }

        internal static CharacterService GetInstance()
        {
            return (Instance != null) ? Instance : Instance = new CharacterService();
        }

        internal void SendCheckName(Account account, string Name)
        {
            bool result = MdbCharacter.GetInstance().IsAvailableName(Name);
            account._Client.SendPacket(new ResponseCheckName(Name, result));
        }

        internal void CreateCharacter(Account account, Character Character)
        {
            Character.AccountName = account.Name;
            Character.ServerId = Configuration.Setting.ServerId;
            Character.Level = 1;
            Character.JobLevel = 1;
            Character.NameStyle = "0000000000000000FFFFFFFFFFFFFFFF0100000000000000FFFFFFFFFFFFFFFF0200000000000000FFFFFFFFFFFFFFFF".ToBytes();
            Character.GameStats = StatsService.GetInstance().InitStats(Character);
            Character.Position = new Model.Map.MapPosition()
                {
                    MapId = 101,
                    X = BitConverter.GetBytes(300.0F),
                    Y = BitConverter.GetBytes(1865.0F),
                    Z = BitConverter.GetBytes(15.0F),
                    oldX = new byte[0],
                    oldY = new byte[0],
                    oldZ = new byte[0],
                };

            MdbCharacter.GetInstance().AddCharacter(Character);

            account._Client.SendPacket(new ResponseCreateCharacter(1));
        }

        internal void DeleteCharacter(Account account, string deletepw, string charname)
        {
            if (account.DeletePasswd != deletepw)
                account._Client.SendPacket(new ResponseDeleteCharacter(false));

            bool result = MdbCharacter.GetInstance().DeleteCharacter(charname);

            account._Client.SendPacket(new ResponseDeleteCharacter(result));
        }

        internal void CharacterEnterGame(Network.Client _Client)
        {
            Character character = _Client._Char;

            new ResponseServerTime(0).Send(_Client);
            new ResponseRunning().Send(_Client);
            new ResponseCharacterInfo(character).Send(_Client);
            
            new ResponseSetBuff(2, 1, 1).Send(_Client);
        }
    }
}
