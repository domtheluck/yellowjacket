// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;
using YellowJacket.Core.Builders;
using YellowJacket.Core.Engine;
using YellowJacket.Core.Engine.Events;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Helpers;
using YellowJacket.Core.Interfaces;
using YellowJacket.Core.Packaging;

namespace YellowJacket.Console
{
    internal class Program
    {
        #region Constants

        private const string ApplicationName = "YellowJacket.Console";

        private const string HelpOptionText = "-?|-h|--help";

        // execute command
        private const string Execute = "execute";

        // create-package command
        private const string CreatePackage = "create-package";
        
        private const string DeploymentFolderLocationArgumentText = "[deploymentFolderLocation]";
        private const string TestAssemblyNameArgumentText = "[testAssemblyName]";
        private const string PackageLocationArgumentText = "[packageLocation]";

        private const string PluginsOptionText = "-p|--plugins";
        private const string OverwriteOptionText = "-o|--overwrite";

        #endregion

        #region Private Methods

        private static void Main(string[] args)
        {
            CommandLineApplication app = new CommandLineApplication { Name = ApplicationName };

            app.HelpOption(HelpOptionText);

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return -1;
            });

            InitializeExecuteCommand(app);

            InitializeCreatePackageCommand(app);

            try
            {
                app.Execute(args);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                Environment.Exit(-1);
            }
        }

        private static void InitializeCreatePackageCommand(CommandLineApplication app)
        {
            app.Command(CreatePackage, (command) =>
            {
                command.Description = "Create a test package from a deployment folder.";
                command.HelpOption(HelpOptionText);

                CommandArgument deploymentFolderLocationArgument = command.Argument(
                    DeploymentFolderLocationArgumentText, 
                    "The location of the deployment folder.");

                CommandArgument testAssemblyNameArgument = command.Argument(
                    TestAssemblyNameArgumentText, 
                    "The test assembly name.");

                CommandArgument packageLocationArgument = command.Argument(
                    PackageLocationArgumentText, 
                    "The location where the package will be created.");

                CommandOption pluginOptions =
                    command.Option(
                        PluginsOptionText,
                        "The plugin assembly names: pluginA.dll pluginB.dll ...",
                        CommandOptionType.MultipleValue);

                CommandOption overwriteOption =
                    command.Option(
                        OverwriteOptionText,
                        "<true> if we want to overwrite the existing package configuration. If set to <false>, the package version will be incremented.",
                        CommandOptionType.SingleValue);

                command.OnExecute(() =>
                {
                    if (string.IsNullOrEmpty(deploymentFolderLocationArgument.Value))
                    {
                        System.Console.WriteLine($"The argument {DeploymentFolderLocationArgumentText} is required.");
                        command.ShowHelp();

                        return -1;
                    }

                    if (string.IsNullOrEmpty(testAssemblyNameArgument.Value))
                    {
                        System.Console.WriteLine($"The argument {TestAssemblyNameArgumentText} is required.");
                        command.ShowHelp();

                        return -1;
                    }

                    if (string.IsNullOrEmpty(packageLocationArgument.Value))
                    {
                        System.Console.WriteLine($"The argument {PackageLocationArgumentText} is required.");
                        command.ShowHelp();

                        return -1;
                    }

                    List<string> plugins = new List<string>();

                    if (pluginOptions.HasValue())
                        plugins = pluginOptions.Values;

                    if (overwriteOption.HasValue())
                    {
                        try
                        {
                            bool result = bool.Parse(overwriteOption.Value());
                        }
                        catch
                        {
                            System.Console.WriteLine($"The specified overwrite value {overwriteOption.Value()} is not valid.");
                            command.ShowHelp();

                            return -1;
                        }
                    }

                    string deploymentFolderLocation = deploymentFolderLocationArgument.Value;
                    string testAssemblyName = testAssemblyNameArgument.Value;
                    string packageLocation = packageLocationArgument.Value;

                    IPackageManager packageManager = new PackageManager();

                    packageManager.Create(deploymentFolderLocation, testAssemblyName, packageLocation, plugins);

                    return 0;
                });
            });
        }

        private static void InitializeExecuteCommand(CommandLineApplication app)
        {
            app.Command(Execute, (command) =>
            {
                command.Description = "Execute one or multiple features.";
                command.HelpOption(HelpOptionText);

                CommandArgument testPackageLocationArgument =
                    command.Argument("[testPackageLocation]", "The test package location");

                CommandArgument testAssemblyNameArgument =
                    command.Argument("[testAssemblyName]", "The test assembly name");

                CommandArgument featuresArgument = command.Argument("[feature1] [feature2] ...", "The feature(s) to execute.");

                featuresArgument.MultipleValues = true;

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
                    if (string.IsNullOrEmpty(testPackageLocationArgument.Value))
                    {
                        System.Console.WriteLine($"The argument [testPackageLocation] is required.");
                        command.ShowHelp();
                        return -1;
                    }

                    if (string.IsNullOrEmpty(testAssemblyNameArgument.Value))
                    {
                        System.Console.WriteLine($"The argument [testAssemblyName] is required.");
                        command.ShowHelp();
                        return -1;
                    }

                    if (!(featuresArgument.Values.Any()))
                    {
                        System.Console.WriteLine("The argument [features] is required.");
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

                    string testPackageLocation = testPackageLocationArgument.Value;
                    string testAssemblyName = testAssemblyNameArgument.Value;
                    List<string> features = featuresArgument.Values;
                    string browser = browserOption.HasValue() ? browserOption.Value() : "None";

                    IEngine executionEngine = ExecutionEngineBuilder.CreateEngine();

                    executionEngine.ExecutionStart += Engine_OnExecutionStart;
                    executionEngine.ExecutionCompleted += Engine_OnExecutionCompleted;
                    executionEngine.ExecutionStop += Engine_OnExecutionStop;
                    executionEngine.ExecutionProgress += Engine_OnExecutionProgress;

                    ExecutionConfiguration executionConfiguration =
                        new ExecutionConfiguration
                        {
                            TestPackageLocation = testPackageLocation,
                            TestAssemblyName = testAssemblyName
                        };

                    if (browser != "None")
                        executionConfiguration.BrowserConfiguration = new BrowserConfiguration
                        {
                            Browser = new EnumHelper().GetEnumTypeFromDescription<BrowserType>(browser)
                        };
                        
                    // TODO: Handles the plugins
                    //executionConfiguration.PluginAssemblies

                    executionEngine.Execute(executionConfiguration);

                    System.Console.ReadLine();

                    return 0;
                });
            });
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
            System.Console.WriteLine($"Execution progress {eventArgs.Progress / 100:P}: {eventArgs.CurrentState}");
        }

        #endregion
    }
}
