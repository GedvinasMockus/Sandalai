using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SwordsAndSandals.Objects
{
    public class Projectile : Sprite
    {
        public Projectile(Texture2D texture, Vector2 position) : base(texture, position)
        {

        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, Position, null, Color.White, Rotation, Origin, Scale, Flip, 1);
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            Position += Velocity * elapsed;
        }
    }
}
