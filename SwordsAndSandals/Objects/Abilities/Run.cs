using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(currDistanceX <= maxDistanceX)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                if (currDistanceX < maxDistanceX/2)
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
                currDistanceX = 0;
            }
        }
    }
}
