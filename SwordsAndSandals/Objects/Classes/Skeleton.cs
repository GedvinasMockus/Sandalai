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
    public class Skeleton : Player
    {
        public override void LoadStartInfo(ContentManager content, Vector2 position ,SpriteEffects flip)
        {
            Animation idle = new SkeletonIdleAnimation(content, 0.1f, flip);
            sprite = new AnimatedSprite(idle, position);
            AddAbility("Idle", new Idle(idle));
            currentAbility = abilities["Idle"];
            AddAbility("Sleep", new Idle(new SkeletonIdleAnimation(content, 0.1f, flip)));
            AddAbility("Ranged_attack_left", new Idle(new SkeletonIdleAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            AddAbility("Melee_attack_left", new Idle(new SkeletonIdleAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            AddAbility("Run_left", new Run(-200f, new SkeletonWalkAnimation(content, 0.1f, SpriteEffects.FlipHorizontally)));
            AddAbility("Evasion", new Idle(new SkeletonIdleAnimation(content, 0.1f, flip)));
            AddAbility("Run_right", new Run(200f, new SkeletonWalkAnimation(content, 0.1f, SpriteEffects.None)));
            AddAbility("Melee_attack_right", new Idle(new SkeletonIdleAnimation(content, 0.1f, SpriteEffects.None)));
            AddAbility("Ranged_attack_right", new Idle(new SkeletonIdleAnimation(content, 0.1f, SpriteEffects.None)));
        }

        public override void LoadButtons(ContentManager content)
        {
            centerY = 32;
            AddAbilityButton("Sleep", content.Load<Texture2D>("Icons/Icon_05"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Ranged_attack_left", content.Load<Texture2D>("Icons/Icon_34"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Evasion", content.Load<Texture2D>("Icons/Icon_17"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Ranged_attack_right", content.Load<Texture2D>("Icons/Icon_34"), 2.0f, SpriteEffects.FlipHorizontally);
        }

        public override void AddWeapons(WeaponFactory factory, ContentManager content)
        {
            melee = factory.CreateMeleeWeapon(content, new Vector2(32, 32), 8);
            ranged = factory.CreateRangedWeapon(content, new Vector2(32, 96), 12);
            shield = factory.CreateShieldWeapon(content, new Vector2(32, 160), 6);
        }
    }
}
