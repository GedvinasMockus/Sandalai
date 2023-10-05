using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Abilities
{
    public class Idle : Ability
    {
        public Idle(AnimatedSprite animation) : base(animation)
        {

        }


        public override void Update(GameTime gameTime, Player player)
        {
            Animation.Update(gameTime);
        }
    }
}
