using System.Collections.Generic;

namespace SwordsAndSandals.States.Command
{
    public class CommandHistory
    {
        private readonly Stack<ICommand> commandStack = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            commandStack.Push(command);
        }

        public void UndoLastCommand()
        {
            if (commandStack.Count > 0)
            {
                ICommand lastCommand = commandStack.Pop();
                lastCommand.Undo();
            }
        }
    }
}
