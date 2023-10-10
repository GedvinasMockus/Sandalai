using System.Numerics;

namespace SignalR.Sandalai
{
    public struct PlayerInfo
    {
        public Vector2 Position { get; private set; }
        public int Flip { get; private set; }
        public string ClassName { get; private set; }
        public PlayerInfo(Vector2 position, int flip, string className)
        {
            Position = position;
            Flip = flip;
            ClassName = className;
        }

    }
}
