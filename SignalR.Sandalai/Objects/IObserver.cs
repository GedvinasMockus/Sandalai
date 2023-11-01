using SignalR.Sandalai.InfoStructs;

using System.Collections.Generic;

namespace SignalR.Sandalai.Objects
{
    public interface IObserver
    {
        void SendInfo(List<BattleInfo> battleInfos);
    }
}
