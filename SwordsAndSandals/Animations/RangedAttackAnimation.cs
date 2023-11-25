using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Animations
{
    public class RangedAttackAnimation : Animation
    {
        public float SpawnDuration
        {
            get
            {
                return ProjectileSpawnFrame * frameDuration;
            }
        }
        public Vector2 RelativePosition
        {
            get
            {
                return (Shared as RangedAttackAnimationShared).RelativePosition;
            }
        }
        public int ProjectileWidth
        {
            get
            {
                return (Shared as RangedAttackAnimationShared).ProjectileWidth;
            }
        }
        public int ProjectileSpawnFrame
        {
            get
            {
                return (Shared as RangedAttackAnimationShared).ProjectileSpawnFrame;
            }
        }
        public RangedAttackAnimation(RangedAttackAnimationShared shared, float duration, SpriteEffects flip) : base(shared, duration, flip)
        {

        }
    }
}
