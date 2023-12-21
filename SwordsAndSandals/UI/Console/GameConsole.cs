using GeonBit.UI;
using GeonBit.UI.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SwordsAndSandals.Interpreter;

using System;

namespace SwordsAndSandals.UI.Console
{
    public class GameConsole
    {
        private static GameConsole instance;
        private CommandExpression commandList;

        private Panel panel;
        private Panel textPanel;
        private TextInput input;
        private RichParagraph paragraph;

        private KeyboardState current;
        private KeyboardState previous;

        private bool showFps;
        private int height;
        private int frameCounter;
        private int frameRate;
        private float screenWidth;
        private float screenHeight;
        private TimeSpan elapsedTime;
        public static GameConsole Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameConsole();
                }
                return instance;
            }
        }
        private GameConsole() { }

        public void CreateConsole(float screenWidth, float screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            panel = new Panel(size: new Vector2(0.5f * this.screenWidth, 0.5f * this.screenHeight), skin: PanelSkin.Simple, anchor: Anchor.TopCenter, offset: Vector2.Zero);
            panel.Padding = new Vector2(10, 10);
            panel.Draggable = true;
            panel.Visible = false;
            height = 0;
            current = Keyboard.GetState();
            commandList = new CommandExpression();

            Expression oneExpression = new TerminalExpression("1");
            Expression zeroExpression = new TerminalExpression("0");
            Expression allExpression = new TerminalExpression("all");
            Expression exitExpression = new TerminalExpression("game");
            Expression exitBattleExpression = new TerminalExpression("battle");
            commandList.AddExpression("/show-fps", new ShowFpsExpression(new OrExpression(zeroExpression, oneExpression)));
            commandList.AddExpression("/clean", new CleanConsoleExpression(allExpression));
            commandList.AddExpression("/exit", new ExitExpression(new OrExpression(exitExpression, exitBattleExpression)));
        }
        public void CreateUI()
        {
            Header header = new Header("Console");
            header.Anchor = Anchor.TopCenter;
            header.FillColor = Color.Orange;

            input = new TextInput(false, anchor: Anchor.BottomCenter);
            input.Anchor = Anchor.Auto;
            textPanel = new Panel(size: new Vector2(0, panel.CalcDestRect().Height - input.CalcDestRect().Height - header.SpaceAfter.Y - header.Padding.Y - 30), skin: PanelSkin.Simple, offset: Vector2.Zero);
            textPanel.Padding = new Vector2(10, 10);
            textPanel.Anchor = Anchor.Auto;

            paragraph = new RichParagraph();
            paragraph.Anchor = Anchor.TopLeft;
            paragraph.BreakWordsIfMust = true;

            textPanel.AddChild(paragraph);

            panel.AddChild(header);
            panel.AddChild(textPanel);
            panel.AddChild(input);

            UserInterface.Active.AddEntity(panel);
        }
        public void OpenClose()
        {
            panel.Visible = !panel.Visible;
            input.Value = "";
            input.IsFocused = false;
        }
        public void InputText()
        {
            paragraph.Text += input.Value + "\n";
            if ((int)(paragraph.GetActualDestRect().Height % (textPanel.GetActualDestRect().Height - paragraph.Padding.Y)) >= height)
            {
                height = paragraph.GetActualDestRect().Height;
            }
            else
            {
                paragraph.Text = paragraph.Text.Substring(paragraph.Text.IndexOf("\n") + 1);
                paragraph.Text = paragraph.Text.Substring(paragraph.Text.IndexOf("\n") + 1);
            }
            if (!commandList.Interpret(input.Value))
            {
                paragraph.Text += "Unrecognized command\n";
            }
            input.Value = "";
            input.IsFocused = false;
        }
        public bool IsVisible() { return panel.Visible; }
        public bool IsFocused() { return input.IsFocused; }
        public void ShowFps(bool value)
        {
            showFps = value;
        }
        public void CleanConsole()
        {
            paragraph.Text = "";
            height = 0;
        }
        public void Update(GameTime gameTime)
        {
            previous = current;
            current = Keyboard.GetState();
            if (current.IsKeyDown(Keys.OemTilde) && !previous.IsKeyDown(Keys.OemTilde))
                OpenClose();
            if (IsFocused() && current.IsKeyDown(Keys.Enter) && !previous.IsKeyDown(Keys.Enter))
            {
                InputText();
            }
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }
        public void Draw(SpriteBatch batch, ContentManager content)
        {
            if (showFps)
            {
                SpriteFont font = content.Load<SpriteFont>("Fonts/vinque");
                frameCounter++;
                var fps = $"FPS: {frameRate}";
                batch.Begin();
                batch.DrawString(font, fps, new Vector2(10, 10), Color.Black);
                batch.End();
            }
        }
    }
}
