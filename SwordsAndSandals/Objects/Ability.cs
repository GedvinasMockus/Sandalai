using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects
{
    public class Ability
    {
        public Vector2 position { get; set; }
        public Texture2D texture { get; private set; }
        public float scale { get; private set; }

        private SpriteEffects flip;
        private Color color;

        public Ability(Texture2D texture, float scale)
        {
            this.texture = texture;
            this.position = new Vector2(-1, -1);
            this.scale = scale;
            flip = SpriteEffects.None;
            color = Color.White;
        }
        public Ability(Texture2D texture, float scale ,SpriteEffects flip)
        {
            this.texture = texture;
            this.position = new Vector2(-1, -1);
            this.scale = scale;
            this.flip = flip;
            color = Color.White;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, null, color, 0.0f, new Vector2(texture.Width/2,texture.Height/2), scale, flip, 1);
        }

        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            float xOffset = scale * texture.Width / 2;
            float yOffset = scale * texture.Height / 2;
            if (position.X - xOffset < state.X && state.X < position.X + xOffset && position.Y - yOffset < state.Y && state.Y < position.Y + yOffset)
            {
                color = Color.Gray;
            }
            else color = Color.White;
        }
    }
}
