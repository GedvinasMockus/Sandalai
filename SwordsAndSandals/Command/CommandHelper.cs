namespace SwordsAndSandals.Command
{
    public static class CommandHelper
    {
        public static void ExecuteCommand(ICommand command)
        {
            CommandHistory.Instance.Push(command);
            command.Execute();
        }
        public static void UndoCommand(int count = 1)
        {
            ICommand command = null;
            for (int i = 0; i < count; i++)
            {
                command = CommandHistory.Instance.Pop();
            }
            command?.Execute();
        }
    }
}
