namespace SwordsAndSandals.Interpreter
{
    public class TerminalExpression : Expression
    {
        private string data;
        public TerminalExpression(string data)
        {
            this.data = data;
        }
        public bool Interpret(string context)
        {
            return context.Equals(data);
        }
    }
}
