using System.Net.Sockets;
using System.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Chat
{
    class Initializer
    {
        internal static void Start()
        {
            Storage.ToSend = new ConcurrentDictionary<long, Message>();
            InitializeConnection();
            Storage.WriterThread = new Thread(Writer.Send);
            Storage.WriterThread.Priority = ThreadPriority.Highest;
            Storage.ReaderThread = new Thread(Reader.Read);
            Storage.ReaderThread.Priority = ThreadPriority.Highest;
            Storage.ReaderThread.Start();
            Storage.WriterThread.Start();     
            Process.GetCurrentProcess().PriorityBoostEnabled = true;
            Process.GetCurrentProcess().PriorityClass
                            = ProcessPriorityClass.High;
            Work();
        }

        private static void Work()
        {
            var id = Process.GetCurrentProcess().Id.ToString();
            string temp = String.Empty;

            while (temp != "exit")
            {
                temp = Console.ReadLine();
                var message = new Message
                {
                    Author = id,
                    CreationTicks = System.DateTime.Now.Ticks,
                    Text = @temp,
                    Picture = "test"//ImageToString(@"avatar.bmp")
                };
                Storage.ToSend.TryAdd(Storage.ToSend.Count, message);
                Console.WriteLine("Отправлено: " + temp);
            }
            Storage.ServerStream.Close();
            Storage.Client.Close();
        }

        private static void InitializeConnection()
        {
            try
            {
                Storage.Client = new TcpClient();
                Storage.Client.Connect("127.0.0.1", 56000);
                Storage.ServerStream = Storage.Client.GetStream();
            }
            catch { Console.WriteLine("Ошибка подключения."); }
        }
    }
}
