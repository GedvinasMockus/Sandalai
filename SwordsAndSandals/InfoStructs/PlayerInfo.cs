using SwordsAndSandals.Stats;
using System.Numerics;

namespace SwordsAndSandals.InfoStructs
{
    public struct PlayerInfo
    {
        public Vector2 Position { get; set; }
        public Attributes BaseAttributes { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string ConnectionID { get; set; }
        public PlayerInfo(Vector2 position, Attributes attributes, string className, string name ,string connectionId)
        {
            Position = position;
            BaseAttributes = attributes;
            ClassName = className;
            ConnectionID = connectionId;
            Name = name;
        }
    }
}
