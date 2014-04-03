using Common.Utilities;
using Nini.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Config
{
    public class Configuration
    {
        private static Configuration Instance;

        private static IConfig _DBCfg = new IniConfigSource("Config/database.ini").Configs["database"];
        private static IConfig _NWCfg = new IniConfigSource("Config/network.ini").Configs["network"];
        private static IConfig _STCfg = new IniConfigSource("Config/setting.ini").Configs["setting"];

        private static IConfigSource _CNSource = new IniConfigSource("Config/channel.ini");

        public static DatabaseStruct Database;
        public static NetworkStruct Network;
        public static SettingStruct Setting;

        public List<ChannelStruct> Channels = new List<ChannelStruct>(10);

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
                _NWCfg.GetString("private.ip"),
                _NWCfg.GetInt("private.port"),
                _NWCfg.GetString("login.ip"),
                _NWCfg.GetInt("login.port")
            );
            Log.Info("Loaded Network Configuration");

            Setting = new SettingStruct(
                _STCfg.GetInt("server.id"),
                _STCfg.GetString("server.name"),
                _STCfg.GetBoolean("debuging"),
                _STCfg.GetInt("rate.exp"),
                _STCfg.GetInt("rate.money"),
                _STCfg.GetInt("rate.sp")
            );
            Log.Info("Loaded Setting Configuration");

            Console.WriteLine("\n-------------------------------------------\n");

            Console.WriteLine("Load Channels Configuration...");
            Console.WriteLine("-------------------------------------------");
            for(int i = 0; i < 10; i++)
            {
                var cConfig = _CNSource.Configs["channel" + (i + 1)];
                if (cConfig == null)
                    continue;

                Channels.Add(new ChannelStruct(
                    cConfig.GetInt("channel.port"),
                    cConfig.GetString("channel.name"),
                    cConfig.GetInt("channel.type"),
                    cConfig.GetInt("channel.max")
                ));

                Log.Info("Channel: {0} Loaded", (i + 1));
            }
            Console.WriteLine("\n-------------------------------------------\n");
        }

        public static Configuration GetInstance()
        {
            return (Instance != null) ? Instance : Instance = Instance = new Configuration();
        }
    }
}
