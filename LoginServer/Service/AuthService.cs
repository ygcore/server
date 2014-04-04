using LoginServer.Config;
using LoginServer.Database;
using LoginServer.Model.Account;

namespace LoginServer.Service
{
    public enum AuthResponse
    {
        Success,
        WrongInfo,
        Banned,
        NoAtKey,
    }

    public class AuthService
    {
        private static AuthService Instance;

        public static AuthService GetInstance()
        {
            return (Instance != null) ? Instance : Instance = new AuthService();
        }

        public AuthResponse AuthAccount(string login, string password, ref Account account)
        {
            AuthResponse result;

            account = MdbAccount.GetInstance().GetAccountByName(login);

            if (account != null)
            {
                if (account.Password == password)
                    result = AuthResponse.Success;
                else
                    result = AuthResponse.WrongInfo;
            }
            else
            {
                if (Configuration.Setting.AutoAccount)
                {
                    account = new Account()
                    {
                        Name = login,
                        Password = password,
                        LastAddress = "0.0.0.0",
                    };
                    MdbAccount.GetInstance().AddAccount(account);
                    result = AuthResponse.Success;
                }
                else
                    result = AuthResponse.WrongInfo;
            }

            return result;
        }
    }
}
