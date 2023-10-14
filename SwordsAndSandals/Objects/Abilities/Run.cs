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
        private Vector2 velocityHolder;

        public Run(float distanceX, Animation animation) : base(animation)
        {
            maxDistanceX = distanceX;
            velocityX = maxDistanceX / animation.Duration;
        }

        public override void Prepare(Player player)
        {
            animation.Reset();
            player.animation = animation;
            currDistanceX = 0;
            velocityHolder = new Vector2(velocityX, 0);
        }

        public override void Update(GameTime gameTime, Player player, List<Sprite> sprites)
        {
            if (currDistanceX < Math.Abs(maxDistanceX))
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                Vector2 delta = velocityHolder * elapsed;
                player.Position += delta;
                currDistanceX += Math.Abs(delta.X);
            }
            else
            {
                done = true;
            }
        }
    }
}