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

namespace DustInTheWind.RandomProblems.Presentation
{
    internal class UseCaseView
    {
        public void DisplayBeginGeneratingMessage(int count)
        {
            Console.WriteLine("-> Step 1");
            Console.WriteLine($"Generating {count:N0} lists of random numbers...");
        }

        public void DisplayEndGeneratingMessage(int count)
        {
            Console.WriteLine($"Successfully generated {count:N0} lists of random numbers. Currently they are empty.");
        }

        public void DisplayBeginGeneratingValues(int countPerList)
        {
            Console.WriteLine("-> Step 2");
            Console.WriteLine($"Generating {countPerList:N0} numbers in each list...");
        }

        public void DisplayEndGeneratingValues(int countPerList)
        {
            Console.WriteLine($"Successfully generated {countPerList:N0} numbers in each list.");
        }
    }
}