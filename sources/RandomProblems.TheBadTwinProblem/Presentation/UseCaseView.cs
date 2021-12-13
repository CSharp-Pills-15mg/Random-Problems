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
using System.Linq;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.TabularData;
using DustInTheWind.RandomProblems.TheBadTwin.Business.RandomNumbers;

namespace DustInTheWind.RandomProblems.TheBadTwin.Presentation
{
    internal class UseCaseView
    {
        public void DisplayBeginGeneratingLists(int count)
        {
            Console.WriteLine();
            Console.WriteLine("-> Step 1");
            Console.WriteLine($"Generating {count:N0} lists of random numbers...");
        }

        public void DisplayEndGeneratingLists(int count)
        {
            Console.WriteLine($"Successfully generated {count:N0} lists of random numbers. Currently they are empty.");
        }

        public void DisplayBeginSearchingForIdenticalLists(int maxIterationCount)
        {
            Console.WriteLine();
            Console.WriteLine("-> Step 2");
            Console.WriteLine($"Searching for identical lists using the first {maxIterationCount} numbers in each list...");
        }

        public void DisplayEndSearchingForIdenticalLists()
        {
            Console.WriteLine($"Finished searching for identical lists.");
        }

        public void DisplayGroupsOfLists(HashSet<HashSet<RandomNumbersList>> randomNumberLists)
        {
            Console.WriteLine($"Groups of identical lists: {randomNumberLists.Count}");

            int index = 0;

            foreach (HashSet<RandomNumbersList> group in randomNumberLists)
            {
                Console.WriteLine();

                string groupName = $"Group {index}";
                DisplayGroup(groupName, group);

                index++;
            }
        }

        private static void DisplayGroup(string groupName, IReadOnlyCollection<RandomNumbersList> group)
        {
            DataGrid dataGrid = new(groupName);

            for (int i = 0; i < group.Count; i++)
                dataGrid.Columns.Add($"List {i}");

            int rowIndex = 0;

            while (true)
            {
                DataRow dataRow = CreateDataRow(group, rowIndex);

                if (dataRow == null)
                    break;

                dataGrid.Rows.Add(dataRow);
                rowIndex++;
            }

            DataRow ellipsisRow = CreateEllipsisRow(group.Count);
            dataGrid.Rows.Add(ellipsisRow);

            dataGrid.Display();
        }

        private static DataRow CreateEllipsisRow(in int columnCount)
        {
            IEnumerable<DataCell> cells = Enumerable.Range(0, columnCount)
                .Select(x =>
                {
                    DataCell dataCell = new("...")
                    {
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    return dataCell;
                });

            return new DataRow(cells);
        }

        private static DataRow CreateDataRow(IEnumerable<RandomNumbersList> group, int rowIndex)
        {
            DataRow dataRow = new();
            bool cellAdded = false;

            foreach (RandomNumbersList randomNumbersList in group)
            {
                DataCell cellContent;

                if (rowIndex < randomNumbersList.Count)
                {
                    cellContent = new DataCell(randomNumbersList[rowIndex].ToString("N0"))
                    {
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    cellAdded = true;
                }
                else
                {
                    cellContent = new DataCell();
                }

                dataRow.AddCell(cellContent);
            }

            return cellAdded
                ? dataRow
                : null;
        }
    }
}