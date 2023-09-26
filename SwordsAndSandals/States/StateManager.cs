using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.States
{
    public class StateManager
    {
        private State currentState;
        private ContentManager content;

        private static StateManager instance;
        public static StateManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new StateManager();
                }
                return instance;
            }
        }
        private StateManager() {}

        public void SetContentManager(ContentManager content)
        {
            this.content = content;
        }

        public void ChangeState(State newState)
        {
            if(newState != null) newState.LoadContent(content);
            currentState = newState;
        }

        public void Update(GameTime gameTime)
        {
            if(currentState != null) currentState.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            if(currentState != null) currentState.Draw(batch);
        }

        public bool NotInAState()
        {
            return currentState == null;
        }

    }
}
