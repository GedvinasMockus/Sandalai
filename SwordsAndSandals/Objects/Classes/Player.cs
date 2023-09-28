using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects.Abilities;

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
        protected AnimatedSprite dAnimation;
        protected AnimatedSprite cAnimation;

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
            currentAbility.active = true;
            currentAbility.done = false;
        }

        public void Draw(SpriteBatch batch)
        {
            cAnimation.Draw(batch, new Vector2(position.X, position.Y - cAnimation.scale * cAnimation.frameHeight / 2), new Vector2(cAnimation.frameWidth / 2, cAnimation.frameHeight / 2));
            int numIcons = abilities.Count;
            float radius = cAnimation.scale * cAnimation.frameHeight/2;
            float angleIncrement = MathHelper.TwoPi / numIcons;
            int index = 0;
            foreach (var b in buttons.Values)
            {
                float angle = index * angleIncrement;
                float xOffset = -(float)Math.Sin(angle) * radius;
                float yOffset = -(float)Math.Cos(angle) * radius;
                b.Position = new Vector2(position.X + xOffset, position.Y - cAnimation.scale * cAnimation.frameHeight / 2 + centerY * cAnimation.scale + yOffset);
                b.Draw(batch);
                index++;
            }
        }

        public void Update(GameTime gameTime)
        {
            cAnimation.Update(gameTime);
            foreach (var b in buttons.Values)
            {
                b.Update(gameTime);
            }
            if (currentAbility != null)
            {
                currentAbility.Update(gameTime, this);
                if (currentAbility.done == true)
                {
                    currentAbility = null;
                }
            }
        }
        public abstract void LoadStartInfo(ContentManager content, SpriteEffects flip);
        public abstract void LoadButtons(ContentManager content);
    }
}
