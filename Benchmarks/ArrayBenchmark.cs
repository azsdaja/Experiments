using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace Benchmarks
{
	[ShortRunJob, MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter, RPlotExporter, RankColumn, MemoryDiagnoser]
	[HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions, HardwareCounter.CacheMisses)]
	public class ArrayBenchmark
	{
		[Params(100, 1000)] public int Size;

		private int[,] _matrix;

		[GlobalSetup]
		public void Setup()
		{
			_matrix = new int[Size, Size];
		}

		[Benchmark]
		public void ProcessVerticalStripesHorizontally()
		{
			for (int x = 1; x < Size - 1; x++)
			{
				for (int y = 0; y < Size; y++)
				{
					_matrix[x - 1, y] = 1;
					_matrix[x + 0, y] = 1;
					_matrix[x + 1, y] = 1;
				}
			}
		}

		[Benchmark]
		public void ProcessVerticalStripesVertically()
		{
			for (int x = 1; x < Size - 1; x++)
			{
				for (int y = 0; y < Size; y++)
				{
					_matrix[x + 0, y] = 1;
				}
				for (int y = 0; y < Size; y++)
				{
					_matrix[x - 1, y] = 1;
				}
				for (int y = 0; y < Size; y++)
				{
					_matrix[x + 1, y] = 1;
				}
			}
		}

		[Benchmark]
		public void ProcessVerticalStripesVertically2()
		{
			for (int x = 1; x < Size - 1; x++)
			{
				int xAdjustment = 0;
				int lastY = Size - 1;
				for (int y = 0; y < Size; y++)
				{
					_matrix[x + xAdjustment, y] = 1;
					if (y == lastY)
					{
						if (xAdjustment == 0)
						{
							xAdjustment = -1;
							y = 0;
						}
						else if (xAdjustment == -1)
						{
							xAdjustment = 1;
							y = 0;
						}
					}
				}
			}
		}

		[Benchmark]
		public void ProcessVerticalStripesVertically3()
		{
			for (int x = 1; x < Size - 1; x++)
			{
				int xAdjustment = 0;
				int lastY = Size - 1;
				for (int y = 0; y < Size; y++)
				{
					if (xAdjustment == 0)
						_matrix[x, y] = 1;
					else if (xAdjustment == -1)
						_matrix[x - 1, y] = 1;
					else if (xAdjustment == 1)
						_matrix[x + 1, y] = 1;
					if (y == lastY)
					{
						if (xAdjustment == 0)
						{
							xAdjustment = -1;
							y = 0;
						}
						else if (xAdjustment == -1)
						{
							xAdjustment = 1;
							y = 0;
						}
					}
				}
			}
		}
	}
}