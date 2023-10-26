using Microsoft.AspNet.SignalR;

using SignalR.Sandalai.InfoStructs;
using SignalR.Sandalai.Objects;
using SignalR.Sandalai.PlayerClasses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Sandalai
{
    public class MainHub : Hub, ISubject
    {
        private readonly Lobby lobby;
        public static List<Battle> battleList = new List<Battle>();
        public static List<Spectator> spectatorList = new List<Spectator>();
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
            Player p1;
            Player p2;
            lobby.TakePair(Context.ConnectionId, out p1, out p2);
            if (p1 != null && p2 != null)
            {
                Battle battle = new Battle(p1, p2);
                battle.BattleStart();
                lock (battleList)
                {
                    battleList.Add(battle);
                }
                Clients.Client(p1.ConnectionId).OpponentFound(battle.GetInfo(p1));
                Clients.Client(p2.ConnectionId).OpponentFound(battle.GetInfo(p2));
                Notify();
            }
        }

        public void AbilityUsed(string name)
        {
            Battle battle = battleList.Find((b) => b.Player1.ConnectionId == Context.ConnectionId || b.Player2.ConnectionId == Context.ConnectionId);

            battle.AbilityUsed(name, Context.ConnectionId);
        }

        public void LeaveBattle()
        {
            Battle battle;
            lock (battleList)
            {
                battle = battleList.FirstOrDefault((b) => b.Player1.ConnectionId == Context.ConnectionId || b.Player2.ConnectionId == Context.ConnectionId);
                if (battle != null)
                {
                    AddAllSpectators(battle.DetachAll());
                }
                battleList.Remove(battle);
            }
            Notify();
            if (battle != null)
            {
                battle.BattleStop();
                Clients.Caller.BattleLeft();

                Player other = battle.FindAnyOtherPlayerById(Context.ConnectionId);
                lobby.AddUser(other.ConnectionId, other.ClassName);
                Clients.Client(other.ConnectionId).BackToLoading();
            }

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            Battle battle;
            lock (battleList)
            {
                battle = battleList.FirstOrDefault((b) => b.Player1.ConnectionId == Context.ConnectionId || b.Player2.ConnectionId == Context.ConnectionId);
                if (battle != null)
                {
                    AddAllSpectators(battle.DetachAll());
                }
                battleList.Remove(battle);
            }
            Notify();
            if (battle == null) lobby.RemoveUser(Context.ConnectionId);
            else
            {
                battle.BattleStop();

                Player other = battle.FindAnyOtherPlayerById(Context.ConnectionId);
                lobby.AddUser(other.ConnectionId, other.ClassName);
                Clients.Client(other.ConnectionId).BackToLoading();
            }
            return base.OnDisconnected(stopCalled);
        }

        public void AddSpectator()
        {
            lock (spectatorList)
            {
                Spectator spe = new Spectator(Context.ConnectionId);
                Console.WriteLine($"Added spectator {spe.ConnectionId}");
                spe.SendInfo(GetBattleData());
                spectatorList.Add(spe);
            }
        }

        public void RemoveSpectator()
        {
            lock (spectatorList)
            {
                Spectator spectator = spectatorList.FirstOrDefault((b) => b.ConnectionId == Context.ConnectionId);
                Console.WriteLine($"Removed spectator {spectator.ConnectionId}");
                spectatorList.Remove(spectator);
            }
        }
        private List<BattleInfo> GetBattleData()
        {
            List<BattleInfo> battleInfos = new List<BattleInfo>();
            lock (battleList)
            {
                foreach (Battle info in battleList)
                {
                    battleInfos.Add(info.GetInfo());
                }
            }
            return battleInfos;
        }

        public void Notify()
        {
            lock (spectatorList)
            {
                foreach (var spe in spectatorList)
                {
                    spe.SendInfo(GetBattleData());
                }
            }
        }
        public void SpectateMatch(string id1, string id2)
        {
            Spectator spectator;
            Battle battle;
            lock (spectatorList)
            {
                spectator = spectatorList.FirstOrDefault((b) => b.ConnectionId == Context.ConnectionId);
            }
            lock (battleList)
            {
                battle = battleList.FirstOrDefault((b) => b.Player1.ConnectionId == id1 && b.Player2.ConnectionId == id2);
            }
            RemoveSpectator();
            battle.AddToBattle(spectator);
            Clients.Client(spectator.ConnectionId).ShowMatch(battle.GetInfo());
        }
        public void LeaveSpectateBattle()
        {
            Battle battle;
            lock (battleList)
            {
                battle = battleList.FirstOrDefault((b) => b.SpectatorObserver.Any((a) => a.ConnectionId == Context.ConnectionId));
                battle.RemoveFromBattle(Context.ConnectionId);
                AddSpectator();
            }
        }
        public void AddAllSpectators(List<Spectator> list)
        {
            lock (spectatorList)
            {
                foreach (Spectator spectator in list)
                {
                    Console.WriteLine($"Added spectator {spectator.ConnectionId}");
                    spectator.SendInfo(GetBattleData());
                    spectatorList.Add(spectator);
                }
            }
        }
    }
}
