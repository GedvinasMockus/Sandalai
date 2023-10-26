using SwordsAndSandals.Objects.Stats;
using System.Numerics;

namespace SwordsAndSandals.InfoStructs
{
    public struct PlayerInfo
    {
        public Vector2 Position { get; set; }
        public Attributes BaseAttributes { get; set; }
        public string ClassName { get; set; }
        public PlayerInfo(Vector2 position, Attributes attributes, string className)
        {
            Position = position;
            BaseAttributes = attributes;
            ClassName = className;
        }
    }
}
