using Common.Utilities;
using System.Collections.Generic;

namespace GameServer.Model.Map
{
    public class Map
    {
        public int MapId;

        public IDFactory NpcUID = new IDFactory(10000);
        public IDFactory DropUID = new IDFactory(20000);

        public List<Character.Character> Characters = new List<Character.Character>();
    }
}
