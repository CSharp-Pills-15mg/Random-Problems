using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DustInTheWind.Step04_ThreadLocalSolution
{
    internal class Problem
    {
        public async Task Execute()
        {
            const int count = 10_000;

            List<RandomNumbersList> randomNumberLists = GenerateInstances(count);
            await GenerateValues(randomNumberLists, 10);

            RandomNumbersListView view = new RandomNumbersListView();
            view.Display(randomNumberLists);
        }

        private static List<RandomNumbersList> GenerateInstances(int count)
        {
            return Enumerable.Range(0, count)
                .Select(x => new RandomNumbersList())
                .ToList();
        }

        private static async Task GenerateValues(IEnumerable<RandomNumbersList> randomNumberLists, int count)
        {
            List<Task> tasks = randomNumberLists
                .Select(x => Task.Run(() =>
                {
                    for (int i = 0; i < count; i++)
                        x.GenerateNext();
                }))
                .ToList();

            await Task.WhenAll(tasks);
        }
    }
}