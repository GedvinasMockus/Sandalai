using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Items.Weapons;
using System;
using System.Collections.Generic;

namespace SwordsAndSandals.Objects.Classes
{
    public abstract class Player
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }

        public event EventHandler<AbilityUsedEventArgs> AbilityUsed;

        protected int centerY;
        protected Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();
        protected Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        protected Dictionary<string, EventHandler> handlers = new Dictionary<string, EventHandler>();
        protected Ability currentAbility;
        protected MeleeWeapon melee;
        protected RangedWeapon ranged;
        protected ShieldWeapon shield;

        public Player(Vector2 position)
        {
            this.position = position;
            velocity = new Vector2(0, 0);
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
            currentAbility = abilities[name];
            currentAbility.done = false;
        }

        public void Draw(SpriteBatch batch)
        {
            currentAbility.Draw(batch, this);
            int numIcons = buttons.Count;
            float radius = currentAbility.Animation.scale * currentAbility.Animation.frameHeight/2;
            float angleIncrement = MathHelper.TwoPi / numIcons;
            int index = 0;
            foreach (var b in buttons.Values)
            {
                float angle = index * angleIncrement;
                float xOffset = -(float)Math.Sin(angle) * radius;
                float yOffset = -(float)Math.Cos(angle) * radius;
                b.Position = new Vector2(position.X + xOffset, position.Y - currentAbility.Animation.scale * currentAbility.Animation.frameHeight / 2 + centerY * currentAbility.Animation.scale + yOffset);
                b.Draw(batch);
                index++;
            }
            if(melee != null) melee.Draw(batch);
            if(ranged != null) ranged.Draw(batch);
            if(shield != null) shield.Draw(batch);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var b in buttons.Values)
            {
                b.Update(gameTime);
            }
            currentAbility.Update(gameTime, this);
            if (currentAbility.done == true)
            {
                currentAbility = abilities["Idle"];
            }
        }
        public abstract void LoadStartInfo(ContentManager content, SpriteEffects flip);
        public abstract void LoadButtons(ContentManager content);
        public abstract void AddWeapons(WeaponFactory factory, ContentManager content);
    }
}
