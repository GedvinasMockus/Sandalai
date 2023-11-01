using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwordsAndSandals.States
{
    public class StateManager
    {
        public State CurrentState { get; private set; }
        private ContentManager content;

        private static StateManager instance;
        public static StateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StateManager();
                }
                return instance;
            }
        }
        private StateManager() { }

        public void SetContentManager(ContentManager content)
        {
            this.content = content;
        }

        public void ChangeState(State newState)
        {
            if (newState != null) newState.LoadContent(content);
            CurrentState = newState;
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentState != null) CurrentState.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            if (CurrentState != null) CurrentState.Draw(batch);
        }

        public bool NotInAState()
        {
            return CurrentState == null;
        }
    }
}
