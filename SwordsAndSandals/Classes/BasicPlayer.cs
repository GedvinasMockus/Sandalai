using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Sprites;

using System.Collections.Generic;

namespace SwordsAndSandals.Classes
{
    public class BasicPlayer : Player
    {
        public override void Draw(SpriteBatch batch)
        {
            animation.Draw(batch, new Vector2(Position.X, Position.Y - animation.Scale * animation.frameHeight / 2), Origin);
        }

        public override void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            Active.UpdateState(gameTime, this);
            if (Active.done == true)
            {
                AbilityFinished();
                Active = Abilities["Idle"];
                Active.prepared = false;
            }
        }
    }
}
