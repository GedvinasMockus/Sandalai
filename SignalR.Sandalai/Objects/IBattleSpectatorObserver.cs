using SignalR.Sandalai.InfoStructs;

namespace SignalR.Sandalai.Objects
{
    public interface IBattleSpectatorObserver
    {
        string ConnectionId { get; set; }
        void SendBattleInfo(int playerNum, string ability, BattleInfo info);
    }
}
