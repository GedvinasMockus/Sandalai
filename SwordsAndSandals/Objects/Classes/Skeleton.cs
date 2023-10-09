using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public class Skeleton : Player
    {
        public Skeleton() : base()
        {

        }

        //public override void AddWeapons(WeaponFactory factory, ContentManager content)
        //{
        //    Melee = factory.CreateMeleeWeapon(content, new Vector2(32, 32), 8);
        //    Ranged = factory.CreateRangedWeapon(content, new Vector2(32, 96), 12);
        //    Shield = factory.CreateShieldWeapon(content, new Vector2(32, 160), 6);
        //}
    }
}
