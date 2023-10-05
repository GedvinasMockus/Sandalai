using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Classes;

namespace SwordsAndSandals.Objects.Abilities
{
    public abstract class Ability
    {
        public bool done { get; set; }

        public Animation animation { get; private set; }

        public Ability(Animation animation)
        {
            done = true;
            this.animation = animation;
        }

        public virtual void Update(GameTime gameTime, AnimatedSprite player)
        {
            player.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch batch, AnimatedSprite player)
        {
            player.DrawAsPlayer(batch);
        }
    }
}