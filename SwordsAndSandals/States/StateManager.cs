using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.States.Command;

using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class StateManager
    {
        public State CurrentState { get; private set; }
        public CommandHistory commandHistory { get; private set; }
        private ContentManager content;

        private static StateManager instance;

        public Stack<State> stateStack = new Stack<State>();
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
            commandHistory = new CommandHistory();
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

        public void AddToHistory(ICommand command)
        {
            commandHistory.ExecuteCommand(command);
            stateStack.Push(CurrentState);
        }
        //public void ExecuteCommand(ICommand command)
        //{
        //    stateStack.Push(CurrentState);
        //    command.Execute();
        //    commandStack.Push(command);
        //}

        //public void UndoLastCommand()
        //{
        //    if (commandStack.Count > 0)
        //    {
        //        ICommand lastCommand = commandStack.Pop();
        //        lastCommand.Undo();
        //    }
        //}

    }
}
