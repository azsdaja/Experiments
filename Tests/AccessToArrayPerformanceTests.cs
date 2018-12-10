using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Tests
{
    public class AccessToArrayPerformanceTests
    {
		[Test]
		public void AccessToArrayTest()
		{
			int size = 3000;
			var matrix = new int[size, size];

			ProcessVerticalStripesHorizontally(matrix);
			ProcessVerticalStripesVertically(matrix);

			for (int run = 0; run < 5; run++)
			{
				Console.WriteLine("run " + run + ": ");

				var stopwatch = Stopwatch.StartNew();
				for (int iteration = 0; iteration < 5; iteration++)
				{
					ProcessVerticalStripesHorizontally(matrix);
				}
				Console.WriteLine("processing stripes horizontally: " + stopwatch.ElapsedMilliseconds + " ms");

				stopwatch.Restart();
				for (int iteration = 0; iteration < 5; iteration++)
				{
					ProcessVerticalStripesVertically(matrix);
				}
				Console.WriteLine("processing stripes vertically: " + stopwatch.ElapsedMilliseconds + " ms");
				Console.WriteLine();

			}
		}

		private void ProcessVerticalStripesHorizontally(int[,] matrix)
		{
			int size = matrix.GetLength(0);
			for (int x = 1; x < size - 1; x++)
			{
				for (int y = 0; y < size; y++)
				{
					var value = matrix[x, y];
					var valueLeft = matrix[x - 1, y];
					var valueRight = matrix[x + 1, y];
				}
			}
		}

		private void ProcessVerticalStripesVertically(int[,] matrix)
		{
			int size = matrix.GetLength(0);
			for (int x = 1; x < size - 1; x++)
			{
				for (int y = 0; y < size; y++)
				{
					var value = matrix[x, y];
				}
				for (int y = 0; y < size; y++)
				{
					var valueLeft = matrix[x - 1, y];
				}
				for (int y = 0; y < size; y++)
				{
					var valueRight = matrix[x + 1, y];
				}
			}
		}

	}
}
