using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects
{
    public class AnimatedSprite
    {
        public SpriteEffects flip { get; set; }
        public int frameWidth { get; private set; }
        public int frameHeight { get; private set; }
        public float scale { get; private set; }

        private Texture2D texture;
        private int totalFrames;
        private int currentFrame;
        private float animationSpeed;
        private float animationTimer;

        public AnimatedSprite(Texture2D texture, float scale, float animationSpeed, SpriteEffects flip)
        {
            this.texture = texture;
            totalFrames = texture.Width / texture.Height;
            frameWidth = texture.Width / totalFrames;
            frameHeight = texture.Height;
            this.scale = scale;
            currentFrame = 0;
            this.animationSpeed = animationSpeed;
            animationTimer = 0.0f;
            this.flip = flip;
        }

        public void Draw(SpriteBatch batch, Vector2 position, Vector2 origin)
        {
            Rectangle sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            batch.Draw(texture, position, sourceRectangle, Color.White, 0.0f, origin, scale, flip, 1);
        }

        public void Update(GameTime gameTime)
        {
            animationTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if(animationTimer >= animationSpeed)
            {
                currentFrame = (currentFrame + 1) % totalFrames;
                animationTimer %= animationSpeed;
            }
        }

    }
}
