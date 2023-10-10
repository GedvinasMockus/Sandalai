namespace SwordsAndSandals.InfoStructs
{
    public struct BattleInfo
    {
        public PlayerInfo Player1 { get; set; }
        public PlayerInfo Player2 { get; set; }

        public BattleInfo(PlayerInfo player1, PlayerInfo player2)
        {
            Player1 = player1;
            Player2 = player2;
        }
    }
}
