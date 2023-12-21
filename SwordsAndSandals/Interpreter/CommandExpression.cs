using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SwordsAndSandals.Interpreter
{
    public class CommandExpression : Expression
    {
        private Dictionary<string, Expression> expressions;
        private Expression currentExpression;
        public CommandExpression()
        {
            expressions = new Dictionary<string, Expression>();
        }
        public bool Interpret(string context)
        {
            Regex regex = new Regex(@"^(/[\w-]+)\s*(.*)");
            Match match = regex.Match(context);
            currentExpression = expressions.GetValueOrDefault(match.Groups[1].Value);
            if (currentExpression != null) return currentExpression.Interpret(match.Groups[2].Value);
            return false;
        }

        public void AddExpression(string name, Expression expression)
        {
            expressions.Add(name, expression);
        }
    }
}

