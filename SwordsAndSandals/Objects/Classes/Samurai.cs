using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public class Samurai : Player
    {
        public override void LoadStartInfo(ContentManager content, Vector2 position, SpriteEffects flip)
        {
            Animation idle = new SamuraiIdleAnimation(content, 0.1f, flip);
            sprite = new AnimatedSprite(idle, position);
            AddAbility("Idle", new Idle(idle));
            currentAbility = abilities["Idle"];
            AddAbility("Sleep", new Idle(new SamuraiIdleAnimation(content, 0.1f, flip)));
            AddAbility("Jump_left", new Jump(-350f,600f, position.Y, new SamuraiJumpAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            AddAbility("Melee_attack_left", new Idle(new SamuraiIdleAnimation(content, 0.1f, flip)));
            AddAbility("Run_left", new Run(-300f, new SamuraiRunAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            AddAbility("Shield", new Idle(new SamuraiIdleAnimation(content, 0.1f, flip)));
            AddAbility("Run_right", new Run(300f, new SamuraiRunAnimation(content, 0.1f, SpriteEffects.None)));
            AddAbility("Melee_attack_right", new Idle(new SamuraiIdleAnimation(content, 0.1f, flip)));
            AddAbility("Jump_right", new Jump(350f, 600f, position.Y, new SamuraiJumpAnimation(content, 0.1f, SpriteEffects.None)));
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

        public override void AddWeapons(WeaponFactory factory, ContentManager content)
        {
            melee = factory.CreateMeleeWeapon(content, new Vector2(32, 32), 10);
            ranged = factory.CreateRangedWeapon(content, new Vector2(32, 96), 3);
            shield = factory.CreateShieldWeapon(content, new Vector2(32, 160), 10);
        }
    }
}
