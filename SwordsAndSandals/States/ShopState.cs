using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Armour;
using SwordsAndSandals.Command;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;
using SwordsAndSandals.Music;
using SwordsAndSandals.Proxy;
using SwordsAndSandals.Memento;

using System;
using System.Collections.Generic;
using SwordsAndSandals.Mediator;

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
        private string playerClass;
        private Caretaker caretaker;

        private int screenWidth;
        private int screenHeight;
        private IMediator mediator;

        public ShopState(GraphicsDeviceManager graphicsDevice, string playerClass, Caretaker caretaker) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;

            PopulateBuyNamesList();
            buttonsCount = buyNames.Count + 1;

            this.playerClass = playerClass;

            this.caretaker = caretaker;
            Memento memento = caretaker.GetLastMemento();
            if (memento != null)
            {
                SetMemento(memento);
            }
        }

        public void SetButtons(List<Button> buttons)
        {
            this.buttons = buttons;
        }

        public void SetBuyNames(List<string> buyNames)
        {
            this.buyNames = buyNames;
        }

        public void SetAttributes(Attributes attributes)
        {
            this.attributes = attributes;
        }

        public void SetButtonsCount(int buttonsCount)
        {
            this.buttonsCount = buttonsCount;
        }

        public Memento CreateMemento()
        {
            return new Memento(this.buttons, this.buyNames, this.attributes, this.buttonsCount);
        }

        public void SetMemento(Memento memento)
        {
            this.buttons = memento.GetSavedButtons();
            this.buyNames = memento.GetSavedBuyNames();
            this.attributes = memento.GetSavedAttributes();
            this.buttonsCount = memento.GetSavedButtonsCount();
        }

        private void LeaveShopButton_Click(object sender, EventArgs e)
        {
            caretaker.SaveShopState(this.buttons, this.buyNames, this.attributes, this.buttonsCount);

            CommandHelper.UndoCommand();
        }

        private void BuyItem_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Armour.Armour armour;
            string armourName;
            IItem item = new ProxyItem();

            switch (button.Text.Remove(0, 4))
            {
                case "Bronze Helmet":
                    armourName = "Bronze Helmet";
                    if (item.CheckItemAvailability(playerClass, armourName))
                    {
                        armour = new Helmet(new Bronze());
                        attributes.ArmourRating += armour.EquipArmour().ArmourRating;

                        buttons.Remove(button);
                        buyNames.Remove(armourName);
                        buttonsCount--;
                    }

                    break;
                case "Iron Platebody":
                    armourName = "Iron Platebody";
                    if (item.CheckItemAvailability(playerClass, armourName))
                    {
                        armour = new Platebody(new Iron());
                        attributes.ArmourRating += armour.EquipArmour().ArmourRating;

                        buttons.Remove(button);
                        buyNames.Remove(armourName);
                        buttonsCount--;
                    }

                    break;
            }

            armourText.TextString = "Armour: " + attributes.ArmourRating;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Town/EnterShop"));

            music = new MusicPlayer(content);
            music.stopSong();

            Button leaveShop = new Button(buttonTexture, buttonFont, "Leave shop", 2f, SpriteEffects.None, mediator)
            {
                Position = new Vector2(225f, 900f)
            };
            leaveShop.Invoke("ShopState", leaveShop.GetType().ToString());
            leaveShop.Click += LeaveShopButton_Click;
            buttons = new List<Button>()
            {
                leaveShop
            };

            float xPosition = 525f;
            for (int i = 0; i < buyNames.Count; i++)
            {
                Button button = new Button(buttonTexture, buttonFont, "Buy " + buyNames[i], 2f, SpriteEffects.None, mediator)
                {
                    Position = new Vector2(xPosition, 250f)
                };
                buttons.Add(button);
                button.Click += BuyItem_Click;
                xPosition += 325f;
                button.Invoke("ShopState", button.GetType().ToString());
            }

            armourText = new Text(content.Load<SpriteFont>("Fonts/vinque"), mediator)
            {
                TextString = "Armour: " + attributes.ArmourRating,
                PenColour = Color.Orange,
                Position = new Vector2(1600f, 800f)
            };
            armourText.Invoke("ShopState", armourText.GetType().ToString());
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

        public class Memento
        {
            private List<Button> buttons;
            private List<string> buyNames;
            private Attributes attributes;
            private int buttonsCount;

            public Memento(List<Button> buttonsToSave, List<string> buyNamesToSave, Attributes attributesToSave, int buttonsCountToSave)
            {
                this.buttons = buttonsToSave;
                this.buyNames = buyNamesToSave;
                this.attributes = attributesToSave;
                this.buttonsCount = buttonsCountToSave;
            }

            public List<Button> GetSavedButtons()
            {
                return this.buttons;
            }

            public List<string> GetSavedBuyNames()
            {
                return this.buyNames;
            }

            public Attributes GetSavedAttributes()
            {
                return this.attributes;
            }

            public int GetSavedButtonsCount()
            {
                return this.buttonsCount;
            }
        }
    }
}