using System.IO;
using System.Threading;

namespace Chat
{
    class Writer
    {
        internal static void Send()
        {
            Storage.Writer = new StreamWriter(Storage.ServerStream);
            Storage.Writer.AutoFlush = true;

            while (true)
            {
                Thread.Sleep(Storage.CommonAwaitPeriod);
                try
                {
                    if (Storage.ToSend.Count > 0)
                    {
                        foreach (var x in Storage.ToSend)
                        {
                            Message temp;
                            Storage.ToSend.TryRemove(x.Key, out temp);
                            string package = Converter.Pack(x.Value);
                            Storage.Writer.WriteLine(package);
                        }
                    }
                }
                catch { }
            }
        }
    }
}
