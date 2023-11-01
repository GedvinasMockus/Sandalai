using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Sandalai.PlayerClasses
{
    public class SkeletonPlayer : Player
    {
        public SkeletonPlayer(string connId, string name) : base(connId, name)
        {
            ClassName = "Skeleton";
            BaseAttributes = new Attributes()
            {
                MaxHealth = 1500,
                CurrHealth = 1500,
                BaseDistance = 25f/192f,
                ArmourRating = 15
            };
        }
        public override void JumpLeft()
        {
            return;
        }

        public override void JumpRight()
        {
            return;
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
