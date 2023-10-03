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
    public class Samurai : Player
    {
        public Samurai(Vector2 position) : base(position)
        {

        }



        public override void LoadStartInfo(ContentManager content, SpriteEffects flip)
        {
            AddAbility("Idle", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip)));
            currentAbility = abilities["Idle"];
            AddAbility("Sleep", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Jump_left", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Melee_attack_left", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Run_left", new Run(300f, -100f, new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Run"), 3.0f, 0.2f, flip)));
            AddAbility("Shield", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Run_right", new Run(300f, 100f, new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Run"), 3.0f, 0.2f, flip)));
            AddAbility("Melle_attack_right", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Jump_right", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip)));
        }

        public override void LoadButtons(ContentManager content)
        {
            centerY = 18;
            AddAbilityButton("Sleep", content.Load<Texture2D>("Icons/Icon_05"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Jump_left", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Shield", content.Load<Texture2D>("Icons/Icon_18"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Jump_right", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.None);
        }

    }
}
