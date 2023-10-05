using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Items.Weapons
{
    public abstract class ShieldWeapon : Weapon
    {
        protected ShieldWeapon(Vector2 Position, Texture2D Texture, int Damage) : base(Position, Texture, Damage)
        {
        }
    }
}
