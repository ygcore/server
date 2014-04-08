using Common.Utilities;
using GameServer.Model.Creature;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace GameServer.DataHolder
{
    public class Data
    {
        public static string DataPath = Path.GetFullPath("data/");

        public static List<CreatureBaseStats> Stats = new List<CreatureBaseStats>();

        protected delegate int Loader();

        protected static List<Loader> Loaders = new List<Loader>
                                                    {
                                                        //LoadMapTemplates,
                                                        //LoadPlayerExperience,
                                                        LoadBaseStats,
                                                        //LoadItemTemplates,
                                                        //LoadSpawns,
                                                        //LoadBindPoints,
                                                        //LoadNpcTemplates,
                                                        //LoadQuests,
                                                        //LoadSkills,
                                                        //LoadAbilities,
                                                        //LoadAbnormalities,
                                                        //LoadDrop,
                                                        //LoadTeleports,
                                                        //CalculateNpcExperience,
                                                    };

        public static void LoadAll()
        {
            Console.WriteLine("Load All Data holders...");
            Console.WriteLine("-------------------------------------------");

            Parallel.For(0, Loaders.Count, i => LoadTask(Loaders[i]));

            Console.WriteLine("\n-------------------------------------------\n");
        }

        private static void LoadTask(Loader loader)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int readed = loader.Invoke();
            stopwatch.Stop();

            Log.Info("Data: {0,-26} {1,7} values in {2}s"
                , loader.Method.Name
                , readed
                , (stopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00"));
        }

        public static int LoadBaseStats()
        {
            using (FileStream fs = File.OpenRead(DataPath + "stats.bin"))
            {
                Stats = Serializer.DeserializeWithLengthPrefix<List<CreatureBaseStats>>(fs, PrefixStyle.Fixed32);
            }

            return Stats.Count;
        }
    }
}
