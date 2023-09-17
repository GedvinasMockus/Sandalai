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
        private int centerY;

        public Player(Texture2D texture, Vector2 position, float scale, Dictionary<string, Ability> availableAbilities)
        {
            this.texture = texture;
            this.position = position;
            frameWidth = texture.Width;
            frameHeight = texture.Height;
            this.scale = scale;
            this.abilities = availableAbilities;
        }

        public Player(Texture2D texture, Vector2 position, float scale, int width, int height, Dictionary<string, Ability> availableAbilities, int centerY)
        {
            this.texture = texture;
            this.position = position;
            frameWidth = width;
            frameHeight = height;
            this.scale = scale;
            this.abilities = availableAbilities;
            this.centerY = centerY;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, new Vector2(position.X, position.Y - scale * frameHeight / 2), new Rectangle(0, 0, frameWidth, frameHeight), Color.White, 0.0f, new Vector2(frameWidth / 2, frameHeight / 2), scale, SpriteEffects.None, 1);

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
            foreach (var v in abilities.Values)
            {
                v.Update(gameTime);
            }
        }
    }
}
