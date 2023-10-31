using System.Collections.Generic;

namespace SwordsAndSandals.States.Command
{
    public class CommandHistory
    {
        private readonly Stack<ICommand> commandStack;
        private static CommandHistory instance;
        public static CommandHistory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommandHistory();
                }
                return instance;
            }
        }
        private CommandHistory()
        {
            commandStack = new Stack<ICommand>();
        }
        public void Push(ICommand command)
        {
            commandStack.Push(command);
        }
        public ICommand Pop()
        {
            if (commandStack.Count > 1)
            {
                commandStack.Pop();
                return commandStack.Peek();
            }
            else if (commandStack.Count > 0)
            {
                commandStack.Pop();
            }
            return new ExitCommand();
        }
    }
}
