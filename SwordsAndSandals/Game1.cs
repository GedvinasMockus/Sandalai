using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Newtonsoft.Json.Linq;

using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.States;

using System;
using System.Collections.Generic;

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
            ConnectionManager.Instance.AddHandler("OpponentFound", info =>
            {
                BattleInfo bInfo = info.ToObject<BattleInfo>();
                StateManager.Instance.ChangeState(new GameState(_graphics, bInfo));
            });
            ConnectionManager.Instance.AddHandler<string>("AbilityUsed", (name) =>
            {
                if (StateManager.Instance.CurrentState is GameState)
                {
                    (StateManager.Instance.CurrentState as GameState).opponent.UseAbility(name);
                }
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
            ConnectionManager.Instance.AddHandler("BattleListInfo", info =>
            {
                JArray jArray = JArray.Parse(info.ToString());

                List<SpectateBattleInfo> battleList = new List<SpectateBattleInfo>();

                foreach (var item in jArray)
                {
                    var innerList = item.ToObject<List<string>>();
                    battleList.Add(new SpectateBattleInfo(innerList));
                }
                StateManager.Instance.ChangeState(new BattleListState(_graphics, battleList));
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