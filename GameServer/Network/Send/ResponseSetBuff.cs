namespace GameServer.Network.Send
{
    public class ResponseSetBuff : ASendPacket
    {
        protected int SkillID;
        protected byte Cmd;
        protected byte SkillLevel;

        public ResponseSetBuff(int skill, byte cmd, int level)
        {
            SkillID = skill;
            Cmd = cmd;
            SkillLevel = (byte)level;
        }

        protected internal override void Write()
        {
            WriteD(SkillID);
            WriteC(Cmd);
            WriteC(SkillLevel);
            WriteH(0);
            WriteD(0);
            WriteD(0);
            WriteD(0);
            WriteD(0);
            WriteD(3000);
        }
    }
}
