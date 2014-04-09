namespace GameServer.Network.Send
{
    public class ResponseExit : ASendPacket
    {
        protected internal override void Write()
        {
            WriteD(1);
            WriteB(new byte[18]);
            WriteS(_Client._Account.Name.ToUpper(), 20);
        }
    }
}
