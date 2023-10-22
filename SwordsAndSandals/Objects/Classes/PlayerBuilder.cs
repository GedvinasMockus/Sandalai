﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public abstract class PlayerBuilder
    {
        protected Player product;

        protected ContentManager content;

        public abstract void reset();

        public abstract PlayerBuilder SetPosition(Vector2 pos);

        public abstract PlayerBuilder SetAttributes(Attributes attributes);

        public abstract PlayerBuilder SetDefaultAbility(SpriteEffects flip);

        public abstract PlayerBuilder SetAbilities(SpriteEffects flip);

        public abstract PlayerBuilder SetCorrection(int correctionY);

        public abstract PlayerBuilder SetButtons();

        public abstract PlayerBuilder SetMeleeWeapon();

        public abstract PlayerBuilder SetRangedWeapon();

        public abstract PlayerBuilder SetShieldWeapon();

        public abstract Player GetPlayer();
    }
}
