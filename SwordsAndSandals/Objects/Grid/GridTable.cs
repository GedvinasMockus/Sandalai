using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace SwordsAndSandals.Objects.Grid
{
    public class GridTable : Component
    {
        private List<GridColumn> columns;
        private List<GridRow> data;
        private SpriteFont font;
        private Vector2 position;
        private Texture2D dot;
        private Texture2D background;
        private float width;
        private float padding;

        public GridTable(SpriteFont font, Texture2D dot, Vector2 position, float width, float padding)
        {
            this.columns = new List<GridColumn>();
            this.data = new List<GridRow>();
            this.font = font;
            this.position = position;
            this.dot = dot;
            this.background = dot;
            this.width = width;
            this.padding = padding;
        }

        public void AddColumn(string header)
        {
            columns.Add(new GridColumn(header));
        }

        public void AddRow(GridRow rowData)
        {
            data.Add(rowData);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            float tableWidth = columns.Count * width;
            float tableHeight = (data.Count + 1) * (font.LineSpacing + padding);

            float x = position.X - tableWidth / 2;
            float y = position.Y - tableHeight / 2;

            spriteBatch.Draw(background, new Rectangle((int)x, (int)y, (int)tableWidth, (int)tableHeight), new Color(0, 0, 0, 128));

            float cellY = y;
            for (int i = 0; i < columns.Count; i++)
            {
                float cellX = x + i * width;
                spriteBatch.DrawString(font, columns[i].Header, new Vector2(cellX + padding, cellY + padding), Color.White);
            }

            cellY += font.LineSpacing + padding;
            foreach (var row in data)
            {
                for (int i = 0; i < columns.Count - 1; i++)
                {
                    float cellX = x + i * width;

                    spriteBatch.DrawString(font, row.Columns[i].Header, new Vector2(cellX + padding, cellY + padding), Color.White);
                }
                foreach (var button in row.Buttons)
                {
                    button.Position = new Vector2(x + (columns.Count - 1) * width + width / 2, cellY + (font.LineSpacing + padding) / 2);
                    button.Draw(spriteBatch);
                }
                cellY += font.LineSpacing + padding;

            }

            float lineX = x;
            float lineY = y;
            dot.SetData(new Color[] { Color.Orange });

            for (int i = 0; i <= data.Count + 1; i++)
            {
                spriteBatch.Draw(dot, new Rectangle((int)x, (int)lineY, (int)(tableWidth + padding / 2), 2), Color.Orange);
                lineY += font.LineSpacing + padding;
            }

            for (int i = 0; i <= columns.Count; i++)
            {
                spriteBatch.Draw(dot, new Rectangle((int)lineX, (int)y, 2, (int)tableHeight), Color.Orange);
                lineX += width;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var item in data)
            {
                foreach (var button in item.Buttons)
                {
                    button.Update(gameTime);
                }
            }

        }
    }

}
