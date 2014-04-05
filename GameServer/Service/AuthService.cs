using GameServer.Database;
using GameServer.Model.Account;

namespace GameServer.Service
{
    public class AuthService
    {
        private static AuthService Instance;

        public static AuthService GetInstance()
        {
            return (Instance != null) ? Instance : Instance = new AuthService();
        }

        public Account AuthAccount(string login, string password)
        {
            var account = MdbAccount.GetInstance().GetAccountByName(login);
            return (account != null) ? account : null;
        }
    }
}
