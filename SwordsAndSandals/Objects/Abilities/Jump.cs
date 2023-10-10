using Microsoft.Xna.Framework;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Abilities
{
    public class Jump : Ability
    {
        private float maxDistanceX;
        private float currDistanceX;
        private float velocityX;
        private float groundY;
        private float velocityY;
        private float accelerationY;

        public Jump(float distanceX, float accelerationY, Animation animation) : base(animation)
        {
            maxDistanceX = distanceX;
            this.accelerationY = accelerationY;
            velocityX = maxDistanceX / animation.Duration;
            velocityY = - accelerationY * animation.Duration / 2;
        }

        public override void Prepare(Player player)
        {
            animation.Reset();
            player.animation = animation;
            player.Velocity = new Vector2(velocityX, velocityY);
            groundY = player.Position.Y;
        }

        public override void Update(GameTime gameTime, Player player, List<Sprite> sprites)
        {
            if (currDistanceX < Math.Abs(maxDistanceX))
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                player.Velocity = new Vector2(player.Velocity.X, player.Velocity.Y + accelerationY * elapsed);
                Vector2 delta = new Vector2(player.Velocity.X * elapsed, player.Velocity.Y * elapsed + accelerationY * elapsed * elapsed / 2);
                player.Position = new Vector2(player.Position.X + delta.X, Math.Min(groundY, player.Position.Y + delta.Y));
                currDistanceX += Math.Abs(delta.X);
            }
            else
            {
                done = true;
                player.Velocity = Vector2.Zero;
                currDistanceX = 0;
            }
        }
    }
}
