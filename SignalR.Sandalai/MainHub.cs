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
                Battle battle = new Battle(opponent, player);
                lock (obj)
                {
                    battleList.Add(battle);
                }
                battle.BattleStart();
                Clients.Client(battle.Player1.ConnectionId).OpponentFound(battle.GetInfo(false));
                Clients.Client(battle.Player2.ConnectionId).OpponentFound(battle.GetInfo(true));
            }
        }

        public void AbilityUsed(string name)
        {
            Battle battle = battleList.Find((b) => b.Player1.ConnectionId == Context.ConnectionId || b.Player2.ConnectionId == Context.ConnectionId);
            if (battle.Player1.ConnectionId == Context.ConnectionId)
            {
                Clients.Client(battle.Player2.ConnectionId).AbilityUsed(name);
            }
            else
            {
                Clients.Client(battle.Player1.ConnectionId).AbilityUsed(name);
            }
        }

        public void LeaveBattle()
        {
            Battle battle = battleList.Find((b) => b.Player1.ConnectionId == Context.ConnectionId || b.Player2.ConnectionId == Context.ConnectionId);
            battleList.Remove(battle);
            Clients.Caller.BattleLeft();
            if (battle.Player1.ConnectionId != Context.ConnectionId)
            {
                lobby.AddUser(battle.Player1.ConnectionId, battle.Player1.ClassName);
                Clients.Client(battle.Player1.ConnectionId).BackToLoading();
            }
            else
            {
                lobby.AddUser(battle.Player2.ConnectionId, battle.Player2.ClassName);
                Clients.Client(battle.Player2.ConnectionId).BackToLoading();
            }

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            Battle battle = battleList.Find((b) => b.Player1.ConnectionId == Context.ConnectionId || b.Player2.ConnectionId == Context.ConnectionId);
            if (battle == null)
            {
                lobby.RemoveUser(Context.ConnectionId);
                return base.OnDisconnected(stopCalled);
            }
            battleList.Remove(battle);
            if (battle.Player1.ConnectionId != Context.ConnectionId)
            {
                lobby.AddUser(battle.Player1.ConnectionId, battle.Player1.ClassName);
                Clients.Client(battle.Player1.ConnectionId).BackToLoading();
            }
            else
            {
                lobby.AddUser(battle.Player2.ConnectionId, battle.Player2.ClassName);
                Clients.Client(battle.Player2.ConnectionId).BackToLoading();
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}
