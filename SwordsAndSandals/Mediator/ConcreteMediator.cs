using System;
using SwordsAndSandals.Logging;
using SwordsAndSandals.UI;

namespace SwordsAndSandals.Mediator
{
    public class ConcreteMediator : IMediator
    {
        private Button button;
        private Spinner spinner;
        private Text text;
        private TextBox textBox;

        public ConcreteMediator(IMediator mediator)
        {
            button = new(mediator);
            spinner = new(mediator);
            text = new(mediator);
            textBox = new(mediator);
        }

        public void Interaction(string state, object obj)
        {
            LogHandler logs = new();

            if (obj.Equals(button.GetType()))
            {
                logs.process(new("INFO", $"Button component is in {state}"));
            }
            else if (obj.Equals(spinner.GetType()))
            {
                logs.process(new("INFO", $"Spinner component is in {state}"));
            }
            else if (obj.Equals(text.GetType()))
            {
                logs.process(new("INFO", $"Text component is in {state}"));
            }
            else if (obj.Equals(textBox.GetType()))
            {
                logs.process(new("INFO", $"TextBox component is in {state}"));
            }
        }
    }
}