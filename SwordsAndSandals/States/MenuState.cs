using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Command;
using SwordsAndSandals.Command.StateChangeCommand;
using SwordsAndSandals.UI;
using SwordsAndSandals.Music;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals
{
    public class MenuState : State
    {
        private List<Button> buttons;
        private Background background;
        private IMusic music;

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
        private void SpectateBattleButton_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Invoke("AddSpectator");
            CommandHelper.ExecuteCommand(new BattleListStateCommand(graphicsDevice));
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));

            music = new MusicPlayer(content);
            music.stopSong();

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
            Button spectateBattleButton = new Button(buttonTexture, buttonFont, "Spectate battles", 2.0f, SpriteEffects.None)
            {
                Position = new Vector2(10 * screenWidth / 11, screenHeight / 15)
            };
            spectateBattleButton.Click += SpectateBattleButton_Click;
            buttons = new List<Button>()
            {
                CharacterSelectionButton,
                settingsButton,
                exitButton,
                spectateBattleButton
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
