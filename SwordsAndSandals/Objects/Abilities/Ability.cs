using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Abilities
{
    public class Ability
    {
        public bool active { get; set; }
        public bool done { get; set; }

        public Ability()
        {
            active = false;
            done = true;
        }

        public virtual void Update(GameTime gameTime, Player player)
        {

        }
    }
}
