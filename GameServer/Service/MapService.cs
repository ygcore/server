using GameServer.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Service
{
    public class MapService
    {
        private static MapService Instance;

        public Dictionary<int, Map> Maps = new Dictionary<int, Map>();

        public MapService()
        {

        }

        public static MapService GetInstance()
        {
            return (Instance != null) ? Instance : Instance = new MapService();
        }
    }
}
