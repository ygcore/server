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
        private MongoCollection<Account> m_Collection;

        private string MDBAccountTable = "accounts";

        public MdbAccount()
        {
            m_Client = new MongoClient(Configuration.Database.Url);
            m_Server = m_Client.GetServer();
            m_Database = m_Server.GetDatabase(Configuration.Database.Name);
            m_Collection = m_Database.GetCollection<Account>(MDBAccountTable);
        }

        public Account GetAccountByName(string name)
        {
            var query = Query<Account>.EQ(a => a.Name, name);
            var account = m_Collection.FindOne(query);
            return (account != null) ? account : null;
        }

        public void AddAccount(Account acc)
        {
            m_Collection.Insert(acc);
        }

        public void UpdateAccount(Account acc)
        {
            var query = Query<Account>.EQ(e => e.Id, acc.Id);
            var update = Update<Account>
                .Set(e => e.LastAddress, acc.LastAddress);
            m_Collection.Update(query, update);
        }
    }
}
