using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Classes;
using SwordsAndSandals.Sprites;

namespace SwordsAndSandals.Abilities
{
    public class RangedAttack : Ability
    {
        private float timer;
        private Texture2D texture;
        private float velocityX;
        private bool projectileSpawned;
        private List<Sprite> spawnCtx;
        public RangedAttack(Texture2D texture, RangedAttackAnimation animation, float velocityX, List<Sprite> ctx) : base(animation)
        {
            this.texture = texture;
            this.velocityX = velocityX;
            spawnCtx = ctx;

            timer = 0;
            projectileSpawned = false;
        }

        protected override void NextState(GameTime gameTime, Player player)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            RangedAttackAnimation rangedAnimation = (animation as RangedAttackAnimation);
            if (!projectileSpawned && timer >= rangedAnimation.spawnDuration)
            {
                Vector2 origin = new Vector2(player.animation.frameWidth / 2, player.animation.frameHeight / 2);
                Vector2 playerOrigin = new Vector2(player.Position.X, player.Position.Y - player.animation.Scale * player.animation.frameHeight / 2);
                Vector2 shift = rangedAnimation.relativePosition - origin;
                Projectile p = new Projectile(texture, playerOrigin + shift * player.animation.Scale)
                {
                    Rotation = 0,
                    Scale = player.animation.Scale * 37f / 45f,
                    Flip = player.animation.Flip,
                    Velocity = new Vector2(velocityX, 0)
                };
                spawnCtx.Add(p);
                projectileSpawned = true;
            }
        }

        protected override void CheckIfDone()
        {
            if (timer >= animation.Duration) done = true;
        }

        protected override void Prepare(Player player)
        {
            timer = 0;
            projectileSpawned = false;
            base.Prepare(player);
        }
    }
}
