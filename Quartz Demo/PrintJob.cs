using Quartz;

namespace Quartz_Demo
{
    public class PrintJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello, Quartz.NET!");
            return Task.CompletedTask;
        }
    }
}
