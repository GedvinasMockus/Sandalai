using Microsoft.Xna.Framework.Input;
using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Iterators
{
    public class ButtonAggregate : Aggregate
    {
        private List<Button> buttons = new List<Button>();
        private List<Button> used = new List<Button>();

        public ButtonAggregate(List<Button> Buttons)
        {
            this.buttons = Buttons;
        }
        public override Iterator CreateIterator(String type)
        {
            switch (type)
            {
                case "Forward":
                    return new ForwardsIterator(this);
                case "Backward":
                    return new BackwardsIterator(this);
                case "Sorted":
                    return new SortedIterator(this);
                default:
                    throw new ArgumentException("Invalid iterator type");
            }
        }

        public int Count()
        {
            return buttons.Count;
        }

        public Button Get(int index)
        {
            return buttons[index];
        }

        public Button GetLargest()
        {
            float max = float.MinValue;

            int index = -1;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Scale > max && !used.Contains(buttons[i]))
                {
                    max = buttons[i].Scale;
                    index = i;
                }

            }

            Button button = buttons[index];
            used.Add(button);

            Debug.WriteLine("Button: " + button.Position);

            return button;
        }
    }
}
