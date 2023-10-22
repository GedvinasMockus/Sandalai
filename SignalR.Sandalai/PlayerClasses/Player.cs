using Microsoft.AspNet.SignalR;
using SignalR.Sandalai.InfoStructs;
using SignalR.Sandalai.Objects;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace SignalR.Sandalai.PlayerClasses
{
    public abstract class Player : Prototype, IObserver
    {
        public string ConnectionId { get; private set; }
        public string ClassName { get; protected set; }
        public Attributes BaseAttributes { get; protected set; }
        public Vector2 Position { get; set; }
        public Player(string connId)
        {
            ConnectionId = connId;
        }

        public override Prototype Clone()
        {
            Player player = (Player)this.MemberwiseClone();
            player.ConnectionId = String.Copy(ConnectionId);
            player.ClassName = String.Copy(ClassName);
            player.Position = new Vector2(this.Position.X, this.Position.Y);

            return player;
        }
        public PlayerInfo GetInfo()
        {
            return new PlayerInfo(Position, BaseAttributes, ClassName);
        }

        public void Update(Player sender, string ability, BattleInfo info)
        {
            var clients = GlobalHost.ConnectionManager.GetHubContext<MainHub>().Clients;
            if(sender.ConnectionId == ConnectionId) 
                clients.Client(ConnectionId).BattleInfoUpdated(info);
            else 
                clients.Client(ConnectionId).AbilityUsed(ability, info);
        }
        public abstract void JumpLeft();

        public abstract void JumpRight();

        public abstract void RunLeft();

        public abstract void RunRight();

        public abstract void MeleeAttackLeft(Player target);

        public abstract void MeleeAttackRight(Player target);

        public abstract void RangedAttackLeft(Player target);

        public abstract void RangedAttackRight(Player targer);

    }

}
