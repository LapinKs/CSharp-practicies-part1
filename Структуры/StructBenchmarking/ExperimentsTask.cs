using System.Collections.Generic;
using System.Threading.Tasks;

namespace StructBenchmarking;
public interface ITaskCreator
{
    ITask Task(int size, string type);
}
public class CreateTask : ITaskCreator
{
    public ITask Task(int size, string type) => type == "struct" ?
new StructArrayCreationTask(size) : new ClassArrayCreationTask(size);
}
public class CallTask : ITaskCreator
{
    public ITask Task(int size, string type) => type == "struct" ?
new MethodCallWithStructArgumentTask(size) : new MethodCallWithClassArgumentTask(size);
}
public class Experiments
{
    public static ChartData CreateChart(ITaskCreator taskCreator, IBenchmark benchmark, int repetitionCount, string title)
    {
        var classesTimes = new List<ExperimentResult>();
        var structuresTimes = new List<ExperimentResult>();
        foreach (var item in Constants.FieldCounts)
        {
            classesTimes.Add(new ExperimentResult(item, benchmark.MeasureDurationInMs(taskCreator.Task(item, "class"), repetitionCount)));
            structuresTimes.Add(new ExperimentResult(item, benchmark.MeasureDurationInMs(taskCreator.Task(item, "struct"), repetitionCount)));
        }
        return new ChartData
        {
            Title = title,
            ClassPoints = classesTimes,
            StructPoints = structuresTimes,
        };
    }
    public static ChartData BuildChartDataForArrayCreation(
        IBenchmark benchmark, int repetitionsCount)
    {
        return CreateChart(new CreateTask(), benchmark, repetitionsCount, "Create array");
    }

    public static ChartData BuildChartDataForMethodCall(
        IBenchmark benchmark, int repetitionsCount)
    {
        return CreateChart(new CallTask(), benchmark, repetitionsCount, "Call method with argument");
    }
}