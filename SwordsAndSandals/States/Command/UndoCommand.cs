namespace SwordsAndSandals.States.Command
{
    public class UndoCommand : ICommand
    {
        private readonly CommandHistory commandHistory;

        public UndoCommand(CommandHistory history)
        {
            commandHistory = history;
        }

        public void Execute()
        {
            commandHistory.UndoLastCommand();
        }

        public void Undo()
        {
        }
    }
}
