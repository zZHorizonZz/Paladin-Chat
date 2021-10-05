using PacketLibrary.Network;
using System;
using System.Net.Sockets;

namespace Paladin
{
    class Paladin
    {
        static void Main(string[] args)
        {
            try
            {
                ClientBootstrap client = new ClientBootstrap(20000);
                DefaultConnection connection = new DefaultConnection(new SimpleProtocol(), client.Connect());
                connection.GetCurrentProtocol().ProtocolRegistry.RegisterInboundWithHandler(0x00, new PingCodec(), new PacketPing().GetType(), new PacketPingHandler());
                connection.GetCurrentProtocol().ProtocolRegistry.RegisterOutbound(0x01, new PongCodec(), new PacketPong().GetType());

                NetworkStream stream = connection.GetClient().GetStream();

                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Reading packet...");
                connection.Read();

                stream.Close();
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine("ArgumentNullException: {0}", exception);
            }
            catch (SocketException exception)
            {
                Console.WriteLine("SocketException: {0}", exception);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
