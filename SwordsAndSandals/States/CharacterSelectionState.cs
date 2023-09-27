using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.States
{
    public class CharacterSelectionState : State
    {
        private Background background;
        private List<Player> classes;
        private List<Button> buttons;
        private IHubProxy hub;

        private int screenWidth;
        private int screenHeight;
        private int classIndex;
        public CharacterSelectionState(GraphicsDeviceManager graphicsDevice, IHubProxy hub) : base(graphicsDevice)
        {
            this.hub = hub;
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void SwitchRightButton_Click(object sender, EventArgs e)
        {
            classIndex = (classIndex + 1) % classes.Count;
        }
        private void SwitchLeftButton_Click(object sender, EventArgs e)
        {
            classIndex--;
            if (classIndex < 0) classIndex += classes.Count;
        }
        private void SelectCharacterButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.ChangeState(new LoadingScreenState(_graphicsDevice, hub));
        }

        public override void LoadContent(ContentManager content)
        {
            classIndex = 0;
            Texture2D arrowTexture = content.Load<Texture2D>("Icons/arrow");
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            var kunoichi = new Kunoichi(new Vector2(screenWidth / 2, screenHeight / 2), 3.0f, 95, SpriteEffects.None);
            kunoichi.LoadTexture(content);
            var skeleton = new Skeleton(new Vector2(screenWidth / 2, screenHeight / 2), 3.0f, 95, SpriteEffects.None);
            skeleton.LoadTexture(content);
            var samurai = new Samurai(new Vector2(screenWidth / 2, screenHeight / 2), 3.0f, 95, SpriteEffects.None);
            samurai.LoadTexture(content);
            classes = new List<Player>()
            {
                kunoichi,
                skeleton,
                samurai
            };
            Button switchRightButton = new Button(arrowTexture, 0.15f, SpriteEffects.None)
            {
                Position = new Vector2(5 * screenWidth / 8, screenHeight / 2)
            };
            switchRightButton.Click += SwitchRightButton_Click;
            Button switchLeftButton = new Button(arrowTexture, 0.15f, SpriteEffects.FlipHorizontally)
            {
                Position = new Vector2(3 * screenWidth / 8, screenHeight / 2)
            };
            switchLeftButton.Click += SwitchLeftButton_Click;
            Button selectCharacterButton = new Button(buttonTexture, buttonFont, "Select character", 2f ,SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 200)
            };
            selectCharacterButton.Click += SelectCharacterButton_Click;
            buttons = new List<Button>()
            {
                switchLeftButton,
                switchRightButton,
                selectCharacterButton
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
            classes[classIndex].Draw(spriteBatch);
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            foreach(var b in buttons)
            {
                b.Update(gameTime);
            }
            classes[classIndex].Update(gameTime);
        }
        public override void UnloadContent()
        {
            
        }

    }
}
