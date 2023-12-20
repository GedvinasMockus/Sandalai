using SwordsAndSandals.Logging;

namespace SwordsAndSandals.Mediator
{
    public class ConcreteMediator : IMediator
    {
        public void Interaction(string state, object obj)
        {
            LogHandler logs = new();
            logs.process(new("INFO", $"{obj} component is in {state}"));
        }
    }
}