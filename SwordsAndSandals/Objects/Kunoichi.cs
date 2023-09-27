using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects
{
    public class Kunoichi : Player
    {
        public Kunoichi(Vector2 position, float scale, int centerY, SpriteEffects effect) : base(position, scale, centerY, effect)
        {

        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("Character/Ninja/Kunoichi/Idle");
            totalFrames = texture.Width / texture.Height;
            frameWidth = texture.Width / this.totalFrames;
            frameHeight = texture.Height;
        }
        public override void LoadAbilites(ContentManager content)
        {
            AddAbility("Melee_attack_left", new Ability());
            AddAbility("Run_left", new Run(350f, -116.66f));
            AddAbility("Jump_left", new Ability());
            AddAbility("Heal", new Ability());
            AddAbility("Jump_right", new Ability());
            AddAbility("Run_right", new Run(350f, 116.66f));
            AddAbility("Melle_attack_right", new Ability());
        }

        public override void LoadAbilityButtons(ContentManager content)
        {
            AddAbilityButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Jump_left", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Heal", content.Load<Texture2D>("Icons/Icon_11"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Jump_right", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
        }
    }
}
