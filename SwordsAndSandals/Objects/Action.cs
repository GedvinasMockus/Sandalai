using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects
{
    public class Action : IDrawable
    {
        public Texture2D texture { get; }
        public Vector2 position { get; set; }
        public string name { get; }

        public Action(Texture2D texture, Vector2 position, string name)
        {
            this.texture = texture;
            this.position = position;
            this.name = name;
        }

        public void Draw(SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return this.name.Equals((obj as Action).name);
        }
    }
}
