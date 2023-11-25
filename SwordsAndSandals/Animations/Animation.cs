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
    public class Animation
    {
        public AnimationShared Shared { get; private set; }
        public int FrameWidth
        {
            get
            {
                return Shared.FrameWidth;
            }
        }
        public int FrameHeight
        {
            get
            {
                return Shared.FrameHeight;
            }
        }
        public int CollisionWidth
        {
            get
            {
                return Shared.CollisionWidth;
            }
        }
        public int CollisionHeight
        {
            get
            {
                return Shared.CollisionHeight;
            }
        }
        public Vector2 CollisionRectPoint
        {
            get
            {
                return Shared.CollisionRectPoint;
            }
        }
        public bool FlipChangeable
        {
            get
            {
                return Shared.FlipChangeable;
            }
        }
        public int TotalFrames
        {
            get
            {
                return Shared.TotalFrames;
            }
        }
        public Texture2D Texture
        {
            get
            {
                return Shared.Texture;
            }
        }

        protected SpriteEffects flip;
        public SpriteEffects Flip
        {
            get
            {
                return flip;
            }
            set
            {
                if (FlipChangeable) flip = value;
            }
        }
        public float Duration
        {
            get
            {
                return frameDuration * TotalFrames;
            }
        }
        public float Scale { get; set; }
        public float Rotation { get; set; }

        protected int currentFrame;
        protected float frameDuration;
        protected float timer;

        public Animation(AnimationShared shared, float duration, SpriteEffects flip)
        {
            Shared = shared;

            frameDuration = duration;
            currentFrame = 0;
            timer = 0f;
            this.flip = flip;
        }

        public virtual void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (timer >= frameDuration)
            {
                currentFrame = (currentFrame + 1) % TotalFrames;
                timer %= frameDuration;
            }
        }

        public virtual void Draw(SpriteBatch batch, Vector2 position, Vector2 origin)
        {
            Rectangle frameRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            batch.Draw(Texture, position, frameRect, Color.White, Rotation, origin, Scale, Flip, 1);
        }

        public void Reset()
        {
            currentFrame = 0;
            timer = 0.0f;
        }
    }
}
