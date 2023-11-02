using Microsoft.AspNet.SignalR;

using SignalR.Sandalai.InfoStructs;
using SignalR.Sandalai.PlayerClasses;

using System;
using System.Collections.Generic;
using System.Numerics;

namespace SignalR.Sandalai.Objects
{
    public class Battle : IBattleSubject
    {
        private const float Pos1x = 1f / 6;
        private const float Pos2x = 5f / 6;
        private const float PosGround = 11f / 17;
        public List<IBattleSpectatorObserver> SpectatorObserver { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public string BattleStarted { get; private set; }
        public Battle(Player Player1, Player Player2)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;
            SpectatorObserver = new List<IBattleSpectatorObserver>();
        }


        public void BattleStart()
        {
            Player1.Position = new Vector2(Pos1x, PosGround);
            Player2.Position = new Vector2(Pos2x, PosGround);
            BattleStarted = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public void BattleStop()
        {
            Player1.Position = new Vector2(-1, -1);
            Player2.Position = new Vector2(-1, -1);
        }

        public BattleInfo GetInfo(Player current)
        {
            Player other = FindAnyOtherPlayerById(current.ConnectionId);
            return new BattleInfo(current.GetInfo(), other.GetInfo());
        }
        public BattleInfo GetInfo()
        {
            return new BattleInfo(Player1.GetInfo(), Player2.GetInfo(), BattleStarted);
        }

        public void AbilityUsed(string ability, string id)
        {
            Player sender = FindPlayerById(id);
            Player other = FindAnyOtherPlayerById(id);
            Act(ability, sender, other);
            var clients = GlobalHost.ConnectionManager.GetHubContext<MainHub>().Clients;
            clients.Client(sender.ConnectionId).BattleInfoUpdated(GetInfo(sender));
            clients.Client(other.ConnectionId).AbilityUsed(ability, GetInfo(other));
            int playerNum = Player1.ConnectionId.Equals(id) ? 0 : 1;
            NotifySpectators(playerNum, ability, GetInfo(sender));
        }


        public Player FindPlayerById(string id)
        {
            return Player1.ConnectionId.Equals(id) ? Player1 : Player2;
        }

        public Player FindAnyOtherPlayerById(string id)
        {
            return Player1.ConnectionId.Equals(id) ? Player2 : Player1;
        }

        private void Act(string ability, Player current, Player other)
        {
            switch (ability)
            {
                case "Run_left":
                    current.RunLeft();
                    break;
                case "Run_right":
                    current.RunRight();
                    break;
                case "Jump_left":
                    current.JumpLeft();
                    break;
                case "Jump_right":
                    current.JumpRight();
                    break;
                case "Melee_attack_left":
                    current.MeleeAttackLeft(other);
                    break;
                case "Melee_attack_right":
                    current.MeleeAttackRight(other);
                    break;
                case "Ranged_attack_left":
                    current.MeleeAttackLeft(other);
                    break;
                case "Ranged_attack_right":
                    current.MeleeAttackRight(other);
                    break;
            }
        }

        public void AddToBattle(IBattleSpectatorObserver spectator)
        {
            lock (SpectatorObserver)
            {
                SpectatorObserver.Add(spectator);
            }
        }

        public void RemoveFromBattle(IBattleSpectatorObserver spectator)
        {
            lock (SpectatorObserver)
            {
                SpectatorObserver.Remove(spectator);
            }
        }

        public void NotifySpectators(int playerNum, string ability, BattleInfo info)
        {
            foreach (var spectator in SpectatorObserver)
            {
                spectator.SendBattleInfo(playerNum, ability, info);
            }
        }

        public List<IBattleSpectatorObserver> DetachAll()
        {
            List<IBattleSpectatorObserver> observerCopy = new List<IBattleSpectatorObserver>(SpectatorObserver);
            lock (SpectatorObserver)
            {
                foreach (var spectator in SpectatorObserver)
                {
                    (spectator as Spectator).RemoveFromSpectate();
                }
                SpectatorObserver.Clear();
            }
            return observerCopy;
        }
    }
}
