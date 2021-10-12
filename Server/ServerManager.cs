using Client;
using PacketLibrary.Logging;
using PacketLibrary.Network;
using Server.client;
using Server.Network;
using System;
using System.Net.Sockets;

namespace Server
{
    class ServerManager
    {
        public static readonly Logger Logger = Logger.LOGGER;
        public static ServerManager Instance;

        public string Name = "Default Server";
        public bool Public = true;

        public ConnectionHandler ConnectionHandler;
        public ClientHandler ClientHandler = new ClientHandler();
        public ClientContainer ClientContainer = new ClientContainer();

        public ServerBootstrap Bootstrap;
        public ServerManager()
        {
            Instance = this;
        }

        public void Start()
        {
            Logger.Info("Server is now booting...");
            Bootstrap = new ServerBootstrap("127.0.0.1", 20000);

            Bootstrap.DefaultProtocol.ProtocolRegistry.RegisterOutbound(0x00, new PingCodec(), new PacketPing().GetType());
            Bootstrap.DefaultProtocol.ProtocolRegistry.RegisterOutbound(0x01, new PacketResponse(), new PacketResponse().GetType());
            Bootstrap.DefaultProtocol.ProtocolRegistry.RegisterInboundWithHandler(0x00, new PongCodec(), new PacketPong().GetType(), new PacketPongHandler());
            Bootstrap.DefaultProtocol.ProtocolRegistry.RegisterInboundWithHandler(0x01, new PacketRequest(), new PacketRequest().GetType(), new PacketRequest());
            Bootstrap.DefaultProtocol.ProtocolRegistry.RegisterInboundWithHandler(0x02, new PacketChat(), new PacketChat().GetType(), new PacketChat());

            if (Bootstrap.DefaultProtocol.ProtocolRegistry.GetPacketHandler(new PacketPong().GetType()) == null)
            {
                Logger.Warn("Something went wrong...");
            }

            Logger.Info("Server bootstrap has been successfully created.");

            TcpListener listener = Bootstrap.Start();
            ConnectionHandler = new ConnectionHandler();

            try
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(2000);
                    foreach (IConnection connection in ConnectionHandler.GetConnections())
                    {
                        connection.Read();
                    }
                }
            }
            catch (SocketException exception)
            {
                Logger.Info("SocketException: {0}", new object[] { exception });
            }
            finally
            {
                listener.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
