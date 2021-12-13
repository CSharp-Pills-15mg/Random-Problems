// C# Pills 15mg
// Copyright (C) 2021 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.RandomProblems.TheBadTwinProblem.Business.RandomNumbers
{
    internal class RandomNumbersList : IEnumerable<int>
    {
        // A possible solution to our bad twin problem could be to make the `Random` instance static, isn't it?
        // If there is a single instance, there is no twin instance with the same seed, because
        // there is no other instance.
        //
        // If you run the program it may seem to work perfectly, but this solution generates another problem.
        // The `Random` class is not thread safe. If the RandomNumbersList is used from multiple threads, it is not good.
        // We may fix it by adding a `lock` on the Random instance whenever we use it, isn't it.
        //
        // See the "the-bad-twin-solution-01b-lock-on-random" branch for the `lock` solution.
        // See the "RandomProblems.MultiThreadingProblem" project for more details regarding the multi-threading problem and other solutions.
        private static readonly Random Random = new();

        private readonly List<int> numbers = new();

        public int Count => numbers.Count;

        public int LastNumber
        {
            get
            {
                if (numbers.Count == 0)
                    throw new Exception("No number was generated yet.");

                return numbers[^1];
            }
        }

        public int this[int index] => numbers[index];

        public int GenerateNext()
        {
            int number = Random.Next();
            numbers.Add(number);
            return number;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return numbers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}