using Client.Network;
using PacketLibrary.Logging;
using System;
using System.Net.Sockets;

namespace Paladin
{
    class Paladin
    {
        public static readonly Logger Logger = Logger.LOGGER;
        public static Client.Client Client;
        public static ChatManager ChatManager;

        static void Main(string[] args)
        {

            Client = new Client.Client(Guid.NewGuid(), "Paladin");
            ChatManager = new ChatManager();

            try
            {
                Client.Connect(20000);

                Client.Connection.GetCurrentProtocol().ProtocolRegistry.RegisterInboundWithHandler(0x00, new PingCodec(), new PacketPing().GetType(), new PacketPingHandler());
                Client.Connection.GetCurrentProtocol().ProtocolRegistry.RegisterInboundWithHandler(0x01, new PacketResponse(), new PacketResponse().GetType(), new PacketResponse());
                Client.Connection.GetCurrentProtocol().ProtocolRegistry.RegisterOutbound(0x00, new PongCodec(), new PacketPong().GetType());
                Client.Connection.GetCurrentProtocol().ProtocolRegistry.RegisterOutbound(0x01, new PacketRequest(), new PacketRequest().GetType());
                Client.Connection.GetCurrentProtocol().ProtocolRegistry.RegisterOutbound(0x02, new PacketChat(), new PacketChat().GetType());

                System.Threading.Thread.Sleep(1000);

                Client.Connection.SendPacket(new PacketRequest(Client.Name, Client.Uuid));

                Logger.Info("Request to connect to server has been successfully sent. Awaiting response...");

                while (Client.Connection.GetClient().Connected)
                {
                    Client.Connection.Read();
                    System.Threading.Thread.Sleep(2000);
                }
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine("ArgumentNullException: {0}", exception);
            }
            catch (SocketException exception)
            {
                Console.WriteLine("SocketException: {0}", exception);
            }
            finally
            {
                Client.Connection.GetClient().Close();
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
