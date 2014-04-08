using Common.Utilities;
using GameServer.DataHolder;
using GameServer.Model.Character;
using GameServer.Model.Creature;
using System.Collections.Generic;

namespace GameServer.Service
{
    public class StatsService
    {
        private static StatsService Instance;

        public static Dictionary<CharacterClass, Dictionary<int, CreatureBaseStats>> CharacterStats = new Dictionary<CharacterClass, Dictionary<int, CreatureBaseStats>>();

        public static int MaxLevel = 130;

        public StatsService()
        {
            for (int i = 0; i < Data.Stats.Count; i++)
            {
                CharacterStats.Add((CharacterClass)i + 1, new Dictionary<int, CreatureBaseStats>());

                CreatureBaseStats firstLevelStats = Data.Stats[i];

                for (int j = 1; j < MaxLevel; j++)
                {
                    CreatureBaseStats stats = firstLevelStats.Clone();

                    switch (stats.PlayerClass)
                    {
                        case CharacterClass.Blademan:
                            stats.HpBase += (j * 12);
                            stats.MpBase += (j * 2);
                            stats.Spirit += j;
                            stats.Strength += j + ((j % 2 == 0) ? 2 : 1);
                            stats.Stamina += j + ((j % 2 == 0) ? 2 : 1);
                            stats.Dexterity += j;
                            stats.Attack += j + ((j % 2 == 0) ? 2 : 1);
                            stats.Defense += j;
                            stats.Accuracy += j;
                            stats.Dodge += j;
                            break;
                        case CharacterClass.Swordman:
                            stats.HpBase += (j * 12);
                            stats.MpBase += (j * 2);
                            stats.Spirit += j + ((j % 2 == 0) ? 2 : 1);
                            stats.Strength += j;
                            stats.Stamina += (j * 2);
                            stats.Dexterity += (j * 2);
                            stats.Attack += (j * 2);
                            stats.Defense += j;
                            stats.Accuracy += (j * 2);
                            stats.Dodge += (j * 2);
                            break;
                        case CharacterClass.Spearman:
                            stats.HpBase += (j * 12);
                            stats.MpBase += (j * 2);
                            stats.Spirit += j;
                            stats.Strength += j;
                            stats.Stamina += (j * 3);
                            stats.Dexterity += j;
                            stats.Attack += (j * 3);
                            stats.Defense += j;
                            stats.Accuracy += j;
                            stats.Dodge += j;
                            break;
                        case CharacterClass.Bowman:
                            stats.HpBase += (j * 12);
                            stats.MpBase += (j * 2);
                            stats.Spirit += (j * 2);
                            stats.Strength += j;
                            stats.Stamina += j + ((j % 2 == 0) ? 2 : 3);
                            stats.Dexterity += (j * 3);
                            stats.Attack += j + ((j % 2 == 0) ? 2 : 3);
                            stats.Defense += j;
                            stats.Accuracy += (j * 3);
                            stats.Dodge += (j * 3);
                            break;
                        case CharacterClass.Medic:
                            stats.HpBase += (j * 7);
                            stats.MpBase += (j * 6);
                            stats.Spirit += (j * 3);
                            stats.Strength += (j * 2);
                            stats.Stamina += (j * 2);
                            stats.Dexterity += j;
                            stats.Attack += (j * 2);
                            stats.Defense += j;
                            stats.Accuracy += j;
                            stats.Dodge += j;
                            break;
                        case CharacterClass.Ninja:
                            stats.HpBase += (j * 10);
                            stats.MpBase += (j * 4);
                            stats.Spirit += (j * 2);
                            stats.Strength += (j * 2);
                            stats.Stamina += j + ((j % 2 == 0) ? 1 : 2);
                            stats.Dexterity += (j * 3);
                            stats.Attack += j + ((j % 2 == 0) ? 2 : 3);
                            stats.Defense += j;
                            stats.Accuracy += j;
                            stats.Dodge += (j * 2);
                            break;
                        case CharacterClass.Busker: // Temp copy from medic
                            stats.HpBase += (j * 7);
                            stats.MpBase += (j * 6);
                            stats.Spirit += (j * 3);
                            stats.Strength += (j * 2);
                            stats.Stamina += (j * 2);
                            stats.Dexterity += j;
                            stats.Attack += (j * 2);
                            stats.Defense += j;
                            stats.Accuracy += j;
                            stats.Dodge += j;
                            break;
                        case CharacterClass.Hanbi:
                            stats.HpBase += (j * 12);
                            stats.MpBase += (j * 2);
                            stats.Spirit += j;
                            stats.Strength += j + ((j % 2 == 0) ? 1 : 2);
                            stats.Stamina += j + ((j % 2 == 0) ? 1 : 2);
                            stats.Dexterity += j;
                            stats.Attack += j + ((j % 2 == 0) ? 1 : 2);
                            stats.Defense += j + ((j % 2 == 0) ? 1 : 2);
                            stats.Accuracy += j + ((j % 4 == 0) ? 1 : 2);
                            stats.Dodge += j + ((j % 2 == 0) ? 1 : 2);
                            break;
                    }

                    stats.SpBase += (j * 10);

                    CharacterStats[(CharacterClass)i + 1].Add(j, stats);
                }
            }
        }

        public CreatureBaseStats InitStats(Creature creature)
        {
            Character character = creature as Character;
            if (character != null)
                return GetBaseStats(character).Clone();

            Log.Error("StatsService: Unknown type: {0}.", creature.GetType().Name);
            return new CreatureBaseStats();
        }

        public CreatureBaseStats GetBaseStats(Character character)
        {
            return CharacterStats[character.Class][character.Level];
        }

        public void UpdateStats(Creature creature)
        {
            Character character = creature as Character;
            if (character != null)
            {
                UpdatePlayerStats(character);
                return;
            }

            Log.Error("StatsService: Unknown type: {0}.", creature.GetType().Name);
        }

        private void UpdatePlayerStats(Character character)
        {
            CreatureBaseStats baseStats = GetBaseStats(character);
            baseStats.CopyTo(character.GameStats);

            int itemsAttack = 0,
                itemsDefense = 0;

            /*lock (character.Inventory.ItemsLock)
            {
                foreach (var item in player.Inventory.EquipItems.Values)
                {
                    if (item == null)
                        continue;

                    ItemTemplate itemTemplate = item.ItemTemplate;

                    if (itemTemplate != null)
                    {
                        itemsAttack += itemTemplate.MinAttack + ((itemTemplate.Category == 3) ? (item.Upgrade * 5) : 0);
                        itemsDefense += itemTemplate.Defense + ((itemTemplate.Category == 1) ? (item.Upgrade * 5) : 0);
                    }
                }
            }*/

            character.GameStats.Attack = (int)(baseStats.Attack + (0.03f * baseStats.Strength + 3) + itemsAttack);
            character.GameStats.Defense = (int)(baseStats.Defense + (0.01f * baseStats.Stamina + 0.5) + itemsDefense);

            //character.EffectsImpact.ResetChanges(character);
            //character.EffectsImpact.ApplyChanges(character.GameStats);

            //new SpPlayerStats(player).Send(player);
            //new SpPlayerHpMpSp(player).Send(player);
        }


        public static StatsService GetInstance()
        {
            return (Instance != null) ? Instance : Instance = new StatsService();
        }
    }
}
