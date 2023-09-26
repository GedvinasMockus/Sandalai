﻿using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;
using SwordsAndSandals.States;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals
{
    public class MenuState : State
    {
        private List<Button> buttons;
        private Background background;
        private IHubProxy hub;
        private GameWindow gw;

        private int screenWidth;
        private int screenHeight;
        public MenuState(GraphicsDeviceManager graphicsDevice, IHubProxy hub, GameWindow gw) : base(graphicsDevice)
        {
            this.hub = hub;
            this.gw = gw;
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.ChangeState(new SettingsState(_graphicsDevice, hub));
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.ChangeState(new LoginState(_graphicsDevice, hub, gw));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.ChangeState(null);
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            Button loginButton = new Button(buttonTexture, buttonFont, "Login", 2f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 100)
            };
            loginButton.Click += LoginButton_Click;
            Button settingsButton = new Button(buttonTexture, buttonFont, "Settings", 2f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 200)
            };
            settingsButton.Click += SettingButton_Click;
            Button exitButton = new Button(buttonTexture, buttonFont, "Exit", 2f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 300)
            };
            exitButton.Click += ExitButton_Click;
            buttons = new List<Button>()
            {
                loginButton,
                settingsButton,
                exitButton
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
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var b in buttons)
            {
                b.Update(gameTime);
            }
        }

        public override void UnloadContent()
        {

        }
    }
}
