using Microsoft.Xna.Framework;
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

        public Jump(float distanceX, float velocityX, float accelerationY, float groundY, AnimatedSprite animation) : base(animation)
        {
            maxDistanceX = distanceX;
            this.velocityX = velocityX;
            this.accelerationY = accelerationY;
            float time = maxDistanceX / Math.Abs(velocityX);
            velocityY = - accelerationY * time / 2;
            this.groundY = groundY;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            Animation.Update(gameTime);
            if (currDistanceX < maxDistanceX)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                player.velocity = new Vector2(velocityX, player.velocity.Y + accelerationY * elapsed);
                Vector2 delta = new Vector2(player.velocity.X * elapsed, (player.velocity.Y + velocityY) * elapsed + accelerationY * elapsed * elapsed / 2);
                player.position = new Vector2(player.position.X + delta.X, Math.Min(groundY, player.position.Y + delta.Y));
                currDistanceX += Math.Abs(delta.X);
            }
            else
            {
                done = true;
                player.velocity = Vector2.Zero;
                currDistanceX = 0;
            }
        }
    }
}
