using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using C5;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class TestsNotepad
	{
		[Test]
		public void ArrayClearPerformance()
		{
			int attempts = 10;
			int size = 3000;
			var array = new int[size, size];

			var stopwatch = Stopwatch.StartNew();

			for (int i = 0; i < attempts; i++)
			{
				Array.Clear(array, 0, size * size);
			}

			Console.WriteLine($"Average time to clear array: {stopwatch.ElapsedMilliseconds / attempts} ms");
			// Output for 3000x3000: Average time to clear array: 12 ms
		}

		[Test]
		public void ArraySetPerformance()
		{
			int attempts = 10;
			int size = 2000;
			var array = new int[size, size];

			var stopwatch = Stopwatch.StartNew();

			for (int i = 0; i < attempts; i++)
			{
				for (int x = 0; x < size; x++)
				{
					for (int y = 0; y < size; y++)
					{
						array[x, y] = int.MaxValue;
					}
				}
			}

			Console.WriteLine($"Average time to set array elements to int.MaxValue: {stopwatch.ElapsedMilliseconds / attempts} ms");
			// Output for 3000x3000: Average time to set array elements to int.MaxValue: 43 ms
		}

		[Test]
		public void IntervalHeap_NoComparer_OrderIsAscendingByDefaultComparer()
		{
			var priorityQueue = new IntervalHeap<int> { 3, 4, 5, 1, 2 };

			var orderedResult = new List<int>();
			while (priorityQueue.Any())
			{
				orderedResult.Add(priorityQueue.FindMin());
				priorityQueue.DeleteMin();
			}

			var expectedResult = new List<int> { 1, 2, 3, 4, 5 };
			orderedResult.Should().BeEquivalentTo(expectedResult, config => config.WithStrictOrderingFor(number => number));
		}

		[Test]
		public void ClosureVariableModificationTest()
		{
			int x = 5;
			Action kuku = () =>
			{
				++x;
			};

			kuku();
			kuku();
			x.Should().Be(7);
		}

		[Test]
		public void BoxedVariableDoesNotChangeAfterModification()
		{
			int x = 5;
			object xo = x;
			xo = 5;
			x.Should().Be(5);
		}

		class CapturingVariablesInAClosure
		{
			void VariableIsImplicitilyCapturedInAClosure(Action<Action> callable1, Action<Action> callable2)
			{
				var p1 = 1;
				var p2 = "hello";

				callable1(() => p1++);    // RESHARPER WARNING: Implicitly captured closure: p2 (bo niby tworzona jest jedna klasa na zasieg 
										  // (wg niektórych na funkcje)), która zawiera w sobie przejete zmienne.
				callable2(() => { p2.ToString(); p1++; });
			}

			void VariableIsNotImplicitilyCapturedInAClosure(Action<Action> callable1, Action<Action> callable2)
			{
				var p1 = 1;
				callable1(() => p1++);    // tu juz nie ma ostrzezenia! dlaczego? zasieg jest ten sam.

				var p2 = "hello";
				callable2(() => { p2.ToString(); p1++; });
			}
		}
	}
}