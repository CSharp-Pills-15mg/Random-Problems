using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.RandomProblems.TheBadTwin.Business.RandomNumbers
{
    internal class RandomNumbersListsSet
    {
        private readonly HashSet<RandomNumbersList> randomNumberListsSet;

        public RandomNumbersListsSet(int count)
        {
            randomNumberListsSet = Enumerable.Range(0, count)
                .Select(x => new RandomNumbersList())
                .ToHashSet();
        }

        public HashSet<HashSet<RandomNumbersList>> FindIdenticalLists(int maxIterationCount)
        {
            return FindIdenticalLists(randomNumberListsSet, maxIterationCount, 0);
        }

        private static HashSet<HashSet<RandomNumbersList>> FindIdenticalLists(HashSet<RandomNumbersList> randomNumberLists, int maxIterationCount, int stepIndex)
        {
            foreach (RandomNumbersList randomNumbersList in randomNumberLists)
                randomNumbersList.GenerateNext();

            HashSet<HashSet<RandomNumbersList>> groups = randomNumberLists
                .GroupBy(x => x.LastNumber)
                .Where(x => x.Count() > 1)
                .Select(x => x.ToHashSet())
                .ToHashSet();

            if (stepIndex < maxIterationCount)
            {
                groups = groups
                    .SelectMany(x => FindIdenticalLists(x, maxIterationCount, stepIndex + 1))
                    .ToHashSet();
            }

            return groups;
        }
    }
}