using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Classes;
using System;

namespace SwordsAndSandals.Objects.Abilities
{
    public class Run : Ability
    {
        private float maxDistanceX;
        private float currDistanceX;
        private float acceleration;

        public Run(float distanceX, float acceleration, AnimatedSprite animation) : base(animation)
        {
            maxDistanceX = distanceX;
            currDistanceX = 0;
            this.acceleration = acceleration;
        }


        public override void Update(GameTime gameTime, Player player)
        {
            Animation.Update(gameTime);
            if (currDistanceX <= maxDistanceX)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                if (currDistanceX < maxDistanceX / 2)
                {
                    float delta = player.velocity.X * elapsed + acceleration * elapsed * elapsed / 2;
                    player.position = new Vector2(player.position.X + delta, player.position.Y);
                    player.velocity = new Vector2(player.velocity.X + acceleration * elapsed, player.velocity.Y);
                    currDistanceX += Math.Abs(delta);
                    Animation.CurrentAnimationSpeed = Animation.DefaultAnimationSpeed - 0.7f * Animation.DefaultAnimationSpeed * 2 * currDistanceX / maxDistanceX;
                }
                else
                {
                    float delta = player.velocity.X * elapsed - acceleration * elapsed * elapsed / 2;
                    player.position = new Vector2(player.position.X + player.velocity.X * elapsed - acceleration * elapsed * elapsed / 2, player.position.Y);
                    player.velocity = new Vector2(player.velocity.X - acceleration * elapsed, player.velocity.Y);
                    currDistanceX += Math.Abs(delta);
                    Animation.CurrentAnimationSpeed = Animation.DefaultAnimationSpeed - 0.7f * Animation.DefaultAnimationSpeed + 0.7f * Animation.DefaultAnimationSpeed * (2 * currDistanceX/maxDistanceX - 1);
                }
            }
            else
            {
                done = true;
                player.velocity = new Vector2(0, 0);
                currDistanceX = 0;
            }
        }
    }
}