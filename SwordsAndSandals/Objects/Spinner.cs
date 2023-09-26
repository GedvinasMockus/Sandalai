using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace SwordsAndSandals.Objects
{
    public class Spinner : Component
    {
        private Texture2D texture;
        private Vector2 position;
        private float scale;
        private float angularVelocity;
        private Color color;
        private float rotation;
        public Spinner(Texture2D texture, Color color, Vector2 position, float scale, float angularVelocity)
        {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            this.angularVelocity = angularVelocity;
            this.color = color;
            this.rotation = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            rotation += (angularVelocity * elapsed);
            if (rotation >= Math.PI)
            {
                rotation -= (float)Math.PI;
            }
        }

    }
}
