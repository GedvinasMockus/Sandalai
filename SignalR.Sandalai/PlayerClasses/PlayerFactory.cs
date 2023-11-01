using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Sandalai.PlayerClasses
{
    public class PlayerFactory
    {
        public Player CreatePlayer(string connid, string className)
        {
            switch(className)
            {
                case "Kunoichi":
                    return new KunoichiPlayer(connid);
                case "Samurai":
                    return new SamuraiPlayer(connid);
                case "Skeleton":
                    return new SkeletonPlayer(connid);
                default:
                    return null;
            }
        }
    }
}
