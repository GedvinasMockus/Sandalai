using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;
using SwordsAndSandals.Objects.Classes;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class TownState : State
    {
        private Background background;
        private List<Button> buttons;
        private PlayerFactory playerFactory;
        private Player player;
        public static string playerClass;

        private int screenWidth;
        private int screenHeight;

        public TownState(GraphicsDeviceManager graphicsDevice, string className) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
            playerClass = className;

            playerFactory = GetPlayerFactory(playerClass);
        }

        private void EnterShop_Click(object sender, EventArgs e)
        {
            StateManager.Instance.ChangeState(new ShopState(graphicsDevice));
        }

        private void FindBattleButton_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Invoke("AddToLobby", playerClass);
            ConnectionManager.Instance.Invoke("FindOpponent");
            StateManager.Instance.ChangeState(new LoadingScreenState(graphicsDevice));
        }

        public override void LoadContent(ContentManager content)
        {
            player = playerFactory.CreatePlayer(content, new Vector2(screenWidth / 2, 800f), SpriteEffects.None, false);

            Texture2D shopTexture = content.Load<Texture2D>("Views/Town/Shop");
            Texture2D arenaTexture = content.Load<Texture2D>("Views/Town/Arena");
            background = new Background(content.Load<Texture2D>("Background/Town/Town"));

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

            player.Update(gameTime, null);
        }

        private PlayerFactory GetPlayerFactory(string className)
        {
            switch (className)
            {
                case "Kunoichi":
                    return new KunoichiFactory();
                case "Samurai":
                    return new SamuraiFactory();
                default:
                    return new SkeletonFactory();
            }
        }
    }
}