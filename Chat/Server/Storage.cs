using System.Net;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Server
{
    class Storage
    {
        internal static IPAddress TcpAddress
                    = IPAddress.Parse("127.0.0.1");
        internal static IPEndPoint EndPoint;        
        internal static TcpListener ServerSocket;
        internal struct ClientMessage
        {
            internal string MessagePackage;
            internal int ClientHash;
        }
        internal static ConcurrentDictionary<int, ClientMessage> MessagesStack;
        internal static int TcpPort = 56000;
        internal static bool AcceptorStarted;
        internal static bool ServerStarted;
        internal const int CommonAwaitPeriod = 300;
    }
}
