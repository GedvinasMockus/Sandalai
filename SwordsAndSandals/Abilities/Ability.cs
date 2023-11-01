using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Classes;
using SwordsAndSandals.Sprites;
using System.Collections.Generic;

namespace SwordsAndSandals.Abilities
{
    public abstract class Ability
    {
        public bool done { get; set; }
        public Animation animation { get; private set; }

        public Ability(Animation animation)
        {
            done = true;
            this.animation = animation;
        }

        public abstract void Prepare(Player player);
        public abstract void Update(GameTime gameTime, Player player, List<Sprite> sprites);
    }
}