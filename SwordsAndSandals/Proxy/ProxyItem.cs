using System.Collections.Generic;

namespace SwordsAndSandals.Proxy
{
    public class ProxyItem : IItem
    {
        private List<string> helmets = new() { "Bronze Helmet", "Iron Helmet", "Gold Helmet" };
        private List<string> platebody = new() { "Bronze Platebody", "Iron Platebody", "Gold Platebody" };
        private List<string> platelegs = new() { "Bronze Platelegs", "Iron Platelegs", "Gold Platelegs" };

        private RealItem item = new();

        public bool CheckItemAvailability(string playerClass, string armour)
        {
            if (playerClass.Equals("Kunoichi") && !helmets.Contains(armour))
            {
                return false;
            }
            else if (playerClass.Equals("Samurai") && helmets.Contains(armour))
            {
                return false;
            }
            else if (playerClass.Equals("Skeleton") && !platelegs.Contains(armour))
            {
                return false;
            }

            return item.CheckItemAvailability(playerClass, armour);
        }
    }
}