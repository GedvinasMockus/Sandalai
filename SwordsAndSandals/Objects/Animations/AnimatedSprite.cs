using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Animations
{
    public abstract class AnimatedSprite
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Origin { get; set; }

        private Animation anim;
        public Animation animation 
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
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(Position.X - anim.frameWidth/2 * anim.Scale), (int)(Position.Y - anim.frameHeight/2 * anim.Scale), (int)(anim.frameWidth * anim.Scale), (int)(anim.frameHeight * anim.Scale));
            }
        }
        public AnimatedSprite(Animation animation, Vector2 position)
        {
            this.animation = animation;
            Position = position;
            Velocity = Vector2.Zero;
        }

        public AnimatedSprite()
        {

        }

        public abstract void Draw(SpriteBatch batch);
        public abstract void Update(GameTime gameTime, List<Sprite> sprites);
    }
}
