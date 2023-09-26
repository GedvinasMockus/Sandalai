using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace SwordsAndSandals
{
    public class Button : Component
    {
        private MouseState _currentMouse;
        private SpriteFont _font;
        private bool _isHovering;
        private MouseState _previousMouse;
        public Texture2D _texture { get; private set; }
        private SpriteEffects flip;
        public event EventHandler Click;
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public float Scale { get; private set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - _texture.Width, (int)Position.Y - _texture.Height, (int)(_texture.Width * 2), (int)(_texture.Height * 2));
            }
        }
        public Vector2 Origin
        {
            get
            {
                return new Vector2(_texture.Width / 2, _texture.Height / 2);
            }
        }
        public float Layer { get; set; }
        public string Text { get; set; }

        public Button(Texture2D texture, float scale, SpriteEffects flip)
        {
            this._texture = texture;
            Position = new Vector2(-1, -1);
            _isHovering = false;
            this.flip = flip;
            this.Scale = scale;
            PenColour = Color.Black;
        }

        public Button(Texture2D texture, SpriteFont font, string text, float scale, SpriteEffects flip)
        {
            this._texture = texture;
            Position = new Vector2(-1, -1);
            this._font = font;
            this.Text = text;
            _isHovering = false;
            this.flip = flip;
            this.Scale = scale;
            PenColour = Color.Black;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if (_isHovering)
                colour = Color.Gray;
            spriteBatch.Draw(_texture, Position, null, colour, 0.0f, Origin, Scale, flip, Layer);
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);
                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            _isHovering = false;
            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}

