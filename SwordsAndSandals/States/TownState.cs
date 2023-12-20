using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Classes;
using SwordsAndSandals.Command;
using SwordsAndSandals.Command.StateChangeCommand;
using SwordsAndSandals.Memento;
using SwordsAndSandals.Music;

using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class TownState : State
    {
        private Background background;
        private List<Button> buttons;
        private Player player;
        private IMusic music;
        public string playerClass;
        private Caretaker caretaker;

        private int screenWidth;
        private int screenHeight;

        public TownState(GraphicsDeviceManager graphicsDevice, string className, Caretaker caretaker) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
            playerClass = className;
            this.caretaker = caretaker;
        }

        private void EnterShop_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(new ShopStateCommand(graphicsDevice, playerClass, caretaker));
        }

        private void FindBattleButton_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Invoke("AddToLobby", playerClass);
            ConnectionManager.Instance.Invoke("FindOpponent");
            CommandHelper.ExecuteCommand(new LoadingScreenStateCommand(graphicsDevice));
        }

        public override void LoadContent(ContentManager content)
        {
            Attributes attributes = new Attributes()
            {
                MaxHealth = 1000,
                CurrHealth = 1000,
                BaseDistance = 300,
                ArmourRating = 10
            };
            PlayerFactory factory = new PlayerFactory(content);
            player = factory.CreatePlayerWithoutButtons(playerClass, new Vector2(screenWidth / 2, 800f), SpriteEffects.None, null, attributes, "tylerAdin");

            Texture2D shopTexture = content.Load<Texture2D>("Views/Town/Shop");
            Texture2D arenaTexture = content.Load<Texture2D>("Views/Town/Arena");
            background = new Background(content.Load<Texture2D>("Background/Town/Town"));

            music = new MusicPlayer(content);
            music.stopSong();

            Button enterShop = new Button(shopTexture, 1f, SpriteEffects.None)
            {
                Position = new Vector2(325f, 300f)
            };
            enterShop.Click += EnterShop_Click;
            Button enterArena = new Button(arenaTexture, 1f, SpriteEffects.None)
            {
                Position = new Vector2(1580f, 280f)
            };
            enterArena.Click += FindBattleButton_Click;
            buttons = new List<Button>()
            {
                enterShop,
                enterArena
            };
            music = new MusicPlayer(content);
            music.stopSong();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach (var b in buttons)
            {
                b.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var b in buttons)
            {
                b.Update(gameTime);
            }

            player.Update(gameTime);
        }
    }
}