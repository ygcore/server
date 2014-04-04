using Common.Utilities;
using LoginServer.Service;

namespace LoginServer.Network.Send
{
    public class ResponseLogin : ASendPacket
    {
        protected AuthResponse Response;

        public ResponseLogin(AuthResponse resp)
        {
            Response = resp;
        }

        protected internal override void Write()
        {
            switch(Response)
            {
                case AuthResponse.Success:
                    WriteH(0);
                    WriteH(0);
                    WriteS(_Client._Account.Name);
                    WriteC(0);
                    WriteS(_Client._Account.Name);
                    break;
                case AuthResponse.WrongInfo:
                    WriteH(1);
                    WriteH(3);
                    break;
                case AuthResponse.NoAtKey:
                    WriteD(36);
                    break;
                case AuthResponse.Banned:
                    Log.Error("Banned Response is not implement");
                    break;
            }
        }
    }
}
