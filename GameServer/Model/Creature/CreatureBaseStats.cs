using GameServer.Model.Character;
using ProtoBuf;
using System.ComponentModel;

namespace GameServer.Model.Creature
{
    [ProtoContract]
    public class CreatureBaseStats
    {
        [Category("Commons"), ProtoMember(1)]
        public CharacterClass PlayerClass { get; set; }
        [Category("Commons"), ProtoMember(2)]
        public int Level { get; set; }
        [Category("Commons"), ProtoMember(3)]
        public long Exp { get; set; }



        [Category("LifeStats"), ProtoMember(10)]
        public int HpBase { get; set; }
        [Category("LifeStats"), ProtoMember(11)]
        public int MpBase { get; set; }
        [Category("LifeStats"), ProtoMember(12)]
        public int SpBase { get; set; }



        [Category("Combat"), ProtoMember(20)]
        public int Attack { get; set; }
        [Category("Combat"), ProtoMember(21)]
        public int SkillAttack { get; set; }
        [Category("Combat"), ProtoMember(22)]
        public int Defense { get; set; }
        [Category("Combat"), ProtoMember(23)]
        public int SkillDefense { get; set; }
        [Category("Combat"), ProtoMember(24)]
        public int Accuracy { get; set; }
        [Category("Combat"), ProtoMember(25)]
        public int Dodge { get; set; }



        [Category("Stats"), ProtoMember(30)]
        public int Spirit { get; set; }
        [Category("Stats"), ProtoMember(31)]
        public int Strength { get; set; }
        [Category("Stats"), ProtoMember(32)]
        public int Stamina { get; set; }
        [Category("Stats"), ProtoMember(33)]
        public int Dexterity { get; set; }


        public CreatureBaseStats Clone()
        {
            return (CreatureBaseStats)MemberwiseClone();
        }

        public void CopyTo(CreatureBaseStats gameStats)
        {
            //HpMp
            gameStats.HpBase = HpBase;
            gameStats.MpBase = MpBase;
            gameStats.SpBase = SpBase;

            //Combat
            gameStats.Attack = Attack;
            gameStats.SkillAttack = SkillAttack;
            gameStats.Defense = Defense;
            gameStats.SkillDefense = SkillDefense;
            gameStats.Accuracy = Accuracy;
            gameStats.Dodge = Dodge;

            //Stats
            gameStats.Spirit = Spirit;
            gameStats.Strength = Strength;
            gameStats.Stamina = Stamina;
            gameStats.Dexterity = Dexterity;
        }
    }
}
