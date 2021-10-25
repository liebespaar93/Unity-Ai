using System;
using System.Collections.Generic;
namespace MacBookAirCSharp
{
    public class Ncoin
    {
        public Ncoin()
        {
            var s = Console.ReadLine().Split(' ');
            List<int> coins = new List<int>();
            foreach (string i in s)
            {
                List<int> newcoins = new List<int>();
                foreach (int coin in coins)
                {
                    newcoins.Add(Convert.ToInt32(i) * coin);
                }
                coins = newcoins;
            }
            Console.WriteLine(coins);
        }
    }
}
