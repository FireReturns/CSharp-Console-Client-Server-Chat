using System;
using System.Threading;

namespace Server
{
    class Cleaner
    {
        internal static void Refresh()
        {
            while (Storage.ServerStarted)
            {
                Thread.Sleep(Storage.CommonAwaitPeriod*10);                
                GC.Collect(GC.MaxGeneration,
                    GCCollectionMode.Optimized,false);
            }
        }
    }
}
