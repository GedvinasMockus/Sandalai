using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects.Abilities;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.Objects
{
    public abstract class Player
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }

        public event EventHandler<AbilityUsedEventArgs> AbilityUsed;

        protected Texture2D texture;
        protected int frameWidth;
        protected int frameHeight;
        protected float scale;
        protected int centerY;
        protected int totalFrames;
        protected int currentFrame = 0;
        protected float animationSpeed = 0.1f;
        protected float animationTimer = 0f;
        protected Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();
        protected Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        protected Dictionary<string, EventHandler> handlers = new Dictionary<string, EventHandler>();
        protected Ability currentAbility;
        protected SpriteEffects effect;

        public Player(Vector2 position, float scale, int centerY, SpriteEffects effect)
        {
            this.position = position;
            this.scale = scale;
            this.centerY = centerY;
            currentAbility = null;
            velocity = new Vector2(0, 0);
            this.effect = effect;
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
            Rectangle sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            batch.Draw(texture, new Vector2(position.X, position.Y - scale * frameHeight / 2), sourceRectangle, Color.White, 0.0f, new Vector2(frameWidth / 2, frameHeight / 2), scale, effect, 1);

            int numIcons = abilities.Count;
            float radius = scale * (frameWidth - centerY) * (float)Math.Sqrt(2);
            float angleIncrement = MathHelper.TwoPi / numIcons;
            int index = 0;
            foreach (var b in buttons.Values)
            {
                float angle = index * angleIncrement;
                float xOffset = -(float)Math.Sin(angle) * (radius);
                float yOffset = -(float)Math.Cos(angle) * (radius);
                b.Position = new Vector2(position.X + xOffset, position.Y + yOffset - (frameHeight - centerY) * scale);
                b.Draw(batch);
                index++;
            }
        }

        public void Update(GameTime gameTime)
        {
            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer >= animationSpeed)
            {
                currentFrame = (currentFrame + 1) % totalFrames;
                animationTimer = 0f;
            }
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
        public abstract void LoadTexture(ContentManager content);
        public abstract void LoadAbilites(ContentManager content);
        public abstract void LoadAbilityButtons(ContentManager content);
    }
}
