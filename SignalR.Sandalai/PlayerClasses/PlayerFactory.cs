using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Sandalai.PlayerClasses
{
    public class PlayerFactory
    {
        public Player CreatePlayer(string connid, string className, string name)
        {
            switch(className)
            {
                case "Kunoichi":
                    return new KunoichiPlayer(connid, name);
                case "Samurai":
                    return new SamuraiPlayer(connid, name);
                case "Skeleton":
                    return new SkeletonPlayer(connid, name);
                default:
                    return null;
            }
        }
    }
}
