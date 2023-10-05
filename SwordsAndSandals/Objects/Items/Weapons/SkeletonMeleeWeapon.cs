using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Items.Weapons
{
    internal class SkeletonMeleeWeapon : MeleeWeapon
    {
        public SkeletonMeleeWeapon(Vector2 Position, Texture2D Texture, int Damage) : base(Position, Texture, Damage)
        {
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, new Rectangle(0, 96, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 1);
        }

        public override void Update(GameTime time)
        {
            throw new NotImplementedException();
        }
    }
}
