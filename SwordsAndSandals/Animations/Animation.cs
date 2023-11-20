using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Animations
{
    public abstract class Animation
    {
        public int FrameWidth { get; protected set; }
        public int FrameHeight { get; protected set; }
        public int CollisionWidth { get; protected set; }
        public int CollisionHeight { get; protected set; }
        public Vector2 CollisionRectPoint { get; protected set; }

        protected SpriteEffects flip;
        public SpriteEffects Flip
        {
            get
            {
                return flip;
            }
            set
            {
                if (flipChangeable) flip = value;
            }
        }
        public float Duration
        {
            get
            {
                return frameDuration * totalFrames;
            }
        }
        public float Scale { get; set; }
        public float Rotation { get; set; }

        protected bool flipChangeable;
        protected int totalFrames;
        protected int currentFrame;
        protected float frameDuration;
        protected float timer;
        protected Texture2D texture;

        public Animation(float duration, SpriteEffects flip, bool flipChangeable)
        {
            frameDuration = duration;
            timer = 0.0f;
            currentFrame = 0;
            this.flip = flip;
            this.flipChangeable = flipChangeable;
        }

        public virtual void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (timer >= frameDuration)
            {
                currentFrame = (currentFrame + 1) % totalFrames;
                timer %= frameDuration;
            }
        }

        public virtual void Draw(SpriteBatch batch, Vector2 position, Vector2 origin)
        {
            Rectangle frameRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            batch.Draw(texture, position, frameRect, Color.White, Rotation, origin, Scale, Flip, 1);
        }

        public void Reset()
        {
            currentFrame = 0;
            timer = 0.0f;
        }
    }
}
