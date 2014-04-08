using GameServer.Model.Item;
using System;
using System.IO;
using System.Text;

namespace GameServer.Network
{
    public abstract class ASendPacket
    {
        private MemoryStream mstream = new MemoryStream();

        public Client _Client;

        public long Length
        {
            get
            {
                return this.mstream.Length;
            }
        }

        protected internal void WriteB(byte[] value)
        {
            this.mstream.Write(value, 0, value.Length);
        }

        protected internal void WriteD(int value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteH(short val)
        {
            WriteB(BitConverter.GetBytes(val));
        }

        protected internal void WriteH(int val)
        {
            WriteB(BitConverter.GetBytes((short)val));
        }

        protected internal void WriteC(byte value)
        {
            this.mstream.WriteByte(value);
        }

        protected internal void WriteC(int value)
        {
            this.mstream.WriteByte((byte)value);
        }

        protected internal void WriteF(double value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteQ(long value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteS(string value)
        {
            if (value == null)
                return;

            WriteH((short)value.Length);
            WriteB(Encoding.Default.GetBytes(value));
        }

        protected internal void WriteS(string name, int count)
        {
            if (name == null)
                return;

            WriteB(Encoding.Default.GetBytes(name));
            WriteB(new byte[count - name.Length]);
        }

        protected void WriteItemInfo(StorageItem item)
        {
            /*if (item != null)
            {
                WriteQ(item.UID);
                WriteQ(item.ItemId);
                WriteD(item.Amount); // Amount
                WriteD(item.Magic0); // FLD_MAGIC0
                WriteD(item.Magic1); // FLD_MAGIC1
                WriteD(item.Magic2); // FLD_MAGIC2
                WriteD(item.Magic3); // FLD_MAGIC3
                WriteD(item.Magic4); // FLD_MAGIC4
                WriteH(0);
                WriteH(0); // (IsBlue == true ? 1 : 0)
                WriteH(item.BonusMagic1); // BonusMagic1
                WriteH(item.BonusMagic2); // BonusMagic2
                WriteH(item.BonusMagic3); // BonusMagic3
                WriteH(item.BonusMagic4); // BonusMagic4
                WriteH(item.BonusMagic5); // BonusMagic5
                WriteD(0);
                WriteD((item.LimitTime > 0 ? 1 : 0));
                WriteD(item.LimitTime);
                WriteH(item.Upgrade);
                WriteH(item.ItemTemplate.Category); // ItemType
                WriteH(0); // 0
                WriteH(0);
                WriteQ(0);
                WriteB(new byte[6]);
            }
            else*/
                WriteB(new byte[88]);
        }

        public byte[] ToByteArray()
        {
            return this.mstream.ToArray();
        }

        protected internal abstract void Write();
    }
}
