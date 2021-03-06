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
using System.Threading.Tasks;
using DustInTheWind.RandomProblems.MultiThreadingProblem.Business.RandomNumbers;
using DustInTheWind.RandomProblems.MultiThreadingProblem.Presentation;

namespace DustInTheWind.RandomProblems.MultiThreadingProblem.Business
{
    internal sealed class UseCase
    {
        private readonly UseCaseView view;

        public UseCase(UseCaseView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public async Task Execute()
        {
            RandomNumbersListsSet randomNumbersListsSet = GenerateLists(300_000);
            await GenerateManyValuesInParallel(randomNumbersListsSet, 10);
            ExportAllValues(randomNumbersListsSet);
        }

        private RandomNumbersListsSet GenerateLists(int count)
        {
            view.DisplayBeginGeneratingLists(count);

            try
            {
                return new RandomNumbersListsSet(count);
            }
            finally
            {
                view.DisplayEndGeneratingLists(count);
            }
        }

        private async Task GenerateManyValuesInParallel(RandomNumbersListsSet randomNumbersListsSet, int countPerList)
        {
            view.DisplayBeginGeneratingNumbers(countPerList);

            try
            {
                await randomNumbersListsSet.GenerateValuesInEachListInParallel(countPerList);
            }
            finally
            {
                view.DisplayEndGeneratingNumbers(countPerList);
            }
        }

        private void ExportAllValues(RandomNumbersListsSet randomNumbersListsSet)
        {
            using NumbersFile numbersFile = new();
            view.DisplayBeginExport(numbersFile.FileName);

            try
            {
                numbersFile.Write(randomNumbersListsSet);
            }
            finally
            {
                view.DisplayEndExport(numbersFile.FileName);
            }
        }
    }
}