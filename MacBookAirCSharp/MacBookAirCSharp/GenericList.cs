using System;
using System.Collections.Generic;
namespace MacBookAirCSharp
{
    public class GenericList
    {
        public GenericList()
        {
            Random rand = new Random();
            var number = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                number.Add(rand.Next(1000));
            }
            var fa = number.FindAll(x => (x % 2) == 0);
            Console.WriteLine("fa type : {0}", fa.GetType());
            foreach (var x in fa)
                Console.WriteLine(x);
        }
    }
}
