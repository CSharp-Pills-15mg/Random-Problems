using System;
using System.Collections.Generic;

namespace DustInTheWind.Step03_ThreadStaticSolution
{
    internal class RandomNumbersListView
    {
        public void Display(List<RandomNumbersList> randomNumberLists)
        {
            foreach (RandomNumbersList randomNumbersList in randomNumberLists)
            {
                string numbersAsString = string.Join(", ", randomNumbersList);
                Console.WriteLine(numbersAsString);
            }
        }
    }
}