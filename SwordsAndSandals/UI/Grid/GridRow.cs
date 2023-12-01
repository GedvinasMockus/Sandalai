using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace SwordsAndSandals.UI.Grid
{
    public class GridRow : GridComponent
    {
        public override List<GridComponent> Component { get; set; }
        public int RowNum { get; set; }
        public GridRow(int rowNum)
        {
            Component = new List<GridComponent>();
            this.RowNum = rowNum + 1;
        }
        public override void Add(GridComponent column)
        {
            Component.Add(column);
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var cell in Component)
            {
                cell.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Component.Count; i++)
            {
                Component[i].Draw(spriteBatch);
            }
        }

        public override void AddPosition(Vector2 position, float width, float wPadding, float hPadding)
        {
            for (int i = 0; i < Component.Count; i++)
            {
                float cellX = position.X + i * width;
                Component[i].AddPosition(new Vector2(cellX, position.Y), width, wPadding, RowNum * hPadding);
            }
        }
    }
}
