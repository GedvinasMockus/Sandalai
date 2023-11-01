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
    public abstract class ShieldWeapon : Weapon
    {
        protected ShieldWeapon(Vector2 Position, Texture2D Texture, Attributes WeaponAttrs) : base(Position, Texture, WeaponAttrs)
        {
        }
    }
}
