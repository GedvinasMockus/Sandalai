using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects
{
    public class SkeletonFactory : PlayerFactory
    {
        public override Player CreatePlayer(Vector2 position, float scale, int centerY, SpriteEffects effect)
        {
            return new Skeleton(position, scale, centerY, effect);
        }
    }
}
