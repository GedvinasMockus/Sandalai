namespace SwordsAndSandals.Interpreter
{
    public interface Expression
    {
        bool Interpret(string context);
    }
}
