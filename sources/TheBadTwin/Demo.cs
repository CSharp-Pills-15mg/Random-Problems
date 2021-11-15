using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.TheBadTwin
{
    internal class Demo
    {
        public async Task Execute()
        {
            // When generating two Random instances at the same time without seed, they end up
            // using the same timestamp of the system as seed, thus they are using the same seed.
            //
            // This will have as a result the generation of the same sequence of "random" numbers by
            // both instances.
            //
            // So, we should ensure that in our applications, we never generate two Random instances
            // at the same time.
            // 
            // I know two ways to fix the problem:
            //
            // 1)
            // Use only one Random instance. Optionally make it static.
            // If there is only one instance, there cannot be any problems, isn't it?
            // Wrong. What if the only Random instance is accessed from multiple threads?
            // A Random instance is not thread safe.
            //
            // Solution?
            // Use a synchronization mechanism like a "lock" block.
            // 
            // Problem?
            // Now the Random instance become a bottleneck. It is slow to be accessed.
            //
            // pros - each thread is guaranteed to have random values different from other threads.
            // cons - it is slow. each thread must wait after the other threads.
            //
            // 2)
            // Use the static single instance to generate seeds for local Random instances we need.
            // 
            // pros - it is faster than the previous option.
            // cons - there still exists a very low probability that two identical seeds to be generated.
            // The probability is sufficiently low though and it is acceptable.
            // Probability of collisions: 1 in 2.147.483.647

            List<Random> randoms = await GenerateRandomInstances(100_000);
            List<List<Random>> identicalRandoms = FindIdenticalRandoms(randoms);
            DisplayRandoms(identicalRandoms);
        }

        private static async Task<List<Random>> GenerateRandomInstances(int count)
        {
            List<Random> randoms = new List<Random>();

            List<Task> tasks = Enumerable.Range(0, count)
                .Select(x => Task.Run(() =>
                {
                    Random random = new Random();

                    // static random and use lock on it.
                    // pro - each thread is guaranteed to have random values different from other threads.
                    // cons - it is slow. each thread must wait after the other threads.

                    // static random used with lock for generating just the seed that will be used after that for creating a local random instance.
                    // pro - each thread has a sufficient high probability (not 0) to have random values different from other threads.
                    // pro - it is faster than the previous option.

                    lock (randoms)
                        randoms.Add(random);
                }))
                .ToList();

            await Task.WhenAll(tasks);

            return randoms;
        }

        private static List<List<Random>> FindIdenticalRandoms(IEnumerable<Random> randoms, int step = 0)
        {
            List<List<Random>> groups = randoms
                .Select(x => new
                {
                    Random = x,
                    Number = x.Next()
                })
                .GroupBy(x => x.Number)
                .Where(x => x.Count() > 1)
                .Select(x => x.Select(z => z.Random).ToList())
                .ToList();

            if (step < 10)
            {
                groups = groups
                    .SelectMany(x => FindIdenticalRandoms(x, step + 1))
                    .ToList();
            }

            return groups;
        }

        internal class RandomNumbers
        {
            private readonly Random random;

            public RandomNumbers(Random random)
            {
                this.random = random;
            }

            public void GenerateNext(int count)
            {

            }
        }

        private static void DisplayRandoms(List<List<Random>> randoms)
        {
            Console.WriteLine("The followings are groups of Random instances that generate the same list of values.");
            Console.WriteLine($"Groups count = {randoms.Count}");

            for (int index = 0; index < randoms.Count; index++)
            {
                Console.WriteLine();
                List<Random> group = randoms[index];

                DataGrid dataGrid = new DataGrid($"Group {index}");

                for (int i = 0; i < group.Count; i++)
                    dataGrid.Columns.Add($"Random {i}");

                for (int i = 0; i < 10; i++)
                {
                    string[] numbersAsStrings = group
                        .Select(x => x.Next().ToString())
                        .ToArray();

                    dataGrid.Rows.Add(numbersAsStrings);
                }

                dataGrid.Rows.Add(group.Select(x => "..."));

                dataGrid.Display();
            }
        }
    }
}