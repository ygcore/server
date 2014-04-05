using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model.Account
{
    public class Account
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string LastAddress { get; set; }

        public bool HasAKey { get; set; }
    }
}
