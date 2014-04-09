using GameServer.Model.Character;
using GameServer.Model.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.Send
{
    public class ResponseCharacterList : ASendPacket
    {
        protected bool Exists;
        private KeyValuePair<int, Character> Kvp;

        public ResponseCharacterList()
        {
            Exists = false;
        }

        public ResponseCharacterList(KeyValuePair<int, Character> kvp)
        {
            Exists = true;
            Kvp = kvp;
        }

        protected internal override void Write()
        {
            if (!Exists)
                WriteC(0xff);
            else
            {
                var character = Kvp.Value;

                WriteC((byte)Kvp.Key);
                WriteS(character.Name, 15);
                WriteC(0);
                WriteD(0);
                WriteS("", 15);
                WriteC(0);
                WriteH(0);

                WriteH(0);
                WriteH(character.Level);

                WriteC((byte)character.Forces);
                WriteC(0); // famous
                WriteC((byte)character.Class);

                WriteC((byte)character.HairStyle);
                WriteC((byte)character.HairColor);
                WriteC((byte)character.Face);
                WriteC((byte)character.Voice);
                WriteC(0);
                WriteC((byte)character.Title);
                WriteC((byte)character.Gender);

                WriteF(character.Position.X);
                WriteF(character.Position.Z);
                WriteF(character.Position.Y);
                WriteD(character.Position.MapId);

                WriteB(new byte[12]);

                WriteB(character.NameStyle);

                WriteB(new byte[8]);

                WriteH(character.GameStats.HpBase); // MaxHp
                WriteH(character.GameStats.MpBase); // MaxMp
                WriteD(character.GameStats.SpBase); // MaxSp
                WriteQ(character.GameStats.Exp); // Exp

                WriteH(character.LifeStats.Hp); // current Hp
                WriteH(character.LifeStats.Mp); // current Mp
                WriteD(character.LifeStats.Sp); // current Sp
                WriteQ(character.Exp); // current Exp

                WriteD(0);
                WriteB(new byte[16]);

                for (int i = 0; i < 30; i++ )
                {
                    StorageItem item;
                    try
                    {
                        item = character.Equipment.Items.Values.ToList()[i];
                    }
                    catch
                    {
                        item = null;
                    }

                    if (item == null)
                        WriteB(new byte[88]);
                    else
                        WriteItemInfo(item);
                }
            }
        }
    }
}
