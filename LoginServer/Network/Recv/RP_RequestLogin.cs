using LoginServer.Database;

namespace LoginServer.Network.Recv
{
    public class RP_RequestLogin : ARecvPacket
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
            var account = MdbAccount.GetInstance().GetAccountByName(Login);

            if (account != null)
            {
                if (account.Password == MD5Pass)
                {
                    // login success
                }
                else
                {
                    // password is not correct login failed
                }
            }
            else
            {
                // account not exists login failed
            }
        }
    }
}
