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
using System.Collections.Generic;
using DustInTheWind.RandomProblems.TheBadTwinProblem.Business.RandomNumbers;
using DustInTheWind.RandomProblems.TheBadTwinProblem.Presentation;

namespace DustInTheWind.RandomProblems.TheBadTwinProblem.Business
{
    internal class UseCase
    {
        private readonly UseCaseView view;

        public UseCase(UseCaseView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Execute()
        {
            RandomNumbersListsSet randomNumbersListsSet = GenerateLists(100_000);
            HashSet<HashSet<RandomNumbersList>> identicalLists = FindIdenticalLists(randomNumbersListsSet, 10);
            view.DisplayGroupsOfLists(identicalLists);
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

        private HashSet<HashSet<RandomNumbersList>> FindIdenticalLists(RandomNumbersListsSet randomNumbersListsSet, int maxIterationCount)
        {
            view.DisplayBeginSearchingForIdenticalLists(maxIterationCount);

            try
            {
                return randomNumbersListsSet.FindIdenticalLists(maxIterationCount);
            }
            finally
            {
                view.DisplayEndSearchingForIdenticalLists();
            }
        }
    }
}