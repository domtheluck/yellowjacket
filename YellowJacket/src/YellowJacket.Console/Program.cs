using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;
using YellowJacket.Core.Engine.Events;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Helpers;
using YellowJacket.Core.Infrastructure;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Console
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            CommandLineApplication app = new CommandLineApplication { Name = "YellowJacket.Console" };

            app.HelpOption("-?|-h|--help");

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return -1;
            });

            app.Command("execute", (command) =>
            {
                command.Description = "Execute a feature.";
                command.HelpOption("-?|-h|--help");

                CommandArgument assemblyPathArgument = command.Argument("[assemblyPath]", "The assembly path.");
                CommandArgument nameArgument = command.Argument("[name]", "The feature name.");

                StringBuilder browserOptionDescription = new StringBuilder();

                browserOptionDescription.Append(
                    "The browser used to execute the test. ");

                browserOptionDescription.Append(
                    "Please note that you must specify a browser if you want to execute a Web UI feature. ");

                EnumHelper enumHelper = new EnumHelper();

                List<string> browsers = (
                    Enum.GetValues(typeof(BrowserType))
                        .Cast<BrowserType>()
                        .Select(browserType => enumHelper.GetEnumFieldDescription(browserType))).ToList();

                browserOptionDescription.Append(
                    $"Possible values are: {string.Join(", ", browsers)}.");

                CommandOption browserOption =
                command.Option(
                    "-b|--browser <browser>",
                    browserOptionDescription.ToString(), 
                    CommandOptionType.SingleValue);

                command.OnExecute(() =>
                {
                    if (string.IsNullOrEmpty(assemblyPathArgument.Value))
                    {
                        System.Console.WriteLine($"The argument [assemblyPath] is required.");
                        command.ShowHelp();
                        return -1;
                    }

                    if (string.IsNullOrEmpty(nameArgument.Value))
                    {
                        System.Console.WriteLine($"The argument [name] is required.");
                        command.ShowHelp();
                        return -1;
                    }

                    if (!string.IsNullOrEmpty(browserOption.Value()))
                    {
                        if (browsers.All(
                                x => !string.Equals(x, browserOption.Value(), StringComparison.CurrentCultureIgnoreCase)))
                        {
                            System.Console.WriteLine($"The specified browser value {browserOption.Value()} is not valid.");
                            command.ShowHelp();
                            return -1;
                        }
                    }

                    string assemblyPath = assemblyPathArgument.Value;
                    string featureName = nameArgument.Value;
                    string browser = browserOption.HasValue() ? browserOption.Value() : "None";

                    IEngine executionEngine = ExecutionEngineManager.CreateEngine();

                    executionEngine.ExecutionStart += Engine_OnExecutionStart;
                    executionEngine.ExecutionCompleted += Engine_OnExecutionCompleted;
                    executionEngine.ExecutionStop += Engine_OnExecutionStop;
                    executionEngine.ExecutionProgress += Engine_OnExecutionProgress;

                    executionEngine.Execute(assemblyPath, featureName, browser, false);

                    System.Console.ReadLine();

                    return 0;
                });
            });

            try
            {
                app.Execute(args);
            }
            catch  (Exception ex)
            {
                System.Console.WriteLine(ex);
                Environment.Exit(-1);
            }
        }

        #endregion

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
