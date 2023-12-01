using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwordsAndSandals.UI.Grid
{
    public class TextCell : GridComponent
    {
        public string CellText { get; private set; }
        public Vector2 Position { get; private set; }
        public Color TextColor { get; private set; }
        public SpriteFont TextFont { get; private set; }
        public float TextSize { get; set; }
        public TextCell(SpriteFont font, Color color, string text)
        {
            CellText = text;
            TextColor = color;
            TextFont = font;
            TextSize = 1f;
        }
        public override void AddPosition(Vector2 position, float width, float wPadding, float hPadding)
        {
            Vector2 size = TextFont.MeasureString(CellText);
            Vector2 textPossition = new Vector2(position.X + wPadding + size.X / 2, position.Y + hPadding + size.Y / 2);
            Position = textPossition;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Text text = new Text(TextFont)
            {
                Position = Position,
                TextString = CellText,
                TextSize = TextSize,
                PenColour = TextColor,
                OutlineColor = Color.Black,
            };
            text.Draw(spriteBatch);
            //spriteBatch.DrawString(TextFont, CellText, Position, TextColor);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
