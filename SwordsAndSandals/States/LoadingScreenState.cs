﻿using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class LoadingScreenState : State
    {
        private List<Component> components;
        private List<Button> buttons;
        private Background background;
        private IHubProxy hub;

        private int screenWidth;
        private int screenHeight;
        public LoadingScreenState(GraphicsDeviceManager graphicsDevice, IHubProxy hub) : base(graphicsDevice)
        {
            this.hub = hub;
            hub.Invoke("AddToLobby");
            hub.Invoke("FindOpponent");
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void LeaveLobby_Click(object sender, EventArgs e)
        {
            hub.Invoke("RemoveFromLobby");
            StateManager.Instance.ChangeState(new MenuState(_graphicsDevice, hub, null));
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            Texture2D spinnerTexture = content.Load<Texture2D>("Objects/Gear");
            SpriteFont font = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            Spinner spinner = new Spinner(spinnerTexture, Color.DarkOrange, new Vector2(screenWidth / 2, screenHeight / 3), 1.0f, 1.0f);
            Button leaveLobby = new Button(buttonTexture, font, "Leave lobby", 2f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 100)
            };
            leaveLobby.Click += LeaveLobby_Click;
            Text text = new Text(font)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 8),
                TextString = "Waiting for opponent",
                TextSize = 2f,
                PenColour = Color.Orange,
                OutlineColor = Color.Black
            };
            components = new List<Component>()
            {
                spinner,
                text
            };
            buttons = new List<Button>()
            {
                leaveLobby
            };

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }
            foreach (var b in buttons)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.Update(gameTime);
            }
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
