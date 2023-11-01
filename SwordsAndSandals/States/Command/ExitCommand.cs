namespace SwordsAndSandals.States.Command
{
    public class ExitCommand : ICommand
    {
        public void Execute()
        {
            StateManager.Instance.ChangeState(null);
        }
    }
}
