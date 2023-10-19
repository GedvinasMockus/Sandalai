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
    public abstract class WeaponFactory
    {
        public abstract MeleeWeapon CreateMeleeWeapon(ContentManager content, Vector2 position);
        public abstract RangedWeapon CreateRangedWeapon(ContentManager content, Vector2 position);
        public abstract ShieldWeapon CreateShieldWeapon(ContentManager content, Vector2 position);
    }
}
