using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace SwordsAndSandals.Objects
{
    public class Button
    {
        public event EventHandler onClick;
        public float Layer { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin
        {
            get
            {
                return new Vector2(texture.Width / 2, texture.Height / 2);
            }
        }
        public Color PenColor { get; set; }
        public Texture2D texture { get; private set; }
        public float Scale { get; private set; }

        private string text;
        private MouseState previous;
        private MouseState current;
        private SpriteFont font;
        private bool isHovering;
        private SpriteEffects flip;

        public Button(Texture2D texture, float scale)
        {
            this.texture = texture;
            Position = new Vector2(-1, -1);
            isHovering = false;
            this.Scale = scale;
            PenColor = Color.Black;
        }

        public Button(Texture2D texture, float scale, SpriteEffects flip)
        {
            this.texture = texture;
            Position = new Vector2(-1, -1);
            isHovering = false;
            this.flip = flip;
            this.Scale = scale;
            PenColor = Color.Black;
        }

        public Button(Texture2D texture, SpriteFont font, string text, float scale, SpriteEffects flip)
        {
            this.texture = texture;
            Position = new Vector2(-1, -1);
            this.font = font;
            this.text = text;
            isHovering = false;
            this.flip = flip;
            this.Scale = scale;
            PenColor = Color.Black;
        }

        public void Draw(SpriteBatch batch)
        {
            Color color = Color.White;
            if(isHovering)
            {
                color = Color.Gray;
            }
            batch.Draw(texture, Position, null, color, 0.0f, Origin, Scale, flip, Layer);
            if(!string.IsNullOrEmpty(text))
            {
                int x = (int)(Position.X - font.MeasureString(text).X / 2);
                int y = (int)(Position.Y - font.MeasureString(text).Y / 2);
                batch.DrawString(font, text, new Vector2(x,y), PenColor, 0.0f, new Vector2(0,0), 1.0f, SpriteEffects.None, Layer + 0.01f);
            }
        }

        public void Update(GameTime gameTime)
        {
            previous = current;
            current = Mouse.GetState();

            Rectangle mouseRect = new Rectangle(current.X,current.Y,1,1);

            isHovering = false;

            if (mouseRect.Intersects(new Rectangle((int)(Position.X - Scale * texture.Width/2), (int)(Position.Y - Scale * texture.Height/2), (int)(texture.Width * Scale), (int)(texture.Height * Scale))))
            {
                isHovering = true;
                if(current.LeftButton == ButtonState.Released && previous.LeftButton == ButtonState.Pressed)
                {
                    onClick?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
