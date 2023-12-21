﻿namespace SwordsAndSandals.Interpreter
{
    public class OrExpression : Expression
    {
        private Expression expr1 = null;
        private Expression expr2 = null;
        public OrExpression(Expression expr1, Expression expr2)
        {
            this.expr1 = expr1;
            this.expr2 = expr2;
        }
        public bool Interpret(string context)
        {
            return expr1.Interpret(context) || expr2.Interpret(context);
        }
    }
}