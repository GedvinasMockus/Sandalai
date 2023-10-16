using System.Numerics;

namespace SwordsAndSandals.InfoStructs
{
    public struct PlayerInfo
    {
        public Vector2 Position { get; set; }
        public int Flip { get; set; }
        public string ClassName { get; set; }
        public PlayerInfo(Vector2 position, int flip, string className)
        {
            Position = position;
            Flip = flip;
            ClassName = className;
        }
    }
}
