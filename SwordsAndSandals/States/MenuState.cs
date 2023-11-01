using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;
using SwordsAndSandals.States.Command;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals
{
    public class MenuState : State
    {
        private List<Button> buttons;
        private Background background;

        private int screenWidth;
        private int screenHeight;
        public MenuState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(new SettingsStateCommand(graphicsDevice));
        }

        private void CharacterSelection_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(new CharacterSelectionStateCommand(graphicsDevice));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            CommandHelper.UndoCommand();
        }

        private void TownButton_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(new TownStateCommand(graphicsDevice));
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            Button CharacterSelectionButton = new Button(buttonTexture, buttonFont, "Select character", 2f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 100)
            };
            CharacterSelectionButton.Click += CharacterSelection_Click;
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
            Button townButton = new Button(buttonTexture, buttonFont, "Town", 2f, SpriteEffects.None)
            {
                Position = new Vector2(10 * screenWidth / 11, screenHeight / 15)
            };
            townButton.Click += TownButton_Click;
            buttons = new List<Button>()
            {
                CharacterSelectionButton,
                settingsButton,
                exitButton,
                townButton
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
