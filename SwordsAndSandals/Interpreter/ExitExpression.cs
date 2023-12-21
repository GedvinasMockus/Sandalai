using SwordsAndSandals.Command;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Interpreter
{
    public class ExitExpression : Expression
    {
        private Expression expr2 = null;
        public ExitExpression(Expression expr2)
        {
            this.expr2 = expr2;
        }
        public bool Interpret(string context)
        {
            if (expr2.Interpret(context))
            {
                if (context.Equals("game"))
                {
                    CommandHelper.ExecuteCommand(new ExitCommand());
                    return true;
                }
                else
                {
                    if (StateManager.Instance.CurrentState is GameState)
                    {
                        ConnectionManager.Instance.Invoke("LeaveBattle");
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
    }
}