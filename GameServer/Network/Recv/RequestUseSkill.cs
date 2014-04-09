namespace GameServer.Network.Recv
{
    public class RequestUseSkill : ARecvPacket
    {
        protected int SkillId;

        protected internal override void Read()
        {
            SkillId = ReadD();
            //ReadQ(); // unk
            //Log.Debug("X:{0}, Z:{1}, Y:{2}", ReadF(), ReadF(), ReadF());
            //ReadD();
        }

        protected internal override void Run()
        {
            
        }
    }
}
