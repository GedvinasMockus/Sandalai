﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Classes.PlayerDecorators;
using SwordsAndSandals.Objects.Items.Weapons;
using SwordsAndSandals.Objects.Stats;
using System;
using System.Collections.Generic;

namespace SwordsAndSandals.Objects.Classes
{
    public abstract class Player : AnimatedSprite
    {
        public virtual Attributes BaseAttributes { get; set; }
        public virtual int CorrectionY { get; set; }
        public virtual Dictionary<string, Button> Buttons { get; private set; }
        public virtual Dictionary<string, EventHandler> Handlers { get; private set; }
        public virtual Ability Active { get; set; }

        protected Dictionary<string, Ability> Abilities;
        public MeleeWeapon Melee { get; set; }
        public RangedWeapon Ranged { get; set; }
        public ShieldWeapon Shield { get; set; }

        public Player() : base()
        {
            Abilities = new Dictionary<string, Ability>();
            Buttons = new Dictionary<string, Button>();
            Handlers = new Dictionary<string, EventHandler>();
        }

        public virtual void AddAbility(string name, Ability ability)
        {
            Abilities.Add(name, ability);
        }

        public virtual void RemoveAbility(string name)
        {
            Abilities.Remove(name);
        }

        public virtual void AddButton(string name, Texture2D texture, float scale, SpriteEffects flip)
        {
            EventHandler handler = (o, e) =>
            {
                ConnectionManager.Instance.Invoke("AbilityUsed", name);
                UseAbility(name);
            };
            Handlers.Add(name, handler);
            Button button = new Button(texture, scale, flip);
            button.Click += handler;
            Buttons.Add(name, button);
        }

        public virtual void RemoveButton(string name)
        {
            EventHandler handler = Handlers[name];
            Handlers.Remove(name);
            Button button = Buttons[name];
            Buttons.Remove(name);
            button.Click -= handler;
        }

        public virtual void UseAbility(string name)
        {
            Active = Abilities[name];
            Active.Prepare(this);
            Active.done = false;
        }

        public virtual void ChangeFlip(SpriteEffects flip)
        {
            foreach (var a in Abilities.Values)
            {
                a.animation.Flip = flip;
            }
        }

        //public override void Draw(SpriteBatch batch)
        //{
        //    //animation.Draw(batch, new Vector2(Position.X, Position.Y - animation.Scale * animation.frameHeight/2), Origin);
        //    //int numIcons = buttons.Count;
        //    //float radius = animation.Scale * animation.frameHeight/2;
        //    //float angleIncrement = MathHelper.TwoPi / numIcons;
        //    //int index = 0;
        //    //foreach (var b in buttons.Values)
        //    //{
        //    //    float angle = index * angleIncrement;
        //    //    float xOffset = -(float)Math.Sin(angle) * radius;
        //    //    float yOffset = -(float)Math.Cos(angle) * radius;
        //    //    b.Position = new Vector2(Position.X + xOffset, Position.Y - animation.Scale * (animation.frameHeight / 2 - CorrectionY) + yOffset);
        //    //    b.Draw(batch);
        //    //    index++;
        //    //}

        //    //if(Melee != null) Melee.Draw(batch);
        //    //if(Ranged != null) Ranged.Draw(batch);
        //    //if(Shield != null) Shield.Draw(batch);
        //}

        //public override void Update(GameTime gameTime, List<Sprite> sprites)
        //{
        //    //foreach (var b in buttons.Values)
        //    //{
        //    //    b.Update(gameTime);
        //    //}
        //    animation.Update(gameTime);
        //    Active.Update(gameTime, this, sprites);
        //    if (Active != abilities["Idle"] && Active.done == true)
        //    {
        //        Active = abilities["Idle"];
        //        Active.Prepare(this);
        //    }
        //}
    }
}
