using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Classes;

namespace SwordsAndSandals.Objects.Abilities
{
    public abstract class Ability
    {
        public bool done { get; set; }

        public AnimatedSprite Animation { get; private set; }

        public Ability(AnimatedSprite animation)
        {
            done = true;
            Animation = animation;
        }

        public abstract void Update(GameTime gameTime, Player player);

        public void Draw(SpriteBatch batch, Player player)
        {
            Animation.Draw(batch, new Vector2(player.position.X, player.position.Y - Animation.scale * Animation.frameHeight / 2), new Vector2(Animation.frameWidth / 2, Animation.frameHeight / 2));
        }
    }
}