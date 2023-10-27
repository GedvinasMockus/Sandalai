using SignalR.Sandalai.InfoStructs;

namespace SignalR.Sandalai.Objects
{
    public interface IBattleSpectatorObserver
    {
        void SendBattleInfo(int playerNum, string ability, BattleInfo info);
    }
}
