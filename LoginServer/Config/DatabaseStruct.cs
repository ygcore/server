namespace LoginServer.Config
{
    public struct DatabaseStruct
    {
        public string Url;

        public string Name;

        public DatabaseStruct(string url, string name)
        {
            Url = url;
            Name = name;
        }
    }
}
