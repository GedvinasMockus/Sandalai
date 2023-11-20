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
        private int collisionWidth;
        private int collisionHeight;
        private Vector2 rectPoint;

        private bool projectileSpawned;

        private List<Sprite> spawnCtx;
        public RangedAttack(Texture2D texture, float velocityX, int width, int height, Vector2 rectPoint, RangedAttackAnimation animation, List<Sprite> ctx) : base(animation)
        {
            this.texture = texture;
            spawnCtx = ctx;

            this.velocityX = velocityX;
            collisionWidth = width;
            collisionHeight = height;
            this.rectPoint = rectPoint;

            timer = 0;
            projectileSpawned = false;
        }

        protected override void NextState(GameTime gameTime, Player player)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            RangedAttackAnimation rangedAnimation = (animation as RangedAttackAnimation);
            if (!projectileSpawned && timer >= rangedAnimation.SpawnDuration)
            {
                Vector2 origin = new Vector2(player.animation.FrameWidth / 2, player.animation.FrameHeight / 2);
                Vector2 playerPos = new Vector2(player.Position.X, player.Position.Y - player.animation.Scale * player.animation.FrameHeight / 2);
                Vector2 shift = rangedAnimation.RelativePosition - origin;
                Projectile p = new Projectile(texture, playerPos + shift * player.animation.Scale)
                {
                    Rotation = 0,
                    Scale = player.animation.Scale * rangedAnimation.ProjectileWidth / collisionWidth,
                    Flip = player.animation.Flip,
                    Velocity = new Vector2(velocityX, 0),
                    CollisionWidth = collisionWidth,
                    CollisionHeight = collisionHeight,
                    CollisionRectPoint = rectPoint
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
