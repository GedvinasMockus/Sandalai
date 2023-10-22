using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Sandalai.PlayerClasses
{
    public class KunoichiPlayer : Player
    {
        public KunoichiPlayer(string connId) : base(connId)
        {
            ClassName = "Kunoichi";
            BaseAttributes = new Attributes()
            {
                MaxHealth = 1000,
                CurrHealth = 1000,
                BaseDistance = 35f/192f,
                ArmourRating = 10
            };
        }

        public override void JumpLeft()
        {
            Position = new Vector2(Position.X - BaseAttributes.BaseDistance * 1.2f, Position.Y);
        }

        public override void JumpRight()
        {
            Position = new Vector2(Position.X + BaseAttributes.BaseDistance * 1.2f, Position.Y);
        }
        public override void RunLeft()
        {
            Position = new Vector2(Position.X - BaseAttributes.BaseDistance, Position.Y);
        }

        public override void RunRight()
        {
            Position = new Vector2(Position.X + BaseAttributes.BaseDistance, Position.Y);
        }

        public override void MeleeAttackLeft(Player target)
        {
            return;
        }

        public override void MeleeAttackRight(Player target)
        {
            return;
        }

        public override void RangedAttackLeft(Player target)
        {
            return;
        }

        public override void RangedAttackRight(Player targer)
        {
            return;
        }

    }
}
