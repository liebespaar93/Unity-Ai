using System;
using System.Threading;
namespace MacBookAirCSharp
{
    public class KeyAvailable
    {
        public KeyAvailable()
        {
            while(true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.RightArrow)
                {
                    Console.WriteLine("<<<");
                }
                Console.WriteLine("====");
                Thread.Sleep(1000);
            }
        }
    }
}
