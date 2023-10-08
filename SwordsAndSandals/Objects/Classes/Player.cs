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

        protected int correctionY;
        protected Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();
        protected Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        protected Dictionary<string, EventHandler> handlers = new Dictionary<string, EventHandler>();
        protected Ability active;

        protected MeleeWeapon melee;
        protected RangedWeapon ranged;
        protected ShieldWeapon shield;

        protected Player(Animation animation, Vector2 position) : base(animation, position)
        {
            active = new Idle(animation);
            abilities.Add("Idle", active);
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
            active = abilities[name];
            active.Prepare(this);
            active.done = false;
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
                b.Position = new Vector2(Position.X + xOffset, Position.Y - animation.Scale * (animation.frameHeight / 2 - correctionY) + yOffset);
                b.Draw(batch);
                index++;
            }

            if(melee != null) melee.Draw(batch);
            if(ranged != null) ranged.Draw(batch);
            if(shield != null) shield.Draw(batch);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (var b in buttons.Values)
            {
                b.Update(gameTime);
            }
            animation.Update(gameTime);
            active.Update(gameTime, this, sprites);
            if (active != abilities["Idle"] && active.done == true)
            {
                active = abilities["Idle"];
                active.Prepare(this);
            }
        }
        public abstract void LoadStartInfo(ContentManager content, SpriteEffects flip);
        public abstract void LoadButtons(ContentManager content);
        public abstract void AddWeapons(WeaponFactory factory, ContentManager content);
    }
}
