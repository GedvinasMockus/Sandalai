using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public class BasicPlayer : Player
    {
        public override void Draw(SpriteBatch batch)
        {
            animation.Draw(batch, new Vector2(Position.X, Position.Y - animation.Scale * animation.frameHeight / 2), Origin);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            animation.Update(gameTime);
            Active.Update(gameTime, this, sprites);
            if (Active != Abilities["Idle"] && Active.done == true)
            {
                Active = Abilities["Idle"];
                Active.Prepare(this);
            }
        }
    }
}
