using Microsoft.Xna.Framework;

using System.Collections.Generic;

using SwordsAndSandals.States;
using SwordsAndSandals.UI;
using SwordsAndSandals.Stats;

namespace SwordsAndSandals.Memento
{
    public class Caretaker
    {
        private List<ShopState.Memento> mementos;
        private ShopState shopState;

        public Caretaker(GraphicsDeviceManager graphicsDevice, string playerClass)
        {
            this.mementos = new();
            this.shopState = new(graphicsDevice, playerClass, this);
        }

        public void SaveShopState(List<Button> buttons, List<string> buyNames, Attributes attributes, int buttonsCount)
        {
            shopState.SetButtons(buttons);
            shopState.SetBuyNames(buyNames);
            shopState.SetAttributes(attributes);
            shopState.SetButtonsCount(buttonsCount);

            mementos.Add(shopState.CreateMemento());
        }

        public ShopState.Memento GetLastMemento()
        {
            if (mementos.Count != 0)
            {
                return mementos[mementos.Count - 1];
            }
            else
            {
                return null;
            }
        }
    }
}