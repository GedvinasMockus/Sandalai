﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SwordsAndSandals.Mediator;
using System;

namespace SwordsAndSandals.UI
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
                return new Rectangle((int)(Position.X - _texture.Width / 2 * Scale), (int)(Position.Y - _texture.Height / 2 * Scale), (int)(_texture.Width * Scale), (int)(_texture.Height * Scale));
            }
        }
        private Color[] data;
        public Color[] TextureData
        {
            get
            {
                if (data == null)
                {
                    data = new Color[_texture.Width * _texture.Height];
                    _texture.GetData(data);
                }
                return data;
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
        private IMediator mediator;

        public Button(Texture2D texture, float scale, SpriteEffects flip, IMediator mediator) : base(mediator)
        {
            _texture = texture;
            Position = new Vector2(-1, -1);
            _isHovering = false;
            this.flip = flip;
            Scale = scale;
            PenColour = Color.Black;

            this.mediator = mediator;
        }

        public Button(Texture2D texture, SpriteFont font, string text, float scale, SpriteEffects flip, IMediator mediator) : base(mediator)
        {
            _texture = texture;
            Position = new Vector2(-1, -1);
            _font = font;
            Text = text;
            _isHovering = false;
            this.flip = flip;
            Scale = scale;
            PenColour = Color.Black;

            this.mediator = mediator;
        }

        public Button(IMediator mediator) : base(mediator)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if (_isHovering)
                colour = Color.Gray;
            spriteBatch.Draw(_texture, Position, null, colour, 0.0f, Origin, Scale, flip, Layer);
            if (!string.IsNullOrEmpty(Text))
            {
                var x = Rectangle.X + Rectangle.Width / 2 - _font.MeasureString(Text).X / 2;
                var y = Rectangle.Y + Rectangle.Height / 2 - _font.MeasureString(Text).Y / 2;
                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            _isHovering = false;
            if (Rectangle.Contains(_currentMouse.Position))
            {
                Matrix transform = Matrix.CreateTranslation(-Origin.X, -Origin.Y, 0) * Matrix.CreateScale(Scale) * Matrix.CreateTranslation(Position.X, Position.Y, 0);
                Matrix inverse = Matrix.Invert(transform);
                Vector2 pos1 = new Vector2(_currentMouse.Position.X, _currentMouse.Position.Y);
                Vector2 pos2 = Vector2.Transform(pos1, inverse);
                if (TextureData[(int)pos2.Y * _texture.Width + (int)pos2.X].A > 254)
                {
                    _isHovering = true;
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        Click?.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        public void RemoveAllHandlers()
        {
            Click = null;
        }

        public void Invoke(string state, object obj)
        {
            mediator = new ConcreteMediator(mediator);
            mediator.Interaction(state, obj);
        }
    }
}

