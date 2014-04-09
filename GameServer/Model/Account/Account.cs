using GameServer.Network;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GameServer.Model.Account
{
    public class Account
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string LastAddress { get; set; }

        public bool HasAKey { get; set; }

        public string DeletePasswd { get; set; }

        public AccountSetting Setting { get; set; }

        [BsonIgnore]
        public Client _Client { get; set; }

        [BsonIgnore]
        public Dictionary<int, Character.Character> _Characters { get; set; }
    }
}
