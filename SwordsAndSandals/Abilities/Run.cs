using Microsoft.Xna.Framework;

using SwordsAndSandals.Animations;
using SwordsAndSandals.Classes;
using SwordsAndSandals.Sprites;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.Abilities
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

            currDistanceX = 0;
            velocityHolder = new Vector2(velocityX, 0);
        }

        protected override void NextState(GameTime gameTime, Player player)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            Vector2 delta = velocityHolder * elapsed;
            player.Position += delta;
            currDistanceX += Math.Abs(delta.X);
        }

        protected override void CheckIfDone()
        {
            if (currDistanceX >= Math.Abs(maxDistanceX)) done = true;
        }

        protected override void Prepare(Player player)
        {
            currDistanceX = 0;
            velocityHolder = new Vector2(velocityX, 0);
            base.Prepare(player);
        }
    }
}