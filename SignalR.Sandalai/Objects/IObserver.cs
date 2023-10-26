using SignalR.Sandalai.InfoStructs;
using SignalR.Sandalai.PlayerClasses;

namespace SignalR.Sandalai.Objects
{
    public interface IObserver
    {
        void Update(Player sender, string ability, BattleInfo info);
    }
}
