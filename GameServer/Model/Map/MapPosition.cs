namespace GameServer.Model.Map
{
    public class MapPosition
    {
        public int MapId { get; set; }

        public byte[] X { get; set; }

        public byte[] Y { get; set; }

        public byte[] Z { get; set; }

        public byte[] oldX { get; set; }

        public byte[] oldY { get; set; }

        public byte[] oldZ { get; set; }
    }
}
