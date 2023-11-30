using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Sprites
{
    public abstract class Sprite
    {
        protected Texture2D texture;

        public float Rotation { get; set; }
        public float Scale { get; set; }
        public Vector2 Origin { get; set; }
        public SpriteEffects Flip { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public int CollisionWidth { get; set; }
        public int CollisionHeight { get; set; }
        public Vector2 CollisionRectPoint { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                Vector2 startPoint = Flip == SpriteEffects.FlipHorizontally ?
                    new Vector2(texture.Width - CollisionRectPoint.X - CollisionWidth, CollisionRectPoint.Y) :
                    CollisionRectPoint;
                Vector2 shift = (startPoint - Origin) * Scale;
                int scaledWidth = (int)(CollisionWidth * Scale);
                int scaledHeight = (int)(CollisionHeight * Scale);
                Vector2 rectPos = Position + shift;
                return new Rectangle((int)rectPos.X, (int)rectPos.Y, scaledWidth, scaledHeight);
            }
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            Position = position;
            Velocity = Vector2.Zero;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public abstract void Draw(SpriteBatch batch);
        public abstract void Update(GameTime gameTime);
    }
}
