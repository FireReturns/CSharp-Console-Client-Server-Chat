using System;
using System.IO;
using System.Threading;

namespace Chat
{
    class Reader
    {
        internal static void Read()
        {
            Storage.Reader = new StreamReader(Storage.ServerStream);

            while (true)
            {
                Thread.Sleep(Storage.CommonAwaitPeriod);
                try
                {
                    while (!Storage.Reader.EndOfStream)
                    {
                        var temp = Storage.Reader.ReadLine();
                        var message = Converter.Unpack(temp);
                        Console.WriteLine("Получено от {0}: {1}",
                            message.Author, message.Text);
                    }
                }
                catch { }
            }
        }
    }
}
