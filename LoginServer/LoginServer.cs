using Common.Model.Server;
using LoginServer.Config;
using LoginServer.Network;
using LoginServer.Network.GameServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginServer
{
    internal class LoginServer
    {
        public static List<ServerStruct> ServerList = new List<ServerStruct>();

        static void Main(string[] args)
        {
            Console.Title = "----===== YGCore LoginServer =====----";
            Console.WriteLine("Authors: Jenose\n"
                              + "Authorized representative: netgame.in.th\n\n"
                              + "-------------------------------------------\n");

            Stopwatch sw = Stopwatch.StartNew();

            Configuration.GetInstance();

            ClientManager.GetInstance();
            GSClientManager.GetInstance();
            GSNetworkFactory.GetInstance();
            NetworkFactory.GetInstance();

            sw.Stop();

            Thread.Sleep(100);
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("           Server start in {0}", (sw.ElapsedMilliseconds / 1000.0).ToString("0.00s"));
            Console.WriteLine("-------------------------------------------");

            Process.GetCurrentProcess().WaitForExit();
        }
    }
}
