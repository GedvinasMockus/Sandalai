using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwordsAndSandals.UI.Grid
{
    public abstract class GridCell
    {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void AddPosition(Vector2 position, float width, float wPadding, float hPadding);
    }
}
