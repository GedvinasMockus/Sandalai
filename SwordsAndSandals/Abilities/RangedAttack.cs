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
        public RangedAttack(Texture2D texture, RangedAttackAnimation animation, float velocityX) : base(animation)
        {
            timer = 0;
            this.texture = texture;
            this.velocityX = velocityX;
            projectileSpawned = false;
        }

        public override void Prepare(Player player)
        {
            animation.Reset();
            player.animation = animation;
        }

        public override void Update(GameTime gameTime, Player player, List<Sprite> sprites)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (!projectileSpawned && timer >= (animation as RangedAttackAnimation).spawnDuration)
            {
                Vector2 origin = new Vector2(player.animation.frameWidth / 2, player.animation.frameHeight / 2);
                Vector2 playerOriginPosition = new Vector2(player.Position.X, player.Position.Y - player.animation.Scale * player.animation.frameHeight / 2);
                Vector2 shift = (animation as RangedAttackAnimation).relativePosition - origin;
                Projectile p = new Projectile(texture, playerOriginPosition + shift * player.animation.Scale)
                {
                    Rotation = 0,
                    Scale = player.animation.Scale * 37.0f / 45.0f,
                    Flip = player.animation.Flip,
                    Velocity = new Vector2(velocityX, 0)
                };
                sprites.Add(p);
                projectileSpawned = true;
            }
            if (timer >= animation.Duration)
            {
                timer = 0;
                projectileSpawned = false;
                done = true;
            }
        }
    }
}
