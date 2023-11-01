using SignalR.Sandalai.PlayerClasses;

using System.Numerics;

namespace SignalR.Sandalai
{
    public struct PlayerInfo
    {
        public Vector2 Position { get; private set; }
        public Attributes BaseAttributes { get; private set; }
        public string ClassName { get; private set; }
        public string ConnectionID { get; private set; }
        public PlayerInfo(Vector2 position, Attributes attributes, string className, string connectionId)
        {
            Position = position;
            BaseAttributes = attributes;
            ClassName = className;
            ConnectionID = connectionId;
        }

    }
}
