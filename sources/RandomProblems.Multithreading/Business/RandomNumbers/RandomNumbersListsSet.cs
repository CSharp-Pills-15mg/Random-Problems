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

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustInTheWind.RandomProblems.Business.RandomNumbers
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

        public async Task GenerateValuesInEachList(int count)
        {
            List<Task> tasks = randomNumberListsSet
                .Select(x => Task.Run(() =>
                {
                    for (int i = 0; i < count; i++)
                        x.GenerateNext();
                }))
                .ToList();

            await Task.WhenAll(tasks);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            foreach (RandomNumbersList randomNumbersList in randomNumberListsSet)
            {
                string numbersAsString = string.Join(", ", randomNumbersList);
                stringBuilder.AppendLine(numbersAsString);
            }

            return stringBuilder.ToString();
        }
    }
}