using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Server
{
    class Reader
    {
        internal static void Read(TcpClient client)
        {
            var networkStream = client.GetStream();
            var reader = new StreamReader(networkStream);
            var hash = client.GetHashCode();

            while (Storage.ServerStarted && client.Connected)
            {
                Thread.Sleep(Storage.CommonAwaitPeriod);

                try
                {
                    while (!reader.EndOfStream)
                    {
                        string dataFromClient = reader.ReadLine();

                        var clientMessage =
                            new Storage.ClientMessage
                                {
                                    ClientHash = hash,
                                    MessagePackage = dataFromClient
                                };
                        Storage.MessagesStack.
                            TryAdd(Storage.MessagesStack.Count, clientMessage);
                    }
                }
                catch { }
            }            
            
        }
    }
}
