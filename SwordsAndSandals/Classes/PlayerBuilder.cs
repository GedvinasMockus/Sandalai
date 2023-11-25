using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Sprites;
using SwordsAndSandals.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Classes
{
    public abstract class PlayerBuilder //galima isidemeti player object dalis kurios gali buti pernaudotos kitu player kurimui
    {
        protected Player product;

        protected ContentManager content;

        protected AnimationFactory animationFactory;

        public abstract void reset();

        public abstract PlayerBuilder SetPosition(Vector2 pos);

        public abstract PlayerBuilder SetName(string name);

        public abstract PlayerBuilder SetAttributes(Attributes attributes);

        public abstract PlayerBuilder SetDefaultAbility(SpriteEffects flip);

        public abstract PlayerBuilder SetAbilities(SpriteEffects flip, List<Sprite> ctx);

        public abstract PlayerBuilder SetButtons();

        public abstract PlayerBuilder SetMeleeWeapon();

        public abstract PlayerBuilder SetRangedWeapon();

        public abstract PlayerBuilder SetShieldWeapon();

        public abstract Player GetPlayer();
    }
}
