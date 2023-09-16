using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects
{
    public class Background
    {
        public Vector2 position { get; set; }

        private Texture2D texture;
        public Background(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
        }
    }
}
