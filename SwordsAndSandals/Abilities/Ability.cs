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
        public Animation animation { get; private set; }
       public bool done { get; set; }
       public bool prepared { get; set; } 

        public Ability(Animation animation)
        {
            done = false;
            prepared = false;
            this.animation = animation;
        }
        public void UpdateState(GameTime gameTime, Player player)
        {
            if (!prepared) Prepare(player);
            NextState(gameTime,player);
            CheckIfDone();
        }

        protected abstract void NextState(GameTime gameTime, Player player);
        protected abstract void CheckIfDone();
        protected virtual void Prepare(Player player)
        {
            animation.Reset();
            player.animation = animation;
            done = false;
            prepared = true;
        }
    }
}