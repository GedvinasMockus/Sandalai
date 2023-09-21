using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace SwordsAndSandals
{
    public class TextBox : Component
    {
        private SpriteFont _font;
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        public float TextSize { get; set; }
        public Color OutlineColor { get; set; }
        public TextBox(SpriteFont font)
        {
            _font = font;
            PenColour = Color.Black;
            OutlineColor = Color.Black;
            TextSize = 1.0f;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float textWidth = _font.MeasureString(Text).X * TextSize;
            float textHeight = _font.MeasureString(Text).Y * TextSize;
            var x = Position.X - (textWidth / 2);
            var y = Position.Y - (textHeight / 2);
            DrawOutline(spriteBatch, Text, new Vector2(x, y), OutlineColor, TextSize);
            spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour, 0f, Vector2.Zero, TextSize, SpriteEffects.None, 0);
        }
        private void DrawOutline(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float scale)
        {
            float offset = 1.5f;
            Color outlineColor = OutlineColor;
            for (float i = 0; i < 360; i += 45)
            {
                float x = position.X + (float)(offset * Math.Cos(i));
                float y = position.Y + (float)(offset * Math.Sin(i));
                spriteBatch.DrawString(_font, text, new Vector2(x, y), outlineColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
