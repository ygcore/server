using Common.Utilities;
using LoginServer.Config;
using LoginServer.Database;
using LoginServer.Network.Send;
using LoginServer.Service;

namespace LoginServer.Network.Recv
{
    public class RequestLogin : ARecvPacket
    {
        protected string Login;
        protected string MD5Pass;

        protected internal override void Read()
        {
            Login = ReadS();
            MD5Pass = ReadS();
        }

        protected internal override void Run()
        {
            var resp = AuthService.GetInstance().AuthAccount(Login, MD5Pass, ref _Client._Account);
            _Client.SendPacket(new ResponseLogin(resp));
        }
    }
}
