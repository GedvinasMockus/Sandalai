using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes.PlayerDecorators
{
    public abstract class PlayerDecorator : Player
    {
        protected Player wrapee;

        public override Attributes BaseAttributes { get => wrapee.BaseAttributes; set => wrapee.BaseAttributes = value; }
        public override int CorrectionY { get => wrapee.CorrectionY; set => wrapee.CorrectionY = value; }
        public override Ability Active { get => wrapee.Active; set => wrapee.Active = value; }
        public override Vector2 Position { get => wrapee.Position; set => wrapee.Position = value; }
        public override Vector2 Origin { get => wrapee.Origin; set => wrapee.Origin = value; }
        public override Animation animation { get => wrapee.animation; set => wrapee.animation = value; }
        public override Rectangle Rectangle => wrapee.Rectangle;

        public PlayerDecorator(Player p)
        {
            wrapee = p;
        }
        public override void AddAbility(string name, Ability ability)
        {
            wrapee.AddAbility(name, ability);
        }
        public override void RemoveAbility(string name)
        {
            wrapee.RemoveAbility(name);
        }
        public override void UseAbility(string name)
        {
            wrapee.UseAbility(name);
        }
        public override void ChangeFlip(SpriteEffects flip)
        {
            wrapee.ChangeFlip(flip);
        }
        public override void AddButton(string name, Texture2D texture, float scale, SpriteEffects flip)
        {
            wrapee.AddButton(name, texture, scale, flip);
        }

        public override void RemoveButton(string name)
        {
            wrapee.RemoveButton(name);
        }

        public override List<Button> GetButtonValues()
        {
            return wrapee.GetButtonValues();
        }
        public override List<string> GetButtonKeys()
        {
            return wrapee.GetButtonKeys();
        }
        public override int GetButtonCount()
        {
            return wrapee.GetButtonCount();
        }
        public override void Draw(SpriteBatch batch)
        {
            wrapee.Draw(batch);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            wrapee.Update(gameTime, sprites);
        }

    }
}
