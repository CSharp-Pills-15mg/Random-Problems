using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.TheBadTwin
{
    internal class RandomNumbersListView
    {
        public void Display(List<List<RandomNumbersList>> randomNumberLists)
        {
            Console.WriteLine("The followings are groups of Random instances that generate the same list of values.");
            Console.WriteLine($"Groups count = {randomNumberLists.Count}");

            for (int index = 0; index < randomNumberLists.Count; index++)
            {
                Console.WriteLine();

                string groupName = $"Group {index}";
                List<RandomNumbersList> group = randomNumberLists[index];

                DisplayGroup(groupName, group);
            }
        }

        private static void DisplayGroup(string groupName, IReadOnlyCollection<RandomNumbersList> group)
        {
            DataGrid dataGrid = new DataGrid(groupName);

            for (int i = 0; i < group.Count; i++)
                dataGrid.Columns.Add($"Random {i}");

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
                    DataCell dataCell = new DataCell("...");
                    dataCell.HorizontalAlignment = HorizontalAlignment.Center;
                    return dataCell;
                });

            return new DataRow(cells);
        }

        private static DataRow CreateDataRow(IReadOnlyCollection<RandomNumbersList> @group, int rowIndex)
        {
            DataRow dataRow = new DataRow();
            bool cellAdded = false;

            foreach (RandomNumbersList randomNumbersList in @group)
            {
                DataCell cellContent;

                if (rowIndex < randomNumbersList.Count)
                {
                    cellContent = new DataCell(randomNumbersList[rowIndex].ToString("N0"));
                    cellContent.HorizontalAlignment = HorizontalAlignment.Right;
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