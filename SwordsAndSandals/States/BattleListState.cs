using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Objects;
using SwordsAndSandals.Objects.Grid;

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SwordsAndSandals.States
{
    public class BattleListState : State
    {
        private List<Button> buttons;
        private List<Component> components;
        private Background background;
        private List<SpectateBattleInfo> spectateInfo;

        private int screenWidth;
        private int screenHeight;

        private List<Button> SpectateButtons = new List<Button>();

        public BattleListState(GraphicsDeviceManager graphicsDevice, List<SpectateBattleInfo> spectateInfo) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
            this.spectateInfo = spectateInfo;
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            StateManager.Instance.ChangeState(new TownState(graphicsDevice));
        }
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Invoke("GetBattleList");
        }
        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            Texture2D dotTexture = content.Load<Texture2D>("Objects/Dot");
            SpriteFont font = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            Text text = new Text(font)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 8),
                TextString = "Battle list",
                TextSize = 2f,
                PenColour = Color.Orange,
                OutlineColor = Color.Black,
            };
            Button backbutton = new Button(buttonTexture, font, "Back", 2f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 6, 7 * screenHeight / 8),
            };
            backbutton.Click += BackButton_Click;
            Button refreshButton = new Button(buttonTexture, font, "Refresh", 2f, SpriteEffects.None)
            {
                Position = new Vector2(10 * screenWidth / 11, screenHeight / 15),
            };
            refreshButton.Click += RefreshButton_Click;
            buttons = new List<Button>()
            {
                backbutton,
                refreshButton
            };
            components = new List<Component>()
            {
                text
            };

            if (spectateInfo.Count > 0)
            {
                GridTable gridTable = new GridTable(font, dotTexture, new Vector2(screenWidth / 2, screenHeight / 2), 300, 5);
                gridTable.AddColumn("Player 1");
                gridTable.AddColumn("Player 2");
                gridTable.AddColumn("Start time");
                gridTable.AddColumn("");

                foreach (SpectateBattleInfo item in spectateInfo)
                {
                    SpectateBattleInfo localItem = item;

                    Button spectate = new Button(buttonTexture, font, "Spectate", 1f, SpriteEffects.None);

                    EventHandler handler = (o, e) =>
                    {
                        Debug.WriteLine($"Tag is a string: {localItem.ToString()}");
                    };
                    spectate.Click += handler;
                    SpectateButtons.Add(spectate);

                    gridTable.AddRow(new List<string> { localItem.Player1, localItem.Player2, localItem.StartTime });
                    gridTable.AddButton(spectate);
                }
                components.Add(gridTable);

            }
            else
            {
                Text noMatch = new Text(font)
                {
                    Position = new Vector2(screenWidth / 2, screenHeight / 2),
                    TextString = "No matches to watch!",
                    TextSize = 2f,
                    PenColour = Color.Orange,
                    OutlineColor = Color.Black,
                };
                components.Add(noMatch);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }
            foreach (var b in buttons)
            {
                b.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.Update(gameTime);
            }
            foreach (var b in buttons)
            {
                b.Update(gameTime);
            }

        }
    }
}
