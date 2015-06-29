using System.Collections.Concurrent;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Chat
{
    class Storage
    {       
        internal static ConcurrentDictionary<long, Message> ToSend;
        internal static TcpClient Client;
        internal static NetworkStream ServerStream;
        internal static StreamWriter Writer;
        internal static StreamReader Reader;
        internal const int CommonAwaitPeriod = 500;
        internal static Thread ReaderThread;
        internal static Thread WriterThread;
    }
}
