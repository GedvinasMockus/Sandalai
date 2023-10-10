using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Items.Weapons
{
    public class KunoichiWeaponFactory : WeaponFactory
    {
        public override MeleeWeapon CreateMeleeWeapon(ContentManager content, Vector2 position, int damage)
        {
            return new KunoichiMeleeWeapon(position, content.Load<Texture2D>("Weapons/Weps"), damage);
        }

        public override RangedWeapon CreateRangedWeapon(ContentManager content, Vector2 position, int damage)
        {
            return new KunoichiRangedWeapon(position, content.Load<Texture2D>("Weapons/Weps"), damage);
        }

        public override ShieldWeapon CreateShieldWeapon(ContentManager content, Vector2 position, int damage)
        {
            return new KunoichiShieldWeapon(position, content.Load<Texture2D>("Weapons/Weps"), damage);
        }
    }
}
