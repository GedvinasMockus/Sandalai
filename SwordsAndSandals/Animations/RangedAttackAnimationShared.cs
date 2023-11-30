using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Animations
{
    public class RangedAttackAnimationShared : AnimationShared
    {
        public Vector2 RelativePosition { get; private set; }
        public int ProjectileWidth { get; private set; }
        public int ProjectileSpawnFrame { get; private set; }
        public RangedAttackAnimationShared(Texture2D texture, int totalFrames, bool flipChangeable, int collisionWidth, int collisionHeight, Vector2 collisionRectPoint, int projectileWidth, int projectileSpawnFrame, Vector2 relativePosition) : base(texture, totalFrames, flipChangeable, collisionWidth, collisionHeight, collisionRectPoint)
        {
            RelativePosition = relativePosition;
            ProjectileWidth = projectileWidth;
            ProjectileSpawnFrame = projectileSpawnFrame;
        }
    }
}
