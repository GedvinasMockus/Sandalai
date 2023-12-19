using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Visitor;
using System;

namespace SwordsAndSandals.UI
{
    public class Spinner : Component, IComponent
    {
        private Texture2D texture;
        private Vector2 position;
        private float scale;
        private float angularVelocity;
        private Color color;
        private float rotation;
        public GameTime GameTime { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public Spinner(Texture2D texture, Color color, Vector2 position, float scale, float angularVelocity)
        {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            this.angularVelocity = angularVelocity;
            this.color = color;
            rotation = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.SpriteBatch.Draw(texture, position, null, color, rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)this.GameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            rotation += angularVelocity * elapsed;
            if (rotation >= Math.PI)
            {
                rotation -= (float)Math.PI;
            }
        }

        public void setSprite(SpriteBatch spriteBatch)
        {
            this.SpriteBatch = spriteBatch;
        }

        public void setTime(GameTime gameTime)
        {
            this.GameTime = gameTime;
        }

        public void Accept(IVisitor Visitor)
        {
            Visitor.VisitSpinner(this);
        }
    }
}
