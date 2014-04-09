
using GameServer.Model.Item;
using GameServer.Model.Map;
namespace GameServer.Model.Character
{
    public class Character : Creature.Creature
    {
        public string AccountName { get; set; }

        public int ServerId { get; set; }

        public string Name { get; set; }

        #region Info
        // ----- Info ----- //

        public CharacterGender Gender { get; set; }

        public CharacterClass Class { get; set; }

        public int Level { get; set; }

        public int JobLevel { get; set; }

        public long Exp { get; set; }

        public int SkillPoint { get; set; }

        public int AbilityPoint { get; set; }

        public int AscensionPoint { get; set; }

        public int CurrentAscensionPoint { get; set; }

        public int HonorPoint { get; set; }

        public int KarmaPoint { get; set; }

        public int DPoint { get; set; }

        public int CraftType { get; set; }

        public int CraftLevel { get; set; }

        public int CraftExp { get; set; }

        // ----- Info ----- //
        #endregion

        #region Appearance
        // ----- Appearance ----- //

        public int Title { get; set; }

        public int Forces { get; set; }

        public int HairStyle { get; set; }

        public int HairColor { get; set; }

        public int Face{ get; set; }

        public int Voice { get; set; }

        public byte[] NameStyle { get; set; }

        // ----- Appearance ----- //
        #endregion

        public MapPosition Position { get; set; }

        public Storage Inventory = new Storage { StorageType = StorageType.Inventory };

        public Storage Equipment = new Storage { StorageType = StorageType.Equipment };
    }
}
