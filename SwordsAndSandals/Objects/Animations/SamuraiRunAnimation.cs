using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Animations
{
    public class SamuraiRunAnimation : Animation
    {
        public SamuraiRunAnimation(ContentManager content, float duration, SpriteEffects flip) : base(duration, flip) 
        {
            Scale = 3.0f;
            texture = content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Run");
            totalFrames = 8;
            frameWidth = texture.Width / totalFrames;
            frameHeight = texture.Height;
        }
    }
}
