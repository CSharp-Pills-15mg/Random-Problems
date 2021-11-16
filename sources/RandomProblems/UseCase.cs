﻿// C# Pills 15mg
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

namespace DustInTheWind.RandomProblems
{
    internal class UseCase
    {
        public void Execute()
        {
            const int count = 100_000;

            List<RandomNumbersList> randoms = GenerateInstances(count);
        }

        private static List<RandomNumbersList> GenerateInstances(int count)
        {
            return Enumerable.Range(0, count)
                .Select(x => new RandomNumbersList())
                .ToList();
        }
    }
}