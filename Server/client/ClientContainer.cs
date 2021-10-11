using System;
using System.Collections.Generic;

namespace Server.client
{
    class ClientContainer
    {
        private readonly IDictionary<string, Client.Client> _clientMap = new Dictionary<string, Client.Client>();

        public Client.Client AddClient(Guid uuid, string name)
        {
            if (_clientMap.ContainsKey(name))
            {
                return _clientMap[name];
            }

            Client.Client client = new Client.Client(uuid, name);
            _clientMap.Add(name, client);
            return client;
        }

        public bool RemoveClient(string name)
        {
            return _clientMap.Remove(name);
        }

        public bool IsClient(string name)
        {
            return _clientMap.ContainsKey(name);
        }

        public int Size()
        {
            return _clientMap.Count;
        }

        public IDictionary<string, Client.Client> GetClientMap()
        {
            return _clientMap;
        }
    }
}
