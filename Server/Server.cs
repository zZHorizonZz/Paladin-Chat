using PacketLibrary.Logging;
using PacketLibrary.Network;
using System;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        static readonly Logger Logger = Logger.LOGGER;

        static void Main(string[] args)
        {
            Logger.Info("Server is now booting...");
            ServerBootstrap bootstrap = new ServerBootstrap("127.0.0.1", 20000);
            bootstrap.DefaultProtocol.ProtocolRegistry.RegisterOutbound(0x00, new PingCodec(), new PacketPing().GetType());
            bootstrap.DefaultProtocol.ProtocolRegistry.RegisterInboundWithHandler(0x01, new PongCodec(), new PacketPong().GetType(), new PacketPongHandler());

            if (bootstrap.DefaultProtocol.ProtocolRegistry.GetPacketHandler(new PacketPong().GetType()) == null)
            {
                Logger.Warn("Something went wrong...");
            }

            Logger.Info("Server bootstrap has been successfully created.");

            TcpListener listener = bootstrap.Start();

            try
            {
                while (true)
                {
                    Logger.Info("Waiting for a connection... ");

                    IConnection connection = bootstrap.HandleIncomingConnection();

                    if (connection != null)
                    {
                        Logger.Info("Connected!");
                        connection.SendPacket(new PacketPing(DateTimeOffset.Now.ToUnixTimeMilliseconds()));
                        Logger.Info("Packet has been sent successfully!");
                    }

                    System.Threading.Thread.Sleep(2000);
                    connection.Read();
                }
            }
            catch (SocketException excepction)
            {
                Logger.Info("SocketException: {0}", new object[] { excepction });
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
