namespace SwordsAndSandals.InfoStructs
{
    public struct BattleInfo
    {
        public PlayerInfo Player1 { get; set; }
        public PlayerInfo Player2 { get; set; }
        public string StartTime { get; set; }
        public BattleInfo(PlayerInfo player1, PlayerInfo player2, string time = "")
        {
            Player1 = player1;
            Player2 = player2;
            StartTime = time;
        }
    }
}
