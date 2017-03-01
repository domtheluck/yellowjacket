using System;
using System.Collections.Generic;
using YellowJacket.Core.Hook;
using YellowJacket.Core.Interfaces;

namespace YellowJacket.Core.Infrastructure
{
    internal class Settings
    {
        internal List<HookInstance> Hooks { get; set; }

        internal List<ILogger> Loggers { get; set; }
    }
}
