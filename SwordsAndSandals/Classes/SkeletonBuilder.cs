using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Abilities;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Classes.PlayerDecorators;
using SwordsAndSandals.Sprites;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;
using System.Collections.Generic;

namespace SwordsAndSandals.Classes
{
    public class SkeletonBuilder : PlayerBuilder
    {
        public SkeletonBuilder(ContentManager content)
        {
            this.content = content;
            product = new Skeleton();
        }

        public override void reset()
        {
            product = new Skeleton();
        }
        public override PlayerBuilder SetPosition(Vector2 pos)
        {
            product.Position = pos;
            return this;
        }
        public override PlayerBuilder SetName(string name)
        {
            Text text = new Text(content.Load<SpriteFont>("Fonts/vinque"))
            {
                PenColour = Color.Orange,
                TextSize = 0.75f,
                TextString = name,
            };
            PlayerNameDecorator decorator = new PlayerNameDecorator(product, text);
            product = decorator;
            return this;
        }
        public override PlayerBuilder SetAttributes(Attributes attributes)
        {
            product.BaseAttributes = attributes;
            Text text = new Text(content.Load<SpriteFont>("Fonts/vinque"))
            {
                PenColour = Color.DarkRed,
                TextSize = 0.75f,
                TextString = "HP:" + attributes.CurrHealth + "/" + attributes.MaxHealth,
            };
            PlayerHPDecorator decorator = new PlayerHPDecorator(product, text);
            product = decorator;
            return this;
        }
        public override PlayerBuilder SetDefaultAbility(SpriteEffects flip)
        {
            product.animation = new SkeletonIdleAnimation(content, 0.1f, flip, true);
            product.Active = new Idle(product.animation);
            product.AddAbility("Idle", product.Active);
            return this;

        }
        public override PlayerBuilder SetAbilities(SpriteEffects flip, List<Sprite> ctx)
        {
            product.AddAbility("Sleep", new Idle(new SkeletonIdleAnimation(content, 0.1f, flip, true)));
            product.AddAbility("Ranged_attack_left", new RangedAttack(content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Arrow"), new SkeletonRangedAttackAnimation(content, 0.1f, SpriteEffects.FlipHorizontally, false), -800, ctx));
            product.AddAbility("Melee_attack_left", new Idle(new SkeletonIdleAnimation(content, 0.1f, SpriteEffects.FlipHorizontally, false)));
            product.AddAbility("Run_left", new Run(product.BaseAttributes.BaseDistance * -1f, new SkeletonWalkAnimation(content, 0.1f, SpriteEffects.FlipHorizontally, false)));
            product.AddAbility("Evasion", new Idle(new SkeletonIdleAnimation(content, 0.1f, flip, true)));
            product.AddAbility("Run_right", new Run(product.BaseAttributes.BaseDistance * 1f, new SkeletonWalkAnimation(content, 0.1f, SpriteEffects.None, false)));
            product.AddAbility("Melee_attack_right", new Idle(new SkeletonIdleAnimation(content, 0.1f, SpriteEffects.None, false)));
            product.AddAbility("Ranged_attack_right", new RangedAttack(content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Arrow"), new SkeletonRangedAttackAnimation(content, 0.1f, SpriteEffects.None, false), 800, ctx));
            return this;
        }
        public override PlayerBuilder SetCorrection(int correctionY)
        {
            product.CorrectionY = correctionY;
            return this;
        }
        public override PlayerBuilder SetButtons()
        {
            product.AddButton("Sleep", content.Load<Texture2D>("Icons/Icon_05"), 2.0f, SpriteEffects.None);
            product.AddButton("Ranged_attack_left", content.Load<Texture2D>("Icons/Icon_34"), 2.0f, SpriteEffects.None);
            product.AddButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddButton("Evasion", content.Load<Texture2D>("Icons/Icon_17"), 2.0f, SpriteEffects.None);
            product.AddButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            product.AddButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
            product.AddButton("Ranged_attack_right", content.Load<Texture2D>("Icons/Icon_34"), 2.0f, SpriteEffects.FlipHorizontally);
            PlayerButtonDecorator decorator = new PlayerButtonDecorator(product);
            product = decorator;
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
