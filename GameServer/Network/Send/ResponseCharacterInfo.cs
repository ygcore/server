using GameServer.Model.Account;
using GameServer.Model.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.Send
{
    public class ResponseCharacterInfo : ASendPacket
    {
        protected Character Character;
        public ResponseCharacterInfo(Character c)
        {
            Character = c;
        }

        protected internal override void Write()
        {
            WriteD(1);

            WriteD(_Client.SessID);
            WriteS(Character.Name, 15);
            WriteC(0);

            WriteD(0); // Guild ID
            WriteS(string.Empty, 15); // Guild Name
            WriteC(0);  //Guild Level

            WriteH(0); // SERVER ID ?

            WriteC(Character.Forces);
            WriteC(Character.Level);
            WriteC(Character.JobLevel); // Job Level
            WriteC((byte)Character.Class);

            WriteC(1);
            WriteC(0);

            WriteH(Character.HairColor);
            WriteH(Character.HairStyle);
            WriteC(1);
            WriteC((byte)Character.Gender);

            WriteH(0);

            WriteB(Character.Position.X);
            WriteB(Character.Position.Z);
            WriteB(Character.Position.Y);
            WriteD(Character.Position.MapId);

            var equips = Character.Equipment.Items;

            WriteQ(/*(equips[0] != null) ? equips[0].ItemId : */0); // Equip slot 0
            WriteQ(/*(equips[1] != null) ? equips[1].ItemId : */0); // Equip slot 1
            WriteQ(/*(equips[2] != null) ? equips[2].ItemId : */0); // Equip slot 2
            WriteQ(/*(equips[4] != null) ? equips[4].ItemId : */0); // Equip slot 4
            WriteQ(/*(equips[3] != null) ? equips[3].ItemId : */0); // Equip slot 3
            WriteQ(/*(equips[5] != null) ? equips[5].ItemId : */0); // Equip slot 5
            WriteD(/*(equips[3] != null) ? equips[3].Upgrade : */0); // Equip slot 3 LevelUpgrade
            WriteQ(/*(equips[11] != null) ? equips[11].ItemId : */0); // Equip slot 11

            int setting = AccountSetting.GetSettings(_Client._Account.Setting);
            WriteC(setting);
            WriteC(_Client._Account.Setting.FameSwitch);
            WriteH(0);

            WriteB(Character.Position.oldX);
            WriteB(Character.Position.oldZ);
            WriteB(Character.Position.oldY);

            WriteD(0);
            WriteD(0);

            WriteD(0xff); // PET
            WriteD(0);
            WriteQ(/*(equips[13] != null) ? equips[13].ItemId : */0); //  Equip slot 13

            WriteH(0); // Word gang door service
            WriteH(0); // Gang colors door service

            WriteD(Character.HonorPoint); // Character_WuXun
            WriteD(1); // Forces side

            WriteD(0); // People head picture
            WriteD(0); // StealthMode

            WriteB(Character.NameStyle);

            WriteD(1);

            // Maried
            WriteC(0);
            WriteS(string.Empty, 15);

            WriteH(0);
            WriteH(0);
            WriteH(0);
            WriteH(0);
            WriteH(0);

            WriteC(0);
            WriteC(0);
            WriteC(0);
            WriteC(0);

            WriteH(0);
        }
    }
}
