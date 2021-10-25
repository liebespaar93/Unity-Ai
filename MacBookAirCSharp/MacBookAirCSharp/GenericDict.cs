using System;
using System.Collections.Generic;
namespace MacBookAirCSharp
{
    public class GenericDict
    {
        public GenericDict()
        {
            var numbers = new Dictionary<String, int>();
            numbers.Add("one", 1);
            numbers.Add("two", 2);
            numbers.Add("three", 3);
            while (true)
            {
                var s = Console.ReadLine();
                if (s == "quit") break;
                if (!numbers.ContainsKey(s))
                {
                    Console.WriteLine(numbers[s]);
                    continue;
                }
                Console.WriteLine("{0} {1}", s, numbers[s]);
            }
        }
    }
}
