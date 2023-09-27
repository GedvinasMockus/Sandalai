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
    public class Skeleton : Player
    {
        public Skeleton(Vector2 position, float scale, int centerY, SpriteEffects effect) : base(position, scale, centerY, effect)
        {

        }
        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("Character/Skeleton/Skeleton_Archer/Idle");
            totalFrames = texture.Width / texture.Height;
            frameWidth = texture.Width / this.totalFrames;
            frameHeight = texture.Height;
        }
        public override void LoadAbilites(ContentManager content)
        {
            AddAbility("Ranged_attack_left", new Ability());
            AddAbility("Melee_attack_left", new Ability());
            AddAbility("Run_left", new Run(250f, -83.33f));
            AddAbility("Evasion", new Ability());
            AddAbility("Sleep", new Ability());
            AddAbility("Run_right", new Run(250f, 83.33f));
            AddAbility("Melee_attack_right", new Ability());
            AddAbility("Ranged_attack_right", new Ability());
        }

        public override void LoadAbilityButtons(ContentManager content)
        {
            AddAbilityButton("Ranged_attack_left", content.Load<Texture2D>("Icons/Icon_34"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            AddAbilityButton("Evasion", content.Load<Texture2D>("Icons/Icon_17"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Sleep", content.Load<Texture2D>("Icons/Icon_05"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
            AddAbilityButton("Ranged_attack_right", content.Load<Texture2D>("Icons/Icon_34"), 2.0f, SpriteEffects.FlipHorizontally);
        }
    }
}
