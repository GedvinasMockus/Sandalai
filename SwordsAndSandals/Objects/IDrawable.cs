using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects
{
    public interface IDrawable
    {
        public Texture2D texture { get; }
        public Vector2 position { get; set; }
        public void Draw(SpriteBatch batch);
    }
}
