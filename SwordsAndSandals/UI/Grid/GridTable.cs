using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace SwordsAndSandals.UI.Grid
{
    public class GridTable : GridComponent
    {
        public List<GridComponent> Component { get; set; }
        private Vector2 position;
        private Texture2D dot;
        private Texture2D background;
        private SpriteFont font;
        private float width;
        private float padding;
        private Color lineColor;
        public int LineThickness { get; set; }

        public GridTable(SpriteFont font, Texture2D dot, Color lineColor, Vector2 position, float width, float padding)
        {
            Component = new List<GridComponent>();
            this.font = font;
            this.position = position;
            this.dot = dot;
            background = dot;
            this.width = width;
            this.padding = padding;
            this.lineColor = lineColor;
            LineThickness = 2;
        }
        public override void Add(GridComponent rowData)
        {
            Component.Add(rowData);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            float tableWidth = (Component[0] as GridRow).Component.Count * width;
            float tableHeight = (Component.Count) * (font.LineSpacing + padding * 2);

            float x = position.X - tableWidth / 2;
            float y = position.Y - tableHeight / 2;

            spriteBatch.Draw(background, new Rectangle((int)x, (int)y, (int)tableWidth, (int)tableHeight), new Color(0, 0, 0, 128));

            float cellY = y;

            dot.SetData(new Color[] { lineColor });
            spriteBatch.Draw(dot, new Rectangle((int)x, (int)(cellY), (int)(tableWidth), LineThickness), lineColor);
            for (int i = 0; i < Component.Count; i++)
            {
                Component[i].AddPosition(new Vector2(x, cellY), width, padding, padding);
                Component[i].Draw(spriteBatch);
                cellY += font.LineSpacing + padding;
                spriteBatch.Draw(dot, new Rectangle((int)x, (int)(cellY + (i + 1) * padding), (int)(tableWidth), LineThickness), lineColor);
            }

            float lineX = x;
            for (int i = 0; i <= (Component[0] as GridRow).Component.Count; i++)
            {
                spriteBatch.Draw(dot, new Rectangle((int)lineX, (int)y, LineThickness, (int)tableHeight), lineColor);
                lineX += width;
            }
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var row in Component)
            {
                row.Update(gameTime);
            }
        }
    }

}
