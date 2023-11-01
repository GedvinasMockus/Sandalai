using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Animations
{
    public class SkeletonRangedAttackAnimation : RangedAttackAnimation
    {
        public SkeletonRangedAttackAnimation(ContentManager content, float duration, SpriteEffects flip, bool flipChangeable) : base(duration, flip, flipChangeable)
        {
            texture = content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Shot_1");
            Scale = 3.0f;
            totalFrames = 15;
            frameWidth = texture.Width / totalFrames;
            frameHeight = texture.Height;
            projectileSpawnFrame = 12;
            relativePosition = new Vector2(71, 76);
        }
    }
}
