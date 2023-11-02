using Microsoft.AspNet.SignalR;

using SignalR.Sandalai.InfoStructs;

using System.Collections.Generic;

namespace SignalR.Sandalai.Objects
{
    public class Spectator : IObserver, IBattleSpectatorObserver
    {
        public string ConnectionId { get; set; }

        public Spectator(string connectionId)
        {
            ConnectionId = connectionId;
        }

        public void SendInfo(List<BattleInfo> battleInfos)
        {
            var clients = GlobalHost.ConnectionManager.GetHubContext<MainHub>().Clients;
            clients.Client(ConnectionId).SpectateBattleInfo(battleInfos);
        }

        public void SendBattleInfo(int playerNum, string ability, BattleInfo info)
        {
            var clients = GlobalHost.ConnectionManager.GetHubContext<MainHub>().Clients;
            clients.Client(ConnectionId).AbilityUsedSpectate(ability, playerNum, info);
        }

        public void RemoveFromSpectate()
        {
            var clients = GlobalHost.ConnectionManager.GetHubContext<MainHub>().Clients;
            clients.Client(ConnectionId).BackToBattleList();
        }
    }
}
