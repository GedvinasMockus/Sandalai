using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwordsAndSandals.Objects.Stats;

namespace SwordsAndSandals.Objects.Items.Weapons
{
    public abstract class Weapon
    {
        public Vector2 position { get; set; }
        public Texture2D texture { get; set; }
        public Attributes WeaponAttrs { get; set; }
        public Weapon(Vector2 Position, Texture2D Texture, Attributes WeaponAttrs)
        {
            this.position = Position;
            this.texture = Texture;
            this.WeaponAttrs = WeaponAttrs;
        }
        public abstract void Draw(SpriteBatch batch);
        public abstract void Update(GameTime time);
    }
}
