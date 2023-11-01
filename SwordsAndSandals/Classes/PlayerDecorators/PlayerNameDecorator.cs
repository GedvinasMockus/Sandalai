using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Classes.PlayerDecorators
{
    public class PlayerNameDecorator : PlayerDecorator
    {
        private Text text;
        public PlayerNameDecorator(Player p, Text text) : base(p)
        {
            this.text = text;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            float textHeigh = text.Font.MeasureString(text.TextString).Y;
            text.Position = new Vector2(Position.X, Position.Y + textHeigh * text.TextSize);
            text.Draw(batch);
        }
    }
}
