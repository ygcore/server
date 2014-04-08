using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model.Creature
{
    public class CreatureLifeStats
    {
        public Creature Creature;

        public bool SendUpdate = true;

        private int _hp;

        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }

        private int _mp;

        public int Mp
        {
            get { return _mp; }
            set { _mp = value; }
        }

        private int _sp;

        public int Sp
        {
            get { return _sp; }
            set { _sp = value; }
        }

        public bool IsDead()
        {
            if (Hp <= 0)
                return true;

            return false;
        }

        public bool IsRage()
        {
            if (Sp >= Creature.GameStats.SpBase)
            {
                Sp = 0;
                return true;
            }

            return false;
        }

        public CreatureLifeStats(Creature creature)
        {
            _hp = creature.GameStats.HpBase;
            _mp = creature.GameStats.MpBase;
            _sp = 0;

            Creature = creature;
        }

        public int PlusHp(int value)
        {
            _hp += value;

            if (_hp > Creature.MaxHp)
            {
                value -= _hp - Creature.MaxHp;
                _hp = Creature.MaxHp;
            }

            return value;
        }

        public int GetHpDiffResult(int value)
        {
            return _hp - value;
        }

        public int MinusHp(int value)
        {
            _hp -= value;

            if (_hp < 0)
            {
                value += _hp;
                _hp = 0;
            }

            return -value;
        }

        public int PlusMp(int value)
        {
            _mp += value;

            if (_mp > Creature.MaxMp)
            {
                value -= _mp - Creature.MaxMp;
                _mp = Creature.MaxMp;
            }

            return value;
        }

        public int MinusMp(int value)
        {
            _mp -= value;

            if (_mp < 0)
            {
                value += _mp;
                _mp = 0;
            }

            return -value;
        }

        public int PlusSp(int value)
        {
            _sp += value;

            if (_sp > Creature.MaxSp)
            {
                value -= _sp - Creature.MaxSp;
                _sp = Creature.MaxSp;
            }

            return value;
        }

        public int MinusSp(int value)
        {
            _sp -= value;

            if (_sp < 0)
            {
                value += _sp;
                _sp = 0;
            }

            return -value;
        }

        public void Kill()
        {
            _hp = 0;
            _mp = 0;
            _sp = 0;

        }

        public void Rebirth()
        {
            _hp = Creature.GameStats.HpBase;
            _mp = Creature.GameStats.MpBase;

            //if (Creature is Npc.Npc)
            //    return;

            _hp /= 10;
            _mp /= 10;
        }

        public void LevelUp()
        {
            _hp = Creature.GameStats.HpBase;
            _mp = Creature.GameStats.MpBase;
        }

        public CreatureLifeStats Clone()
        {
            return (CreatureLifeStats)MemberwiseClone();
        }

        public void Release()
        {
            Creature = null;
        }
    }
}
