using SwordsAndSandals.States;

namespace SwordsAndSandals.Command
{
    public class ExitCommand : ICommand
    {
        public void Execute()
        {
            StateManager.Instance.ChangeState(null);
        }
    }
}
