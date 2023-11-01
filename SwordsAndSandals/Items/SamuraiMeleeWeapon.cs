using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Items
{
    internal class SamuraiMeleeWeapon : MeleeWeapon
    {
        public SamuraiMeleeWeapon(Vector2 Position, Texture2D Texture, Attributes WeaponAttrs) : base(Position, Texture, WeaponAttrs)
        {
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, new Rectangle(0, 352, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 1);
        }

        public override void Update(GameTime time)
        {
            throw new NotImplementedException();
        }
    }
}
