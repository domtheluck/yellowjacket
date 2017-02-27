using System;
using System.Collections.Generic;
using YellowJacket.Core.Hook;

namespace YellowJacket.Core.Infrastructure
{
    [Serializable]
    internal class Settings
    {
        internal List<HookInstance> Hooks { get; set; }

        internal List<ILogger> Loggers { get; set; }
    }
}
