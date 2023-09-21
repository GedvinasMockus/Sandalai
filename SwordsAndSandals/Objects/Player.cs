using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects.Abilities;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.Objects
{
    public class Player
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        private Texture2D texture;
        private int frameWidth;
        private int frameHeight;
        private float scale;
        private int centerY;
        private int totalFrames;
        private int currentFrame = 0;
        private float animationSpeed = 0.1f;
        private float animationTimer = 0f;
        private Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();
        private Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        private Dictionary<string, EventHandler> handlers = new Dictionary<string, EventHandler>();
        private Ability currentAbility;

        public Player(Texture2D texture, Vector2 position, float scale, int centerY)
        {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            this.centerY = centerY;
            this.totalFrames = texture.Width / texture.Height;
            frameWidth = texture.Width / this.totalFrames;
            frameHeight = texture.Height;
            currentAbility = null;
            velocity = new Vector2(0, 0);
        }
        public void AddAbility(string name, Ability ability, Texture2D bTexture, float scale, SpriteEffects flip)
        {
            abilities.Add(name, ability);
            Action<object, EventArgs> fun = (o, e) =>
            {
                if (currentAbility == null)
                {
                    currentAbility = ability;
                    currentAbility.active = true;
                    currentAbility.done = false;
                }
            };
            EventHandler handler = new EventHandler(fun);
            handlers.Add(name, handler);
            Button button = new Button(bTexture, scale, flip);
            int index = buttons.Count;
            button.Position = new Vector2(position.X + button.Scale * (frameWidth / 2 - frameWidth * (index & 1)), position.Y - button._texture.Height * button.Scale * (index / 2) - button.Scale * button._texture.Height / 2);
            button.Click += handler;
            buttons.Add(name, button);
        }
        public void RemoveAbility(string name)
        {
            abilities.Remove(name);
            buttons[name].Click -= handlers[name];
            handlers.Remove(name);
            buttons.Remove(name);
        }

        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            Rectangle sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            batch.Draw(texture, new Vector2(position.X, position.Y - scale * frameHeight / 2), sourceRectangle, Color.White, 0.0f, new Vector2(frameWidth / 2, frameHeight / 2), scale, SpriteEffects.None, 1);

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
                b.Draw(gameTime, batch);
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
    }
}
