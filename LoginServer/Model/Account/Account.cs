using MongoDB.Bson;

namespace LoginServer.Model.Account
{
    public class Account
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string LastAddress { get; set; }
    }
}
