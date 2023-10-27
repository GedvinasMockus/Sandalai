namespace SwordsAndSandals.States.Command
{
    public class ChangeStateCommand : ICommand
    {
        private State newState;

        public ChangeStateCommand(State newState)
        {
            this.newState = newState;
        }

        public void Execute()
        {
            StateManager.Instance.AddToHistory(this);
            StateManager.Instance.ChangeState(newState);
        }

        public void Undo()
        {
            StateManager.Instance.ChangeState(StateManager.Instance.stateStack.Pop());
        }
    }
}
