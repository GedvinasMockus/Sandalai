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
    public class SamuraiWeaponFactory : WeaponFactory
    {
        public override MeleeWeapon CreateMeleeWeapon(ContentManager content, Vector2 position)
        {
            Attributes WeaponAttrs = new Attributes()
            {
                //MeleeDamage = 81
            };
            return new SamuraiMeleeWeapon(position, content.Load<Texture2D>("Weapons/Weps"), WeaponAttrs);
        }

        public override RangedWeapon CreateRangedWeapon(ContentManager content, Vector2 position)
        {
            Attributes WeaponAttrs = new Attributes()
            {
                //RangedDamage = 22
            };
            return new SamuraiRangedWeapon(position, content.Load<Texture2D>("Weapons/Weps"), WeaponAttrs);
        }

        public override ShieldWeapon CreateShieldWeapon(ContentManager content, Vector2 position)
        {
            Attributes WeaponAttrs = new Attributes()
            {
                //ShieldDamage = 47,
                //ArmourRating = 40,
                //BaseDistance = -50
            };
            return new SamuraiShieldWeapon(position, content.Load<Texture2D>("Weapons/Weps"), WeaponAttrs);
        }
    }
}
