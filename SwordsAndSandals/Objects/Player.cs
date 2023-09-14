using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public Player(Texture2D texture, Vector2 position, float scale, int width, int height)
        {
            this.texture = texture;
            this.position = position;
            frameWidth = width;
            frameHeight = height;
            this.scale = scale;
        }

        public void Draw(SpriteBatch batch)
        {
            //Matrix m = Matrix.CreateScale(scale) * Matrix.CreateTranslation((float)(-frameWidth / 2), (float)(-frameHeight / 2), 0) * Matrix.CreateTranslation(position.X, position.Y, 0);
            //batch.Begin(transformMatrix: m);
            //batch.Draw(texture, new Vector2(0, 0), Color.White);
            //batch.End();
            batch.Draw(texture, new Vector2(position.X, position.Y), new Rectangle(0, 0, frameWidth, frameHeight), Color.White, 0.0f, new Vector2(frameWidth / 2, frameHeight), scale, SpriteEffects.None, 1);
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
