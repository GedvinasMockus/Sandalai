using SwordsAndSandals.UI.Console;

namespace SwordsAndSandals.Interpreter
{
    public class ShowFpsExpression : Expression
    {
        private Expression expr2 = null;
        public ShowFpsExpression(Expression expr2)
        {
            this.expr2 = expr2;
        }
        public bool Interpret(string context)
        {
            if (expr2.Interpret(context))
            {
                GameConsole.Instance.ShowFps(context.Equals("1"));
                return true;
            }
            return false;
        }
    }
}
