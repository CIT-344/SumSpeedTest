using System;
using System.Threading.Tasks;
using System.Threading;

namespace SumSpeedTest
{
    class Program
    {
        

        static void Main(string[] args)
        {
            LaunchTest();
        }

        static void LaunchTest()
        {
            Console.Title = "Testing speeds of large sum calculations";



            var blockingResults = GetRunStats();
            var threadedResults = Task.Factory.StartNew(()=>{
                return GetRunStats();
            }).Result;

            Console.WriteLine($"Blocking Results: {blockingResults.ExecutionTime.TotalSeconds} Thread: {blockingResults.RunnerInformation.ManagedThreadId}");
            Console.WriteLine($"Threaded Results: {threadedResults.ExecutionTime.TotalSeconds} Thread: {threadedResults.RunnerInformation.ManagedThreadId}");

            Console.WriteLine($"Current Thread: {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("End of Test");

            Console.WriteLine("Press esc to end program");
            Console.WriteLine("Press any key to re-run the test");

            var _key = Console.ReadKey();
            if (_key.Key != ConsoleKey.Escape)
            {
                Console.Clear();
                LaunchTest();
            }
        }

        static SpeedStatistics GetRunStats()
        {
            var results = new SpeedStatistics();
            // Handle the running, recording, returning of data
            var startTime = DateTime.Now;

            CountSums(int.MaxValue);
            results.ExecutionTime = DateTime.Now - startTime;
            results.RunnerInformation = Thread.CurrentThread;


            return results;
        }



        static float CountSums(int iterations)
        {
            // Run a loop that adds this number up x times
            float runningTotal = 0;
            for (int i = 0; i < iterations; i++)
            {
                runningTotal += iterations;
            }
            return runningTotal;
        }
    }

    class SpeedStatistics
    { 
        public TimeSpan ExecutionTime {get;set; }
        public Thread RunnerInformation { get; set; }
    }
}
