using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using NUnit.Engine;
using YellowJacket.Core.Hook;
using YellowJacket.Core.Utils;

namespace YellowJacket.Core.Engine
{
    public class Engine
    {
        #region Private Members

        private ITestEngine _testEngine;

        private readonly TypeLocator _typeLocator = new TypeLocator();

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
            Assembly assembly = Assembly.LoadFile(assemblyPath);

            RegisterHooks(assembly);

            TestPackage testPackage = new TestPackage(assemblyPath);

            ITestFilterBuilder filterBuilder = new TestFilterBuilder();

            filterBuilder.AddTest("YellowJacket.WebApp.Automation.Features.MyFeatureFeature");

            TestFilter testFilter = filterBuilder.GetFilter();

            ITestRunner testRunner = _testEngine.GetRunner(testPackage);

            int count = testRunner.CountTestCases(testFilter);

            XmlNode tests = testRunner.Explore(testFilter);

            Console.WriteLine(count);


        }

        #region Private Methods

        /// <summary>
        /// Registers the hooks in the context.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        private void RegisterHooks(Assembly assembly)
        {
            Context.CurrentContext.CleanHook();

            List<Type> hooks = _typeLocator.GetHookTypes(assembly);

            foreach (Type type in hooks)
            {
                int priority = 0;

                HookPriorityAttribute hookPriorityAttribute =
                    (HookPriorityAttribute)Attribute.GetCustomAttribute(type, typeof(HookPriorityAttribute));

                if (hookPriorityAttribute != null)
                    priority = hookPriorityAttribute.Priority;

                Context.CurrentContext.RegisterHook(
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

        #endregion
    }
}
