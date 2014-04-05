using GameServer.Config;
using GameServer.Network;
using GameServer.Network.LoginServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    class GameServer
    {
        static void Main(string[] args)
        {
            Console.Title = "YGCore GameServer";
            Console.WriteLine("Authors: Jenose\n"
                              + "Authorized representative: netgame.in.th\n\n"
                              + "-------------------------------------------\n");

            Stopwatch sw = Stopwatch.StartNew();

            Configuration.GetInstance();

            ClientManager.GetInstance();
            LSClient.GetInstance();

            foreach(var channel in Configuration.GetInstance().Channels)
                NetworkFactory.NewInstance(channel.Port);

            sw.Stop();

            Thread.Sleep(100);
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("           Server start in {0}", (sw.ElapsedMilliseconds / 1000.0).ToString("0.00s"));
            Console.WriteLine("-------------------------------------------");

            Process.GetCurrentProcess().WaitForExit();
        }
    }
}
