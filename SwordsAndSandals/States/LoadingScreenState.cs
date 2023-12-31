﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Command;
using SwordsAndSandals.UI;
using SwordsAndSandals.Music;
using System;
using System.Collections.Generic;
using SwordsAndSandals.Visitor;
using SwordsAndSandals.Mediator;

namespace SwordsAndSandals.States
{
    public class LoadingScreenState : State
    {
        private List<Component> components;
        private List<Button> buttons;
        private Background background;

        private int screenWidth;
        private int screenHeight;
        private IMediator mediator;

        public LoadingScreenState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void LeaveLobby_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Invoke("RemoveFromLobby");
            CommandHelper.UndoCommand();
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            Texture2D spinnerTexture = content.Load<Texture2D>("Objects/Gear");
            SpriteFont font = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Town/FindBattle"));

            music = new MusicPlayer(content);
            music.stopSong();

            Spinner spinner = new Spinner(spinnerTexture, Color.DarkOrange, new Vector2(screenWidth / 2, screenHeight / 3), 1.0f, 1.0f, mediator);
            Button leaveLobby = new Button(buttonTexture, font, "Leave lobby", 2f, SpriteEffects.None, mediator)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 100)
            };
            leaveLobby.Click += LeaveLobby_Click;
            Text text = new Text(font, mediator)
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
                if (component.GetType() == typeof(Spinner))
                {
                    Spinner spinner = component as Spinner;
                    spinner.setSprite(spriteBatch);

                    IVisitor visitor = new DrawVisitor();
                    spinner.Accept(visitor);
                }
                else
                {
                    component.Draw(spriteBatch);
                }
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
                if (component.GetType() == typeof(Spinner))
                {
                    Spinner spinner = component as Spinner;
                    spinner.setTime(gameTime);

                    IVisitor visitor = new UpdateVisitor();
                    spinner.Accept(visitor);
                }
                else
                {
                    component.Update(gameTime);
                }
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
