﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Classes;
using System.Collections.Generic;

namespace SwordsAndSandals.Objects.Abilities
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