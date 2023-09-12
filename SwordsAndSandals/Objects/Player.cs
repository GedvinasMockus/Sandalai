using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects
{
    public class Player : IDrawable
    {
        public Texture2D texture { get; }
        public Vector2 position { get; set; }

        private int frameWidth;
        private int frameHeight;
        private float scale;


        public Player(Texture2D texture, Vector2 position, float scale)
        {
            this.texture = texture;
            this.position = position;
            frameWidth = texture.Width;
            frameHeight = texture.Height;
            this.scale = scale;
        }

        public Player(Texture2D texture, Vector2 position, float scale ,int width, int height)
        {
            this.texture = texture;
            this.position = position;
            frameWidth = width;
            frameHeight = height;
            this.scale = scale;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, new Vector2(position.X, position.Y - frameHeight / 2), new Rectangle(0, 0, frameWidth, frameHeight), Color.White, 0.0f, new Vector2(frameWidth/2,frameHeight/2), scale, SpriteEffects.None, 1);
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
