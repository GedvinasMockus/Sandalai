using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Animations
{
    public class AnimatedSprite
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public Animation animation { get; set; }

        public AnimatedSprite(Animation start, Vector2 position)
        {
            animation = start;
            this.position = position;
            velocity = Vector2.Zero;
        }
        public void Draw(SpriteBatch batch)
        {
            animation.Draw(batch, position, new Vector2(animation.frameWidth/2, animation.frameHeight/2));
        }

        public void DrawAsPlayer(SpriteBatch batch)
        {
            animation.Draw(batch, new Vector2(position.X, position.Y - animation.Scale * animation.frameHeight / 2), new Vector2(animation.frameWidth / 2, animation.frameHeight / 2));
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }
    }
}
