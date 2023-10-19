using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Animations
{
    public class RangedAttackAnimation : Animation
    {
        public float spawnDuration
        {
            get
            {
                return projectileSpawnFrame * frameDuration;
            }
        }
        public Vector2 relativePosition { get; protected set; }

        protected int projectileSpawnFrame;
        public RangedAttackAnimation(float duration, SpriteEffects flip, bool flipChangeable) : base(duration, flip, flipChangeable)
        {

        }
    }
}
