using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace Benchmarks
{
	[ShortRunJob, MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter, RPlotExporter, RankColumn, MemoryDiagnoser]
	[HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions, HardwareCounter.CacheMisses)]
	public class ArrayClearBenchmark
	{
		[Params(20, 200, 2000)] public int Size;

		private int[,] _matrix;

		[GlobalSetup]
		public void Setup()
		{
			_matrix = new int[Size, Size];
		}

		[Benchmark]
		public void Clear()
		{
			Array.Clear(_matrix, 0, _matrix.Length);
		}

		[Benchmark]
		public void SetToIntMax()
		{
			for (int x = 0; x < Size; x++)
			{
				for (int y = 0; y < Size; y++)
				{
					_matrix[x, y] = int.MaxValue;
				}
			}
		}
	}
}
