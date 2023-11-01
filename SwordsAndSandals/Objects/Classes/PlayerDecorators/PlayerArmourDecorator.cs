using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SwordsAndSandals.Objects.Classes.PlayerDecorators
{
    public class PlayerArmourDecorator : PlayerDecorator
    {
        private Text text;
        public PlayerArmourDecorator(Player p) : base(p)
        {
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            float textHeight = text.Font.MeasureString(text.TextString).Y;
            text.Position = new Vector2(Position.X, Position.Y + 2 * textHeight * text.TextSize);
            text.Draw(batch);

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
            text.TextString = "Armour: " + BaseAttributes.ArmourRating;
        }

        public void AddText(Text text)
        {
            this.text = text;
        }
    }
}
