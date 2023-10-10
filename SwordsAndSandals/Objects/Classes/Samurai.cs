using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
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
        public Samurai(Vector2 position) : base(position)
        {

        }



        public override void LoadStartInfo(ContentManager content, SpriteEffects flip)
        {
            SpriteEffects flipLeft = SpriteEffects.FlipHorizontally;
            SpriteEffects flipRight = SpriteEffects.None;
            if (flip == SpriteEffects.FlipHorizontally)
            {
                flipLeft = SpriteEffects.None;
                flipRight = flip;
            }
            AddAbility("Idle", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip)));
            currentAbility = abilities["Idle"];
            AddAbility("Sleep", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Jump_left", new Jump(-350,600,position.Y,new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Jump"), 3.0f, 0.1f, flip == SpriteEffects.None ? flipLeft : flipRight)));
            AddAbility("Melee_attack_left", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip == SpriteEffects.None ? flipLeft : flipRight)));
            AddAbility("Run_left", new Run(-250, new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Run"), 3.0f, 0.1f, flip == SpriteEffects.None ? flipLeft : flipRight)));
            AddAbility("Shield", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip)));
            AddAbility("Run_right", new Run(250, new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Run"), 3.0f, 0.1f, flip == SpriteEffects.None ? flipRight : flipLeft)));
            AddAbility("Melle_attack_right", new Idle(new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Idle"), 3.0f, 0.1f, flip == SpriteEffects.None ? flipRight : flipLeft)));
            AddAbility("Jump_right", new Jump(350, 600, position.Y, new AnimatedSprite(content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Jump"), 3.0f, 0.1f, flip == SpriteEffects.None ? flipRight : flipLeft)));
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
