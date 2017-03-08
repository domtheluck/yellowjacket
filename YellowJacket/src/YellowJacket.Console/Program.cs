using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;
using YellowJacket.Core.Engine.Events;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Helpers;

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
                    string name = nameArgument.Value;

                    return 0;
                });
            });

            try
            {
                app.Execute(args);
            }
            catch
            {
                System.Console.WriteLine("Invalid command.");
                Environment.Exit(-1);
            }

            //CommandLineApplication executeCommand = app.Command("execute", config =>
            //{
            //    config.Description = "execute a feature";
            //    config.HelpOption("-? | -h | --help");
            //    CommandArgument name = config.Argument("name", "feature name");
            //    CommandArgument assemblyPath = config.Argument("assemblyPath", "the path of the assembly who contains the feature");
            //    config.OnExecute(() =>
            //    {
            //        //actually do something
            //        System.Console.WriteLine($"threw snowball: {name.Value} with {assemblyPath.Value}");
            //        return 0;
            //    });
            //});

            //executeCommand.Command("help", config =>
            //{
            //    config.Description = "execute a feature";
            //    config.OnExecute(() =>
            //    {
            //        executeCommand.ShowHelp("execute");
            //        return 1;
            //    });
            //});

            //CommandLineApplication catapult = app.Command("catapult", config => {
            //    config.OnExecute(() => {
            //        config.ShowHelp(); //show help for catapult
            //        return 1; //return error since we didn't do anything
            //    });
            //    config.HelpOption("-? | -h | --help"); //show help on --help
            //});
            //catapult.Command("help", config => {
            //    config.Description = "get help!";
            //    config.OnExecute(() => {
            //        catapult.ShowHelp("catapult");
            //        return 1;
            //    });
            //});
            //catapult.Command("list", config => {
            //    config.Description = "list catapults";
            //    config.HelpOption("-? | -h | --help");
            //    config.OnExecute(() => {

            //        System.Console.WriteLine("a");
            //        System.Console.WriteLine("b");
            //        return 0;
            //    });
            //});
            //catapult.Command("add", config => {
            //    config.Description = "Add a catapult";
            //    config.HelpOption("-? | -h | --help");
            //    CommandArgument arg = config.Argument("name", "name of the catapult", false);
            //    config.OnExecute(() => {
            //        if (!string.IsNullOrWhiteSpace(arg.Value))
            //        {
            //            //add snowballs somehow
            //            System.Console.WriteLine($"added {arg.Value}");
            //            return 0;
            //        }
            //        return 1;


            //    });
            //});
            //catapult.Command("fling", config => {
            //    config.Description = "fling snow";
            //    config.HelpOption("-? | -h | --help");
            //    CommandArgument ball = config.Argument("snowballId", "snowball id", false);
            //    CommandArgument cata = config.Argument("catapultId", "id of catapult to use", false);
            //    config.OnExecute(() => {

            //        //actually do something
            //        System.Console.WriteLine($"threw snowball: {ball.Value} with {cata.Value}");
            //        return 0;
            //    });
            //});
            //CommandLineApplication snowball = app.Command("snowball", config => {
            //    config.OnExecute(() => {
            //        config.ShowHelp(); //show help for catapult
            //        return 1; //return error since we didn't do anything
            //    });
            //    config.HelpOption("-? | -h | --help"); //show help on --help
            //});
            //snowball.Command("help", config => {
            //    config.Description = "get help!";
            //    config.OnExecute(() => {
            //        catapult.ShowHelp("snowball");
            //        return 1;
            //    });
            //});
            //snowball.Command("list", config => {
            //    config.HelpOption("-? | -h | --help");
            //    config.Description = "list snowballs";
            //    config.OnExecute(() => {

            //        System.Console.WriteLine("1");
            //        System.Console.WriteLine("2");
            //        return 0;
            //    });
            //});
            //snowball.Command("add", config => {
            //    config.Description = "Add a snowball";
            //    config.HelpOption("-? | -h | --help");
            //    var arg = config.Argument("name", "name of the snowball", false);
            //    config.OnExecute(() => {
            //        if (!string.IsNullOrWhiteSpace(arg.Value))
            //        {
            //            //add snowballs somehow
            //            System.Console.WriteLine($"added {arg.Value}");
            //            return 0;
            //        }
            //        return 0;


            //    });
            //});

            //app.HelpOption("-? | -h | --help");

            //try
            //{
            //    int result = app.Execute(args);
            //    Environment.Exit(result);
            //}
            //catch
            //{
            //    System.Console.WriteLine("invalid option");
            //    Environment.Exit(-1);
            //}

            //IExecutionEngine executionEngine = ExecutionEngineManager.CreateEngine();

            //executionEngine.ExecutionStart += Engine_OnExecutionStart;
            //executionEngine.ExecutionCompleted += Engine_OnExecutionCompleted;
            //executionEngine.ExecutionStop += Engine_OnExecutionStop;
            //executionEngine.ExecutionProgress += Engine_OnExecutionProgress;

            ////executionEngine.Execute(@"C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll", "MyFeature");
            //executionEngine.Execute(@"D:\Projects\DEV\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\debug\YellowJacket.WebApp.Automation.dll", "MyFeature");

            //System.Console.ReadLine();
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
