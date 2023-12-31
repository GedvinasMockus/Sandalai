using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SwordsAndSandals.Classes;
using SwordsAndSandals.Command;
using SwordsAndSandals.Command.StateChangeCommand;
using SwordsAndSandals.Sprites;
using SwordsAndSandals.UI;
using SwordsAndSandals.Music;

using System;
using System.Collections.Generic;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Logging;
using System.IO;
using System.Diagnostics;
using SwordsAndSandals.Iterators;
using SwordsAndSandals.Memento;
using SwordsAndSandals.Mediator;

namespace SwordsAndSandals.States
{
    public class CharacterSelectionState : State
    {
        private Background background;
        private List<Player> sprites;
        private List<string> classes;
        private List<Button> buttons;

        private int screenWidth;
        private int screenHeight;
        private int spriteIndex;
        private IMediator mediator;

        public CharacterSelectionState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void SwitchRightButton_Click(object sender, EventArgs e)
        {
            spriteIndex = (spriteIndex + 1) % sprites.Count;
            sprites[spriteIndex].animation.Reset();
        }
        private void SwitchLeftButton_Click(object sender, EventArgs e)
        {
            spriteIndex--;
            if (spriteIndex < 0) spriteIndex += sprites.Count;
            sprites[spriteIndex].animation.Reset();
        }
        private void SelectCharacterButton_Click(object sender, EventArgs e)
        {
            Caretaker caretaker = new(graphicsDevice, classes[spriteIndex]);
            CommandHelper.ExecuteCommand(new TownStateCommand(graphicsDevice, classes[spriteIndex], caretaker));
            LogHandler logs = new LogHandler();
            logs.process(new Log("INFO", "New Character Created"));
            logs.process(new Log("DEBUG", "New Character Created"));
            logs.process(new Log("ERROR", "New Character Created"));
            logs.process(new Log("WARNING", "New Character Created"));
        }
        private void LeaveSelectionButton_Click(object sender, EventArgs e)
        {
            CommandHelper.UndoCommand();
        }

        public override void LoadContent(ContentManager content)
        {
            spriteIndex = 0;
            Texture2D arrowTexture = content.Load<Texture2D>("Icons/arrow");
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            //ITarget target = new ITarget();

            music = new MusicPlayer(content);
            music.stopSong();
            music.playSong("Music/CharacterSelectMusic");

            Vector2 spritePos = new Vector2(screenWidth / 2, screenHeight / 2 + arrowTexture.Height / 2 * 0.15f);
            AnimationFactory factory = new AnimationFactory();
            sprites = new List<Player>()
            {
                new KunoichiBuilder(content, factory).SetPosition(spritePos).SetDefaultAbility(SpriteEffects.None).GetPlayer(),
                new SamuraiBuilder(content, factory).SetPosition(spritePos).SetDefaultAbility(SpriteEffects.None).GetPlayer(),
                new SkeletonBuilder(content, factory).SetPosition(spritePos).SetDefaultAbility(SpriteEffects.None).GetPlayer(),
            };
            classes = new List<string>()
            {
                "Kunoichi",
                "Samurai",
                "Skeleton"
            };
            Button switchLeftButton = new Button(arrowTexture, 0.15f, SpriteEffects.FlipHorizontally, mediator)
            {
                Position = new Vector2(3 * screenWidth / 8, screenHeight / 2)
            };
            switchLeftButton.Click += SwitchLeftButton_Click;
            Button switchRightButton = new Button(arrowTexture, 0.15f, SpriteEffects.None, mediator)
            {
                Position = new Vector2(5 * screenWidth / 8, screenHeight / 2)
            };
            switchRightButton.Click += SwitchRightButton_Click;
            Button selectCharacterButton = new Button(buttonTexture, buttonFont, "Select character", 2f, SpriteEffects.None, mediator)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 200)
            };
            selectCharacterButton.Click += SelectCharacterButton_Click;
            Button leaveSelection = new Button(buttonTexture, buttonFont, "Main menu", 2f, SpriteEffects.None, mediator)
            {
                Position = new Vector2(screenWidth / 8, screenHeight / 12)
            };
            leaveSelection.Click += LeaveSelectionButton_Click;
            buttons = new List<Button>()
            {
                switchLeftButton,
                switchRightButton,
                selectCharacterButton,
                leaveSelection
            };

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            ButtonAggregate agg = new ButtonAggregate(buttons);
            Iterator iter = agg.CreateIterator("Sorted");

            while (iter.hasMore())
            {
                iter.getNext().Draw(spriteBatch);
            }
            /*foreach (var b in buttons)
            {
                b.Draw(spriteBatch);
            }*/
            sprites[spriteIndex].Draw(spriteBatch);
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var b in buttons)
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

