using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace SwordsAndSandals.UI.Grid
{
    public abstract class GridComponent
    {
        public virtual List<GridComponent> Component { get; set; }
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
