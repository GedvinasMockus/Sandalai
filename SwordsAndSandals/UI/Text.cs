using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace SwordsAndSandals.UI
{
    public class Text : Component
    {
        public SpriteFont Font { get; private set; }
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public string TextString { get; set; }
        public float TextSize { get; set; }
        public Color OutlineColor { get; set; }
        public Text(SpriteFont font)
        {
            Font = font;
            PenColour = Color.Black;
            OutlineColor = Color.Black;
            TextSize = 1.0f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float textWidth = Font.MeasureString(TextString).X * TextSize;
            float textHeight = Font.MeasureString(TextString).Y * TextSize;
            var x = Position.X - textWidth / 2;
            var y = Position.Y - textHeight / 2;
            DrawOutline(spriteBatch, TextString, new Vector2(x, y), OutlineColor, TextSize);
            spriteBatch.DrawString(Font, TextString, new Vector2(x, y), PenColour, 0f, Vector2.Zero, TextSize, SpriteEffects.None, 0);
        }
        private void DrawOutline(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float scale)
        {
            float offset = 1.5f;
            Color outlineColor = OutlineColor;
            for (float i = 0; i < 360; i += 45)
            {
                float x = position.X + (float)(offset * Math.Cos(i));
                float y = position.Y + (float)(offset * Math.Sin(i));
                spriteBatch.DrawString(Font, text, new Vector2(x, y), outlineColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
