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

        public override void Prepare(Player player)
        {
            animation.Reset();
            player.animation = animation;
        }

        public override void Update(GameTime gameTime, Player player, List<Sprite> sprites)
        {
            return;
        }
    }
}
