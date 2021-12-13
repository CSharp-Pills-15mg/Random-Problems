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

namespace DustInTheWind.RandomToZero
{
    internal class Generator
    {
        private static readonly Random Random = new();
        private long currentBatchIndex;

        public long BatchSize { get; set; } = 100_000_000;

        public int TargetNumber { get; set; }

        public long TargetNumberIndex { get; private set; } = -1;

        public event EventHandler<BatchStartEventArgs> BatchStart;

        public void Run()
        {
            TargetNumberIndex = -1;
            currentBatchIndex = 0;

            while (true)
            {
                currentBatchIndex++;

                int foundIndex = GenerateNextBatch();

                if (foundIndex > 0)
                {
                    long batchStartIndex = currentBatchIndex * BatchSize;
                    TargetNumberIndex = batchStartIndex + foundIndex;
                    break;
                }
            }
        }

        private int GenerateNextBatch()
        {
            BatchStartEventArgs args = new(currentBatchIndex);
            OnBatchStart(args);

            for (int i = 1; i <= BatchSize; i++)
            {
                int generatedNumber = Random.Next();

                if (generatedNumber == TargetNumber)
                    return i;
            }

            return -1;
        }

        protected virtual void OnBatchStart(BatchStartEventArgs e)
        {
            BatchStart?.Invoke(this, e);
        }
    }
}