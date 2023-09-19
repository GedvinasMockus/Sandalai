using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;

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
        private Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();
        private Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        private Dictionary<string, EventHandler> handlers = new Dictionary<string, EventHandler>();
        private Ability currentAbility;


        public Player(Texture2D texture, Vector2 position, float scale)
        {
            this.texture = texture;
            this.position = position;
            frameWidth = texture.Width;
            frameHeight = texture.Height;
            this.scale = scale;
            currentAbility = null;
            velocity = new Vector2(0, 0);
        }

        public Player(Texture2D texture, Vector2 position, float scale, int width, int height)
        {
            this.texture = texture;
            this.position = position;
            frameWidth = width;
            frameHeight = height;
            this.scale = scale;
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
            Button button = new Button(bTexture,scale,flip);
            int index = buttons.Count;
            button.Position = new Vector2(position.X + button.Scale * (frameWidth/2 - frameWidth * (index & 1)), position.Y - button.texture.Height * button.Scale * (index / 2) - button.Scale * button.texture.Height/2);
            button.onClick += handler;
            buttons.Add(name, button);
        }

        public void RemoveAbility(string name)
        {
            abilities.Remove(name);
            buttons[name].onClick -= handlers[name];
            handlers.Remove(name);
            buttons.Remove(name);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, new Vector2(position.X, position.Y - scale * frameHeight/2), new Rectangle(0, 0, frameWidth, frameHeight), Color.White, 0.0f, new Vector2(frameWidth / 2, frameHeight / 2), scale, SpriteEffects.None, 1);

            int index = 0;
            foreach(var b in buttons.Values)
            {
                b.Position = new Vector2(position.X + b.Scale * (frameWidth / 2 - frameWidth * (index & 1)), position.Y - b.texture.Height * b.Scale * (index / 2) - b.Scale * b.texture.Height / 2);
                b.Draw(batch);
                index++;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(var b in buttons.Values)
            {
                b.Update(gameTime);
            }
            if (currentAbility != null)
            {
                currentAbility.Update(gameTime, this);
                if(currentAbility.done == true)
                {
                    currentAbility = null;
                    velocity = new Vector2(0, 0);
                }
            }
        }
    }
}
