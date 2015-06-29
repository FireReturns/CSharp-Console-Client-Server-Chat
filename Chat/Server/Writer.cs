using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Server
{
    class Writer
    {
        internal static void Write(TcpClient client)
        {
            var networkStream = client.GetStream();
            var writer = new StreamWriter(networkStream);
            writer.AutoFlush = true;
            var hash = client.GetHashCode();
            var messagesTicksLog = new List<long>();

            while (Storage.ServerStarted && client.Connected)
            {
                Thread.Sleep(Storage.CommonAwaitPeriod);
                try
                {
                    if (Storage.MessagesStack.Count > 0)
                    {
                        ProcessSend(writer,hash,messagesTicksLog);                        
                    }
                }
                catch { }
            }          
        }

        private static void ProcessSend
                (StreamWriter writer,int hash,List<long> log)
        {            
                        foreach (var x in Storage.MessagesStack)
                        {
                            if (x.Value.ClientHash != hash)
                            {
                                var unpacked=
                                    Converter.Unpack(x.Value.MessagePackage);
                                bool flag=log.Contains(unpacked.CreationTicks);
                                if(flag)
                                {}
                                else{
                                /*Storage.ClientMessage temp;
                                Storage.MessagesStack.
                                    TryRemove(x.Key,out temp);*/
                                    log.Add(unpacked.CreationTicks);
                                writer.WriteLine(x.Value.MessagePackage);
                                }
                            }
                        }                     
        }
    }
}
