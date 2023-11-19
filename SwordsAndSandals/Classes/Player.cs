using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Abilities;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Items;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwordsAndSandals.Classes
{
    public abstract class Player : AnimatedSprite
    {
        private event EventHandler AbilityDone;
        public virtual Attributes BaseAttributes { get; set; }
        public virtual int CorrectionY { get; set; }
        public virtual Ability Active { get; set; }

        protected Dictionary<string, Button> Buttons = new Dictionary<string, Button>();
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
            Button button = new Button(texture, scale, flip);
            button.Click += (o, e) =>
            {
                ConnectionManager.Instance.Invoke("AbilityUsed", name);
                UseAbility(name);
            };
            Buttons.Add(name, button);
        }

        public virtual void RemoveButton(string name)
        {
            Button button = Buttons[name];
            Buttons.Remove(name);
            button.RemoveAllHandlers();
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
            Active.prepared = false;
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
    }
}
