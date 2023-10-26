using Microsoft.AspNet.SignalR;

using SignalR.Sandalai.Objects;
using SignalR.Sandalai.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Sandalai
{
    public class MainHub : Hub
    {
        private readonly Lobby lobby;
        public static List<Battle> battleList = new List<Battle>();
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
        public void GetBattleList()
        {
            List<List<string>> list = new List<List<string>>();
            foreach (Battle battle in battleList)
            {
                list.Add(battle.GetBattleInfo());
            }
            Clients.Client(Context.ConnectionId).BattleListInfo(list);
        }
        public void FindOpponent()
        {
            Player p1;
            Player p2;
            lobby.TakePair(Context.ConnectionId, out p1, out p2);
            if(p1 != null && p2 != null)
            {
                Battle battle = new Battle();
                battle.Attach(p1);
                battle.Attach(p2);
                battle.BattleStart();

                lock (battleList)
                {
                    battleList.Add(battle);
                }

                Clients.Client(p1.ConnectionId).OpponentFound(battle.GetInfo(p1));
                Clients.Client(p2.ConnectionId).OpponentFound(battle.GetInfo(p2));
            }
        }

        public void AbilityUsed(string name)
        {
            Battle battle = battleList.Find((b) => b.GetPlayer(0).ConnectionId == Context.ConnectionId || b.GetPlayer(1).ConnectionId == Context.ConnectionId);

            battle.AbilityUsed(name, Context.ConnectionId);
        }

        public void LeaveBattle()
        {
            Battle battle;
            lock (battleList)
            {
                battle = battleList.FirstOrDefault((b) => b.GetPlayer(0).ConnectionId == Context.ConnectionId || b.GetPlayer(1).ConnectionId == Context.ConnectionId);
                battleList.Remove(battle);
            }
            if (battle != null)
            {
                battle.BattleStop();
                Clients.Caller.BattleLeft();

                Player other = battle.FindAnyOtherPlayerById(Context.ConnectionId);
                lobby.AddUser(other.ConnectionId, other.ClassName);
                Clients.Client(other.ConnectionId).BackToLoading();

                battle.Detach(battle.GetPlayer(1));
                battle.Detach(battle.GetPlayer(0));
            }

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            Battle battle;
            lock (battleList)
            {
                battle = battleList.FirstOrDefault((b) => b.GetPlayer(0).ConnectionId == Context.ConnectionId || b.GetPlayer(1).ConnectionId == Context.ConnectionId);
                battleList.Remove(battle);
            }
            if (battle == null) lobby.RemoveUser(Context.ConnectionId);
            else
            {
                battle.BattleStop();

                Player other = battle.FindAnyOtherPlayerById(Context.ConnectionId);
                lobby.AddUser(other.ConnectionId, other.ClassName);
                Clients.Client(other.ConnectionId).BackToLoading();

                battle.Detach(battle.GetPlayer(1));
                battle.Detach(battle.GetPlayer(0));
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}
