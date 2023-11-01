using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Items
{
    public class KunoichiWeaponFactory : WeaponFactory
    {
        public override MeleeWeapon CreateMeleeWeapon(ContentManager content, Vector2 position)
        {
            Attributes WeaponAttrs = new Attributes()
            {
                //MeleeDamage = 43
            };
            return new KunoichiMeleeWeapon(position, content.Load<Texture2D>("Weapons/Weps"), WeaponAttrs);
        }

        public override RangedWeapon CreateRangedWeapon(ContentManager content, Vector2 position)
        {
            Attributes WeaponAttrs = new Attributes()
            {
                //RangedDamage = 52
            };
            return new KunoichiRangedWeapon(position, content.Load<Texture2D>("Weapons/Weps"), WeaponAttrs);
        }

        public override ShieldWeapon CreateShieldWeapon(ContentManager content, Vector2 position)
        {
            Attributes WeaponAttrs = new Attributes()
            {
                //ShieldDamage = 17,
                //ArmourRating = 25,
                //BaseDistance = -15
            };
            return new KunoichiShieldWeapon(position, content.Load<Texture2D>("Weapons/Weps"), WeaponAttrs);
        }
    }
}
