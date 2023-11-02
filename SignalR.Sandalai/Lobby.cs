using SignalR.Sandalai.Objects;
using SignalR.Sandalai.PlayerClasses;
using System.Collections.Generic;
using System.Linq;

namespace SignalR.Sandalai
{
    public class Lobby
    {
        private Dictionary<string, Player> myUsers;
        private PlayerFactory factory;
        private static object Obj = new object();
        private static volatile Lobby instance;
        private Lobby()
        {
            myUsers = new Dictionary<string, Player>();
            factory = new PlayerFactory();
        }
        public static Lobby Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (Obj)
                    {
                        if (instance == null)
                        {
                            instance = new Lobby();
                        }
                    }
                }
                return instance;
            }
        }
        public void AddUser(string userId, string className, string name)
        {
            lock (myUsers)
            {
                myUsers.Add(userId, factory.CreatePlayer(userId, className, name));
            }
        }
        public void RemoveUser(string userId)
        {
            lock (myUsers)
            {
                myUsers.Remove(userId);
            }
        }
        public void TakePair(string id1, out Player p1, out Player p2)
        {
            p1 = null;
            p2 = null;
            lock(myUsers)
            {
                if(myUsers.TryGetValue(id1, out p1))
                {
                    string id2 = myUsers.FirstOrDefault(pair => !pair.Key.Equals(id1)).Key;
                    if(id2 != null)
                    {
                        p1 = (Player)p1.Clone();
                        p2 = (Player)myUsers[id2].Clone();
                        myUsers.Remove(id1);
                        myUsers.Remove(id2);
                    }
                }
            }
        }
    }
}
