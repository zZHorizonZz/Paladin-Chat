using PacketLibrary.Logging;
using PacketLibrary.Network;
using System.Collections.Generic;
using System.Threading;

namespace Client
{
    class ConnectionHandler
    {
        public static readonly Logger Logger = Logger.LOGGER;
        public Thread Thread;

        private readonly IList<IConnection> _connections = new List<IConnection>();

        public ConnectionHandler()
        {
            Thread = new Thread(new ThreadStart(Run));
            Thread.Start();
        }

        public void Run()
        {
            while (true)
            {
                Logger.Info("Waiting for a connection... ");

                IConnection connection = Server.ServerManager.Instance.Bootstrap.HandleIncomingConnection();
                if (connection != null)
                {
                    _connections.Add(connection);
                    Logger.Info("Connection between {0} and server has been successfully established.", new object[] { connection.GetClient().Client.AddressFamily.ToString() });
                }
            }
        }

        public IList<IConnection> GetConnections()
        {
            return _connections;
        }
    }
}
