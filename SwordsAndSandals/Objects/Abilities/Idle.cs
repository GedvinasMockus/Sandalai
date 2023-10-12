﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Animations;
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