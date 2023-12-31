﻿using Microsoft.AspNet.SignalR;

using SignalR.Sandalai.InfoStructs;

using System;
using System.Numerics;

namespace SignalR.Sandalai.PlayerClasses
{
    public abstract class Player : IPrototype
    {
        public string ConnectionId { get; private set; }
        public string ClassName { get; protected set; }
        public string Name { get; private set; }
        public Attributes BaseAttributes { get; protected set; }
        public Vector2 Position { get; set; }
        public Player(string connId, string name)
        {
            ConnectionId = connId;
            Name = name;
        }

        public IPrototype Clone()
        {
            Player player = (Player)this.MemberwiseClone();
            player.ConnectionId = String.Copy(ConnectionId);
            player.ClassName = String.Copy(ClassName);
            player.Name = String.Copy(Name);
            player.Position = new Vector2(this.Position.X, this.Position.Y);
            return player;
        }
        public PlayerInfo GetInfo()
        {
            return new PlayerInfo(Position, BaseAttributes, ClassName, Name, ConnectionId);
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
