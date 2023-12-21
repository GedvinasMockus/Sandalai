using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Classes;
using SwordsAndSandals.Command;
using SwordsAndSandals.Command.StateChangeCommand;
using SwordsAndSandals.Mediator;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.States
{
    public class BenchmarkState : State
    {
        private int screenWidth;
        private int screenHeight;

        private PlayerFactory factory;
        private List<Player> players;
        private List<string> classes;
        private List<Button> buttons;
        private Text ramUsed;

        private bool running;
        private Random rng;
        private IMediator mediator;
        
        public BenchmarkState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;

            rng = new Random();
            running = false;
        }
        private void StartBenchmarkWithCacheButton_Click(object sender, EventArgs e)
        {
            factory.UseAnimationCache = true;
            running = true;
        }
        private void StartBenchmarkWithoutCacheButton_click(object sender, EventArgs e)
        {
            factory.UseAnimationCache = false;
            running = true;
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            CommandHelper.UndoCommand();
        }
        public override void LoadContent(ContentManager content)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("Views/Button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/vinque");

            factory = new PlayerFactory(content);
            players = new List<Player>();
            classes = new List<string>()
            {
                "Kunoichi",
                "Samurai",
                "Skeleton"
            };

            ramUsed = new Text(buttonFont, mediator)
            {
                PenColour = Color.Orange,
                TextSize = 1.5f,
                Position = new Vector2(screenWidth / 2, screenHeight / 2 - 100),
            };
            Button startBenchmarkWithCache = new Button(buttonTexture, buttonFont, "Start benchmark (wc)", 2f, SpriteEffects.None, mediator)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2),
            };
            startBenchmarkWithCache.Click += StartBenchmarkWithCacheButton_Click;
            Button startBenchmarkWithoutCache = new Button(buttonTexture, buttonFont, "Start benchmark (nc)", 2f, SpriteEffects.None, mediator)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 100),
            };
            startBenchmarkWithoutCache.Click += StartBenchmarkWithoutCacheButton_click;
            Button backButton = new Button(buttonTexture, buttonFont, "Back", 2f, SpriteEffects.None, mediator)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 2 + 200),
            };
            backButton.Click += BackButton_Click;
            buttons = new List<Button>()
            {
                startBenchmarkWithCache,
                startBenchmarkWithoutCache,
                backButton
            };

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (running)
            {
                foreach (var p in players)
                {
                    p.Draw(spriteBatch);
                }
            }
            else
            {
                foreach (var b in buttons)
                {
                    b.Draw(spriteBatch);
                }
                if (ramUsed.TextString != null) ramUsed.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            if (running)
            {
                if (players.Count >= 2500)
                {
                    using (Process proc = Process.GetCurrentProcess())
                    {
                        proc.Refresh();
                        ramUsed.TextString = (proc.PrivateMemorySize64 / 1024).ToString() + "KB of ram is used";
                    }
                    running = false;
                    players.Clear();
                }
                else
                {
                    foreach (var p in players)
                    {
                        p.Update(gameTime);
                    }
                    for (int i = 0; i < 25; i++)
                    {
                        int health = rng.Next(1000, 2001);
                        Attributes attrs = new Attributes()
                        {
                            MaxHealth = health,
                            CurrHealth = health,
                            BaseDistance = 0,
                            ArmourRating = 0,
                        };
                        players.Add(factory.CreatePlayerWithoutButtons(classes[rng.Next(0, classes.Count)], new Vector2(rng.Next(0, screenWidth + 1), rng.Next(0, screenHeight + 1)), SpriteEffects.None, null, attrs, "name"));
                    }
                }
            }
            else
            {
                foreach (var b in buttons)
                {
                    b.Update(gameTime);
                }
            }
        }
        public override void UnloadContent()
        {

        }

    }
}
