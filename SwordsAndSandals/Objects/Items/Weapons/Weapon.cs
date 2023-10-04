using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Items.Weapons
{
    public abstract class Weapon
    {
        public Vector2 position { get; set; }
        public Texture2D texture { get; set; }
        public int damage { get; set; }
        public Weapon(Vector2 Position, Texture2D Texture, int Damage)
        {
            this.position = Position;
            this.texture = Texture;
            this.damage = Damage;
        }
        public abstract void Draw(SpriteBatch batch);
        public abstract void Update(GameTime time);
    }
}
