using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public class Kunoichi : Player
    {
        public override void LoadStartInfo(ContentManager content, Vector2 position, SpriteEffects flip)
        {
            Animation idle = new KunoichiIdleAnimation(content, 0.1f, flip);
            sprite = new AnimatedSprite(idle, position);
            AddAbility("Idle", new Idle(idle));
            currentAbility = abilities["Idle"];
            AddAbility("Heal", new Idle(new KunoichiIdleAnimation(content, 0.1f, flip)));
            AddAbility("Jump_left", new Jump(-400f, 600f, position.Y, new KunoichiJumpAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            AddAbility("Melee_attack_left", new Idle(new KunoichiIdleAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            AddAbility("Run_left", new Run(-350f, new KunoichiRunAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            AddAbility("Run_right", new Run(350f, new KunoichiRunAnimation(content, 0.1f, SpriteEffects.None)));
            AddAbility("Melle_attack_right", new Idle(new KunoichiIdleAnimation(content, 0.1f, SpriteEffects.None)));
            AddAbility("Jump_right", new Jump(400f, 600f, position.Y, new KunoichiJumpAnimation(content, 0.1f, SpriteEffects.None)));
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

        public override void AddWeapons(WeaponFactory factory, ContentManager content)
        {
            melee = factory.CreateMeleeWeapon(content, new Vector2(32, 32), 12);
            ranged = factory.CreateRangedWeapon(content, new Vector2(32, 96),7);
            shield = factory.CreateShieldWeapon(content, new Vector2(32,160),4);
        }
    }
}
