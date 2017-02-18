using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using NUnit.Engine;
using NUnit.Engine.Runners;
using YellowJacket.Core.Hook;
using YellowJacket.Core.Utils;

namespace YellowJacket.Core.Engine
{
    public delegate void ExecutionStartHandler(object sender, ExecutionStartEventArgs eventArgs);
    public delegate void ExecutionStopHandler(object sender, ExecutionStopEventArgs eventArgs);
    public delegate void ExecutionCompletedHandler(object sender, ExecutionCompletedEventArgs eventArgs);
    public delegate void ExecutionProgressHandler(object sender, ExecutionProgressEventArgs eventArgs);

    public class Engine
    {
        #region Private Members

        private ITestEngine _testEngine;

        private readonly TypeLocator _typeLocator = new TypeLocator();

        #endregion

        protected event ExecutionStartHandler ExecutionStart;
        protected event ExecutionStopHandler ExecutionStop;
        protected event ExecutionCompletedHandler ExecutionCompleted;
        protected event ExecutionProgressHandler ExecutionProgress;

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
                Assembly assembly = Assembly.LoadFile(assemblyPath);

                RegisterHooks(assembly);

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

                XmlNode testNode = testRunner.Explore(testFilter);

                Console.WriteLine(count);

                ITestEventListener testEventListener = new TestEventDispatcher();
        
                XmlNode run = testRunner.Run(testEventListener, testFilter);
            }
            catch (Exception ex)
            {
                // TODO: we need to log the possible exception
                Console.WriteLine(ex);

                RaiseExecutionStopEvent();
            }

            RaiseExecutionCompletedEvent();
        }

        #region Private Methods

        /// <summary>
        /// Registers the hooks in the context.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        private void RegisterHooks(Assembly assembly)
        {
            ExecutionContext.CurrentContext.CleanHook();

            List<Type> hooks = _typeLocator.GetHookTypes(assembly);

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
            // initialize the nunit test engine
            _testEngine = TestEngineActivator.CreateInstance();

            //_testEngine.WorkDirectory = ""; // TODO: check if we need to put the WorkDirectory elsewhere
            _testEngine.InternalTraceLevel = InternalTraceLevel.Off; // TODO: should be customizable
        }

        private void RaiseExecutionStartEvent()
        {
            ExecutionStart?.Invoke(this, new ExecutionStartEventArgs());
        }

        private void RaiseExecutionStopEvent()
        {
            ExecutionStop?.Invoke(this, new ExecutionStopEventArgs());
        }

        private void RaiseExecutionCompletedEvent()
        {
            ExecutionCompleted?.Invoke(this, new ExecutionCompletedEventArgs());
        }

        private void RaiseExecutionProgressEvent(double progress)
        {
            ExecutionProgress?.Invoke(this, new ExecutionProgressEventArgs(progress));
        }

        private void ParseTestNode(XmlNode testNode)
        {
            
        }

        #endregion
    }
}
