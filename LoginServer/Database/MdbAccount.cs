using LoginServer.Config;
using LoginServer.Model.Account;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace LoginServer.Database
{
    public class MdbAccount
    {
        private static MdbAccount Instance;

        public static MdbAccount GetInstance()
        {
            return (Instance != null) ? Instance : Instance = new MdbAccount();
        }

        private MongoClient m_Client;
        private MongoServer m_Server;
        private MongoDatabase m_Database;

        private string MDBAccountTable = "accounts";

        public MdbAccount()
        {
            m_Client = new MongoClient(Configuration.Database.Url);
            m_Server = m_Client.GetServer();
            m_Database = m_Server.GetDatabase(Configuration.Database.Name);
        }

        public Account GetAccountByName(string name)
        {
            var collection = m_Database.GetCollection<Account>(MDBAccountTable);
            var query = Query<Account>.EQ(a => a.Name, name);
            var account = collection.FindOne(query);
            return (account != null) ? account : null;
        }

        public void AddAccount(Account acc)
        {
            var collection = m_Database.GetCollection<Account>(MDBAccountTable);
            collection.Insert(acc);
        }
    }
}
