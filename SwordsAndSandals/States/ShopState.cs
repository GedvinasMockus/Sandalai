using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Command;
using SwordsAndSandals.UI;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class ShopState : State
    {
        private Background background;
        private Music music;
        private List<Button> buttons;

        private int screenWidth;
        private int screenHeight;

        public ShopState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void LeaveShopButton_Click(object sender, EventArgs e)
        {
            CommandHelper.UndoCommand();
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Town/EnterShop"));

            music = new MusicPlayer(content);
            music.stopSong();

            Button leaveShop = new Button(buttonTexture, buttonFont, "Leave shop", 2f, SpriteEffects.None)
            {
                Position = new Vector2(225f, 900f)
            };
            leaveShop.Click += LeaveShopButton_Click;
            buttons = new List<Button>()
            {
                leaveShop
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
        }
    }
}