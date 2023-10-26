using System.Collections.Generic;

namespace SwordsAndSandals.InfoStructs
{
    public struct SpectateBattleInfo
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string StartTime { get; set; }
        public SpectateBattleInfo(List<string> data)
        {
            Player1 = data[0];
            Player2 = data[1];
            StartTime = data[2];
        }

        public override string ToString()
        {
            return string.Format($"{Player1},{Player2},{StartTime}");
        }
    }
}
