using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Sprites;

using System.Collections.Generic;

namespace SwordsAndSandals.Animations
{
    public abstract class AnimatedSprite
    {
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Origin { get; set; }

        private Animation anim;
        public virtual Animation animation
        {
            get
            {
                return anim;
            }
            set
            {
                anim = value;
                Origin = new Vector2(anim.FrameWidth / 2, anim.FrameHeight / 2);
            }
        }
        public virtual Rectangle Rectangle
        {
            get
            {
                Vector2 startPoint = anim.Flip == SpriteEffects.FlipHorizontally ?
                    new Vector2(anim.FrameWidth - anim.CollisionRectPoint.X - anim.CollisionWidth, anim.CollisionRectPoint.Y) :
                    anim.CollisionRectPoint;
                Vector2 shift = (startPoint - Origin) * anim.Scale;
                int collisionWidth = (int)(anim.CollisionWidth * anim.Scale);
                int collisionHeight = (int)(anim.CollisionHeight * anim.Scale);
                Vector2 rectPos = Position + shift;
                return new Rectangle((int)rectPos.X, (int)rectPos.Y, collisionWidth, collisionHeight);
            }
        }

        public abstract void Draw(SpriteBatch batch);
        public abstract void Update(GameTime gameTime);
    }
}
