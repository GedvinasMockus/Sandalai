using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects;
using SwordsAndSandals.Objects.Animations;
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
        private List<Animation> sprites;
        private List<string> classes;
        private List<Button> buttons;
        private IHubProxy hub;

        private int screenWidth;
        private int screenHeight;
        private int spriteIndex;
        public CharacterSelectionState(GraphicsDeviceManager graphicsDevice, IHubProxy hub) : base(graphicsDevice)
        {
            this.hub = hub;
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void SwitchRightButton_Click(object sender, EventArgs e)
        {
            spriteIndex = (spriteIndex + 1) % sprites.Count;
            sprites[spriteIndex].Reset();
        }
        private void SwitchLeftButton_Click(object sender, EventArgs e)
        {
            spriteIndex--;
            if (spriteIndex < 0) spriteIndex += sprites.Count;
            sprites[spriteIndex].Reset();
        }
        private void SelectCharacterButton_Click(object sender, EventArgs e)
        {
            hub.Invoke("AddToLobby", classes[spriteIndex]);
            hub.Invoke("FindOpponent");
            StateManager.Instance.ChangeState(new LoadingScreenState(_graphicsDevice, hub));
        }

        public override void LoadContent(ContentManager content)
        {
            spriteIndex = 0;
            Texture2D arrowTexture = content.Load<Texture2D>("Icons/arrow");
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            sprites = new List<Animation>()
            {
                new KunoichiIdleAnimation(content, 0.1f, SpriteEffects.None),
                new SamuraiIdleAnimation(content, 0.1f, SpriteEffects.None),
                new SkeletonIdleAnimation(content, 0.1f, SpriteEffects.None)
            };
            classes = new List<string>()
            {
                "Kunoichi",
                "Samurai",
                "Skeleton"
            };
            Button switchLeftButton = new Button(arrowTexture, 0.15f, SpriteEffects.FlipHorizontally)
            {
                Position = new Vector2(3 * screenWidth / 8, screenHeight / 2)
            };
            switchLeftButton.Click += SwitchLeftButton_Click;
            Button switchRightButton = new Button(arrowTexture, 0.15f, SpriteEffects.None)
            {
                Position = new Vector2(5 * screenWidth / 8, screenHeight / 2)
            };
            switchRightButton.Click += SwitchRightButton_Click;
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
            Animation active = sprites[spriteIndex];
            active.Draw(spriteBatch, new Vector2(screenWidth / 2, screenHeight / 2 - active.frameHeight/2 * active.Scale + 38.4f), new Vector2(active.frameWidth / 2, active.frameHeight / 2));
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            foreach(var b in buttons)
            {
                b.Update(gameTime);
            }
            sprites[spriteIndex].Update(gameTime);
        }
        public override void UnloadContent()
        {
            
        }

    }
}
