using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Classes;
using System;

namespace SwordsAndSandals.Objects.Abilities
{
    public class Run : Ability
    {
        private float maxDistanceX;
        private float currDistanceX;
        private float velocityX;

        public Run(float distanceX, Animation animation) : base(animation)
        {
            maxDistanceX = distanceX;
            currDistanceX = 0;
            this.velocityX = maxDistanceX / animation.Duration;
        }


        public override void Update(GameTime gameTime, AnimatedSprite player)
        {
            base.Update(gameTime, player);
            if (currDistanceX < Math.Abs(maxDistanceX))
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                float delta = velocityX * elapsed;
                player.position = new Vector2(player.position.X + delta, player.position.Y);
                currDistanceX += Math.Abs(delta);
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