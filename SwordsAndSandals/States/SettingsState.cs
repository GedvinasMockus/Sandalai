using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;
using SwordsAndSandals.States;
using System;
using System.Collections.Generic;

namespace SwordsAndSandals
{
    public class SettingsState : State
    {
        private List<Component> components;
        private List<Button> buttons;
        private Background background;
        private IHubProxy hub;

        private int screenWidth;
        private int screenHeight;
        public SettingsState(GraphicsDeviceManager graphicsDevice, IHubProxy hub) : base(graphicsDevice)
        {
            this.hub = hub;
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.ChangeState(new MenuState(graphicsDevice, hub));
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            TextBox text = new TextBox(buttonFont)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 8),
                Text = "Edit settings",
                TextSize = 2f,
                PenColour = Color.Orange,
                OutlineColor = Color.Black,
            };
            Button backbutton = new Button(buttonTexture, buttonFont, "Back", 2f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 6, 7 * screenHeight / 8),
            };
            backbutton.Click += BackButton_Click;
            components = new List<Component>()
            {
                text,
            };
            buttons = new List<Button>()
            {
                backbutton
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
            foreach(var b in buttons)
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
            foreach(var b in buttons)
            {
                b.Update(gameTime);
            }
        }

        public override void UnloadContent()
        {
            
        }
    }
}
