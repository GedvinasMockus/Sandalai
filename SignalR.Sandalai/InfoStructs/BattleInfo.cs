namespace SignalR.Sandalai.InfoStructs
{
    public struct BattleInfo
    {
        public PlayerInfo Player1 { get; private set; }
        public PlayerInfo Player2 { get; private set; }
        public string StartTime { get; private set; }
        public BattleInfo(PlayerInfo player1, PlayerInfo player2, string time = "")
        {
            Player1 = player1;
            Player2 = player2;
            StartTime = time;
        }
    }
}
