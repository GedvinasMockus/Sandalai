using SignalR.Sandalai.InfoStructs;

namespace SignalR.Sandalai.Objects
{
    public interface IBattleSubject
    {
        void AddToBattle(Spectator spectator);
        void RemoveFromBattle(string connectionId);
        void NotifySpectators(int playerNum, string ability, BattleInfo info);
    }
}
