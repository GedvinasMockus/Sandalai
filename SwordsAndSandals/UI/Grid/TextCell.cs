using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwordsAndSandals.UI.Grid
{
    public class TextCell : GridCell
    {
        public string CellText { get; private set; }
        public Vector2 Position { get; private set; }
        public Color TextColor { get; private set; }
        public SpriteFont TextFont { get; private set; }
        public TextCell(SpriteFont font, Color color, string text)
        {
            CellText = text;
            TextColor = color;
            TextFont = font;
        }
        public override void AddPosition(Vector2 position, float width, float wPadding, float hPadding)
        {
            Vector2 textPossition = new Vector2(position.X + wPadding, position.Y + hPadding);
            Position = textPossition;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextFont, CellText, Position, TextColor);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
