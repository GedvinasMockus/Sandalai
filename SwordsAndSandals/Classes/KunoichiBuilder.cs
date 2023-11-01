using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Abilities;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Classes.PlayerDecorators;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;

namespace SwordsAndSandals.Classes
{
    public class KunoichiBuilder : PlayerBuilder
    {
        public KunoichiBuilder(ContentManager content)
        {
            this.content = content;
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

        public override PlayerBuilder SetAttributes(Attributes attributes)
        {
            product.BaseAttributes = attributes;
            PlayerHPDecorator decorator1 = new PlayerHPDecorator(product);
            Text text1 = new Text(content.Load<SpriteFont>("Fonts/vinque"))
            {
                PenColour = Color.DarkRed,
                TextSize = 0.75f,
            };
            decorator1.AddText(text1);
            PlayerArmourDecorator decorator2 = new PlayerArmourDecorator(decorator1);
            Text text2 = new Text(content.Load<SpriteFont>("Fonts/vinque"))
            {
                PenColour = Color.Gray,
                TextSize = 0.75f,
            };
            decorator2.AddText(text2);
            product = decorator2;
            return this;
        }

        public override PlayerBuilder SetDefaultAbility(SpriteEffects flip)
        {
            product.animation = new KunoichiIdleAnimation(content, 0.1f, flip, true);
            product.Active = new Idle(product.animation);
            product.AddAbility("Idle", product.Active);
            return this;
        }

        public override PlayerBuilder SetAbilities(SpriteEffects flip)
        {
            product.AddAbility("Heal", new Idle(new KunoichiIdleAnimation(content, 0.1f, flip, true)));
            product.AddAbility("Jump_left", new Jump(product.BaseAttributes.BaseDistance * -1.2f, 50, new KunoichiJumpAnimation(content, 0.1f, SpriteEffects.FlipHorizontally, false)));
            product.AddAbility("Melee_attack_left", new Idle(new KunoichiIdleAnimation(content, 0.1f, SpriteEffects.FlipHorizontally, false)));
            product.AddAbility("Run_left", new Run(product.BaseAttributes.BaseDistance * -1f, new KunoichiRunAnimation(content, 0.1f, SpriteEffects.FlipHorizontally, false)));
            product.AddAbility("Run_right", new Run(product.BaseAttributes.BaseDistance * 1f, new KunoichiRunAnimation(content, 0.1f, SpriteEffects.None, false)));
            product.AddAbility("Melee_attack_right", new Idle(new KunoichiIdleAnimation(content, 0.1f, SpriteEffects.None, false)));
            product.AddAbility("Jump_right", new Jump(product.BaseAttributes.BaseDistance * 1.2f, 50, new KunoichiJumpAnimation(content, 0.1f, SpriteEffects.None, false)));
            return this;
        }

        public override PlayerBuilder SetCorrection(int correctionY)
        {
            product.CorrectionY = correctionY;
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
            return product;
        }
    }
}
