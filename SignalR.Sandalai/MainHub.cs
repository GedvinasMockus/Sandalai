using Microsoft.AspNet.SignalR;

using SignalR.Sandalai.Objects;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.Sandalai
{
    public class MainHub : Hub
    {
        private readonly Lobby lobby;
        public static List<Battle> battleList = new List<Battle>();
        private object obj = new object();
        public MainHub() : this(Lobby.Instance) { }
        public MainHub(Lobby lobby)
        {
            this.lobby = lobby;
        }
        public void AddToLobby(string className)
        {
            Console.WriteLine("Connected");
            lobby.AddUser(Context.ConnectionId, className);
            Console.WriteLine(Context.ConnectionId);
        }
        public void RemoveFromLobby()
        {
            Console.WriteLine("Disonnected");
            lobby.RemoveUser(Context.ConnectionId);
            Console.WriteLine(Context.ConnectionId);
        }
        public void FindOpponent()
        {
            Player opponent = lobby.FindAnotherUser(Context.ConnectionId);
            if (opponent != null)
            {
                Player player = lobby.GetUser(Context.ConnectionId);
                lobby.RemoveUser(Context.ConnectionId);
                lobby.RemoveUser(opponent.ConnectionId);
                Battle battle = new Battle();
                battle.Attach(player);
                battle.Attach(opponent);
                lock (obj)
                {
                    battleList.Add(battle);
                }
                battle.BattleStart();

                Clients.Client(battle.GetPlayer(0).ConnectionId).OpponentFound(battle.GetInfo(false));
                Clients.Client(battle.GetPlayer(1).ConnectionId).OpponentFound(battle.GetInfo(true));
            }
        }

        public void AbilityUsed(string name)
        {
            Battle battle = battleList.Find((b) => b.GetPlayer(0).ConnectionId == Context.ConnectionId || b.GetPlayer(1).ConnectionId == Context.ConnectionId);
            if (battle.GetPlayer(0).ConnectionId == Context.ConnectionId)
            {
                Clients.Client(battle.GetPlayer(1).ConnectionId).AbilityUsed(name);
                battle.AbilityUsed(name, battle.GetPlayer(0).ConnectionId);
            }
            else
            {
                Clients.Client(battle.GetPlayer(0).ConnectionId).AbilityUsed(name);
                battle.AbilityUsed(name, battle.GetPlayer(1).ConnectionId);
            }
        }

        public void LeaveBattle()
        {
            Battle battle = battleList.Find((b) => b.GetPlayer(0).ConnectionId == Context.ConnectionId || b.GetPlayer(1).ConnectionId == Context.ConnectionId);
            battleList.Remove(battle);
            Clients.Caller.BattleLeft();
            if (battle.GetPlayer(0).ConnectionId != Context.ConnectionId)
            {
                lobby.AddUser(battle.GetPlayer(0).ConnectionId, battle.GetPlayer(0).ClassName);
                Clients.Client(battle.GetPlayer(0).ConnectionId).BackToLoading();
            }
            else
            {
                lobby.AddUser(battle.GetPlayer(1).ConnectionId, battle.GetPlayer(1).ClassName);
                Clients.Client(battle.GetPlayer(1).ConnectionId).BackToLoading();
            }
            battle.Detach(battle.GetPlayer(1));
            battle.Detach(battle.GetPlayer(0));

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            Battle battle = battleList.Find((b) => b.GetPlayer(0).ConnectionId == Context.ConnectionId || b.GetPlayer(1).ConnectionId == Context.ConnectionId);
            if (battle == null)
            {
                lobby.RemoveUser(Context.ConnectionId);
                return base.OnDisconnected(stopCalled);
            }
            battleList.Remove(battle);
            if (battle.GetPlayer(0).ConnectionId != Context.ConnectionId)
            {
                lobby.AddUser(battle.GetPlayer(0).ConnectionId, battle.GetPlayer(0).ClassName);
                Clients.Client(battle.GetPlayer(0).ConnectionId).BackToLoading();
            }
            else
            {
                lobby.AddUser(battle.GetPlayer(1).ConnectionId, battle.GetPlayer(1).ClassName);
                Clients.Client(battle.GetPlayer(1).ConnectionId).BackToLoading();
            }
            battle.Detach(battle.GetPlayer(1));
            battle.Detach(battle.GetPlayer(0));
            return base.OnDisconnected(stopCalled);
        }
    }
}
