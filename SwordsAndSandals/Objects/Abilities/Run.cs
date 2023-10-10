using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Classes;
using System;
using System.Collections.Generic;

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

        public override void Prepare(Player player)
        {
            animation.Reset();
            player.animation = animation;
            player.Velocity = new Vector2(velocityX, 0);
        }

        public override void Update(GameTime gameTime, Player player, List<Sprite> sprites)
        {
            if (currDistanceX < Math.Abs(maxDistanceX))
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                Vector2 delta = player.Velocity * elapsed;
                player.Position += delta;
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