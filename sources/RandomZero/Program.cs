using System;

namespace RandomZero
{
    internal class Program
    {
        // Generate random numbers until a specific number is generated. For example 0.

        private static void Main(string[] args)
        {
            Random random = new Random();

            const int targetNumber = 0;
            const int batchSize = 100_000_000;
            int batchIndex = 0;
            bool targetNumberWasFound = false;

            while (!targetNumberWasFound)
            {
                batchIndex++;
                long batchStartIndex = (long)batchIndex * batchSize;

                Console.WriteLine($"Batch {batchStartIndex:N0}");

                for (int i = 1; i <= batchSize; i++)
                {
                    int generatedNumber = random.Next();

                    if (generatedNumber == targetNumber)
                    {
                        long index = batchStartIndex + i;
                        targetNumberWasFound = true;

                        Console.WriteLine($"Number {targetNumber} was generated at index {index:N0}.");

                        // 1.385.893.022
                        // 1.207.086.931
                        //   223.735.804
                        // 5.364.100.000
                        // 2.768.044.585
                        // 3.067.987.830
                    }
                }
            }
        }
    }
}