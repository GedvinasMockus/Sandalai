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
                Origin = new Vector2(anim.frameWidth / 2, anim.frameHeight / 2);
            }
        }
        public virtual Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(Position.X - anim.frameWidth / 2 * anim.Scale), (int)(Position.Y - anim.frameHeight / 2 * anim.Scale), (int)(anim.frameWidth * anim.Scale), (int)(anim.frameHeight * anim.Scale));
            }
        }

        public abstract void Draw(SpriteBatch batch);
        public abstract void Update(GameTime gameTime);
    }
}
