using YellowJacket.Core.Engine.Events;
using YellowJacket.Core.Infrastructure;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            IExecutionEngine executionEngine = ExecutionEngineManager.CreateEngine();

            executionEngine.ExecutionStart += Engine_OnExecutionStart;
            executionEngine.ExecutionCompleted += Engine_OnExecutionCompleted;
            executionEngine.ExecutionStop += Engine_OnExecutionStop;
            executionEngine.ExecutionProgress += Engine_OnExecutionProgress;

            executionEngine.Execute(@"C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll", "MyFeature");
            //executionEngine.Execute(@"D:\Projects\DEV\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\debug\YellowJacket.WebApp.Automation.dll", "MyFeature");

            System.Console.ReadLine();
        }

        #region Event Handlers

        /// <summary>
        /// Handles the OnExecutionStart event of the Engine.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="ExecutionStartEventArgs"/> instance containing the event data.</param>
        private static void Engine_OnExecutionStart(object sender, ExecutionStartEventArgs eventArgs)
        {
           System.Console.WriteLine("Execution Start...");
        }

        private static void Engine_OnExecutionCompleted(object sender, ExecutionCompletedEventArgs eventArgs)
        {
            System.Console.WriteLine("Execution Completed...");
        }

        private static void Engine_OnExecutionStop(object sender, ExecutionStopEventArgs eventArgs)
        {
            System.Console.WriteLine($"Execution Stop... {eventArgs.Exception}");
        }

        private static void Engine_OnExecutionProgress(object sender, ExecutionProgressEventArgs eventArgs)
        {
            System.Console.WriteLine($"Execution progress {eventArgs.Progress}");
        }

        #endregion
    }
}
