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

            TestFilter testFilter = NUnitEngineHelper.CreateTestFilter(_assembly, feature);

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

        #endregion
    }
}
