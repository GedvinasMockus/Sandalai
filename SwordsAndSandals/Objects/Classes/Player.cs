using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Items.Weapons;
using SwordsAndSandals.Objects.Stats;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SwordsAndSandals.Objects.Classes
{
    public abstract class Player : AnimatedSprite
    {
        private event EventHandler AbilityDone;
        public virtual Attributes BaseAttributes { get; set; }
        public virtual int CorrectionY { get; set; }
        public virtual Ability Active { get; set; }

        protected Dictionary<string, Button> Buttons = new Dictionary<string, Button>();
        protected Dictionary<string, EventHandler> Handlers = new Dictionary<string, EventHandler>();
        protected Dictionary<string, Ability> Abilities = new Dictionary<string, Ability>();
        public MeleeWeapon Melee { get; set; }
        public RangedWeapon Ranged { get; set; }
        public ShieldWeapon Shield { get; set; }

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

        public virtual List<Button> GetButtonValues()
        {
            return Buttons.Values.ToList();
        }

        public virtual List<string> GetButtonKeys()
        {
            return Buttons.Keys.ToList();
        }

        public virtual int GetButtonCount()
        {
            return Buttons.Count();
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
        public void AbilityFinished()
        {
            AbilityDone?.Invoke(this, new EventArgs());
        }

        public virtual void AddAbilityDoneHandler(EventHandler handler)
        {
            AbilityDone += handler;
        }

        public virtual void RemoveAbilityDoneHandler(EventHandler handler)
        {
            AbilityDone -= handler;
        }

        public static Player GetNewPlayer(PlayerFactory playerFactory, ContentManager content, Vector2 position, SpriteEffects flip, Attributes attributes, bool setButtons)
        {
            Player player = playerFactory.CreatePlayer(content, position, flip, attributes, setButtons);
            return player;
        }
    }
}
