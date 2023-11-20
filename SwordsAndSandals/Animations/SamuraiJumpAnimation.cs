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
    public class SamuraiJumpAnimation : Animation
    {
        public SamuraiJumpAnimation(ContentManager content, float duration, SpriteEffects flip, bool flipChangeable) : base(duration, flip, flipChangeable)
        {
            Scale = 3.0f;
            texture = content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Jump");
            totalFrames = 7;
            FrameWidth = texture.Width / totalFrames;
            FrameHeight = texture.Height;
            CollisionWidth = 16;
            CollisionHeight = 68;
            CollisionRectPoint = new Vector2(51, 60);
        }
    }
}
