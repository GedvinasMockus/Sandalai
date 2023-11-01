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
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(Position.X - texture.Width / 2 * Scale), (int)(Position.Y - texture.Height / 2 * Scale), (int)(texture.Width * Scale), (int)(texture.Height * Scale));
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
