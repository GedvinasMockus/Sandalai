using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Items.Weapons
{
    public class SkeletonWeaponFactory : WeaponFactory
    {
        public override MeleeWeapon CreateMeleeWeapon(ContentManager content, Vector2 position)
        {
            Attributes WeaponAttrs = new Attributes()
            {
                //MeleeDamage = 34
            };
            return new SkeletonMeleeWeapon(position, content.Load<Texture2D>("Weapons/Weps"), WeaponAttrs);
        }

        public override RangedWeapon CreateRangedWeapon(ContentManager content, Vector2 position)
        {
            Attributes WeaponAttrs = new Attributes()
            {
                //RangedDamage = 67,
                //BaseDistance = -10
            };
            return new SkeletonRangedWeapon(position, content.Load<Texture2D>("Weapons/Weps"), WeaponAttrs);
        }

        public override ShieldWeapon CreateShieldWeapon(ContentManager content, Vector2 position)
        {
            Attributes WeaponAttrs = new Attributes()
            {
                //ShieldDamage = 17,
                //ArmourRating = 25,
                //BaseDistance = -10
            };
            return new SkeletonShieldWeapon(position, content.Load<Texture2D>("Weapons/Weps"), WeaponAttrs);
        }
    }
}
