using Microsoft.AspNet.SignalR.Client;

using System;

namespace SwordsAndSandals
{
    public class ConnectionManager
    {
        private HubConnection connection;
        private static ConnectionManager instance;
        public IHubProxy hub { get; private set; }
        public static ConnectionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectionManager();
                }
                return instance;
            }
        }
        private ConnectionManager()
        {
            connection = new HubConnection("http://localhost:8081");
        }
        public void AddHub(string name)
        {
            hub = connection.CreateHubProxy(name);
        }
        public void StartConnection()
        {
            connection.Start().Wait();
        }
        public void StopConnection()
        {
            connection.Stop();
        }
        public void Invoke(string method, params object[] args)
        {
            hub.Invoke(method, args);
        }
        public void AddHandler(string name, Action handler)
        {
            hub.On(name, handler);
        }
        public void AddHandler(string name, Action<dynamic> handler)
        {
            hub.On(name, handler);
        }
        public void AddHandler<A>(string name, Action<A> handler)
        {
            hub.On<A>(name, handler);
        }
        public void AddHandler<A, B>(string name, Action<A, B> handler)
        {
            hub.On<A, B>(name, handler);
        }
        public void AddHandler<A, B, C>(string name, Action<A, B, C> handler)
        {
            hub.On<A, B, C>(name, handler);
        }
        public void AddHandler<A, B, C, D>(string name, Action<A, B, C, D> handler)
        {
            hub.On<A, B, C, D>(name, handler);
        }
        public void AddHandler<A, B, C, D, E>(string name, Action<A, B, C, D, E> handler)
        {
            hub.On<A, B, C, D, E>(name, handler);
        }
        public void AddHandler<A, B, C, D, E, F>(string name, Action<A, B, C, D, E, F> handler)
        {
            hub.On<A, B, C, D, E, F>(name, handler);
        }
        public void AddHandler<A, B, C, D, E, F, G>(string name, Action<A, B, C, D, E, F, G> handler)
        {
            hub.On<A, B, C, D, E, F, G>(name, handler);
        }

    }
}
