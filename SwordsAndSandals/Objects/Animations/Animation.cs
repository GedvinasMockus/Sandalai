using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Animations
{
    public abstract class Animation
    {
        public int frameWidth { get; protected set; }
        public int frameHeight { get; protected set; }
        public SpriteEffects Flip { get; protected set; }
        public float Duration
        {
            get
            {
                return frameDuration * totalFrames;
            }
        }
        public float Scale { get; set; }
        public float Rotation { get; set; }

        protected int totalFrames;
        protected int currentFrame;
        protected float frameDuration;
        protected float timer;
        protected Texture2D texture;

        public Animation(float duration, SpriteEffects flip)
        {
            frameDuration = duration;
            timer = 0.0f;
            currentFrame = 0;
            Flip = flip;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (timer >= frameDuration)
            {
                currentFrame = (currentFrame + 1) % totalFrames;
                timer %= frameDuration;
            }
        }

        public void Draw(SpriteBatch batch, Vector2 position, Vector2 origin)
        {
            Rectangle frameRect = new Rectangle(currentFrame * frameHeight, 0, frameWidth, frameHeight);
            batch.Draw(texture, position, frameRect, Color.White, Rotation, origin, Scale, Flip, 1);
        }

        public void Reset()
        {
            currentFrame = 0;
            timer = 0.0f;
        }

    }
}
