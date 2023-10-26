using SignalR.Sandalai.InfoStructs;
using SignalR.Sandalai.PlayerClasses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SignalR.Sandalai.Objects
{
    public class Battle
    {
        private const float Pos1x = 1f / 6;
        private const float Pos2x = 5f / 6;
        private const float PosGround = 11f / 17;
        private List<Player> observers = new List<Player>();

        public void BattleStart()
        {
            observers[0].Position = new Vector2(Pos1x, PosGround);
            observers[1].Position = new Vector2(Pos2x, PosGround);
        }

        public void BattleStop()
        {
            observers[0].Position = new Vector2(-1, -1);
            observers[1].Position = new Vector2(-1, -1);
        }

        public BattleInfo GetInfo(Player current)
        {
            Player other = FindAnyOtherPlayerById(current.ConnectionId);
            return new BattleInfo(current.GetInfo(), other.GetInfo());
        }

        public void Attach(Player observer)
        {
            observers.Add(observer);
        }

        public void Detach(Player observer)
        {
            observers.Remove(observer);
        }

        public void Notify(Player sender, string ability)
        {
            foreach (var observer in observers)
            {
                observer.Update(sender, ability, GetInfo(observer));
            }
        }

        public void AbilityUsed(string ability, string id)
        {
            Player sender = FindPlayerById(id);
            Player other = FindAnyOtherPlayerById(id);
            Act(ability, sender, other);
            Notify(sender, ability);
        }

        public Player GetPlayer(int index)
        {
            return observers[index];
        }

        public Player FindPlayerById(string id)
        {
            foreach(var o in observers)
            {
                if (o.ConnectionId.Equals(id)) return o;
            }
            return null;
        }

        public Player FindAnyOtherPlayerById(string id)
        {
            foreach(var o in observers)
            {
                if (!o.ConnectionId.Equals(id)) return o;
            }
            return null;
        }

        private void Act(string ability, Player current, Player other)
        {
            switch(ability)
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
    }
}
