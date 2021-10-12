using Client.Network;
using PacketLibrary.Logging;
using System;
using System.Threading;

namespace Paladin
{
    class ChatManager
    {
        public static readonly Logger Logger = Logger.LOGGER;
        public Thread Thread;

        bool Chat;

        public ChatManager()
        {
            Thread = new Thread(new ThreadStart(Run));
            Thread.Start();
        }

        public void Run()
        {
            OpenChat();
        }

        public void OpenChat()
        {
            while (Console.ReadKey().Key != ConsoleKey.Enter && !Chat)
            {
                Console.Read();
                Chat = true;
                CloseChat();
            }
        }

        public void CloseChat()
        {
            while (Console.ReadKey().Key == ConsoleKey.Enter && Chat)
            {
                string message = Console.ReadLine();
                SendMessage(message);
                Chat = false;
                OpenChat();
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                PacketChat chat = new PacketChat(Paladin.Client.Uuid, Paladin.Client.Name, message);
                Paladin.Client.Connection.SendPacket(chat);
                Logger.Info("[CHAT] {0}> {1}", new object[] { Paladin.Client.Name, message });
            }
            catch (Exception exception)
            {
                Logger.Error("Message can not be sent because error occured.", exception);
            }
        }
    }
}
