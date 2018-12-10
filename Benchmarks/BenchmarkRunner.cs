using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;

namespace Benchmarks
{
	public class BenchmarkRunner
	{
		public static void Main(string[] args)
		{
			var config = ManualConfig.Create(DefaultConfig.Instance)
				.With(MemoryDiagnoser.Default);

			var summary = BenchmarkDotNet.Running.BenchmarkRunner.Run<ArrayClearBenchmark>(config);
			//var summary = BenchmarkDotNet.Running.BenchmarkRunner.Run<ArrayBenchmark>(config);
		}
	}
}