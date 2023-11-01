using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace SwordsAndSandals.UI.Grid
{
    public class GridRow
    {
        public List<GridCell> Columns { get; private set; }
        public int RowNum { get; set; }
        public GridRow()
        {
            Columns = new List<GridCell>();
        }
        public void AddCell(GridCell column)
        {
            Columns.Add(column);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 table, float width, float padding)
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                float cellX = table.X + i * width;
                Columns[i].AddPosition(new Vector2(cellX, table.Y), width, padding, RowNum * padding);
                Columns[i].Draw(spriteBatch);
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach (var cell in Columns)
            {
                cell.Update(gameTime);
            }
        }
    }
}
