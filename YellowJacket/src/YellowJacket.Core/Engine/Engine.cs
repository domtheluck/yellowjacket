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
using NUnit.Engine.Services;
using YellowJacket.Common.Helpers;
using YellowJacket.Core.Engine.EventArgs;
using YellowJacket.Core.Enums;
using YellowJacket.Core.Gherkin;
using YellowJacket.Core.Hook;
using YellowJacket.Core.Interfaces;
using YellowJacket.Core.Logging;
using YellowJacket.Core.NUnitWrapper;
using YellowJacket.Core.Plugins;
using YellowJacket.Core.Plugins.Interfaces;
using ExecutionContext = YellowJacket.Core.Contexts.ExecutionContext;
using ITestRunner = NUnit.Engine.ITestRunner;

namespace YellowJacket.Core.Engine
{
    /// <inheritdoc />
    /// <summary>
    /// YellowJacket engine.
    /// </summary>
    public sealed class Engine : IEngine
    {
        #region Private Members

        private Assembly _testAssembly;
        private Configuration _configuration;
        private readonly List<Assembly> _pluginAssemblies = new List<Assembly>();

        private TestEngine _testEngine;

        private readonly GherkinManager _gherkinManager = new GherkinManager();

        private List<GherkinFeature> _features = new List<GherkinFeature>();

        private string _workingFolder;

        private int _executedFeatureCount;
        private int _executedScenarioCount;
        private int _executedStepCount;

        private readonly object _updateProgressLock = new object();

        #endregion

        #region Events

        public event ExecutionProgressHandler ExecutionProgress;

        public event FeatureExecutionProgressHandler FeatureExecutionProgress;
        public event ScenarioExecutionProgressHandler ScenarioExecutionProgress;
        public event StepExecutionProgressHandler StepExecutionProgress;

        public event ExecutionStartHandler ExecutionStart;
        public event ExecutionCompletedHandler ExecutionCompleted;
        public event ExecutionStopHandler ExecutionStop;

        #endregion Events

        #region Properties

        /// <inheritdoc />
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public EngineStatus Status { get; internal set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the run summary.
        /// </summary>
        /// <value>
        /// The run summary.
        /// </value>
        public RunSummary RunSummary { get; internal set; }

        #endregion

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
            ValidateConfiguration(configuration);

            _configuration = configuration;

            InitializeFileSystem();

            try
            {
                LoadPluginAssemblies();

                RegisterPlugins();

                LoadTestAssembly();

                RegisterHooks();

                GetFeaturesFromFiles();

                RegisterEventHandlers();

                ExecuteFeatures();
            }
            catch (Exception ex)
            {
                // if an exception is raised, we are raising a specific event to inform the caller.
                FireExecutionStopEvent(ex);

                throw;
            }
            finally
            {
                Cleanup();

                FireExecutionCompletedEvent();
            }
        }

        /// <summary>
        /// Registers the event handlers.
        /// </summary>
        private void RegisterEventHandlers()
        {
            ExecutionContext.Instance.BeforeExecution += OnBeforeExecution;
            ExecutionContext.Instance.AfterExecution += OnAfterExecution;

            ExecutionContext.Instance.ExecutionBeforeFeature += OnExecutionBeforeFeature;
            ExecutionContext.Instance.ExecutionAfterFeature += OnExecutionAfterFeature;

            ExecutionContext.Instance.ExecutionBeforeScenario += OnExecutionBeforeScenario;
            ExecutionContext.Instance.ExecutionAfterScenario += OnExecutionAfterScenario;

            ExecutionContext.Instance.ExecutionBeforeStep += OnExecutionBeforeStep;
            ExecutionContext.Instance.ExecutionAfterStep += OnExecutionAfterStep;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Gets the features from files.
        /// </summary>
        /// <exception cref="InvalidOperationException">.</exception>
        private void GetFeaturesFromFiles()
        {
            _configuration.Features.ForEach(x =>
            {
                _features
                    .Add(_gherkinManager.ParseFeature(_testAssembly, x, Path.Combine(_workingFolder, "Features")));
            });
        }

        /// <summary>
        /// Initializes the file system.
        /// </summary>
        private void InitializeFileSystem()
        {
            _workingFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName(), "YellowJacket");
        }

        /// <summary>
        /// Updates the execution progress.
        /// </summary>
        /// <param name="hookType">Type of the hook.</param>
        private void UpdateProgress(HookType hookType)
        {
            lock (_updateProgressLock)
            {
                switch (hookType)
                {
                    case HookType.AfterFeature:
                        UpdateProgressFeatures();

                        break;

                    case HookType.AfterScenario:
                        UpdateProgressScenarios();

                        break;

                    case HookType.AfterStep:
                        UpdateProgressSteps();

                        break;
                }

                FireExecutionProgressEvent();
            }
        }

        /// <summary>
        /// Updates the progress of the steps.
        /// </summary>
        private void UpdateProgressSteps()
        {
            List<GherkinStep> steps = _features.SelectMany(x => x.Scenarios).SelectMany(x => x.Steps).ToList();

            int stepIndex = steps.IndexOf(RunSummary.CurrentStep);

            RunSummary.NextStep = null;

            if (steps.Count > stepIndex + 1)
            {
                RunSummary.PreviousStep = RunSummary.CurrentStep;
                RunSummary.CurrentStep = steps[stepIndex + 1];
            }

            if (steps.Count > stepIndex + 2)
                RunSummary.NextStep = steps[stepIndex + 2];

            _executedStepCount += 1;

            RunSummary.StepExecutionPercentage =
                Math.Round((double)_executedStepCount / RunSummary.StepCount * 100, 2);

            FireStepExecutionProgressEvent();
        }

        /// <summary>
        /// Updates the progress of the scenarios.
        /// </summary>
        private void UpdateProgressScenarios()
        {
            List<GherkinScenario> scenarios = _features.SelectMany(x => x.Scenarios).ToList();

            int scenarioIndex = scenarios.IndexOf(RunSummary.CurrentScenario);

            RunSummary.NextScenario = null;

            if (scenarios.Count > scenarioIndex + 1)
            {
                RunSummary.PreviousScenario = RunSummary.CurrentScenario;
                RunSummary.CurrentScenario = scenarios[scenarioIndex + 1];
            }

            if (scenarios.Count > scenarioIndex + 2)
                RunSummary.NextScenario = scenarios[scenarioIndex + 2];

            _executedScenarioCount += 1;

            RunSummary.ScenarioExecutionPercentage =
                Math.Round((double)_executedScenarioCount / RunSummary.ScenarioCount * 100, 2);

            FireScenarioExecutionProgressEvent();
        }

        /// <summary>
        /// Updates the progress of the features.
        /// </summary>
        private void UpdateProgressFeatures()
        {
            int featureIndex = _features.IndexOf(RunSummary.CurrentFeature);

            RunSummary.NextFeature = null;

            if (_features.Count > featureIndex + 1)
            {
                RunSummary.PreviousFeature = RunSummary.CurrentFeature;
                RunSummary.CurrentFeature = _features[featureIndex + 1];
            }

            if (_features.Count > featureIndex + 2)
                RunSummary.NextFeature = _features[featureIndex + 2];

            _executedFeatureCount += 1;

            RunSummary.FeatureExecutionPercentage =
                Math.Round((double)_executedFeatureCount / RunSummary.FeatureCount * 100, 2);

            FireFeatureExecutionProgressEvent();
        }

        /// <summary>
        /// Validates the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private void ValidateConfiguration(Configuration configuration)
        {
            if (string.IsNullOrEmpty(configuration.TestAssemblyFullName))
                throw new ArgumentException("You must provide a value for the Test assembly");

            string location = Path.GetDirectoryName(configuration.TestAssemblyFullName);

            if (string.IsNullOrEmpty(location))
                throw new IOException("The test assembly location is invalid");

            if (!File.Exists(configuration.TestAssemblyFullName))
                throw new IOException($"Cannot found the Test assembly {configuration.TestAssemblyFullName}");
        }

        /// <summary>
        /// Cleanups after run.
        /// </summary>
        private void Cleanup()
        {
            IoHelper.DeleteDirectory(_workingFolder, true);

            _configuration = null;
            _testAssembly = null;
            _pluginAssemblies.Clear();
            _features = new List<GherkinFeature>();
            _workingFolder = null;

            ExecutionContext.Instance.BeforeExecution -= OnBeforeExecution;
            ExecutionContext.Instance.AfterExecution -= OnAfterExecution;

            ExecutionContext.Instance.ExecutionBeforeFeature -= OnExecutionBeforeFeature;
            ExecutionContext.Instance.ExecutionAfterFeature -= OnExecutionAfterFeature;

            ExecutionContext.Instance.ExecutionBeforeScenario -= OnExecutionBeforeScenario;
            ExecutionContext.Instance.ExecutionAfterScenario -= OnExecutionAfterScenario;

            ExecutionContext.Instance.ExecutionBeforeStep -= OnExecutionBeforeStep;
            ExecutionContext.Instance.ExecutionAfterStep -= OnExecutionAfterStep;
        }

        /// <summary>
        /// Executes the specified features.
        /// </summary>
        private void ExecuteFeatures()
        {
            FireExecutionStartEvent();

            TestPackage testPackage =
                NUnitEngineHelper.CreateTestPackage(new List<string> { _configuration.TestAssemblyFullName });

            TestFilter testFilter =
                NUnitEngineHelper.CreateTestFilter(_testAssembly, _configuration.Features);

            ITestRunner testRunner = _testEngine.GetRunner(testPackage);

            // TODO: Not sure if we need to keep this
            //_testSuite = NUnitEngineHelper.ParseTestRun(testRunner.Explore(testFilter)).TestSuite;

            CustomTestEventListener testEventListener = new CustomTestEventListener();

            // TODO: Not sure if we need to keep this
            //testEventListener.TestReport += OnTestReport;

            // TODO: Not sure if we need to keep this
            //NUnitEngineHelper.ParseTestRun(testRunner.Run(testEventListener, testFilter));

            RunSummary.CurrentFeature = _features.First();
            RunSummary.CurrentScenario = RunSummary.CurrentFeature.Scenarios.First();
            RunSummary.CurrentStep = RunSummary.CurrentScenario.Steps.First();

            RunSummary.FeatureCount = _features.Count;
            RunSummary.ScenarioCount = _features.SelectMany(x => x.Scenarios).Count();
            RunSummary.StepCount = _features.SelectMany(x => x.Scenarios).SelectMany(x => x.Steps).Count();

            testRunner.Run(testEventListener, testFilter);

            testRunner.StopRun(true);

            //while (testRunner.IsTestRunning) { }

            testRunner.Unload();
            testRunner.Dispose();

            _testEngine.Services.ServiceManager.StopServices();
            _testEngine.Services.ServiceManager.ClearServices();
            _testEngine.Services.ServiceManager.Dispose();
        }

        /// <summary>
        /// Initializes the engine.
        /// </summary>
        private void Initialize()
        {
            _testEngine = new TestEngine();

            _testEngine.Services.Add(new SettingsService(false));
            _testEngine.Services.Add(new ExtensionService());
            _testEngine.Services.Add(new DriverService());
            _testEngine.Services.Add(new InProcessTestRunnerFactory());
            _testEngine.Services.Add(new RuntimeFrameworkService());
            _testEngine.Services.Add(new TestFilterService());
            _testEngine.Services.Add(new DomainManager());
            _testEngine.Services.ServiceManager.StartServices();

            _testEngine.InternalTraceLevel = InternalTraceLevel.Verbose; // TODO: should be customizable

            RunSummary = new RunSummary();
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
                plugins.Add(
                    ClassActivatorHelper<BasicFileLogPlugin>
                        .CreateInstance(
                            typeof(BasicFileLogPlugin), 
                            Path.Combine(Path.GetTempPath(), "YellowJacket"))); // TODO: need to have this in the input args

            return plugins;
        }

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List of plugins.</returns>
        private List<T> GetPlugins<T>()
        {
            List<T> plugins = new List<T>();

            foreach (Assembly assembly in _pluginAssemblies)
            {
                List<Type> types = TypeLocatorHelper.GetImplementedTypes<T>(assembly);

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
            string location = Path.GetDirectoryName(_configuration.TestAssemblyFullName) ?? "";

            _configuration.PluginAssemblies.ForEach(x =>
            {
                string fullName = Path.Combine(location, x);

                if (File.Exists(fullName))
                _pluginAssemblies.Add(
                    Assembly.LoadFile(fullName));
            });
        }

        /// <summary>
        /// Registers the plugins in the execution context.
        /// </summary>
        private void RegisterPlugins()
        {
            ExecutionContext.Instance.ClearPlugins();

            GetLogPlugins().ForEach(x =>
            {
                ExecutionContext.Instance.RegisterLogPlugin(x);
            });
        }

        #endregion Plugins

        #region Hooks

        /// <summary>
        /// Registers the hooks in the execution context.
        /// </summary>
        private void RegisterHooks()
        {
            ExecutionContext.Instance.ClearHooks();

            List<Type> hooks = TypeLocatorHelper.GetImplementedTypes<IHook>(_testAssembly);

            foreach (Type type in hooks)
            {
                int priority = 0;

                HookPriorityAttribute hookPriorityAttribute =
                    (HookPriorityAttribute)Attribute.GetCustomAttribute(type, typeof(HookPriorityAttribute));

                if (hookPriorityAttribute != null)
                    priority = hookPriorityAttribute.Priority;

                ExecutionContext.Instance.RegisterHook(
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
            Status = EngineStatus.Completed;

            ExecutionCompleted?.Invoke(this, new ExecutionCompletedEventArgs());
        }

        /// <summary>
        /// Fires the execution progress event.
        /// </summary>
        private void FireExecutionProgressEvent()
        {
            ExecutionProgress?.Invoke(this, new ExecutionProgressEventArgs(RunSummary));
        }

        /// <summary>
        /// Fires the feature execution progress event.
        /// </summary>
        private void FireFeatureExecutionProgressEvent()
        {
            FeatureExecutionProgress?.Invoke(
                this, 
                new FeatureExecutionProgressEventArgs(RunSummary.FeatureExecutionPercentage));
        }

        /// <summary>
        /// Fires the scenario execution progress event.
        /// </summary>
        private void FireScenarioExecutionProgressEvent()
        {
            ScenarioExecutionProgress?.Invoke(
                this,
                new ScenarioExecutionProgressEventArgs(RunSummary.ScenarioExecutionPercentage));
        }

        /// <summary>
        /// Fires the step execution progress event.
        /// </summary>
        private void FireStepExecutionProgressEvent()
        {
            StepExecutionProgress?.Invoke(
                this,
                new StepExecutionProgressEventArgs(RunSummary.StepExecutionPercentage));
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
        /// Handler for the BeforeExecution event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnBeforeExecution(object sender, System.EventArgs e)
        {
            LogManager.WriteLine("OnBeforeExecution");
        }

        /// <summary>
        /// Handler for the AfterExecution event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnAfterExecution(object sender, System.EventArgs e)
        {
            LogManager.WriteLine("OnAfterExecution");
        }

        /// <summary>
        /// Handler for the ExecutionBeforeFeature event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnExecutionBeforeFeature(object sender, System.EventArgs e)
        {
            LogManager.WriteLine("OnExecutionBeforeFeature");
        }

        /// <summary>
        /// Handler for the ExecutionAfterFeature event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnExecutionAfterFeature(object sender, System.EventArgs e)
        {
            LogManager.WriteLine("OnExecutionAfterFeature");

            UpdateProgress(HookType.AfterFeature);
        }

        /// <summary>
        /// Handler for the ExecutionBeforeScenario event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnExecutionBeforeScenario(object sender, System.EventArgs e)
        {
            LogManager.WriteLine("OnExecutionBeforeScenario");
        }

        /// <summary>
        /// Handler for the ExecutionAfterScenario event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnExecutionAfterScenario(object sender, System.EventArgs e)
        {
            LogManager.WriteLine("OnExecutionAfterScenario");

            UpdateProgress(HookType.AfterScenario);
        }

        /// <summary>
        /// Handler for the ExecutionBeforeStep event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnExecutionBeforeStep(object sender, System.EventArgs e)
        {
            LogManager.WriteLine("OnExecutionBeforeStep");
        }

        /// <summary>
        /// Handler for the ExecutionAfterStep event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnExecutionAfterStep(object sender, System.EventArgs e)
        {
            LogManager.WriteLine("OnExecutionAfterStep");

            UpdateProgress(HookType.AfterStep);
        }

        ///// <summary>
        ///// TestReport event handlers.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The <see cref="TestReportEventArgs"/> instance containing the event data.</param>
        //private void OnTestReport(object sender, TestReportEventArgs e)
        //{
        //    //if (eventArgs.Report.StartsWith("<start-suite"))
        //    //{
        //    //}
        //    //if (eventArgs.Report.StartsWith("<start-test"))
        //    //{
        //    //}
        //    //else if (eventArgs.Report.StartsWith("<test-case"))
        //    //{
        //    //    _testCases.Add(NUnitEngineHelper.ParseTestCase(eventArgs.Report));
        //    //    UpdateProgress();
        //    //}

        //    //// TODO: need to analyse the test report structure to be able to report progress and generate the result output.
        //    //Console.WriteLine(eventArgs.Report);

        //    /*

        //    Test Reports:
        //    -------------

        //    <start-run count='2'/>

        //    <start-suite id="0-1003" parentId="" name="YellowJacket.WebApp.Automation.dll" fullname="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll"/>

        //    <start-suite id="0-1004" parentId="0-1003" name="YellowJacket" fullname="YellowJacket"/>

        //    <start-suite id="0-1005" parentId="0-1004" name="WebApp" fullname="YellowJacket.WebApp"/>

        //    <start-suite id="0-1006" parentId="0-1005" name="Automation" fullname="YellowJacket.WebApp.Automation"/>

        //    <start-suite id="0-1007" parentId="0-1006" name="Features" fullname="YellowJacket.WebApp.Automation.Features"/>

        //    <start-suite id="0-1000" parentId="0-1007" name="MyFeatureFeature" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature"/>

        //    <start-test id="0-1002" parentId="0-1000" name="AddThreeNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddThreeNumbers"/>

        //    <test-case id="0-1002" name="AddThreeNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddThreeNumbers" methodname="AddThreeNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="13049240" result="Passed" start-time="2017-02-24 16:40:27Z" end-time="2017-02-24 16:40:31Z" duration="4.175424" asserts="0" parentId="0-1000"><properties><property name="Description" value="Add three numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
        //    And I have entered 70 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
        //    When I press add
        //    -> done: HomeSteps.WhenIPressAdd() (1.0s)
        //    Then the result should be 120 on the screen
        //    -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
        //    ]]></output></test-case>

        //    <start-test id="0-1001" parentId="0-1000" name="AddTwoNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddTwoNumbers"/>

        //    <test-case id="0-1001" name="AddTwoNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddTwoNumbers" methodname="AddTwoNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="907031790" result="Passed" start-time="2017-02-24 16:40:31Z" end-time="2017-02-24 16:40:35Z" duration="4.000395" asserts="0" parentId="0-1000"><properties><property name="Description" value="Add two numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
        //    And I have entered 70 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
        //    When I press add
        //    -> done: HomeSteps.WhenIPressAdd() (1.0s)
        //    Then the result should be 120 on the screen
        //    -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
        //    ]]></output></test-case>

        //    <test-suite type="TestFixture" id="0-1000" name="MyFeatureFeature" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.489387" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId="0-1007"><properties><property name="Description" value="MyFeature" /></properties></test-suite>

        //    <test-suite type="TestSuite" id="0-1007" name="Features" fullname="YellowJacket.WebApp.Automation.Features" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.494632" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId="0-1006" />

        //    <test-suite type="TestSuite" id="0-1006" name="Automation" fullname="YellowJacket.WebApp.Automation" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.497524" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId="0-1005" />

        //    <test-suite type="TestSuite" id="0-1005" name="WebApp" fullname="YellowJacket.WebApp" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.498849" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId="0-1004" />

        //    <test-suite type="TestSuite" id="0-1004" name="YellowJacket" fullname="YellowJacket" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.534994" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId="0-1003" />

        //    <test-suite type="Assembly" id="0-1003" name="YellowJacket.WebApp.Automation.dll" fullname="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.543382" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0" parentId=""><properties><property name="_PID" value="8976" /><property name="_APPDOMAIN" value="domain-" /></properties></test-suite>

        //    <test-suite type="Assembly" id="0-1003" name="YellowJacket.WebApp.Automation.dll" fullname="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.543382" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><environment framework-version="3.6.0.0" clr-version="4.0.30319.42000" os-version="Microsoft Windows NT 6.1.7601 Service Pack 1" platform="Win32NT" cwd="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug" machine-name="VM-LACHAND" user="Administrator" user-domain="VM-LACHAND" culture="en-CA" uiculture="en-US" os-architecture="x64" /><settings><setting name="ImageRuntimeVersion" value="4.0.30319" /><setting name="ImageTargetFrameworkName" value=".NETFramework,Version=v4.6.1" /><setting name="ImageRequiresX86" value="False" /><setting name="ImageRequiresDefaultAppDomainAssemblyResolver" value="False" /><setting name="NumberOfTestWorkers" value="8" /></settings><properties><property name="_PID" value="8976" /><property name="_APPDOMAIN" value="domain-" /></properties><test-suite type="TestSuite" id="0-1004" name="YellowJacket" fullname="YellowJacket" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.534994" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1005" name="WebApp" fullname="YellowJacket.WebApp" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.498849" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1006" name="Automation" fullname="YellowJacket.WebApp.Automation" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.497524" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1007" name="Features" fullname="YellowJacket.WebApp.Automation.Features" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.494632" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestFixture" id="0-1000" name="MyFeatureFeature" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.489387" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><properties><property name="Description" value="MyFeature" /></properties><test-case id="0-1002" name="AddThreeNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddThreeNumbers" methodname="AddThreeNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="13049240" result="Passed" start-time="2017-02-24 16:40:27Z" end-time="2017-02-24 16:40:31Z" duration="4.175424" asserts="0"><properties><property name="Description" value="Add three numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
        //    And I have entered 70 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
        //    When I press add
        //    -> done: HomeSteps.WhenIPressAdd() (1.0s)
        //    Then the result should be 120 on the screen
        //    -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
        //    ]]></output></test-case><test-case id="0-1001" name="AddTwoNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddTwoNumbers" methodname="AddTwoNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="907031790" result="Passed" start-time="2017-02-24 16:40:31Z" end-time="2017-02-24 16:40:35Z" duration="4.000395" asserts="0"><properties><property name="Description" value="Add two numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
        //    And I have entered 70 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
        //    When I press add
        //    -> done: HomeSteps.WhenIPressAdd() (1.0s)
        //    Then the result should be 120 on the screen
        //    -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
        //    ]]></output></test-case></test-suite></test-suite></test-suite></test-suite></test-suite></test-suite>

        //    <test-run id="2" testcasecount="2" result="Passed" total="2" passed="2" failed="0" inconclusive="0" skipped="0" asserts="0" engine-version="3.6.0.0" clr-version="4.0.30319.42000" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:43:50Z" duration="203.513055"><command-line><![CDATA["C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.Console.vshost.exe" ]]></command-line><filter><test re="1">MyFeatureFeature</test></filter><test-suite type="Assembly" id="0-1003" name="YellowJacket.WebApp.Automation.dll" fullname="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.543382" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><environment framework-version="3.6.0.0" clr-version="4.0.30319.42000" os-version="Microsoft Windows NT 6.1.7601 Service Pack 1" platform="Win32NT" cwd="C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug" machine-name="VM-LACHAND" user="Administrator" user-domain="VM-LACHAND" culture="en-CA" uiculture="en-US" os-architecture="x64" /><settings><setting name="ImageRuntimeVersion" value="4.0.30319" /><setting name="ImageTargetFrameworkName" value=".NETFramework,Version=v4.6.1" /><setting name="ImageRequiresX86" value="False" /><setting name="ImageRequiresDefaultAppDomainAssemblyResolver" value="False" /><setting name="NumberOfTestWorkers" value="8" /></settings><properties><property name="_PID" value="8976" /><property name="_APPDOMAIN" value="domain-" /></properties><test-suite type="TestSuite" id="0-1004" name="YellowJacket" fullname="YellowJacket" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.534994" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1005" name="WebApp" fullname="YellowJacket.WebApp" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.498849" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1006" name="Automation" fullname="YellowJacket.WebApp.Automation" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.497524" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestSuite" id="0-1007" name="Features" fullname="YellowJacket.WebApp.Automation.Features" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.494632" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><test-suite type="TestFixture" id="0-1000" name="MyFeatureFeature" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" testcasecount="2" result="Passed" start-time="2017-02-24 16:40:26Z" end-time="2017-02-24 16:40:35Z" duration="8.489387" total="2" passed="2" failed="0" warnings="0" inconclusive="0" skipped="0" asserts="0"><properties><property name="Description" value="MyFeature" /></properties><test-case id="0-1002" name="AddThreeNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddThreeNumbers" methodname="AddThreeNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="13049240" result="Passed" start-time="2017-02-24 16:40:27Z" end-time="2017-02-24 16:40:31Z" duration="4.175424" asserts="0"><properties><property name="Description" value="Add three numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
        //    And I have entered 70 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
        //    When I press add
        //    -> done: HomeSteps.WhenIPressAdd() (1.0s)
        //    Then the result should be 120 on the screen
        //    -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
        //    ]]></output></test-case><test-case id="0-1001" name="AddTwoNumbers" fullname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature.AddTwoNumbers" methodname="AddTwoNumbers" classname="YellowJacket.WebApp.Automation.Features.MyFeatureFeature" runstate="Runnable" seed="907031790" result="Passed" start-time="2017-02-24 16:40:31Z" end-time="2017-02-24 16:40:35Z" duration="4.000395" asserts="0"><properties><property name="Description" value="Add two numbers" /></properties><output><![CDATA[Given I have entered 50 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(50) (1.0s)
        //    And I have entered 70 into the calculator
        //    -> done: HomeSteps.GivenIHaveEnteredIntoTheCalculator(70) (1.0s)
        //    When I press add
        //    -> done: HomeSteps.WhenIPressAdd() (1.0s)
        //    Then the result should be 120 on the screen
        //    -> done: HomeSteps.ThenTheResultShouldBeOnTheScreen(120) (1.0s)
        //    ]]></output></test-case></test-suite></test-suite></test-suite></test-suite></test-suite></test-suite></test-run>

        //    */
        //}

        #endregion Event Handlers
    }
}