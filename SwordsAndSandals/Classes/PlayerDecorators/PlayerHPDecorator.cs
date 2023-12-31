﻿using Microsoft.Xna.Framework;
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
        public PlayerHPDecorator(Player p, Text text) : base(p)
        {
            this.text = text;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            float textHeight = text.Font.MeasureString(text.TextString).Y;
            text.Position = new Vector2(Position.X, Position.Y + 2 * textHeight * text.TextSize);
            text.Draw(batch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            text.TextString = "HP: " + BaseAttributes.CurrHealth + "/" + BaseAttributes.MaxHealth;
        }
    }
}
