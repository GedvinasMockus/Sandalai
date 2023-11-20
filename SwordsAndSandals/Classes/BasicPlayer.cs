using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Sprites;

using System.Collections.Generic;

namespace SwordsAndSandals.Classes
{
    public class BasicPlayer : Player
    {
        public override Rectangle Rectangle
        {
            get
            {
                Vector2 startPoint = animation.Flip == SpriteEffects.FlipHorizontally ?
                    new Vector2(animation.FrameWidth - animation.CollisionRectPoint.X - animation.CollisionWidth, animation.CollisionRectPoint.Y) :
                    animation.CollisionRectPoint;
                Vector2 shift = (startPoint - Origin) * animation.Scale;
                int collisionWidth = (int)(animation.CollisionWidth * animation.Scale);
                int collisionHeight = (int)(animation.CollisionHeight * animation.Scale);
                Vector2 playerPos = new Vector2(Position.X, Position.Y - animation.Scale * animation.FrameHeight / 2);
                Vector2 rectPos = playerPos + shift;
                return new Rectangle((int)rectPos.X, (int)rectPos.Y, collisionWidth, collisionHeight);
            }
        }
        public override void Draw(SpriteBatch batch)
        {
            animation.Draw(batch, new Vector2(Position.X, Position.Y - animation.Scale * animation.FrameHeight / 2), Origin);
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
