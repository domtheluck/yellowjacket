using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowJacket.Core.Engine;

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
