﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Abilities;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Classes.PlayerDecorators;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;

namespace SwordsAndSandals.Classes
{
    public class SamuraiBuilder : PlayerBuilder
    {
        public SamuraiBuilder(ContentManager content)
        {
            this.content = content;
            product = new Samurai();
        }

        public override void reset()
        {
            product = new Samurai();
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
            PlayerHPDecorator decorator = new PlayerHPDecorator(product,text);
            product = decorator;
            return this;
        }
        public override PlayerBuilder SetDefaultAbility(SpriteEffects flip)
        {
            product.animation = new SamuraiIdleAnimation(content, 0.1f, flip, true);
            product.Active = new Idle(product.animation); ;
            product.AddAbility("Idle", product.Active);
            return this;
        }

        public override PlayerBuilder SetAbilities(SpriteEffects flip)
        {
            product.AddAbility("Sleep", new Idle(new SamuraiIdleAnimation(content, 0.1f, flip, true)));
            product.AddAbility("Jump_left", new Jump(product.BaseAttributes.BaseDistance * -1.2f, 50, new SamuraiJumpAnimation(content, 0.1f, SpriteEffects.FlipHorizontally, false)));
            product.AddAbility("Melee_attack_left", new Idle(new SamuraiIdleAnimation(content, 0.1f, SpriteEffects.FlipHorizontally, false)));
            product.AddAbility("Run_left", new Run(product.BaseAttributes.BaseDistance * -1f, new SamuraiRunAnimation(content, 0.1f, SpriteEffects.FlipHorizontally, false)));
            product.AddAbility("Shield", new Idle(new SamuraiIdleAnimation(content, 0.1f, flip, true)));
            product.AddAbility("Run_right", new Run(product.BaseAttributes.BaseDistance * 1f, new SamuraiRunAnimation(content, 0.1f, SpriteEffects.None, false)));
            product.AddAbility("Melee_attack_right", new Idle(new SamuraiIdleAnimation(content, 0.1f, SpriteEffects.None, false)));
            product.AddAbility("Jump_right", new Jump(product.BaseAttributes.BaseDistance * 1.2f, 50, new SamuraiJumpAnimation(content, 0.1f, SpriteEffects.None, false)));
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
            product.AddButton("Jump_left", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddButton("Shield", content.Load<Texture2D>("Icons/Icon_18"), 2.0f, SpriteEffects.None);
            product.AddButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            product.AddButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
            product.AddButton("Jump_right", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.None);
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
