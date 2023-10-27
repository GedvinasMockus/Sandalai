using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Objects;
using SwordsAndSandals.Objects.Grid;
using SwordsAndSandals.States.Command;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class BattleListState : State
    {
        private List<Button> buttons;
        private List<Component> components = new List<Component>();
        private Background background;

        private int screenWidth;
        private int screenHeight;

        private List<Button> SpectateButtons = new List<Button>();
        private Texture2D buttonTexture;
        private Texture2D dotTexture;
        private SpriteFont font;
        private Text text;
        public event EventHandler UpdateNeeded;
        public bool InfoAvailable { get; set; }

        public BattleListState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
            InfoAvailable = false;
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Invoke("RemoveSpectator");
            ICommand undoCommand = new UndoCommand(StateManager.Instance.commandHistory);
            undoCommand.Execute();
            //StateManager.Instance.ChangeState(new TownState(graphicsDevice));
        }
        public override void LoadContent(ContentManager content)
        {

            buttonTexture = content.Load<Texture2D>("Views/Button");
            dotTexture = content.Load<Texture2D>("Objects/Dot");
            font = content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            text = new Text(font)
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
            buttons = new List<Button>()
            {
                backbutton
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            text.Draw(spriteBatch);
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
            if (InfoAvailable)
            {
                UpdateNeeded?.Invoke(this, new EventArgs());
                InfoAvailable = false;
            }
        }

        public void UpdateGrid(List<BattleInfo> info)
        {
            components.Clear();
            if (info.Count > 0)
            {
                GridTable gridTable = new GridTable(font, dotTexture, new Vector2(screenWidth / 2, screenHeight / 2), 300, 5);
                gridTable.AddColumn("Player 1");
                gridTable.AddColumn("Player 2");
                gridTable.AddColumn("Start time");
                gridTable.AddColumn("");

                foreach (BattleInfo item in info)
                {
                    GridRow row = new GridRow();
                    Button spectate = new Button(buttonTexture, font, "Spectate", 1f, SpriteEffects.None);
                    EventHandler handler = (o, e) =>
                    {
                        ConnectionManager.Instance.Invoke("SpectateMatch", item.Player1.ConnectionID, item.Player2.ConnectionID);
                    };
                    spectate.Click += handler;
                    SpectateButtons.Add(spectate);
                    row.AddData(new GridColumn(item.Player1.ClassName));
                    row.AddData(new GridColumn(item.Player2.ClassName));
                    row.AddData(new GridColumn(item.StartTime));
                    row.AddButton(spectate);
                    gridTable.AddRow(row);
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
            UpdateNeeded = null;
        }
    }
}
