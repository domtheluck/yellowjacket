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

using NUnit.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YellowJacket.Core.Contexts;
using YellowJacket.Core.Engine.Events;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Gherkin;
using YellowJacket.Core.Helpers;
using YellowJacket.Core.Hook;
using YellowJacket.Core.Interfaces;
using YellowJacket.Core.NUnit;
using YellowJacket.Core.NUnit.Models;
using YellowJacket.Core.Plugins;
using YellowJacket.Core.Plugins.Interfaces;

namespace YellowJacket.Core.Engine
{
    /// <inheritdoc />
    /// <summary>
    /// YellowJacket engine.
    /// </summary>
    public sealed class Engine : IEngine
    {
        #region Constants

        private const string BrowserNone = "None"; // TODO: move that somewhere else

        #endregion Constants

        #region Private Members

        private readonly TypeLocatorHelper _typeLocatorHelper = new TypeLocatorHelper();
        private Assembly _testAssembly;
        private Configuration _configuration;
        private readonly List<Assembly> _pluginAssemblies = new List<Assembly>();

        private List<TestCase> _testCases = new List<TestCase>();
        private ITestEngine _testEngine;
        private TestSuite _testSuite;
        private int _testCaseCount;

        private readonly GherkinManager _gherkinManager = new GherkinManager();

        private string _tempFolder;

        #endregion Private Members

        #region Events

        public event ExecutionCompletedHandler ExecutionCompleted;
        public event ExecutionProgressHandler ExecutionProgress;
        public event ExecutionStartHandler ExecutionStart;
        public event ExecutionStopHandler ExecutionStop;

        #endregion Events

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine" /> class.
        /// </summary>
        internal Engine()
        {
            Initialize();
        }

        #endregion Constructors

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Runs the engine with the specified configuration.
        /// </summary>
        /// <param name="configuration">The engine configuration.</param>
        public void Run(Configuration configuration)
        {
            Cleanup();

            _configuration = configuration;

            ValidateConfiguration();

            InitializeFileSystem();

            try
            {
                LoadPluginAssemblies();

                RegisterPlugins();

                LoadTestAssembly();

                ExtractFeatures();

                InitializeWebDriver();

                ExecuteFeatures();
            }
            catch (Exception ex)
            {
                // if an exception is raised, we are raising a specific event to inform the caller.
                FireExecutionStopEvent(ex);
            }
            finally
            {
                FireExecutionCompletedEvent();
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Extracts the features.
        /// </summary>
        private void ExtractFeatures()
        {
            _configuration.Features.ForEach(x =>
            {
                _gherkinManager.ExtractFeature(_testAssembly, x, Path.Combine(_tempFolder, "Features"));
            });
        }

        /// <summary>
        /// Initializes the file system.
        /// </summary>
        private void InitializeFileSystem()
        {
            _tempFolder = Path.Combine(Path.GetTempPath(), "YellowJacket");

            if (Directory.Exists(_tempFolder))
                DeleteDirectory(_tempFolder, true);
        }

        /// <summary>
        /// Deletes the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="recursive">if set to <c>true</c> [recursive].</param>
        public void DeleteDirectory(string path, bool recursive)
        {
            if (recursive)
            {
                string[] subfolders = Directory.GetDirectories(path);

                foreach (string folder in subfolders)
                {
                    DeleteDirectory(folder, true);
                }
            }

            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                try
                {
                    FileAttributes attr = File.GetAttributes(file);

                    if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(file, attr ^ FileAttributes.ReadOnly);
                    }

                    File.Delete(file);
                }
                catch (IOException)
                {
                    //IOErrorOnDelete = true;
                }
            }

            Directory.Delete(path);
        }

        private void InitializeWebDriver()
        {
            if (_configuration.BrowserConfiguration.Browser == BrowserType.None)
                return;

            ExecutionContext.Current.WebDriver = ExecutionContext.Current
                .GetWebDriverConfigurationPlugin()
                .Get(_configuration.BrowserConfiguration.Browser);
        }

        /// <summary>
        /// Updates the execution progress.
        /// </summary>
        private void UpdateProgress()
        {
            // TODO: temp code. Need something more flexible. Also, we need to think about the result output.
            int testCaseCount = _testSuite.TestCaseCount;

            int finishedTestCaseCount = _testCases.Count;

            decimal progress = finishedTestCaseCount / (decimal)testCaseCount * 100;

            FireExecutionProgressEvent(
                Math.Round(progress, 2),
                $"Execution of {_testCases.Last().ClassName.Split('.').Last().Substring(0, _testCases.Last().ClassName.Split('.').Last().Length - 7)} - {_testCases.Last().Name}: {_testCases.Last().Result}");
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(_configuration.TestAssemblyFullName))
                throw new ArgumentException("You must provide a value for the Test Assembly");

            string location = Path.GetDirectoryName(_configuration.TestAssemblyFullName);

            if (string.IsNullOrEmpty(location))
                throw new IOException("The test assembly location is invalid");

            if (!File.Exists(_configuration.TestAssemblyFullName))
                throw new IOException($"Cannot found the The Test Assembly {_configuration.TestAssemblyFullName}");
        }

        /// <summary>
        /// Cleanups the engine setup.
        /// </summary>
        private void Cleanup()
        {
            _configuration = null;
            _testAssembly = null;
            _pluginAssemblies.Clear();
            _testSuite = null;
            _testCases = new List<TestCase>();
        }

        /// <summary>
        /// Executes the specified features.
        /// </summary>
        private void ExecuteFeatures()
        {
            //GherkinManager gherkinManager = new GherkinManager();

            //gherkinManager.ExtractFeature();

            TestPackage testPackage = NUnitEngineHelper.CreateTestPackage(new List<string> { _configuration.TestAssemblyFullName });

            TestFilter testFilter = NUnitEngineHelper.CreateTestFilter(_testAssembly, _configuration.Features);

            ITestRunner testRunner = _testEngine.GetRunner(testPackage);

            _testSuite = NUnitEngineHelper.ParseTestRun(testRunner.Explore(testFilter)).TestSuite;

            CustomTestEventListener testEventListener = new CustomTestEventListener();

            testEventListener.TestReport += OnTestReport;

            TestRun testRun = NUnitEngineHelper.ParseTestRun(testRunner.Run(testEventListener, testFilter));
        }

        /// <summary>
        /// Initializes the engine.
        /// </summary>
        private void Initialize()
        {
            _testCaseCount = 0;

            // initialize the NUnit test engine
            _testEngine = TestEngineActivator.CreateInstance();

            //_testEngine.WorkDirectory = ""; // TODO: check if we need to put the WorkDirectory elsewhere
            _testEngine.InternalTraceLevel = InternalTraceLevel.Verbose; // TODO: should be customizable

        }

        /// <summary>
        /// Loads the test assembly.
        /// </summary>
        private void LoadTestAssembly()
        {
            _testAssembly = Assembly.LoadFrom(_configuration.TestAssemblyFullName);
        }

        #region Plugins

        /// <summary>
        /// Gets the log plugins.
        /// </summary>
        /// <returns></returns>
        private List<ILogPlugin> GetLogPlugins()
        {
            List<ILogPlugin> plugins = GetPlugins<ILogPlugin>();

            if (!plugins.Any())
                plugins.Add(ClassActivatorHelper<FileLogPlugin>.CreateInstance(typeof(FileLogPlugin), @"c:\temp")); // TODO: need to have this in the input args

            return plugins;
        }

        /// <summary>
        /// Gets the web driver configuration plugin.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">You cannot have more than one instance of the IWebDriverConfiguration plugin</exception>
        private IWebDriverConfigurationPlugin GetWebDriverConfigurationPlugin()
        {
            List<IWebDriverConfigurationPlugin> plugins = GetPlugins<IWebDriverConfigurationPlugin>();

            if (plugins.Any() && plugins.Count > 1)
                throw new ArgumentException("You cannot have more than one instance of the IWebDriverConfiguration plugin");

            if (plugins.Any())
                return plugins.First();

            return ClassActivatorHelper<WebDriverConfigurationPlugin>.CreateInstance(
                typeof(WebDriverConfigurationPlugin));
        }

        private List<T> GetPlugins<T>()
        {
            List<T> plugins = new List<T>();

            TypeLocatorHelper typeLocatorHelper = new TypeLocatorHelper();

            foreach (Assembly assembly in _pluginAssemblies)
            {
                List<Type> types = typeLocatorHelper.GetImplementedTypes<T>(assembly);

                if (!types.Any())
                    continue;

                types.ForEach(x =>
                {
                    plugins.Add(ClassActivatorHelper<T>.CreateInstance(x));
                });
            }

            return plugins;
        }

        /// <summary>
        /// Loads the plugin assemblies from the test assembly folder.
        /// </summary>
        private void LoadPluginAssemblies()
        {
            string location = Path.GetDirectoryName(_configuration.TestAssemblyFullName);

            if (string.IsNullOrEmpty(location))
                return;

            _configuration.PluginAssemblies.ForEach(x =>
            {
                _pluginAssemblies.Add(Assembly.LoadFile(Path.Combine(location, x)));
            });     
        }

        /// <summary>
        /// Registers the plugins in the execution context.
        /// </summary>
        private void RegisterPlugins()
        {
            // cleanup the existing plugins
            ExecutionContext.Current.ClearPlugins();

            GetLogPlugins().ForEach(x =>
            {
                ExecutionContext.Current.RegisterLogPlugin(x);
            });

            ExecutionContext.Current.RegisterWebDriverConfigurationPlugin(GetWebDriverConfigurationPlugin());
        }

        #endregion Plugins

        #region Hooks

        /// <summary>
        /// Registers the hooks in the execution context.
        /// </summary>
        private void RegisterHooks()
        {
            // cleanup the existing hooks
            ExecutionContext.Current.ClearHooks();

            // get the hook list from the test assembly
            List<Type> hooks = _typeLocatorHelper.GetImplementedTypes<IHook>(_testAssembly);

            // instantiate each hook class and register it in the execution context
            foreach (Type type in hooks)
            {
                int priority = 0;

                HookPriorityAttribute hookPriorityAttribute =
                    (HookPriorityAttribute)Attribute.GetCustomAttribute(type, typeof(HookPriorityAttribute));

                if (hookPriorityAttribute != null)
                    priority = hookPriorityAttribute.Priority;

                ExecutionContext.Current.RegisterHook(
                    new HookInstance
                    {
                        Instance = ClassActivatorHelper<IHook>.CreateInstance(type),
                        Priority = priority
                    });
            }
        }

        #endregion Hooks

        #region Events

        /// <summary>
        /// Fires the execution completed event.
        /// </summary>
        private void FireExecutionCompletedEvent()
        {
            ExecutionCompleted?.Invoke(this, new ExecutionCompletedEventArgs());
        }

        /// <summary>
        /// Fires the execution progress event.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <param name="currentState">The current state.</param>
        private void FireExecutionProgressEvent(decimal progress, string currentState)
        {
            ExecutionProgress?.Invoke(this, new ExecutionProgressEventArgs(progress, currentState));
        }

        /// <summary>
        /// Fires the execution start event.
        /// </summary>
        private void FireExecutionStartEvent()
        {
            ExecutionStart?.Invoke(this, new ExecutionStartEventArgs());
        }

        /// <summary>
        /// Fires the execution stop event.
        /// </summary>
        private void FireExecutionStopEvent(Exception ex)
        {
            ExecutionStop?.Invoke(this, new ExecutionStopEventArgs(ex));
        }
        
        #endregion Events

        #endregion Private Methods

        #region Event Handlers

        /// <summary>
        /// TestReport event handlers.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="TestReportEventArgs"/> instance containing the event data.</param>
        private void OnTestReport(object sender, TestReportEventArgs eventArgs)
        {
            if (eventArgs.Report.StartsWith("<start-suite"))
            {
            }
            if (eventArgs.Report.StartsWith("<start-test"))
            {
            }
            else if (eventArgs.Report.StartsWith("<test-case"))
            {
                _testCases.Add(NUnitEngineHelper.ParseTestCase(eventArgs.Report));
                UpdateProgress();
            }

            // TODO: need to analyse the test report structure to be able to report progress and generate the result output.
            Console.WriteLine(eventArgs.Report);

            /*

            Test Reports:
            -------------

            <start-run count='2'/>

            <start-suite id="0-1003" parentId="" name="YellowJacket.WebApp.Automation.dll" fullname="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll"/>

            <start-suite id="0-1004" parentId="0-1003" name="YellowJacket" fullname="YellowJacket"/>

            <start-suite id="0-1005" parentId="0-1004" name="WebApp" fullname="YellowJacket.WebApp"/>

            <start-suite id="0-1006" parentId="0-1005" name="Automation" fullname="YellowJacket.WebApp.Automation"/>

            <start-suite id="0-1007" parentId="0-1006" name="Features" fullname="YellowJacket.WebApp.Automation.Features"/>

            <start-suite id="0-1000" parentId="0-1007" name="MyFeatureFeature" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature"/>

            <start-test id="0-1002" parentId="0-1000" name="AddThreeNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddThreeNumbers"/>

            <test-case id="0-1002" name="AddThreeNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddThreeNumbers" methodname="AddThreeNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="13049240" result="Passed" start-time="2017-02-24 16:40:27Z" end-time="2017-02-24 16:40:31Z" duration="4.175424" asserts="0" parentId="0-1000"><properties><property name="Description" value="Add three numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
            And I have entered 70 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
            When I press add
            -> done: HomeSteps.WhenIPressAdd() (1.0s)
            Then the result should be 120 on the screen
            -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
            ]]></output></test-case>

            <start-test id="0-1001" parentId="0-1000" name="AddTwoNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddTwoNumbers"/>

            <test-case id="0-1001" name="AddTwoNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddTwoNumbers" methodname="AddTwoNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="907031790" result="Passed" start-time="2017-02-24 16:40:31Z" end-time="2017-02-24 16:40:35Z" duration="4.000395" asserts="0" parentId="0-1000"><properties><property name="Description" value="Add two numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
            And I have entered 70 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
            When I press add
            -> done: HomeSteps.WhenIPressAdd() (1.0s)
            Then the result should be 120 on the screen
            -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
            ]]></output></test-case>

            <test-suite type="TestFixture" id="0-1000" name="MyFeatureFeature" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.489387" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId="0-1007"><properties><property name="Description" value="MyFeature" /></properties></test-suite>

            <test-suite type="TestSuite" id="0-1007" name="Features" fullname="YellowJacket.WebApp.Automation.Features" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.494632" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId="0-1006" />

            <test-suite type="TestSuite" id="0-1006" name="Automation" fullname="YellowJacket.WebApp.Automation" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.497524" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId="0-1005" />

            <test-suite type="TestSuite" id="0-1005" name="WebApp" fullname="YellowJacket.WebApp" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.498849" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId="0-1004" />

            <test-suite type="TestSuite" id="0-1004" name="YellowJacket" fullname="YellowJacket" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.534994" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId="0-1003" />

            <test-suite type="Assembly" id="0-1003" name="YellowJacket.WebApp.Automation.dll" fullname="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.543382" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId=""><properties><property name="_PID" value="8976" /><property name="_APPDOMAIN" value="domain-" /></properties></test-suite>

            <test-suite type="Assembly" id="0-1003" name="YellowJacket.WebApp.Automation.dll" fullname="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.543382" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><environment framework-version="3.6.0.0" clr-version="4.0.30319.42000" os-version="Microsoft Windows NT 6.1.7601 Service Pack 1" platform="Win32NT" cwd="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug" machine-name="VM-LACHAND" user="Administrator" user-domain="VM-LACHAND" culture="en-CA" uiculture="en-US" os-architecture="x64" /><settings><setting name="ImageRuntimeVersion" value="4.0.30319" /><setting name="ImageTargetFrameworkName" value=".NETFramework,Version=v4.6.1" /><setting name="ImageRequiresX86" value="False" /><setting name="ImageRequiresDefaultAppDomainAssemblyResolver" value="False" /><setting name="NumberOfTestWorkers" value="8" /></settings><properties><property name="_PID" value="8976" /><property name="_APPDOMAIN" value="domain-" /></properties><test-suite type="TestSuite" id="0-1004" name="YellowJacket" fullname="YellowJacket" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.534994" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1005" name="WebApp" fullname="YellowJacket.WebApp" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.498849" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1006" name="Automation" fullname="YellowJacket.WebApp.Automation" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.497524" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1007" name="Features" fullname="YellowJacket.WebApp.Automation.Features" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.494632" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestFixture" id="0-1000" name="MyFeatureFeature" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.489387" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><properties><property name="Description" value="MyFeature" /></properties><test-case id="0-1002" name="AddThreeNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddThreeNumbers" methodname="AddThreeNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="13049240" result="Passed" start-time="2017-02-24 16:40:27Z" end-time="2017-02-24 16:40:31Z" duration="4.175424" asserts="0"><properties><property name="Description" value="Add three numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
            And I have entered 70 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
            When I press add
            -> done: HomeSteps.WhenIPressAdd() (1.0s)
            Then the result should be 120 on the screen
            -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
            ]]></output></test-case><test-case id="0-1001" name="AddTwoNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddTwoNumbers" methodname="AddTwoNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="907031790" result="Passed" start-time="2017-02-24 16:40:31Z" end-time="2017-02-24 16:40:35Z" duration="4.000395" asserts="0"><properties><property name="Description" value="Add two numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
            And I have entered 70 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
            When I press add
            -> done: HomeSteps.WhenIPressAdd() (1.0s)
            Then the result should be 120 on the screen
            -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
            ]]></output></test-case></test-suite></test-suite></test-suite></test-suite></test-suite></test-suite>

            <test-run id="2" testcasecount="2" result="Passed" total="2" passed="2" failed="0" inconclusive="0" skipped="0" asserts="0" engine-version="3.6.0.0" clr-version="4.0.30319.42000" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:43:50Z" duration="203.513055"><command-line><![CDATA["C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.Console.vshost.exe" ]]></command-line><filter><test re="1">MyFeatureFeature</test></filter><test-suite type="Assembly" id="0-1003" name="YellowJacket.WebApp.Automation.dll" fullname="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.543382" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><environment framework-version="3.6.0.0" clr-version="4.0.30319.42000" os-version="Microsoft Windows NT 6.1.7601 Service Pack 1" platform="Win32NT" cwd="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug" machine-name="VM-LACHAND" user="Administrator" user-domain="VM-LACHAND" culture="en-CA" uiculture="en-US" os-architecture="x64" /><settings><setting name="ImageRuntimeVersion" value="4.0.30319" /><setting name="ImageTargetFrameworkName" value=".NETFramework,Version=v4.6.1" /><setting name="ImageRequiresX86" value="False" /><setting name="ImageRequiresDefaultAppDomainAssemblyResolver" value="False" /><setting name="NumberOfTestWorkers" value="8" /></settings><properties><property name="_PID" value="8976" /><property name="_APPDOMAIN" value="domain-" /></properties><test-suite type="TestSuite" id="0-1004" name="YellowJacket" fullname="YellowJacket" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.534994" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1005" name="WebApp" fullname="YellowJacket.WebApp" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.498849" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1006" name="Automation" fullname="YellowJacket.WebApp.Automation" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.497524" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1007" name="Features" fullname="YellowJacket.WebApp.Automation.Features" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.494632" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestFixture" id="0-1000" name="MyFeatureFeature" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.489387" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><properties><property name="Description" value="MyFeature" /></properties><test-case id="0-1002" name="AddThreeNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddThreeNumbers" methodname="AddThreeNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="13049240" result="Passed" start-time="2017-02-24 16:40:27Z" end-time="2017-02-24 16:40:31Z" duration="4.175424" asserts="0"><properties><property name="Description" value="Add three numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
            And I have entered 70 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
            When I press add
            -> done: HomeSteps.WhenIPressAdd() (1.0s)
            Then the result should be 120 on the screen
            -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
            ]]></output></test-case><test-case id="0-1001" name="AddTwoNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddTwoNumbers" methodname="AddTwoNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="907031790" result="Passed" start-time="2017-02-24 16:40:31Z" end-time="2017-02-24 16:40:35Z" duration="4.000395" asserts="0"><properties><property name="Description" value="Add two numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
            And I have entered 70 into the calculator
            -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
            When I press add
            -> done: HomeSteps.WhenIPressAdd() (1.0s)
            Then the result should be 120 on the screen
            -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
            ]]></output></test-case></test-suite></test-suite></test-suite></test-suite></test-suite></test-suite></test-run>

            */
        }

        #endregion Event Handlers
    }
}