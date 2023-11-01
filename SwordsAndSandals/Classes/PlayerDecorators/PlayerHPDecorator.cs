using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Classes;
using SwordsAndSandals.Sprites;
using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Classes.PlayerDecorators
{
    public class PlayerHPDecorator : PlayerDecorator
    {
        private Text text;
        public PlayerHPDecorator(Player p) : base(p)
        {

        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            float textHeight = text.Font.MeasureString(text.TextString).Y;
            text.Position = new Vector2(Position.X, Position.Y + textHeight * text.TextSize);
            text.Draw(batch);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
            text.TextString = BaseAttributes.CurrHealth + "/" + BaseAttributes.MaxHealth;
        }

        public void AddText(Text text)
        {
            this.text = text;
        }
    }
}
