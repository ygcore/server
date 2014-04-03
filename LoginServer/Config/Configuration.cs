using Common.Utilities;
using Nini.Config;
using System;

namespace LoginServer.Config
{
    public class Configuration
    {
        private static Configuration Instance;

        private static IConfig _DBCfg = new IniConfigSource("Config/database.ini").Configs["database"];
        private static IConfig _NWCfg = new IniConfigSource("Config/network.ini").Configs["network"];
        private static IConfig _STCfg = new IniConfigSource("Config/setting.ini").Configs["setting"];

        public static DatabaseStruct Database;
        public static NetworkStruct Network;
        public static SettingStruct Setting;

        public Configuration()
        {
            Console.WriteLine("Load All Configuration...");
            Console.WriteLine("-------------------------------------------");
            Database = new DatabaseStruct(
                _DBCfg.GetString("db.mongo.url"),
                _DBCfg.GetString("db.mongo.name")
            );
            Log.Info("Loaded Database Configuration");
            Network = new NetworkStruct(
                _NWCfg.GetString("public.ip"),
                _NWCfg.GetInt("public.port"),
                _NWCfg.GetString("private.ip"),
                _NWCfg.GetInt("private.port")
            );
            Log.Info("Loaded Network Configuration");
            Setting = new SettingStruct(
                _STCfg.GetBoolean("debuging"),
                _STCfg.GetBoolean("autoaccount"),
                _STCfg.GetBoolean("gmonly")
            );
            Log.Info("Loaded Setting Configuration");
            Console.WriteLine("\n-------------------------------------------\n");
        }

        public static Configuration GetInstance()
        {
            return (Instance != null) ? Instance : Instance = Instance = new Configuration();
        }
    }
}
