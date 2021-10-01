using System;
using System.Collections.Generic;

namespace MacBookAirCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            var number = new List<int>();
            for(int i = 0; i < 100; i++)
            {
                number.Add(rand.Next(1000));
            }
            var fa = number.FindAll(x => (x % 2) == 0);
            var test = 1;
            Console.WriteLine("fa type : {0}", fa.GetType());
            foreach (var x in fa)
                Console.WriteLine(x);
        }
    }
}
