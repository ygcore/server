namespace GameServer.Network.Send
{
    public class ResponseCheckName : ASendPacket
    {
        protected string Name;
        protected short Result;

        public ResponseCheckName(string name, bool res)
        {
            Name = name;
            Result = (short)((res == true) ? 1 : 0);
        }

        protected internal override void Write()
        {
            WriteH((short)Result);
            WriteH(0);
            WriteS(Name, 15);
        }
    }
}
