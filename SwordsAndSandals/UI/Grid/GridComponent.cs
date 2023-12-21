using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwordsAndSandals.UI.Grid
{
    public abstract class GridComponent
    {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public virtual void AddPosition(Vector2 position, float width, float wPadding, float hPadding)
        {
        }
        public virtual void Add(GridComponent component)
        {
        }
    }
}
