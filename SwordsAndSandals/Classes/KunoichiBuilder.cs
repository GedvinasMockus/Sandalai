using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Abilities;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Classes.PlayerDecorators;
using SwordsAndSandals.Mediator;
using SwordsAndSandals.Sprites;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;
using System.Collections.Generic;

namespace SwordsAndSandals.Classes
{
    public class KunoichiBuilder : PlayerBuilder
    {
        private IMediator mediator;

        public KunoichiBuilder(ContentManager content, AnimationFactory factory)
        {
            this.content = content;
            animationFactory = factory;
            product = new Kunoichi();
        }

        public override void reset()
        {
            product = new Kunoichi();
        }

        public override PlayerBuilder SetPosition(Vector2 pos)
        {
            product.Position = pos;
            return this;
        }

        public override PlayerBuilder SetName(string name)
        {
            Text text = new Text(content.Load<SpriteFont>("Fonts/vinque"), mediator)
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
            Text text = new Text(content.Load<SpriteFont>("Fonts/vinque"), mediator)
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
            product.animation = animationFactory.CreateAnimation("KunoichiIdle", content, 3.0f, 0.1f, flip);
            product.Active = new Idle(product.animation);
            product.AddAbility("Idle", product.Active);
            return this;
        }

        public override PlayerBuilder SetAbilities(SpriteEffects flip, List<Sprite> ctx)
        {
            product.AddAbility("Heal", new Idle(animationFactory.CreateAnimation("KunoichiIdle", content, 3.0f, 0.1f, flip)));
            product.AddAbility("Jump_left", new Jump(product.BaseAttributes.BaseDistance * -1.2f, 50, animationFactory.CreateAnimation("KunoichiJump", content, 3.0f, 0.1f, SpriteEffects.FlipHorizontally)));
            product.AddAbility("Melee_attack_left", new Idle(animationFactory.CreateAnimation("KunoichiIdle", content, 3.0f, 0.1f, flip)));
            product.AddAbility("Run_left", new Run(product.BaseAttributes.BaseDistance * -1f, animationFactory.CreateAnimation("KunoichiRun", content, 3.0f, 0.1f, SpriteEffects.FlipHorizontally)));
            product.AddAbility("Run_right", new Run(product.BaseAttributes.BaseDistance * 1f, animationFactory.CreateAnimation("KunoichiRun", content, 3.0f, 0.1f, SpriteEffects.None)));
            product.AddAbility("Melee_attack_right", new Idle(animationFactory.CreateAnimation("KunoichiIdle", content, 3.0f, 0.1f, flip)));
            product.AddAbility("Jump_right", new Jump(product.BaseAttributes.BaseDistance * 1.2f, 50, animationFactory.CreateAnimation("KunoichiJump", content, 3.0f, 0.1f, SpriteEffects.None)));
            return this;
        }

        public override PlayerBuilder SetButtons()
        {
            product.AddButton("Heal", content.Load<Texture2D>("Icons/Icon_11"), 2.0f, SpriteEffects.None);
            product.AddButton("Jump_left", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            product.AddButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
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
            Player complete = product;
            reset();
            return complete;
        }
    }
}
