using Microsoft.Xna.Framework;
using SwordsAndSandals.Objects.Classes;
using System;

namespace SwordsAndSandals.Objects.Abilities
{
    public class Run : Ability
    {
        private float maxDistanceX;
        private float currDistanceX;
        private float acceleration;

        public Run(float distanceX, float acceleration) : base()
        {
            maxDistanceX = distanceX;
            currDistanceX = 0;
            this.acceleration = acceleration;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if (currDistanceX <= maxDistanceX)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                if (currDistanceX < maxDistanceX / 2)
                {
                    float delta = player.velocity.X * elapsed + acceleration * elapsed * elapsed / 2;
                    player.position = new Vector2(player.position.X + delta, player.position.Y);
                    player.velocity = new Vector2(player.velocity.X + acceleration * elapsed, player.velocity.Y);
                    currDistanceX += Math.Abs(delta);
                }
                else
                {
                    float delta = player.velocity.X * elapsed - acceleration * elapsed * elapsed / 2;
                    player.position = new Vector2(player.position.X + player.velocity.X * elapsed - acceleration * elapsed * elapsed / 2, player.position.Y);
                    player.velocity = new Vector2(player.velocity.X - acceleration * elapsed, player.velocity.Y);
                    currDistanceX += Math.Abs(delta);
                }
            }
            else
            {
                done = true;
                active = false;
                player.velocity = new Vector2(0, 0);
                currDistanceX = 0;
            }
        }
    }
}