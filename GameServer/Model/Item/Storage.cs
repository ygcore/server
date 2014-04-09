using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model.Item
{
    public class Storage
    {
        public StorageType StorageType;

        public SortedDictionary<int, StorageItem> Items = new SortedDictionary<int, StorageItem>();

        public object ItemsLock = new object();

        public long Money = 100;

        public short Size = 36;

        public int MaxWeight = 500;

        public bool Locked = false;

        public Storage()
        {
            if (StorageType == Item.StorageType.Equipment)
                for (int i = 0; i < 15; i++)
                    Items.Add(i, null);
        }

        public StorageItem GetItem(int slot)
        {
            lock (ItemsLock)
            {
                if (Items.ContainsKey(slot))
                    return Items[slot];
            }

            return null;
        }
    }
}
