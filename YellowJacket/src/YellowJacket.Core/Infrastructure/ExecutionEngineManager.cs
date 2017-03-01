using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Infrastructure
{
    public class ExecutionEngineManager
    {
        public static IExecutionEngine CreateEngine()
        {
            return new Engine.Engine();
        }
    }
}
