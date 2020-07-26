using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    public class FibonacciCalc
    {
        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add MemoryDiagnoser to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;

            return Recursive(n - 1) + Recursive(n - 2);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            return RecursiveWithMemoization(n, new ulong[n + 1]);
        }

        private ulong RecursiveWithMemoization(ulong n, ulong[] memo)
        {
            if (n == 0 || n == 1) return n;

            if (memo[n] == 0)
                memo[n] = RecursiveWithMemoization(n - 1, memo) + RecursiveWithMemoization(n - 2, memo);

            return memo[n];
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            if (n == 1 || n == 2) return 1;

            ulong a = 1, b = 1;
            for (ulong i = 2; i < n; ++i)
            {
                var c = a + b;
                a = b;
                b = c;
            }

            return b;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
