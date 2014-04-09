using GameServer.Config;
using GameServer.Model.Character;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;

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
        private MongoCollection<Character> m_Collection;

        private string MDBTable = "characters";

        public MdbCharacter()
        {
            m_Client = new MongoClient(Configuration.Database.Url);
            m_Server = m_Client.GetServer();
            m_Database = m_Server.GetDatabase(Configuration.Database.Name);
            m_Collection = m_Database.GetCollection<Character>(MDBTable);
        }

        public void AddCharacter(Character Char)
        {
            m_Collection.Insert(Char);
        }

        public Dictionary<int, Character> GetAccountCharacter(string AccountName)
        {
            var query = Query<Character>.EQ(c => c.AccountName, AccountName);
            var characters = m_Collection.Find(query);

            if (characters.Count() <= 0)
                return null;

            Dictionary<int, Character> list = new Dictionary<int, Character>();
            int i = 0;
            foreach (var character in characters)
            {
                list.Add(i, character);
                i++;
            }

            return list;
        }

        public bool IsAvailableName(string name)
        {
            var query = Query<Character>.EQ(c => c.Name, name);
            long num = m_Collection.Find(query).Count();
            return (num <= 0) ? true : false;
        }

        internal bool DeleteCharacter(string name)
        {
            var query = Query<Character>.EQ(c => c.Name, name);
            var result = m_Collection.Remove(query);
            return result.Ok;
        }
    }
}
