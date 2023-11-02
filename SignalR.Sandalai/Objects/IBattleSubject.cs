using SignalR.Sandalai.InfoStructs;

using System.Collections.Generic;

namespace SignalR.Sandalai.Objects
{
    public interface IBattleSubject
    {
        void AddToBattle(IBattleSpectatorObserver spectator);
        void RemoveFromBattle(IBattleSpectatorObserver spectator);
        void NotifySpectators(int playerNum, string ability, BattleInfo info);
        List<IBattleSpectatorObserver> DetachAll();
    }
}
