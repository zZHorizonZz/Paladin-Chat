using PacketLibrary.Logging;
using System;

namespace Server
{
    class Server
    {
        static readonly Logger Logger = Logger.LOGGER;

        static void Main(string[] args)
        {
            ServerManager serverManager = new ServerManager();
            serverManager.Start();

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
