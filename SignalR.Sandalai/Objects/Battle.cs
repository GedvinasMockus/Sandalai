using SignalR.Sandalai.InfoStructs;

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
        public Battle(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
        }
        public void BattleStart()
        {
            this.Player1.Position = new Vector2(Pos1x, PosGround);
            this.Player2.Position = new Vector2(Pos2x, PosGround);
            this.Player1.Flip = FlipEnum.None;
            this.Player2.Flip = FlipEnum.FlipHorizontally;
        }
        public void BattleStop()
        {
            this.Player1.Position = new Vector2(-1, -1);
            this.Player2.Position = new Vector2(-1, -1);
            this.Player1.Flip = FlipEnum.None;
            this.Player2.Flip = FlipEnum.None;
        }
        public BattleInfo GetInfo(bool flip)
        {
            return flip ? new BattleInfo(Player2.GetInfo(), Player1.GetInfo()) : new BattleInfo(Player1.GetInfo(), Player2.GetInfo());
        }
    }
}
