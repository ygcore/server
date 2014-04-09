using GameServer.Database;
using GameServer.Model.Account;
using GameServer.Network.Send;
using GameServer.Service;
using System.Text;

namespace GameServer.Network.Recv
{
    public class RequestAuth : ARecvPacket
    {
        protected string Name;
        protected string Md5Pass;
        protected int chnId;
        protected string IpAddress;

        protected internal override void Read()
        {
            Name = Encoding.Default.GetString(ReadB(31)).Replace("\0", "");
            Md5Pass = Encoding.Default.GetString(ReadB(31));
            ReadH();
            ReadH();
            chnId = ReadH();
            IpAddress = Encoding.Default.GetString(ReadB(15)).Replace("\0", "");
        }

        protected internal override void Run()
        {
            _Client._Account = AuthService.GetInstance().AuthAccount(Name, Md5Pass);
            if (_Client._Account != null)
            {
                _Client._Account._Client = _Client;
                _Client._Account.LastAddress = IpAddress;
                _Client._Account.Setting = new AccountSetting();
                MdbAccount.GetInstance().UpdateAccount(_Client._Account);
                _Client.SendPacket(new ResponseAuth());
            }
        }
    }
}
