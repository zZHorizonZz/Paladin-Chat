using PacketLibrary.Network;
using System;

namespace Client
{
    public class Client
    {
        public Guid Uuid;
        public string Name;

        public IConnection Connection;

        public Client(Guid uuid, string name)
        {
            Uuid = uuid;
            Name = name;
        }

        public void Initialize(IConnection connection)
        {
            Connection = connection;
        }
    }
}
