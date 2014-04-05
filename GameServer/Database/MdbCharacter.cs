using GameServer.Config;
using GameServer.Model.Character;
using MongoDB.Driver;

namespace GameServer.Database
{
    public class MdbCharacter
    {
        private static MdbCharacter Instance;

        public static MdbCharacter GetInstance()
        {
            return (Instance != null) ? Instance : Instance = new MdbCharacter();
        }

        private MongoClient m_Client;
        private MongoServer m_Server;
        private MongoDatabase m_Database;

        private string MDBCharacterTable = "characters";

        public MdbCharacter()
        {
            m_Client = new MongoClient(Configuration.Database.Url);
            m_Server = m_Client.GetServer();
            m_Database = m_Server.GetDatabase(Configuration.Database.Name);
        }

        public void AddCharacter(Character Char)
        {
            var collection = m_Database.GetCollection<Character>(MDBCharacterTable);
            collection.Insert(Char);
        }
    }
}
