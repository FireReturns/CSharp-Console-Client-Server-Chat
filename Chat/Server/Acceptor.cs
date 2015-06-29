using System;

using System.Threading;

namespace Server
{
    class Acceptor
    {
        internal static void WaitAndAccept()
        {
            while (Storage.ServerStarted)
            {
                Thread.Sleep(Storage.CommonAwaitPeriod);
                try
                {
                    if (Storage.ServerSocket.Pending())
                    {
                        var client = Storage.ServerSocket.AcceptTcpClient();
                        Thread reader = new Thread(() => Reader.Read(client));
                        reader.Priority = ThreadPriority.Highest;
                        Thread writer = new Thread(() => Writer.Write(client));
                        writer.Priority = ThreadPriority.Highest;
                        writer.Start();
                        reader.Start();

                        /*bool added = false;
                        while (!added)
                            added = Storage.ClientSockets.TryAdd
                                (Storage.ClientSockets.Count,
                                new Storage.Client { ProcessId = -1, TcpClient = client, Nick = null });
                        var test = Storage.ClientSockets.Count;*/
                        Console.WriteLine
                            ("Подключен пользователь.");
                    }
                }
                catch { }
            }
        }
    }
}
