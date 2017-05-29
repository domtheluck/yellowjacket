using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowJacket.Core.Contexts
{
    public sealed class ExecutionContext
    {
        private static readonly Lazy<ExecutionContext> _instance =
            new Lazy<ExecutionContext>(() => new ExecutionContext());

        public static ExecutionContext GetInstance => _instance.Value;

        private ExecutionContext()
        {
        }
    }
}
