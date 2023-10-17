using System.Numerics;
using System;

namespace SignalR.Sandalai.Objects
{
    public class Player : Prototype
    {
        public string ConnectionId { get; private set; }

        public string ClassName { get; private set; }
        public Vector2 Position { get; set; }
        public FlipEnum Flip { get; set; }
        public Player(string connId, string className)
        {
            ConnectionId = connId;
            ClassName = className;
        }

        public override Prototype Clone()
        {
            Player player = (Player)this.MemberwiseClone();
            player.ConnectionId = String.Copy(ConnectionId);
            player.ClassName = String.Copy(ClassName);
            player.Position = new Vector2(this.Position.X, this.Position.Y);
            player.Flip = this.Flip;

            return player;
        }
        public PlayerInfo GetInfo()
        {
            return new PlayerInfo(Position, (int)Flip, ClassName);
        }
    }

}
