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
    public class Skeleton : Player
    {
        public Skeleton(Vector2 position) : base(position)
        {

        }
        public override void LoadStartInfo(ContentManager content, SpriteEffects flip)
        {
            AnimatedSprite animation = new AnimatedSprite(content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Idle"), 3.0f, 0.1f, flip);
            dAnimation = animation;
            cAnimation = animation;
            AddAbility("Sleep", new Ability());
            AddAbility("Ranged_attack_left", new Ability());
            AddAbility("Melee_attack_left", new Ability());
            AddAbility("Run_left", new Run(250f, -83.33f));
            AddAbility("Evasion", new Ability());
            AddAbility("Run_right", new Run(250f, 83.33f));
            AddAbility("Melee_attack_right", new Ability());
            AddAbility("Ranged_attack_right", new Ability());
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
    }
}
