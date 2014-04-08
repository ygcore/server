using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model.Creature
{
    public abstract class Creature : GameObject
    {
        private CreatureLifeStats _lifeStats;

        public CreatureLifeStats LifeStats
        {
            get { return _lifeStats ?? (_lifeStats = new CreatureLifeStats(this)); }
        }

        public CreatureBaseStats GameStats;

        public int MaxHp
        {
            get { return GameStats.HpBase; }
        }

        public int MaxMp
        {
            get { return GameStats.MpBase; }
        }

        public int MaxSp
        {
            get { return GameStats.SpBase; }
        }
    }
}
