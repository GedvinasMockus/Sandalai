using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SwordsAndSandals.Items
{
    public abstract class WeaponFactory
    {
        public abstract MeleeWeapon CreateMeleeWeapon(ContentManager content, Vector2 position);
        public abstract RangedWeapon CreateRangedWeapon(ContentManager content, Vector2 position);
        public abstract ShieldWeapon CreateShieldWeapon(ContentManager content, Vector2 position);
    }
}
