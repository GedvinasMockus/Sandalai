using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Items.Weapons;
using System;
using System.Collections.Generic;

namespace SwordsAndSandals.Objects.Classes
{
    public abstract class Player : AnimatedSprite
    {
        public event EventHandler<AbilityUsedEventArgs> AbilityUsed;

        public int CorrectionY { get; set; }

        protected Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();
        protected Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        protected Dictionary<string, EventHandler> handlers = new Dictionary<string, EventHandler>();

        public Ability Active { get; set; }

        public MeleeWeapon Melee { get; set; }
        public RangedWeapon Ranged { get; set; }
        public ShieldWeapon Shield { get; set; }

        public Player() : base()
        {

        }

        public void AddAbility(string name, Ability ability)
        {
            abilities.Add(name, ability);
        }

        public void AddAbilityButton(string name, Texture2D bTexture, float bScale, SpriteEffects bFlip)
        {
            EventHandler handler = (o, e) =>
            {
                AbilityUsedEventArgs args = new AbilityUsedEventArgs();
                args.Name = name;
                AbilityUsed?.Invoke(this, args);
                UseAbility(name);
            };
            handlers.Add(name, handler);
            Button button = new Button(bTexture, bScale, bFlip);
            button.Click += handler;
            buttons.Add(name, button);
        }

        public void RemoveAbility(string name)
        {
            abilities.Remove(name);
            Button button;
            if (buttons.TryGetValue(name, out button))
            {
                button.Click -= handlers[name];
                handlers.Remove(name);
                buttons.Remove(name);
            }
        }

        public void UseAbility(string name)
        {
            Active = abilities[name];
            Active.Prepare(this);
            Active.done = false;
        }

        public override void Draw(SpriteBatch batch)
        {
            animation.Draw(batch, new Vector2(Position.X, Position.Y - animation.Scale * animation.frameHeight/2), Origin);
            int numIcons = buttons.Count;
            float radius = animation.Scale * animation.frameHeight/2;
            float angleIncrement = MathHelper.TwoPi / numIcons;
            int index = 0;
            foreach (var b in buttons.Values)
            {
                float angle = index * angleIncrement;
                float xOffset = -(float)Math.Sin(angle) * radius;
                float yOffset = -(float)Math.Cos(angle) * radius;
                b.Position = new Vector2(Position.X + xOffset, Position.Y - animation.Scale * (animation.frameHeight / 2 - CorrectionY) + yOffset);
                b.Draw(batch);
                index++;
            }

            if(Melee != null) Melee.Draw(batch);
            if(Ranged != null) Ranged.Draw(batch);
            if(Shield != null) Shield.Draw(batch);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (var b in buttons.Values)
            {
                b.Update(gameTime);
            }
            animation.Update(gameTime);
            Active.Update(gameTime, this, sprites);
            if (Active != abilities["Idle"] && Active.done == true)
            {
                Active = abilities["Idle"];
                Active.Prepare(this);
            }
        }
    }
}
