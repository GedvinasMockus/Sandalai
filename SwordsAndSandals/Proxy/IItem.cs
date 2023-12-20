namespace SwordsAndSandals.Proxy
{
    public interface IItem
    {
        public bool CheckItemAvailability(string playerClass, string armour);
    }
}