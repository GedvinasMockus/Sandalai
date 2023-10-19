using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Stats
{
    public class Attributes
    {
        public int Health { get; set; }
        public float BaseDistance { get; set; }
        public int MeleeDamage { get; set; }
        public int RangedDamage { get; set; }
        public int ShieldDamage { get; set; }
        public int ArmourRating { get; set; }

        public static Attributes operator +(Attributes a1, Attributes a2)
        {
            return new Attributes()
            {
                Health = a1.Health + a2.Health,
                BaseDistance = a1.BaseDistance + a2.BaseDistance,
                MeleeDamage = a1.MeleeDamage + a2.MeleeDamage,
                RangedDamage = a1.RangedDamage + a2.RangedDamage,
                ShieldDamage = a1.ShieldDamage + a2.ShieldDamage,
                ArmourRating = a1.ArmourRating + a2.ArmourRating
            };
        }
    }
}
