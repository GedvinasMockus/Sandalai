using SwordsAndSandals.UI.Console;

namespace SwordsAndSandals.Interpreter
{
    public class CleanConsoleExpression : Expression
    {
        private Expression expr2 = null;
        public CleanConsoleExpression(Expression expr2)
        {
            this.expr2 = expr2;
        }
        public bool Interpret(string context)
        {
            if (expr2.Interpret(context))
            {
                GameConsole.Instance.CleanConsole();
                return true;
            }
            return false;
        }
    }
}
