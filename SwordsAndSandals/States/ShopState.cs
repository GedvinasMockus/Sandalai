using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Armour;
using SwordsAndSandals.Command;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class ShopState : State
    {
        private List<string> buyNames;
        private Background background;
        private List<Button> buttons;
        private int buttonsCount;
        // Change to server side
        private Text armourText;
        private Attributes attributes;
        // Change to server side

        private int screenWidth;
        private int screenHeight;

        public ShopState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;

            PopulateBuyNamesList();
            buttonsCount = buyNames.Count + 1;
        }

        private void LeaveShopButton_Click(object sender, EventArgs e)
        {
            CommandHelper.UndoCommand();
        }

        private void BuyItem_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Armour.Armour armour;

            switch (button.Text.Remove(0, 4))
            {
                case "Bronze Helmet":
                    armour = new Helmet(new Bronze());
                    attributes.ArmourRating += armour.EquipArmour().ArmourRating;

                    break;
                case "Iron Platebody":
                    armour = new Platebody(new Iron());
                    attributes.ArmourRating += armour.EquipArmour().ArmourRating;

                    break;
            }

            buttons.Remove(button);
            buttonsCount--;

            armourText.TextString = "Armour: " + attributes.ArmourRating;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Town/EnterShop"));

            Button leaveShop = new Button(buttonTexture, buttonFont, "Leave shop", 2f, SpriteEffects.None)
            {
                Position = new Vector2(225f, 900f)
            };
            leaveShop.Click += LeaveShopButton_Click;
            buttons = new List<Button>()
            {
                leaveShop
            };

            float xPosition = 525f;
            for (int i = 0; i < buyNames.Count; i++)
            {
                Button button = new Button(buttonTexture, buttonFont, "Buy " + buyNames[i], 2f, SpriteEffects.None)
                {
                    Position = new Vector2(xPosition, 250f)
                };
                buttons.Add(button);
                button.Click += BuyItem_Click;
                xPosition += 325f;
            }

            armourText = new Text(content.Load<SpriteFont>("Fonts/vinque"))
            {
                TextString = "Armour: " + attributes.ArmourRating,
                PenColour = Color.Orange,
                Position = new Vector2(1600f, 800f)
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            background.Draw(spriteBatch);
            foreach (var b in buttons)
            {
                b.Draw(spriteBatch);
            }
            armourText.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < buttonsCount; i++)
            {
                buttons[i].Update(gameTime);
            }
        }

        private void PopulateBuyNamesList()
        {
            buyNames = new()
            {
                "Bronze Helmet",
                "Iron Platebody"
            };
        }
    }
}