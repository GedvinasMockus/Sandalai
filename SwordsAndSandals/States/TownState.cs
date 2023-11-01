using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Command;
using SwordsAndSandals.Command.StateChangeCommand;
using SwordsAndSandals.UI;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class TownState : State
    {
        private Background background;
        private List<Button> buttons;
        private List<Component> components;

        private int screenWidth;
        private int screenHeight;
        public TownState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void FindBattleButton_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(new LoadingScreenStateCommand(graphicsDevice));
        }
        private void BackButton_Click(object sender, EventArgs e)
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
            Text text = new Text(buttonFont)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 8),
                TextString = "Town",
                TextSize = 2f,
                PenColour = Color.Orange,
                OutlineColor = Color.Black,
            };
            Button findBattle = new Button(buttonTexture, buttonFont, "Find battle", 2.0f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 100)
            };
            //findBattle.Click += FindBattleButton_Click;
            Button backbutton = new Button(buttonTexture, buttonFont, "Back", 2f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 6, 7 * screenHeight / 8),
            };
            backbutton.Click += BackButton_Click;
            Button spectateBattleButton = new Button(buttonTexture, buttonFont, "Spectate battles", 2.0f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 200)
            };
            spectateBattleButton.Click += SpectateBattleButton_Click;
            buttons = new List<Button>()
            {
                findBattle,
                backbutton,
                spectateBattleButton
            };
            components = new List<Component>()
            {
                text,
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
            foreach (var c in components)
            {
                c.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
        public override void UnloadContent()
        {
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
