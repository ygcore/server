namespace GameServer.Network.Send
{
    public class ResponseDeleteCharacter : ASendPacket
    {
        protected bool Success;

        public ResponseDeleteCharacter(bool success)
        {
            Success = success;
        }

        protected internal override void Write()
        {
            if (Success)
            {
                WriteD(1);
                WriteD(0);
            }
            else
            {
                WriteD(99);
                WriteD(9);
            }
            
        }
    }
}
