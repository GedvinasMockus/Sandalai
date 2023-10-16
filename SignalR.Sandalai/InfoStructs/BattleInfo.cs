namespace SignalR.Sandalai.InfoStructs
{
    public struct BattleInfo
    {
        public PlayerInfo Player1 { get; private set; }
        public PlayerInfo Player2 { get; private set; }

        public BattleInfo(PlayerInfo player1, PlayerInfo player2)
        {
            Player1 = player1;
            Player2 = player2;
        }
    }
}
