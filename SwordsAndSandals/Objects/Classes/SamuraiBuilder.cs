using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public class SamuraiBuilder : PlayerBuilder
    {
        public SamuraiBuilder(ContentManager content)
        {
            this.content = content;
            product = new Samurai();
        }
        public override PlayerBuilder SetPosition(Vector2 pos)
        {
            product.Position = pos;
            return this;
        }
        public override PlayerBuilder SetDefaultAbility(SpriteEffects flip)
        {
            product.animation = new SamuraiIdleAnimation(content, 0.1f, flip);
            product.Active = new Idle(product.animation); ;
            product.AddAbility("Idle", product.Active);
            return this;
        }

        public override PlayerBuilder SetAbilities(SpriteEffects flip)
        {
            product.AddAbility("Sleep", new Idle(new SamuraiIdleAnimation(content, 0.1f, flip)));
            product.AddAbility("Jump_left", new Jump(-350f, 600f, new SamuraiJumpAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            product.AddAbility("Melee_attack_left", new Idle(new SamuraiIdleAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            product.AddAbility("Run_left", new Run(-300f, new SamuraiRunAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            product.AddAbility("Shield", new Idle(new SamuraiIdleAnimation(content, 0.1f, flip)));
            product.AddAbility("Run_right", new Run(300f, new SamuraiRunAnimation(content, 0.1f, SpriteEffects.None)));
            product.AddAbility("Melee_attack_right", new Idle(new SamuraiIdleAnimation(content, 0.1f, SpriteEffects.None)));
            product.AddAbility("Jump_right", new Jump(350f, 600f, new SamuraiJumpAnimation(content, 0.1f, SpriteEffects.None)));
            return this;
        }
        public override PlayerBuilder SetCorrection(int correction)
        {
            product.CorrectionY = correction;
            return this;
        }

        public override PlayerBuilder SetButtons()
        {
            product.AddAbilityButton("Sleep", content.Load<Texture2D>("Icons/Icon_05"), 2.0f, SpriteEffects.None);
            product.AddAbilityButton("Jump_left", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddAbilityButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddAbilityButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddAbilityButton("Shield", content.Load<Texture2D>("Icons/Icon_18"), 2.0f, SpriteEffects.None);
            product.AddAbilityButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            product.AddAbilityButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
            product.AddAbilityButton("Jump_right", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.None);
            return this;
        }

        public override PlayerBuilder SetMeleeWeapon()
        {
            return this;
        }

        public override PlayerBuilder SetRangedWeapon()
        {
            return this;
        }

        public override PlayerBuilder SetShieldWeapon()
        {
            return this;
        }
        public override Player GetPlayer()
        {
            return product;
        }
    }
}
