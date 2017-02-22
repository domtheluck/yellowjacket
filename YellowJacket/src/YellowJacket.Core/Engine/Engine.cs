using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Engine;
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

        /// <summary>
        /// Execute the specified Feature contains in the related assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="feature">The feature.</param>
        public void ExecuteFeature(string assemblyPath, string feature)
        {
            RaiseExecutionStartEvent();

            try
            {
                LoadTestAssembly(assemblyPath);

                RegisterHooks();

                // TEMP CODE FOR TESTING PURPOSE

                TestPackage testPackage = new TestPackage(assemblyPath);

                ITestFilterBuilder filterBuilder = new TestFilterBuilder();

                //filterBuilder.AddTest("YellowJacket.WebApp.Automation.Features.MyFeatureFeature");

                filterBuilder.SelectWhere($"test =~ {feature}Feature");

                TestFilter testFilter = filterBuilder.GetFilter();

                ITestRunner testRunner = _testEngine.GetRunner(testPackage);

                int count = testRunner.CountTestCases(testFilter);

                //if (count == 0)
                //    throw new Exception($"The feature {feature} doesn't exist in assembly {assemblyPath}");

                //if (count > 1)
                //    throw new Exception($"More than one feature have been found for the name {feature}");
                //    throw new Exception($"More than one feature have been found for the name {feature}");

                XmlSerializer serializer = new XmlSerializer(typeof(TestRun));

                string value = testRunner.Explore(testFilter).OuterXml;

                TestRun testRun;

                using (TextReader reader = new StringReader(value))
                {
                    testRun = (TestRun)serializer.Deserialize(reader);
                }

                Console.WriteLine(count);
                Console.WriteLine(testRun);

                CustomTestEventListener testEventListener = new CustomTestEventListener();
        
                XmlNode run = testRunner.Run(testEventListener, testFilter);

                using (TextReader reader = new StringReader(run.OuterXml))
                {
                    testRun = (TestRun)serializer.Deserialize(reader);
                }

                Console.Write(testRun);

                // TEMP CODE FOR TESTING PURPOSE
            }
            catch (Exception ex)
            {
                // TODO: we need to log the possible exception
                Console.WriteLine(ex);

                RaiseExecutionStopEvent();
            }

            // if an exception is raised, we are raising a specific event to inform the caller.
            RaiseExecutionCompletedEvent();
        }

        #region Private Methods

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
        private void RaiseExecutionStopEvent()
        {
            ExecutionStop?.Invoke(this, new ExecutionStopEventArgs());
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

        #endregion
    }
}
