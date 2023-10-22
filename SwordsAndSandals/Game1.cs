using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.States;

using System;
using System.Diagnostics;

namespace SwordsAndSandals
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int _screenHeight = 1080;
        private const int _screenWidth = 1920;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ConnectionManager.Instance.AddHub("MainHub");
            ConnectionManager.Instance.AddHandler<BattleInfo>("OpponentFound", (info) =>
            {
                StateManager.Instance.ChangeState(new GameState(_graphics, info));
            });
            ConnectionManager.Instance.AddHandler<string, BattleInfo>("AbilityUsed", (name, info) =>
            {
                if (StateManager.Instance.CurrentState is GameState)
                {
                    //TODO implement battle state update
                    (StateManager.Instance.CurrentState as GameState).opponent.UseAbility(name);
                }
            });
            ConnectionManager.Instance.AddHandler<BattleInfo>("BattleInfoUpdated", (info) =>
            {
                //TODO implement battle state update
            });
            ConnectionManager.Instance.AddHandler("BattleLeft", () =>
            {
                StateManager.Instance.ChangeState(new CharacterSelectionState(_graphics));
            });
            ConnectionManager.Instance.AddHandler("BackToLoading", () =>
            {
                ConnectionManager.Instance.Invoke("FindOpponent");
                StateManager.Instance.ChangeState(new LoadingScreenState(_graphics));
            });
            ConnectionManager.Instance.StartConnection();
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            StateManager.Instance.SetContentManager(Content);
            StateManager.Instance.ChangeState(new MenuState(_graphics));
        }

        protected override void Update(GameTime gameTime)
        {
            if (StateManager.Instance.NotInAState()) this.Exit();
            StateManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);
            StateManager.Instance.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            ConnectionManager.Instance.StopConnection();
            base.OnExiting(sender, args);
        }
    }
}