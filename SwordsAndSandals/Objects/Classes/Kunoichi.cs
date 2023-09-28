using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public class Kunoichi : Player
    {
        public Kunoichi(Vector2 position) : base(position)
        {

        }
        public override void LoadStartInfo(ContentManager content, SpriteEffects flip)
        {
            AnimatedSprite animation = new AnimatedSprite(content.Load<Texture2D>("Character/Ninja/Kunoichi/Idle"), 3.0f, 0.1f, flip);
            dAnimation = animation;
            cAnimation = animation;
            AddAbility("Heal", new Ability());
            AddAbility("Jump_left", new Ability());
            AddAbility("Melee_attack_left", new Ability());
            AddAbility("Run_left", new Run(350f, -116.66f));
            AddAbility("Run_right", new Run(350f, 116.66f));
            AddAbility("Melle_attack_right", new Ability());
            AddAbility("Jump_right", new Ability());
        }

        public override void LoadButtons(ContentManager content)
        {
            centerY = 32;
            AddAbilityButton("Heal", content.Load<Texture2D>("Icons/Icon_11"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Jump_left", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Jump_right", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.None);
        }
    }
}
