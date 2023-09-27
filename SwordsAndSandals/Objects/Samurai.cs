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
    public class Samurai : Player
    {
        public Samurai(Vector2 position, float scale, int centerY, SpriteEffects effect) : base(position, scale, centerY, effect)
        {
            
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle");
            totalFrames = texture.Width / texture.Height;
            frameWidth = texture.Width / this.totalFrames;
            frameHeight = texture.Height;
        }
        public override void LoadAbilites(ContentManager content)
        {
            AddAbility("Melee_attack_left", new Ability());
            AddAbility("Run_left", new Run(300f, -100f));
            AddAbility("Jump_left", new Ability());
            AddAbility("Shield", new Ability());
            AddAbility("Sleep", new Ability());
            AddAbility("Jump_right", new Ability());
            AddAbility("Run_right", new Run(300f, 100f));
            AddAbility("Melle_attack_right", new Ability());
        }

        public override void LoadAbilityButtons(ContentManager content)
        {
            AddAbilityButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Jump_left", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Shield", content.Load<Texture2D>("Icons/Icon_18"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Sleep", content.Load<Texture2D>("Icons/Icon_05"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Jump_right", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
        }
    }
}
