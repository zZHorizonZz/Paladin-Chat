using System.Collections.Generic;

namespace Server.client
{
    class ClientContainer
    {
        private readonly IDictionary<string, Client> _clientMap = new Dictionary<string, Client>();

        public Client AddClient(string name)
        {
            Client client = _clientMap[name];
            if (client != null)
            {
                return client;
            }

            client = new Client(name);
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

        public IDictionary<string, Client> GetClientMap()
        {
            return _clientMap;
        }
    }
}
