using System.Collections;
using System.Runtime.CompilerServices;

namespace GameServer.Model.Account
{
    public class AccountSetting
    {
        public int PartyFriend { get; set; }

        public int PartySearch { get; set; }

        public int CanTrade { get; set; }

        public int CanWhisper { get; set; }

        public int CostumeType { get; set; }

        public int HairSwitch { get; set; }

        public int WeaponSwitch { get; set; }

        public int PetExp { get; set; }

        public int FameSwitch { get; set; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int GetSettings(AccountSetting Setting)
        {
            int flags = 0;
            if (Setting.HairSwitch == 1)
            {
                SetFlags(ref flags, 7, true);
            }
            if (Setting.CostumeType == 1)
            {
                SetFlags(ref flags, 4, true);
                return flags;
            }
            SetFlags(ref flags, 6, true);
            return flags;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SetFlags(ref int Flags, int ItemFlag, bool value)
        {
            int[] values = new int[] { Flags };
            BitArray array = new BitArray(values);
            array.Set(ItemFlag, value);
            Flags = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array.Get(i))
                {
                    Flags |= ((int)1) << i;
                }
            }
        }
    }
}
