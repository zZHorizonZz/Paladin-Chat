using Client.Network;
using PacketLibrary.Logging;
using System;
using System.Net.Sockets;

namespace Paladin
{
    class Paladin
    {
        public static readonly Logger Logger = Logger.LOGGER;

        static void Main(string[] args)
        {

            Client.Client client = new Client.Client(Guid.NewGuid(), "Paladin");

            try
            {
                client.Connect(20000);

                client.Connection.GetCurrentProtocol().ProtocolRegistry.RegisterInboundWithHandler(0x00, new PingCodec(), new PacketPing().GetType(), new PacketPingHandler());
                client.Connection.GetCurrentProtocol().ProtocolRegistry.RegisterInboundWithHandler(0x01, new PacketResponse(), new PacketResponse().GetType(), new PacketResponse());
                client.Connection.GetCurrentProtocol().ProtocolRegistry.RegisterOutbound(0x00, new PongCodec(), new PacketPong().GetType());
                client.Connection.GetCurrentProtocol().ProtocolRegistry.RegisterOutbound(0x01, new PacketRequest(), new PacketRequest().GetType());

                System.Threading.Thread.Sleep(1000);

                client.Connection.SendPacket(new PacketRequest(client.Name, client.Uuid));
                Logger.Info("Request to connect to server has been successfully sent. Awaiting response...");

                while (client.Connection.GetClient().Connected)
                {
                    client.Connection.Read();
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
                client.Connection.GetClient().Close();
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
