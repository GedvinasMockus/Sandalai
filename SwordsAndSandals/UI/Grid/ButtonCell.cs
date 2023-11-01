using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace SwordsAndSandals.UI.Grid
{
    public class ButtonCell : GridCell
    {
        public Button CellButton { get; private set; }
        public SpriteFont Font { get; private set; }

        public ButtonCell(SpriteFont font, Texture2D texture, string text, float scale)
        {
            CellButton = new Button(texture, font, text, scale, SpriteEffects.None);
            Font = font;
        }
        public override void AddPosition(Vector2 position, float width, float wPadding, float hPadding)
        {
            Vector2 buttonPosition = new Vector2(position.X + width / 2, position.Y + hPadding + (Font.LineSpacing) / 2);
            CellButton.Position = buttonPosition;
        }
        public void AddHandler(EventHandler handler)
        {
            CellButton.Click += handler;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            CellButton.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            CellButton.Update(gameTime);
        }
    }
}
