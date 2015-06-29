namespace Server
{
    class Program
    {
        static void Main()
        {
            Initializer.Start();
            System.Console.ReadKey();
            Storage.ServerStarted = false;
        }
    }
}
