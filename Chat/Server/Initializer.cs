using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace Server
{
    class Initializer
    {
        private static void InitializeServer()
        {
            try
            {
                Storage.EndPoint =
                       new IPEndPoint(Storage.TcpAddress, Storage.TcpPort);
                Storage.ServerSocket = new TcpListener(Storage.EndPoint);
                Storage.ServerSocket.Start();
            }
            catch { Console.WriteLine("Ошибка подключения."); }
        }

        internal static void Start()
        {
            Storage.ServerStarted = true;
            Process.GetCurrentProcess().PriorityBoostEnabled = true;
            Process.GetCurrentProcess().PriorityClass 
                            = ProcessPriorityClass.High;
            InitializeServer();
            Storage.AcceptorStarted=false;
            Storage.MessagesStack =
                    new ConcurrentDictionary<int, Storage.ClientMessage>();
            StartAcceptor();
            StartCleaner();
        }

        private static void StartCleaner()
        {
            Thread t = new Thread(Cleaner.Refresh);
            t.Priority = ThreadPriority.Lowest;
            t.Start();
        }

        private static void StartAcceptor()
        {
            Thread t= new Thread(Acceptor.WaitAndAccept);
            t.Priority=ThreadPriority.Highest;
            t.Start();
            Storage.AcceptorStarted=true;
        }
    }
}
