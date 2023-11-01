using Microsoft.Xna.Framework;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Classes;
using SwordsAndSandals.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Abilities
{
    public class Jump : Ability
    {
        private float maxDistanceX;
        private float currDistanceX;
        private float velocityX;
        private float groundY;
        private float velocityY;
        private float accelerationY;
        private Vector2 velocityHolder;

        public Jump(float distanceX, int degrees, Animation animation) : base(animation)
        {

            maxDistanceX = distanceX;
            velocityX = maxDistanceX / animation.Duration;
            velocityY = -1 * Math.Abs(velocityX) * (float)Math.Tan(MathHelper.ToRadians(degrees));
            accelerationY = -2 * velocityY / animation.Duration;
        }

        public override void Prepare(Player player)
        {
            animation.Reset();
            player.animation = animation;
            currDistanceX = 0;
            velocityHolder = new Vector2(velocityX, velocityY);
            groundY = player.Position.Y;
        }

        public override void Update(GameTime gameTime, Player player, List<Sprite> sprites)
        {
            if (currDistanceX < Math.Abs(maxDistanceX))
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                velocityHolder = new Vector2(velocityHolder.X, velocityHolder.Y + accelerationY * elapsed);
                Vector2 delta = new Vector2(velocityHolder.X * elapsed, velocityHolder.Y * elapsed + accelerationY * elapsed * elapsed / 2);
                player.Position = new Vector2(player.Position.X + delta.X, Math.Min(groundY, player.Position.Y + delta.Y));
                currDistanceX += Math.Abs(delta.X);
            }
            else
            {
                done = true;
            }
        }
    }
}
