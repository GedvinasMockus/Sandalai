using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Classes;
using SwordsAndSandals.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Abilities
{
    public class Idle : Ability
    {
        public Idle(Animation animation) : base(animation)
        {

        }

        protected override void NextState(GameTime gameTime, Player player)
        {
            return;
        }

        protected override void CheckIfDone()
        {
            return;
        }
    }
}
