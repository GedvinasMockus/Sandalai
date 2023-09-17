using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwordsAndSandals.Objects
{
    public class Player
    {
        public Vector2 position { get; set; }

        private Texture2D texture;
        private int frameWidth;
        private int frameHeight;
        private float scale;
        private Dictionary<string, Ability> abilities;
        //private Texture2D lineTexture;
        private int centerY;
        private int totalFrames;
        private int currentFrame = 0;
        private float animationSpeed = 0.1f;
        private float animationTimer = 0f;

        public Player(Texture2D texture, Vector2 position, float scale, Dictionary<string, Ability> availableAbilities)
        {
            this.texture = texture;
            this.position = position;
            frameWidth = texture.Width;
            frameHeight = texture.Height;
            this.scale = scale;
            this.abilities = availableAbilities;
        }

        public Player(Texture2D texture, Vector2 position, float scale, Dictionary<string, Ability> availableAbilities, int centerY)
        {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            this.abilities = availableAbilities;
            this.centerY = centerY;
            this.totalFrames = texture.Width / texture.Height;
            frameWidth = texture.Width / this.totalFrames;
            frameHeight = texture.Height;
        }

        public void Draw(SpriteBatch batch)
        {
            //lineTexture = new Texture2D(batch.GraphicsDevice, 1, 1);
            //lineTexture.SetData(new[] { Color.White });
            //Vector2 center = new Vector2(position.X, position.Y - (frameHeight - centerY) * scale);
            Rectangle sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            batch.Draw(texture, new Vector2(position.X, position.Y - scale * frameHeight / 2), sourceRectangle, Color.White, 0.0f, new Vector2(frameWidth / 2, frameHeight / 2), scale, SpriteEffects.None, 1);
            //Vector2 bottomPoint = center + new Vector2(0, scale * (frameHeight - centerY));
            //DrawLine(batch, center, bottomPoint, Color.Red);

            //Vector2 topLeft = new Vector2(center.X - (frameWidth - centerY) * scale, center.Y - scale * (frameHeight - centerY));
            //Vector2 topRight = new Vector2(center.X + (frameWidth - centerY) * scale, center.Y - scale * (frameHeight - centerY));
            //Vector2 bottomLeft = new Vector2(center.X - (frameWidth - centerY) * scale, center.Y + scale * (frameHeight - centerY));
            //Vector2 bottomRight = new Vector2(center.X + (frameWidth - centerY) * scale, center.Y + scale * (frameHeight - centerY));
            //DrawLine(batch, topLeft, topRight, Color.Red);          
            //DrawLine(batch, topRight, bottomRight, Color.Green);    
            //DrawLine(batch, bottomRight, bottomLeft, Color.Blue);  
            //DrawLine(batch, bottomLeft, topLeft, Color.Yellow);

            int numIcons = abilities.Count;
            float radius = scale * (frameWidth - centerY) * (float)Math.Sqrt(2);
            float angleIncrement = MathHelper.TwoPi / numIcons;
            int index = 0;
            foreach (var v in abilities.Values)
            {
                float angle = index * angleIncrement;
                float xOffset = -(float)Math.Sin(angle) * (radius);
                float yOffset = -(float)Math.Cos(angle) * (radius);
                v.position = new Vector2(position.X + xOffset, position.Y + yOffset - (frameHeight - centerY) * scale);
                v.Draw(batch);
                index++;
            }
        }

        public void Update(GameTime gameTime)
        {
            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer >= animationSpeed)
            {
                currentFrame = (currentFrame + 1) % totalFrames;
                animationTimer = 0f;
            }
            foreach (var v in abilities.Values)
            {
                v.Update(gameTime);
            }
        }
        //private void DrawLine(SpriteBatch batch, Vector2 start, Vector2 end, Color color)
        //{
        //    float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
        //    float length = Vector2.Distance(start, end);

        //    batch.Draw(lineTexture, start, null, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
        //}
    }
}
