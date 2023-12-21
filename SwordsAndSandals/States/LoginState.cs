using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Command;
using SwordsAndSandals.UI;
using SwordsAndSandals.Music;
using System;
using System.Collections.Generic;
using SwordsAndSandals.Mediator;

namespace SwordsAndSandals.States
{
    public class LoginState : State
    {
        private List<Button> buttons;
        private List<Component> components;
        private Background background;
        private GameWindow gw;
        private IMediator mediator;

        private int screenWidth;
        private int screenHeight;
        public LoginState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            CommandHelper.UndoCommand();
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            Texture2D inputBoxTexture = content.Load<Texture2D>("Views/InputBox");
            Texture2D cursorTexture = content.Load<Texture2D>("Views/Cursor");
            SpriteFont font = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));

            music = new MusicPlayer(content);
            music.stopSong();

            Text text = new Text(font, mediator)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 8),
                TextString = "Login",
                TextSize = 2f,
                PenColour = Color.Orange,
                OutlineColor = Color.Black,
            };
            Button backbutton = new Button(buttonTexture, font, "Back", 2f, SpriteEffects.None, mediator)
            {
                Position = new Vector2(screenWidth / 6, 7 * screenHeight / 8),
            };
            TextBox username = new TextBox(new Vector2(screenWidth / 2, screenHeight / 2), new Vector2(screenWidth / 2, screenHeight / 2), new Vector2(screenWidth / 2, screenHeight / 2), gw, inputBoxTexture, cursorTexture, font, Color.White, 1.5f, 1.5f, mediator);
            TextBox password = new TextBox(new Vector2(screenWidth / 2, screenHeight / 2 + 100), new Vector2(screenWidth / 2, screenHeight / 2), new Vector2(screenWidth / 2, screenHeight / 2), gw, inputBoxTexture, cursorTexture, font, Color.White, 1.5f, 1.5f, mediator);
            backbutton.Click += BackButton_Click;
            buttons = new List<Button>()
            {
                backbutton
            };
            components = new List<Component>()
            {
                text,
                username,
                password
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
            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var b in buttons)
            {
                b.Update(gameTime);
            }
            foreach (var component in components)
            {
                component.Update(gameTime);
            }
        }
        public override void UnloadContent()
        {
            // throw new NotImplementedException();
        }
    }
}
