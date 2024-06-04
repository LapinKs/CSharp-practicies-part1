using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using static StructBenchmarking.Benchmark;

namespace StructBenchmarking;
public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        task.Run();
        GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
        GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();                  // и как-то повлияет на них.
        for (int i = 0; i < repetitionCount; i++)
            task.Run();
        stopwatch.Stop();

        return stopwatch.Elapsed.TotalMilliseconds / repetitionCount;
    }
    public class StringCreator : ITask
    {
        public void Run()
        {
            string str = new string('a', 10000);
        }
    }
    public class StringBuilderCreator : ITask
    {
        public void Run()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < 10000; i++)
                str.Append('a');
            str.ToString();
        }
    }

}

[TestFixture]
public class RealBenchmarkUsageSample
{
    [Test]
    public void StringConstructorFasterThanStringBuilder()
    {
        int repetitionCount = 10000;
        Benchmark benchmark = new Benchmark();
        StringCreator str = new StringCreator();
        StringBuilderCreator builder = new StringBuilderCreator();
        double strDur = benchmark.MeasureDurationInMs(str, repetitionCount);
        double sbDur = benchmark.MeasureDurationInMs(builder, repetitionCount);
        Assert.Less(strDur, sbDur);
    }
}