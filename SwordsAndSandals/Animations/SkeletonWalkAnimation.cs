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
    public class SkeletonWalkAnimation : Animation
    {
        public SkeletonWalkAnimation(ContentManager content, float duration, SpriteEffects flip, bool flipChangeable) : base(duration, flip, flipChangeable)
        {
            Scale = 3.0f;
            texture = content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Walk");
            totalFrames = 8;
            frameWidth = texture.Width / totalFrames;
            frameHeight = texture.Height;
        }
    }
}
