
namespace GameServer.Network.Send
{
    public class ResponseServerTime : ASendPacket
    {
        protected int Time;

        public ResponseServerTime(int time)
        {
            Time = time;
        }

        protected internal override void Write()
        {
            WriteD(Time);
        }
    }
}
