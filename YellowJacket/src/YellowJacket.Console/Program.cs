using System;
using System.Runtime.Remoting.Channels;
using YellowJacket.Core.Engine;

namespace YellowJacket.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assembly assembly = Assembly.LoadFile();

            //TypeLocator typeLocator = new TypeLocator();

            //typeLocator.GetHookTypes(assembly);

            Engine engine = new Engine();

            engine.ExecutionStart += Engine_OnExecutionStart;
            engine.ExecutionCompleted += Engine_OnExecutionCompleted;
            engine.ExecutionStop += Engine_OnExecutionStop;

            engine.ExecuteFeature(@"C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll", "MyFeature");
            //engine.ExecuteFeature(@"D:\Projects\DEV\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\debug\YellowJacket.WebApp.Automation.dll", "MyFeature");

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
            System.Console.WriteLine("Execution Stop...");
        }

        #endregion
    }
}
