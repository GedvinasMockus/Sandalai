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
            AddAbility("Idle", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Ninja/Kunoichi/Idle"), 3.0f, 0.1f, flip)));
            currentAbility = abilities["Idle"];
            AddAbility("Heal", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Ninja/Kunoichi/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Jump_left", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Ninja/Kunoichi/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Melee_attack_left", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Ninja/Kunoichi/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Run_left", new Run(350f, -116.66f, new AnimatedSprite(content.Load<Texture2D>("Character/Ninja/Kunoichi/Run"), 3.0f, 0.1f, flip)));
            AddAbility("Run_right", new Run(350f, 116.66f, new AnimatedSprite(content.Load<Texture2D>("Character/Ninja/Kunoichi/Run"), 3.0f, 0.1f, flip)));
            AddAbility("Melle_attack_right", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Ninja/Kunoichi/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Jump_right", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Ninja/Kunoichi/Idle"), 3.0f, 0.1f, flip)));
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
