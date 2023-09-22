using System.Numerics;

namespace SignalR.Sandalai.Objects
{
    public class Battle
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        private const float Pos1x = 1f / 6;
        private const float Pos2x = 5f / 6;
        private const float PosGround = 11f / 17;
        public Vector2 Position1 { get; set; }
        public Vector2 Position2 { get; set; }
        public FlipEnum Flip1 { get; set; }
        public FlipEnum Flip2 { get; set; }
        public Battle(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            Position1 = new Vector2(Pos1x, PosGround);
            Position2 = new Vector2(Pos2x, PosGround);
            Flip1 = FlipEnum.None;
            Flip2 = FlipEnum.FlipHorizontally;
        }
    }
}
