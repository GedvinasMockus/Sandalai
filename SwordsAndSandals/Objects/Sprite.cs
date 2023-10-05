using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects
{
    public class Sprite
    {
        private Texture2D texture;
        private float rotation;
        private float scale;
        private Vector2 origin;
        private SpriteEffects flip;

        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Draw(SpriteBatch batch)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
