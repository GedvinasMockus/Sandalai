using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Sprites;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.Classes.PlayerDecorators
{
    public class PlayerButtonDecorator : PlayerDecorator
    {
        public PlayerButtonDecorator(Player p) : base(p)
        {

        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            int numIcons = wrapee.GetButtonCount();
            float radius = animation.Scale * animation.frameHeight / 2;
            float angleIncrement = MathHelper.TwoPi / numIcons;
            int index = 0;
            foreach (var b in wrapee.GetButtonValues())
            {
                float angle = index * angleIncrement;
                float xOffset = -(float)Math.Sin(angle) * radius;
                float yOffset = -(float)Math.Cos(angle) * radius;
                b.Position = new Vector2(Position.X + xOffset, Position.Y - animation.Scale * (animation.frameHeight / 2 - CorrectionY) + yOffset);
                b.Draw(batch);
                index++;
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
            foreach (var b in wrapee.GetButtonValues())
            {
                b.Update(gameTime);
            }
        }
    }
}
