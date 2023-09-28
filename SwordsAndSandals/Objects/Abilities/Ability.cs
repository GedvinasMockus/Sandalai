﻿using Microsoft.Xna.Framework;
using SwordsAndSandals.Objects.Classes;

namespace SwordsAndSandals.Objects.Abilities
{
    public class Ability
    {
        public bool active { get; set; }
        public bool done { get; set; }

        public Ability()
        {
            active = false;
            done = true;
        }

        public virtual void Update(GameTime gameTime, Player player)
        {

        }



    }
}