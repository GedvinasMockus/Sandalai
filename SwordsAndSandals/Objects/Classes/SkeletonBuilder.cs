using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public class SkeletonBuilder : PlayerBuilder
    {
        public SkeletonBuilder(ContentManager content)
        {
            this.content = content;
            product = new Skeleton();
        }
        public override PlayerBuilder SetPosition(Vector2 pos)
        {
            product.Position = pos;
            return this;
        }
        public override PlayerBuilder SetDefaultAbility(SpriteEffects flip)
        {
            product.animation = new SkeletonIdleAnimation(content, 0.1f, flip);
            product.Active = new Idle(product.animation);
            product.AddAbility("Idle", product.Active);
            return this;

        }

        public override PlayerBuilder SetAbilities(SpriteEffects flip)
        {
            product.AddAbility("Sleep", new Idle(new SkeletonIdleAnimation(content, 0.1f, flip)));
            product.AddAbility("Ranged_attack_left", new RangedAttack(content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Arrow"), new SkeletonRangedAttackAnimation(content, 0.1f, SpriteEffects.FlipHorizontally), -800));
            product.AddAbility("Melee_attack_left", new Idle(new SkeletonIdleAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            product.AddAbility("Run_left", new Run(-200f, new SkeletonWalkAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            product.AddAbility("Evasion", new Idle(new SkeletonIdleAnimation(content, 0.1f, flip)));
            product.AddAbility("Run_right", new Run(200f, new SkeletonWalkAnimation(content, 0.1f, SpriteEffects.None)));
            product.AddAbility("Melee_attack_right", new Idle(new SkeletonIdleAnimation(content, 0.1f, SpriteEffects.None)));
            product.AddAbility("Ranged_attack_right", new RangedAttack(content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Arrow"), new SkeletonRangedAttackAnimation(content, 0.1f, SpriteEffects.None), 800));
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
            product.AddAbilityButton("Ranged_attack_left", content.Load<Texture2D>("Icons/Icon_34"), 2.0f, SpriteEffects.None);
            product.AddAbilityButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddAbilityButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddAbilityButton("Evasion", content.Load<Texture2D>("Icons/Icon_17"), 2.0f, SpriteEffects.None);
            product.AddAbilityButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            product.AddAbilityButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
            product.AddAbilityButton("Ranged_attack_right", content.Load<Texture2D>("Icons/Icon_34"), 2.0f, SpriteEffects.FlipHorizontally);
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
