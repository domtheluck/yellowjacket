using YellowJacket.Core.Engine;

namespace YellowJacket.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assembly assembly = Assembly.LoadFile();

            //TypeLocator typeLocator = new TypeLocator();

            //typeLocator.GetHookTypes(assembly);

            Engine engine = new Engine();

            engine.ExecuteFeature(@"C:\Projects\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\Debug\YellowJacket.WebApp.Automation.dll", "MyFeature");
            //engine.ExecuteFeature(@"D:\Projects\DEV\yellowjacket\YellowJacket\src\YellowJacket.Console\bin\debug\YellowJacket.WebApp.Automation.dll", "MyFeature");
        }
    }
}
