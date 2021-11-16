using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.TheBadTwin
{
    internal class Problem
    {
        public void Execute()
        {
            List<RandomNumbersList> randoms = GenerateInstances(100_000);
            List<List<RandomNumbersList>> identicalRandoms = FindIdenticalLists(randoms, 10);

            RandomNumbersListView view = new RandomNumbersListView();
            view.Display(identicalRandoms);
        }

        private static List<RandomNumbersList> GenerateInstances(int count)
        {
            return Enumerable.Range(0, count)
                .Select(x => new RandomNumbersList())
                .ToList();
        }

        private static List<List<RandomNumbersList>> FindIdenticalLists(List<RandomNumbersList> randomNumberLists, int stepCount)
        {
            randomNumberLists.ForEach(x => x.GenerateNext());

            List<List<RandomNumbersList>> groups = randomNumberLists
                .GroupBy(x => x.LastNumber)
                .Where(x => x.Count() > 1)
                .Select(x => x.ToList())
                .ToList();

            if (stepCount > 0)
            {
                groups = groups
                    .SelectMany(x => FindIdenticalLists(x, stepCount - 1))
                    .ToList();
            }

            return groups;
        }
    }
}