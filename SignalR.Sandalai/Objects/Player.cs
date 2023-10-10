using System.Numerics;

namespace SignalR.Sandalai.Objects
{
    public class Player
    {
        public string ConnectionId { get; private set; }

        public string ClassName { get; private set; }
        public Vector2 Position { get; set; }
        public FlipEnum Flip { get; set; }

        public Player(string connId, string className)
        {
            ConnectionId = connId;
            ClassName = className;
        }
        public PlayerInfo GetInfo()
        {
            return new PlayerInfo(Position, (int)Flip, ClassName);
        }
    }

}
