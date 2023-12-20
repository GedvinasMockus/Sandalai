namespace SwordsAndSandals.Proxy
{
    public class RealItem : IItem
    {
        public bool CheckItemAvailability(string playerClass, string armour)
        {
            return true;
        }
    }
}