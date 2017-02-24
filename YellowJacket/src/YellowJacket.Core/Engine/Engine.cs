using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Engine;
using YellowJacket.Core.Framework;
using YellowJacket.Core.Helpers;
using YellowJacket.Core.Hook;
using YellowJacket.Core.NUnit;
using YellowJacket.Core.NUnit.Models;

namespace YellowJacket.Core.Engine
{
    public delegate void ExecutionStartHandler(object sender, ExecutionStartEventArgs eventArgs);
    public delegate void ExecutionStopHandler(object sender, ExecutionStopEventArgs eventArgs);
    public delegate void ExecutionCompletedHandler(object sender, ExecutionCompletedEventArgs eventArgs);
    public delegate void ExecutionProgressHandler(object sender, ExecutionProgressEventArgs eventArgs);

    /// <summary>
    /// YellowJacket engine.
    /// </summary>
    public class Engine
    {
        #region Private Members

        private ITestEngine _testEngine;

        private readonly TypeLocatorHelper _typeLocatorHelper = new TypeLocatorHelper();

        private Assembly _assembly;

        private TestSuite _testSuite;

        #endregion

        #region Events

        public event ExecutionStartHandler ExecutionStart;
        public event ExecutionStopHandler ExecutionStop;
        public event ExecutionCompletedHandler ExecutionCompleted;
        public event ExecutionProgressHandler ExecutionProgress;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine" /> class.
        /// </summary>
        public Engine()
        {
            Initialize();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Execute the specified Feature contains in the related assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="feature">The feature.</param>
        public void Execute(string assemblyPath, string feature)
        {
            Cleanup();

            RaiseExecutionStartEvent();

            try
            {
                ValidateParameters(assemblyPath);

                LoadTestAssembly(assemblyPath);

                RegisterHooks();

                ExecuteFeature(assemblyPath, feature);
            }
            catch (Exception ex)
            {
                // if an exception is raised, we are raising a specific event to inform the caller.
                RaiseExecutionStopEvent(ex);

                // TODO: for debugging purpose only. Don't forget to remove it.
                throw;
            }

            RaiseExecutionCompletedEvent();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Cleanups the engine setup.
        /// </summary>
        private void Cleanup()
        {
            _assembly = null;
            _testSuite = null;
        }

        /// <summary>
        /// Loads the test assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        private void LoadTestAssembly(string assemblyPath)
        {
            _assembly = Assembly.LoadFile(assemblyPath);
        }

        /// <summary>
        /// Registers the hooks in the execution context.
        /// </summary>
        private void RegisterHooks()
        {
            // cleanup the existing hooks
            ExecutionContext.CurrentContext.CleanHook();

            // get the hook list from the test assembly
            List<Type> hooks = _typeLocatorHelper.GetHookTypes(_assembly);

            // instantiate each hook class and register it in the execution context
            foreach (Type type in hooks)
            {
                int priority = 0;

                HookPriorityAttribute hookPriorityAttribute =
                    (HookPriorityAttribute)Attribute.GetCustomAttribute(type, typeof(HookPriorityAttribute));

                if (hookPriorityAttribute != null)
                    priority = hookPriorityAttribute.Priority;

                ExecutionContext.CurrentContext.RegisterHook(
                    new HookInstance
                    {
                        Instance = ClassActivator<IHook>.CreateInstance(type),
                        Priority = priority
                    });
            }
        }

        /// <summary>
        /// Initializes the engine.
        /// </summary>
        private void Initialize()
        {
            // initialize the NUnit test engine
            _testEngine = TestEngineActivator.CreateInstance();

            //_testEngine.WorkDirectory = ""; // TODO: check if we need to put the WorkDirectory elsewhere
            _testEngine.InternalTraceLevel = InternalTraceLevel.Off; // TODO: should be customizable
        }

        /// <summary>
        /// Raises the execution start event.
        /// </summary>
        private void RaiseExecutionStartEvent()
        {
            ExecutionStart?.Invoke(this, new ExecutionStartEventArgs());
        }

        /// <summary>
        /// Raises the execution stop event.
        /// </summary>
        private void RaiseExecutionStopEvent(Exception ex)
        {
            ExecutionStop?.Invoke(this, new ExecutionStopEventArgs(ex));
        }

        /// <summary>
        /// Raises the execution completed event.
        /// </summary>
        private void RaiseExecutionCompletedEvent()
        {
            ExecutionCompleted?.Invoke(this, new ExecutionCompletedEventArgs());
        }

        /// <summary>
        /// Raises the execution progress event.
        /// </summary>
        /// <param name="progress">The progress.</param>
        private void RaiseExecutionProgressEvent(double progress)
        {
            ExecutionProgress?.Invoke(this, new ExecutionProgressEventArgs(progress));
        }

        private void ValidateParameters(string assemblyPath)
        {
            if (!File.Exists(assemblyPath))
                throw new FileNotFoundException($"Cannot find the assembly {assemblyPath}");
        }

        /// <summary>
        /// Executes the specified feature.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="feature">The feature.</param>
        private void ExecuteFeature(string assemblyPath, string feature)
        {
            // get the test package
            TestPackage testPackage = NUnitEngineHelper.CreateTestPackage(new List<string> { assemblyPath });

            TestFilter testFilter = NUnitEngineHelper.CreateTestFilter(feature);

            ITestRunner testRunner = _testEngine.GetRunner(testPackage);

            _testSuite = NUnitEngineHelper.ParseTestRun(testRunner.Explore(testFilter)).TestSuite;

            CustomTestEventListener testEventListener = new CustomTestEventListener();

            testEventListener.TestReport += OnTestReport;

            TestRun testRun = NUnitEngineHelper.ParseTestRun(testRunner.Run(testEventListener, testFilter));
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// TestReport event handlers.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="TestReportEventArgs"/> instance containing the event data.</param>
        private void OnTestReport(object sender, TestReportEventArgs eventArgs)
        {
            // TODO: need to analyse the test report structure to be able to report progress.
            Console.WriteLine(eventArgs.Report);
        }

        #endregion
    }
}
